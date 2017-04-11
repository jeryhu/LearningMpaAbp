using Abp.Runtime.Session;

namespace LearningMpaAbp.Extensions
{
    /*
         因为ApplicationService, AbpController 和 AbpApiController 这3个基类已经注入了AbpSession属性。
        所以我们需要在领域层，也就是.Core结尾的项目中对AbpSession进行扩展。
        现在假设我们需要扩展一个Email属性。
     */
    public interface IAbpSessionExtension : IAbpSession
    {
        string Email { get; }
    }
}