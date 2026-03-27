using System;
using System.Collections.Generic;
using Framework.Library;

namespace Common.Data
{
    public class StaticInfo
    {
        private static readonly Dictionary<Enums.CssOperator, string> cssOperatorMap = new Dictionary<Enums.CssOperator, string>()
        {
            { Enums.CssOperator.Exact, "=" },
            { Enums.CssOperator.OneOf, "~=" },
            { Enums.CssOperator.BeginsWith, "^=" },
            { Enums.CssOperator.EndsWith, "$=" },
            { Enums.CssOperator.Contains, "*=" }
        };

        private static readonly Dictionary<string, string> destinationTypeNameIdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Email Report Split By Contact Field", "Scout.Services.TaskService.Destinations.BurstEmailDestination" },
            { "FTP", "Scout.Services.TaskService.Destinations.FtpDestination" },
            { "Intranet", "Scout.Services.TaskService.Destinations.IntranetDestination" },
            { "Marketo Add Users To Static List", "Scout.Services.TaskService.Destinations.MarketoListDestination" },
            { "Marketo Sync Users", "Scout.Services.TaskService.Destinations.MarketoSyncUsersDestination" },
            { "Email Report To Specified List", "Scout.Services.TaskService.Destinations.SimpleEmailDestination" },
            { "Zuora Billable Usage", "Scout.Services.TaskService.Destinations.ZuoraBillableUsageDestination" },
            { "BurstEmailDestination", "Scout.Services.TaskService.Destinations.BurstEmailDestination" },
            { "FtpDestination", "Scout.Services.TaskService.Destinations.FtpDestination" },
            { "IntranetDestination", "Scout.Services.TaskService.Destinations.IntranetDestination" },
            { "MarketoListDestination", "Scout.Services.TaskService.Destinations.MarketoListDestination" },
            { "MarketoSyncUsersDestination", "Scout.Services.TaskService.Destinations.MarketoSyncUsersDestination" },
            { "SimpleEmailDestination", "Scout.Services.TaskService.Destinations.SimpleEmailDestination" },
            { "ZuoraBillableUsageDestination", "Scout.Services.TaskService.Destinations.ZuoraBillableUsageDestination" }
        };

