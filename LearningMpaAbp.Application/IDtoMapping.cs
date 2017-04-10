using AutoMapper;

namespace LearningMpaAbp
{
   //如果在映射规则既有通过特性方式又有通过代码方式创建，这时就会容易混乱不便维护。
 //为了解决这个问题，统一采用代码创建映射规则的方式。并通过IOC容器注册所有的映射规则类，再循环调用注册方法
    /// <summary>
    ///    创建统一入口注册AutoMapper映射规则 实现该接口以进行映射规则创建
    /// </summary>
    internal interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}