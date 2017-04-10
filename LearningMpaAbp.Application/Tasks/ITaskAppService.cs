using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using LearningMpaAbp.Tasks.Dtos;


namespace LearningMpaAbp.Tasks
{
    //继承自接口IApplicationService ABP会自动帮助依赖注入 因为IApplicationService:ITransientDependency
    //ABP为IApplicationService提供了默认的实现ApplicationService 该基类提供了方便的日志记录功能和本地化功能
    //ABP中，一个应用服务方法默认是一个工作单元（Unit of Work）。ABP针对UOW模式自动进行数据库的连接及事务管理，且会自动保存数据修改
    /// <summary>
    /// Defines an application service for <see cref="Task"/> operations.
    /// 
    /// It extends <see cref="IApplicationService"/>.
    /// Thus, ABP enables automatic dependency injection, validation and other benefits for it.
    /// 
    /// Application services works with DTOs (Data Transfer Objects).
    /// Service methods gets and returns DTOs.
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {
        GetTasksOutput GetTasks(GetTasksInput input);

        PagedResultDto<TaskDto> GetPagedTasks(GetTasksInput input);

        void UpdateTask(UpdateTaskInput input);

        int CreateTask(CreateTaskInput input);

        Task<TaskDto> GetTaskByIdAsync(int taskId);

        TaskDto GetTaskById(int taskId);

        void DeleteTask(int taskId);

        TaskCacheItem GetTaskFromCacheById(int taskId);

        IList<TaskDto> GetAllTasks();
    }
}