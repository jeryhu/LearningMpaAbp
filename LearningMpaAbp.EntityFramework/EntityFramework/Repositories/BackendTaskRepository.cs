using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using LearningMpaAbp.IRepositories;
using Task = LearningMpaAbp.Tasks.Task;

namespace LearningMpaAbp.EntityFramework.Repositories
{
    //说明： 仓储方法中，ABP自动进行数据库连接的开启和关闭
    //仓储方法被调用的时候，数据库连接自动开启且启动事务
    //当仓储方法调用另外一个仓储的方法，它们实际上共享的是同一个数据库连接和事务
    //仓储对象都是暂时性的，因为IRepository接口默认继承自ITransientDependency接口，所以仓储对象只有在需要注入的时候，才会由Ioc容器自动创建新实例
    public class BackendTaskRepository:LearningMpaAbpRepositoryBase<Task>,IBackendTaskRepository
    {
        public BackendTaskRepository(IDbContextProvider<LearningMpaAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取某个用户分配了哪些任务
        /// </summary>
        /// <param name="personId">用户Id</param>
        /// <returns>任务列表</returns>
        public List<Task> GetTaskByAssignedPersonId(long personId)
        {
            var query = GetAll();

            if (personId>0)
            {
                query = query.Where(t => t.AssignedPersonId == personId);
            }

            return query.ToList();
        }
    }
}
