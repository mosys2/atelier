using Atelier.Application.Interfaces.Contexts;
using Atelier.Common.Constants;
using Atelier.Common.Dto;
using Atelier.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Users.Commands.AddPageAccess
{
    public interface IAddPageAccessService
    {
        Task<ResultDto> Execute(string userId, List<string> pageIds);
    }
    public class AddPageAccessService : IAddPageAccessService
    {
        private readonly IDatabaseContext _context;
        public AddPageAccessService(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(string userId, List<string> pageIds)
        {
            try
            {
                // بررسی وجود کاربر
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = Messages.INVALID_USER
                    };
                // ایجاد دسترسی برای هر صفحه و ثبت در دیتابیس
                foreach (var pageId in pageIds)
                {

                    //return new ResultDto
                    //{
                    //    IsSuccess = false,
                    //    Message = string.Format("دسترسی {0} برای صفحه {1} قبلاً تعریف شده است", existingPermission.Role.Name, existingPermission.Page.Name)
                    //}; // قبلاً دسترسی تعریف شده است

                    var page = await _context.Pages.FindAsync(pageId);
                    if (page == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Messages.INVALID_PAGE
                        };
                    }

                    var permission = new PageAccess
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        PageId = pageId,
                        InsertTime = DateTime.Now,
                        InsertByUserId = userId,
                        CanAccess = true // یا هر مقدار دلخواه برای دسترسی
                    };
                    // بررسی تکراری بودن دسترسی
                    var existingPermission = await _context.PageAccess.Include(p => p.Page)
                        .FirstOrDefaultAsync(rpp => rpp.UserId == userId && rpp.PageId == pageId);

                    if (existingPermission == null)
                    {
                        await _context.PageAccess.AddAsync(permission);
                    }
                }

                // ذخیره تغییرات در دیتابیس
                await _context.SaveChangesAsync();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Messages.MessageInsert
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }
        }
    }
}