        private static readonly Dictionary<string, string> exportTypeIdBurstIdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Scout.Services.TaskService.Sources.PlaybookCsvSource", "Scout.Services.TaskService.Sources.PlaybookCsvBurstSource" },
            { "Scout.Services.TaskService.Sources.QueryCsvSource", "Scout.Services.TaskService.Sources.QueryCsvBurstSource" },
            { "PlaybookCsvSource", "Scout.Services.TaskService.Sources.PlaybookCsvBurstSource" },
            { "QueryCsvSource", "Scout.Services.TaskService.Sources.QueryCsvBurstSource" }
        };

        private static readonly Dictionary<string, string> exportTypeNameIdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Zuora Billable Usage Definition", "Scout.Services.TaskService.Sources.BillableUsageSource" },
            { "Marketo Add Contacts to Static List", "Scout.Services.TaskService.Sources.CampaignUsersCsvSource" },
            { "Playbook Customer Results To CSV", "Scout.Services.TaskService.Sources.PlaybookCsvSource" },
            { "Playbook Subscription Results To CSV", "Scout.Services.TaskService.Sources.PlaybookCsvSource" },
            { "Playbook User Results To CSV", "Scout.Services.TaskService.Sources.PlaybookCsvSource" },
            { "Playbook Product Results To CSV", "Scout.Services.TaskService.Sources.PlaybookCsvSource" },
            { "General Query Results To CSV", "Scout.Services.TaskService.Sources.QueryCsvSource" },
            { "Marketo Sync Users", "Scout.Services.TaskService.Sources.SyncUsersCsvSource" },
            { "BillableUsageSource", "Scout.Services.TaskService.Sources.BillableUsageSource" },
            { "CampaignUsersCsvSource", "Scout.Services.TaskService.Sources.CampaignUsersCsvSource" },
            { "PlaybookCsvSource", "Scout.Services.TaskService.Sources.PlaybookCsvSource" },
            { "QueryCsvSource", "Scout.Services.TaskService.Sources.QueryCsvSource" },
            { "SyncUsersCsvSource", "Scout.Services.TaskService.Sources.SyncUsersCsvSource" },
            { "PlaybookCsvBurstSource", "Scout.Services.TaskService.Sources.PlaybookCsvBurstSource" },
            { "QueryCsvBurstSource", "Scout.Services.TaskService.Sources.QueryCsvBurstSource" }
        };

        private static readonly Dictionary<Enums.Icon, string> iconClassMap = new Dictionary<Enums.Icon, string>()
        {
            { Enums.Icon.Book, "fa-book" },    
            { Enums.Icon.Calendar, "fa-calendar" },
            { Enums.Icon.Copy, "fa-copy" },
            { Enums.Icon.Delete, "icon-trash" },
            { Enums.Icon.DownCaret, "icon-caret-down" },
            { Enums.Icon.DownChevron, "icon-chevron-down" },
            { Enums.Icon.Edit, "fa-edit" },
            { Enums.Icon.Exclude, "icon-minus-sign" },
            { Enums.Icon.Export, "icon-cloud-download" },
            { Enums.Icon.Filter, "icon-filter" },
            { Enums.Icon.First, "fa-angle-double-left" },
            { Enums.Icon.Hamburger, "fa-reorder" },
            { Enums.Icon.Help, "fa-question" },
            { Enums.Icon.Include, "icon-plus-sign" },
            { Enums.Icon.Info, "fa-info-circle" },
            { Enums.Icon.Last, "fa-angle-double-right" },
            { Enums.Icon.LeftArrow, "fa-arrow-circle-left" },
            { Enums.Icon.LeftChevron, "icon-chevron-left" },
            { Enums.Icon.List, "icon-list-ul" },
            { Enums.Icon.Logout, "fa-sign-out" },
            { Enums.Icon.Minus, "icon-minus" },
            { Enums.Icon.Next, "fa-angle-right" },
            { Enums.Icon.On, "fa-rotate-90" },
            { Enums.Icon.Off, "fa-rotate-270" },
            { Enums.Icon.Plus, "icon-plus" },
            { Enums.Icon.Previous, "fa-angle-left" },
            { Enums.Icon.Remove, "fa-remove" },
            { Enums.Icon.ResetPassword, "fa-cogs" },
            { Enums.Icon.RightArrow, "fa-arrow-circle-right" },
            { Enums.Icon.RightChevron, "icon-chevron-right" },
            { Enums.Icon.Save, "icon-save" },
            { Enums.Icon.Search, "icon-search" },
            { Enums.Icon.Settings, "fa-cogs" },
            { Enums.Icon.Sort, "icon-sort" },
            { Enums.Icon.SortAsc, "icon-sort-up" },
            { Enums.Icon.SortDesc, "icon-sort-down" },
            { Enums.Icon.UpCaret, "icon-caret-up" },
            { Enums.Icon.UpChevron, "icon-chevron-up" },
            { Enums.Icon.Undo, "icon-undo" }
        };

        private static readonly BiDictionary<Enums.UiPage, string> pageUriMap = new BiDictionary<Enums.UiPage, string>()
        {
            { Enums.UiPage.Accounts, "/platform/#/{0}/users" },
            { Enums.UiPage.AccountDetails, "/platform/#/{0}/users/{1}" },
            { Enums.UiPage.Analyze, "/suite/#/{0}/analyze/{1}" },
            { Enums.UiPage.ChooseResource, "/authorize" },
            { Enums.UiPage.ChooseResource, "/authorize/#/" },
            { Enums.UiPage.ChooseResource, "/authorize/#/exit" },
            { Enums.UiPage.ContractDetail, "/suite/#/{0}/contract/{1}/ContractDetailSubscriptionSummary" },
            { Enums.UiPage.CustomerPeerGroups, "/platform/#/{0}/peer-customer" },
            { Enums.UiPage.Destinations, "/platform/#/{0}/destinations" },
            { Enums.UiPage.Exports, "/platform/#/{0}/exports" },
            { Enums.UiPage.Formulas, "/platform/#/{0}/formulas" },
            { Enums.UiPage.Home, "/suite/#/{0}" },
            { Enums.UiPage.Home, "/suite/#/{0}/home" },
            { Enums.UiPage.Login, "/authorize" },
            { Enums.UiPage.Login,"/authorize/#/" },
            { Enums.UiPage.OrganizationDetail, "/suite/#/{0}/organization/{1}/OrganizationDetailCustomerSummary" },
            { Enums.UiPage.Playbooks, "/suite/#/{0}/playbooks" },
            { Enums.UiPage.Reports, "/suite/#/{0}/reports/{1}" },
            { Enums.UiPage.Schedules, "/platform/#/{0}/schedules" },
            { Enums.UiPage.SubscriptionPeerGroups, "/platform/#/{0}/peer-contract" },
            { Enums.UiPage.VisitorDetail, "/suite/#/{0}/visitor/{1}/VisitorDetailUserSummary" }
        };

        public static Dictionary<Enums.CssOperator, string> CssOperatorMap
        {
            get
            {
                return cssOperatorMap;
            }
        }

        public static Dictionary<string, string> DestinationTypeNameIdMap
        {
            get
            {
                return destinationTypeNameIdMap;
            }
        }

        public static Dictionary<string, string> ExportTypeIdBurstIdMap
        {
            get
            {
                return exportTypeIdBurstIdMap;
            }
        }

        public static Dictionary<string, string> ExportTypeNameIdMap
        {
            get
            {
                return exportTypeNameIdMap;
            }
        }

        public static Dictionary<Enums.Icon, string> IconClassMap
        {
            get
            {
                return iconClassMap;
            }
        }

        public static BiDictionary<Enums.UiPage, string> PageUriMap
        {
            get
            {
                return pageUriMap;
            }
        }
    }
}
