using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Sockets;

namespace Atelier.Application.Services.Banks.Commands
{
    public interface IEditBankService
    {
        Task<ResultDto> Execute(RequestBankDto request, Guid userId, Guid branchId);
    }
    public class EditBankService : IEditBankService
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        private readonly IMongoRepository<Bank> _bankRepository;

        public EditBankService(IMongoRepository<Cheque> chequeRepository, IMongoRepository<Bank> bankRepository)
        {
            _chequeRepository = chequeRepository ?? throw new ArgumentNullException(nameof(chequeRepository));
            _bankRepository = bankRepository ?? throw new ArgumentNullException(nameof(bankRepository));
        }

        public async Task<ResultDto> Execute(RequestBankDto request, Guid userId, Guid branchId)
        {
            using (var session = await _bankRepository.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    // بررسی اعتبار وجود Id
                    if (!request.Id.HasValue)
                    {
                        return new ResultDto { IsSuccess = false, Message = Messages.NotFind };
                    }

                    // یافتن بانک فعلی
                    var currentBank = await _bankRepository.GetAsync(request.Id.Value);
                    if (currentBank == null)
                    {
                        return new ResultDto { IsSuccess = false, Message = Messages.NOT_FOUND_BANK };
                    }

                    // بررسی تفاوت نام بانک
                    if (string.Equals(request.Name.Trim(), currentBank.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return new ResultDto { IsSuccess = false, Message = Messages.EXISTS_NAME_BANK };
                    }

                    // ذخیره نام جدید بانک
                    currentBank.Name = request.Name;
                    currentBank.UpdateTime = DateTime.Now;
                    currentBank.UpdateByUserId = userId;
                    await _bankRepository.UpdateAsync(currentBank, session);

                    // به‌روزرسانی چک‌های مرتبط با بانک
                    var (listChequeBankUpdate, total) = await _chequeRepository.GetAllAsync(c => c.BranchId == branchId && c.Bank.Id == currentBank.Id, null);
                    foreach (var item in listChequeBankUpdate)
                    {
                        item.Bank.Name = currentBank.Name;
                        item.Bank.UpdateTime = DateTime.Now;
                        item.Bank.UpdateByUserId = userId;
                        await _chequeRepository.UpdateAsync(item, session);
                    }

                    await session.CommitTransactionAsync();

                    return new ResultDto { IsSuccess = true, Message = Messages.MessageUpdate };
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine(ex.Message);
                    return new ResultDto { IsSuccess = false, Message = Messages.FAILED_OPERATION };
                }
            }
        }
    }
}

