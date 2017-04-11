using System.Linq;
using System.Security.Claims;
using Abp.Runtime.Session;

namespace LearningMpaAbp.Extensions
{
    /// <summary>
    /// 通过扩展方法来对AbpSession进行扩展
    /// 既然只要我们在登录的时候通过在身份信息中添加要扩展的属性，我们就可以通过CliamsPrincipal中获取扩展的属性，所以我们可以通过
    /// 对IAbpSession进行扩展，通过扩展方法从 CliamsPrincipal中获取扩展属性
    /// </summary>
    public static class AbpSessionExtension2 //推荐这种方法
    {
        public static string GetUserEmail(this IAbpSession session)
        {
            return GetClaimValue(ClaimTypes.Email);//System.Security.Claims中定义了很多常量 证件单元类型
        }
        private static string GetClaimValue(string claimType)
        {
            var claimsPrincipal = DefaultPrincipalAccessor.Instance.Principal;
            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            return string.IsNullOrEmpty(claim?.Value) ? null : claim.Value;
        }
    }
}