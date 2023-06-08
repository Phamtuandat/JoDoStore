using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace App.Services
{
      public class AppbarService
      {
            private readonly ILogger<AppbarService> _logger;
            private readonly IUrlHelper urlHelper;
            public List<SidebarItem> items = new List<SidebarItem>();
            public AppbarService(IUrlHelperFactory factory, IActionContextAccessor action, ILogger<AppbarService> logger)
            {
                  _logger = logger;
                  urlHelper = factory.GetUrlHelper(action.ActionContext);
                  items = new List<SidebarItem>(){
                new SidebarItem()
                {
                    Title = "Database Management",
                    FontAwesomeIcon = "fa-duotone fa-database",
                    Action = "Index",
                    Controller = "Database",
                    Type= SidebarItemType.NavItem,
                    Area = "Database",
                },
                new SidebarItem()
                {
                    Title = "Contacts Management",
                    FontAwesomeIcon = "fa-duotone fa-mailbox",
                    Action = "Index",
                    Controller = "Contact",
                    Type= SidebarItemType.NavItem,
                    Area = "Contact",
                },
                new SidebarItem()
                {
                    Title = "File management",
                    Action = "Index",
                    Controller = "File",
                    Type= SidebarItemType.NavItem,
                    Area="File",
                    FontAwesomeIcon="fa-duotone fa-folder-open"
                },
                new SidebarItem()
                {
                    Title = "User management",
                    Type= SidebarItemType.NavItem,
                    FontAwesomeIcon= "fa-thin fa-people-roof" ,
                    collapseId= "userManageId",
                    Items = new List<SidebarItem>(){
                        new SidebarItem()
                        {
                            Title = "Role Management",
                            FontAwesomeIcon ="fa-solid fa-users-between-lines",
                            Action = "Index",
                            Controller = "Role",
                            Area="Identity",
                            Type= SidebarItemType.NavItem,
                        },
                        new SidebarItem()
                        {
                            Title = "User Management",
                            FontAwesomeIcon ="fa-solid fa-users",
                            Action = "Index",
                            Controller = "User",
                            Area="Identity",
                            Type= SidebarItemType.NavItem,
                        }
                    }
                },
                new SidebarItem()
                        {
                            Title = "Account Management",
                            FontAwesomeIcon ="fa-solid fa-users-between-lines",
                            Action = "Index",
                            Controller = "Manage",
                            Area="Identity",
                            Type= SidebarItemType.NavItem,
                        },
                  new SidebarItem()
                {
                    Title = "Product Management",
                    Type= SidebarItemType.NavItem,
                    Area = "Product",
                    FontAwesomeIcon="fa-brands fa-product-hunt",
                    collapseId="ProductManagement",
                    Items= new List<SidebarItem>(){
                        new SidebarItem()
                        {
                            Title = "List of Product",
                            Action = "Index",
                            Controller = "Product",
                            Area="Product",
                            Type= SidebarItemType.NavItem,
                        },
                        new SidebarItem()
                        {
                            Title = "Create Product",
                            Action = "Create",
                            Controller = "Product",
                            Area="Product",
                            Type= SidebarItemType.NavItem,
                        },
                        new SidebarItem()
                        {
                            Title = "Categories",
                            Action = "Index",
                            Controller = "Category",
                            Area="Product",
                            Type= SidebarItemType.NavItem,
                        },
                        new SidebarItem()
                        {
                            Title = "Create Category",
                            Action = "Create",
                            Controller = "Category",
                            Area="Product",
                            Type= SidebarItemType.NavItem,
                        },
                    }
                },

            };
            }


            public string RenderHtml()
            {
                  var html = new StringBuilder();
                  foreach (var item in items)
                  {
                        html.Append(item.RenderHtml(urlHelper));
                  }
                  return html.ToString();
            }
            public void SetActive(string controller, string action, string area)
            {
                  foreach (var item in items)
                  {
                        if (controller == item.Controller && action == item.Action && area == item.Area)
                        {
                              item.IsActive = true;
                              _logger.LogInformation(item.IsActive.ToString());
                              return;
                        }
                        else
                        {
                              if (item.Items != null)
                              {
                                    foreach (var subItem in item.Items)
                                    {
                                          if (controller == subItem.Controller && action == subItem.Action && area == subItem.Area)
                                          {
                                                item.IsActive = true;
                                                subItem.IsActive = true;
                                                return;
                                          }
                                    }
                              }
                        }
                  }
            }
      }




      public enum SidebarItemType
      {
            Divider,
            Heading,
            NavItem
      }
      public class SidebarItem
      {
            public string Title { get; set; }
            public bool IsActive { get; set; }
            public SidebarItemType Type { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string Area { get; set; }
            public string FontAwesomeIcon { get; set; }
            public List<SidebarItem> Items { get; set; }
            public string collapseId { get; set; }
            public string CssClass { get; set; }
            public string GetUrl(IUrlHelper urlHelper)
            {
                  return urlHelper.Action(Action, Controller, new { area = Area });
            }

            public string RenderHtml(IUrlHelper urlHelper)
            {
                  var html = new StringBuilder();
                  switch (Type)
                  {
                        case SidebarItemType.Divider:
                              html.Append("<hr class=\"sidebar-divider my-0\"/>");
                              break;
                        case SidebarItemType.Heading:
                              html.Append($"<div class=\"sidebar-heading\">{Title}</div>");
                              break;
                        case SidebarItemType.NavItem:
                              var target = Title == "File management" ? "_blank" : null;
                              if (Items == null)
                              {
                                    var url = GetUrl(urlHelper);
                                    var icon = (FontAwesomeIcon != null) ? $"<i class=\"{FontAwesomeIcon}\"></i>" : "";
                                    CssClass ??= "nav-item";
                                    if (IsActive) CssClass += " active";

                                    html.Append(@$"
                    <li class=""{CssClass}"">
                        <a target=""{target}"" class=""nav-link"" href=""{url}"">
                            {icon}
                            <span>{Title}</span>
                        </a>
                    </li>
                    ");
                              }
                              else
                              {
                                    var icon = (FontAwesomeIcon != null) ? $"<i class=\"{FontAwesomeIcon}\"></i>" : "";
                                    CssClass = "nav-item";
                                    if (IsActive) CssClass += " active";
                                    var collapsedItem = "";
                                    var collapse = "collapse";
                                    if (IsActive) collapse += " show";
                                    foreach (var item in Items)
                                    {
                                          var iconItem = (item.FontAwesomeIcon != null) ? $"<i class=\"{FontAwesomeIcon}\"></i>" : "";
                                          var url = item.GetUrl(urlHelper);
                                          var itemCssClass = "collapse-item d-flex";
                                          if (item.IsActive) itemCssClass += " active";
                                          collapsedItem += $"<a class=\"{itemCssClass}\" href=\"{url}\">{item.Title}</a>";
                                    }
                                    html.Append(@$"
                            <li class=""{CssClass}"">
                                <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#{collapseId}"" aria-expanded=""true""
                                    aria-controls=""{collapseId}"">
                                    {icon}
                                    <span>{Title}</span>
                                </a>
                                <div id=""{collapseId}"" class=""{collapse}"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                    <div class=""bg-white py-2 collapse-inner rounded"">
                                        {collapsedItem}
                                    </div>
                                </div>
                            </li>
                        ");
                              }
                              break;

                        default: break;
                  }

                  return html.ToString();
            }
      }
}