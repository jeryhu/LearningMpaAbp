using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using LearningMpaAbp.Sessions;
using LearningMpaAbp.Web.Models.Layout;

namespace LearningMpaAbp.Web.Controllers
{
    public class LayoutController : LearningMpaAbpControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;

        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
        }
        //备注:ChildActionOnly表示它只能在View中通过Html.Action或Html.RenderAction来使用
        //不能被 Controller 直接调用， 一般返回的是局部视图，例如更新局部页面


        //1.首先执行 Layout控制器下的TopMenu Action 传参 当前激活的菜单 亦即在每个前台视图中 都有给ViewBag.ActiveMenu赋值 如：ViewBag.ActiveMenu = "About";
        [ChildActionOnly]
        public PartialViewResult TopMenu(string activeMenu = "")
        {
            //shared中    @Html.Partial("_TopBar") 去加载部分视图   _TopBar.cshtml
            var model = new TopMenuViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_TopMenu", model);
        }

        //2.去加载 多语言下拉菜单数据
        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages()
            };
            return PartialView("_LanguageSelection", model);
        }

        //3.根据当前是否用户登录过 加载用户个人相关菜单或者登录链接
        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model = new UserMenuOrLoginLinkViewModel{
                                                     IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                                                 };
            if (AbpSession.UserId.HasValue) model.LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());
            return PartialView("_UserMenuOrLoginLink", model);
        }
    }
}