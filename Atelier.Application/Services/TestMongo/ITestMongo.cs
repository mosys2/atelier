using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.TestMongo
{
    public interface ITestMongo
    {
        //Task<ResultDto<List<Test>>> Execute();
        Task<ResultDto> ExecuteAdd();


    }
    //public class TestMongo : ITestMongo
    //{
    //    private readonly IMongoRepository<Test> _repository;
    //    private readonly IMongoRepository<Test2> _repository2;

    //    public TestMongo(IMongoRepository<Test> repository, IMongoRepository<Test2> repository2)
    //    {
    //        _repository = repository;
    //        _repository2=repository2;

    //    }
    //    public async Task<ResultDto<List<Test>>> Execute()
    //    {
    //        var items = _repository.GetAllAsync().Result.Select(a=> new Test
    //        {
    //            Name = a.Name,
    //            Id = a.Id,
    //            InsertTime = a.InsertTime,
    //        }).ToList();
    //        return new ResultDto<List<Test>>
    //        {
    //            Data = items,
    //            IsSuccess = true,

    //        };
    //    }

    //    public async Task<ResultDto> ExecuteAdd()
    //    {
    //        Test test = new Test()
    //        {
    //            Name= "test",
    //            InsertTime= DateTime.Now,
    //        };
    //        Test2 test2 = new Test2()
    //        {
    //            Name="test2",
    //        };

    //        await _repository.CreateAsync(test);
    //        await _repository2.CreateAsync(test2);
    //        return new ResultDto
    //        {
    //            IsSuccess = true,
    //            Message=test.Id.ToString()
    //        };
    //    }
    //}
}
