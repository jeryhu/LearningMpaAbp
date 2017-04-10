using Abp.Application.Navigation;
using Abp.Localization;
using LearningMpaAbp.Authorization;

namespace LearningMpaAbp.Web
{
    /// <summary>
    ///     This class defines menus for the application.
    ///     It uses ABP's menu system.
    ///     When you add menu items here, they are automatically appear in angular application.
    ///     See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class LearningMpaAbpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("HomePage"),
                        url: "",
                        icon: "fa fa-home",
                        requiresAuthentication: true //说明只有登录后才可以显示该菜单
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "Tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants //用户具有指定的权限才显示该菜单
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "Users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        L("About"),
                        url: "About",
                        icon: "fa fa-info" //About菜单没有给定特定的权限 默认任意用户都可以看到 都显示
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "TaskList",
                        L("Task List"),
                        url: "Tasks/Index",
                        icon: "fa fa-tasks",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "BackendTaskList",
                        L("Backend Task List"),
                        url: "BackendTasks/List",
                        icon: "fa fa-tasks"
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LearningMpaAbpConsts.LocalizationSourceName);
        }
    }
}