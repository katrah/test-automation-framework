using System;
using System.ComponentModel;
using System.Reflection;
using Framework.Library;

namespace Common.Data
{
    public class Enums
    {
        public enum AggregateSecondaryArgumentType
        {
            PeerEntity
        }

        public enum Browser
        {
            Chrome,
            Firefox,
            IE32,
            IE64
        }

        public enum ChartType
        {
            None,
            Bar,
            Column,
            [Synonyms("Column & Line", "Column and Line")]
            ColumnAndLine,
            Line,
            Pie,
            Scatter,
            [Synonyms("Stacked Column")]
            StackedColumn
        }

        public enum CssOperator
        {
            [Synonyms("=")]
            Exact,      // The entire attribute value must match this
            [Synonyms("~=")]
            OneOf,      // One of the attribute values matches this
            [Synonyms("^=")]
            BeginsWith, // The entire attribute value begins with this
            [Synonyms("$=")]
            EndsWith,   // The entire attribute value ends with this
            [Synonyms("*=")]
            Contains    // The attribute value contains this substring
        }

        public enum DataType
        {
            ActionStateType,
            AnnualSalesSizeRange,
            Any,
            Boolean,
            ContractStatusType,
            Date,
            DateTime,
            DayOfWeek,
            Did,
            DidNamePair,
            EmployeeSizeRange,
            Enumeration,
            Float,
            HourOfDay,
            Integer,
            LicenseType,
            Long,
            LoyaltyType,
            Money,
            OpportunityPrediction,
            OpportunityPredictionReason,
            OpportunityResolution,
            Percent,
            String,
            TimeSpan,
            VisitorType
        }

        public enum DateRangePeriodType
        {
            Day = 2,
            Hour = 1,
            Month = 4,
            Quarter = 5,
            Week = 3,
            Year = 6
        }

        public enum DateRangeType
        {
            Absolute,
            Relative
        }

        public enum DateRangeRelativeTo
        {
            [Synonyms("Today")]
            Now,
            Usage
        }

        public enum DialogType
        {
            ConfigureGrid,
            ExportMarketoStatic,
            ExportMarketoSync,
            ExportPlaybook,
            ExportQuery,
            ExportZuora,
            Play,
            Playbook,
        }

        public enum DialogTab
        {
            Columns,
            Email,
            ExportTypes,
            Fields,
            Filters,
            Pivots,
            SecondaryGroupBy,
            UnitOfMeasure
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum Endpoint
        {
            Create,
            Delete,
            Find, 
            Search,
            Update
        }

        public enum Entity
        {
            Address,
            AffinityOrganization,
            AppLookup,
            Booking,
            CoveredAsset,
            LineItems,
            Offer,
            Opportunity,
            Organization,
            Person,
            Product,
            ProductFindAssocation,
            Quote,
            ServiceAsset,
            Team
        }

        public enum ExportType
        {
            Unknown,
            [Synonyms("General Query Results To CSV")]
            GeneralQuery,
            [Synonyms("Playbook Customer Results To CSV", "Playbook Subscription Results To CSV", "Playbook User Results To CSV", "Playbook Product Results To CSV")]
            Playbook,
            [Synonyms("Marketo Add Contacts to Static List")]
            MarketoStatic,
            [Synonyms("Marketo Sync Users")]
            MarketoSync,
            [Synonyms("Zuora Billable Usage Definition")]
            Zuora
        }

        public enum Icon
        {
            Unknown,
            Book,
            Calendar,
            Copy,
            Edit,
            Exclude,
            Export,
            Filter,
            First,
            Delete,
            DownCaret,
            DownChevron,
            Hamburger,
            Help,
            Include,
            Info,
            Last,
            LeftArrow,
            LeftChevron,
            List,
            Logout,
            Minus,
            Next,
            Null,
            Off,
            On,
            Plus,
            Previous,
            Remove,
            ResetPassword,
            RightArrow,
            RightChevron,
            Save,
            Search,
            Settings,
            Sort,
            SortAsc,
            SortDesc,
            UpCaret,
            UpChevron,
            Undo
        }

        public enum MenuAggregation
        {
            None,
            Mixed,
            Only
        }

        public enum ObjectType
        {
            Export,
            Filter,
            Report,
            ReportItem,
            Resolution,
            Trigger
        }

        public enum Operator
        {
            [Synonyms("Is Between")]
            Between,
            Contains,
            [Synonyms("Does Not Contain", "Doesn't Contain")]
            DoesNotContain,
            [Synonyms("Empty", "Is Empty")]
            Empty,
            [Synonyms("Is", "Is On")]
            Equals,
            [Synonyms("Is Greater Than", "Greater Than", "Is After", "After")]
            GreaterThan,
            [Synonyms("Is Not Null", "Not Null")]
            IsNotNull,
            [Synonyms("Is Null")]
            IsNull,
            [Synonyms("Is Less Than", "Less Than", "Is Before", "Before")]
            LessThan,
            [Synonyms("Is Not Between", "Not Between")]
            NotBetween,
            [Synonyms("Not Empty", "Is Not Empty")]
            NotEmpty,
            [Synonyms("Not Equals", "Is Not", "Is Not On", "Not")]
            NotEquals
        }

        public enum ReportId
        {
            ContractDetailGeneral,
            ContractDetailSubscriptionFields,
            ContractDetailSubscriptionSummary,
            OrganizationDetailCustomerFields,
            OrganizationDetailCustomerSummary,
            OrganizationDetailGeneral,
            VisitorDetailGeneral,
            VisitorDetailUserFields,
            VisitorDetailUserSummary
        }

        public enum ReportItemType
        {
            Chart,
            Grid,
            Scoreboard
        }

        public enum SearchResultType
        {
            [Synonyms("Subscription")]
            Contract,
            Customer,
            Destination,
            Export,
            Formula,
            Group,
            Pivot,
            Playbook,
            Report,
            Schedule,
            [Synonyms("Account")]
            User,
            [Synonyms("User")]
            Visitor
        }

        public enum TestBase
        {
            JobSystem,
            Web
        }

        public enum TestProgress
        {
            Complete,
            Failure,
            Running
        }

        public enum UiPage
        {
            Accounts,
            AccountDetails,
            Admin,
            Analyze,
            ChooseResource,
            [Synonyms("SubscriptionDetail", "Contract", "ContractDetails", "Subscription", "SubscriptionDetails")]
            ContractDetail,
            [Synonyms("Customer Peer Groups")]
            CustomerPeerGroups,
            Destinations,
            Exports,
            Formulas,
            [Synonyms("Track")]
            Home,
            Login,
            [Synonyms("CustomerDetail", "Customer", "CustomerDetails", "OrganizationDetails", "OrgDetails", "OrgDetail")]
            OrganizationDetail,
            [Synonyms("Playbook")]
            Playbooks,
            [Synonyms("Report")]
            Reports,
            Schedules,
            [Synonyms("Subscription Peer Groups")]
            SubscriptionPeerGroups,
            Unknown,
            [Synonyms("UserDetail", "User", "UserDetails", "Visitor", "VisitorDetails")]
            VisitorDetail
        }

        /// <summary>
        /// Checks the Enum Name, the DescriptionAttribute, and the SynonymsAttribute for the value passed in
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T Parse<T>(string description) where T : struct, IConvertible
        {
            Type type = typeof(T);
            if (type.IsEnum)
            {
                foreach (FieldInfo field in type.GetFields())
                {
                    if (field.Name.Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)field.GetValue(null);
                    }

                    SynonymsAttribute synAttribute = (SynonymsAttribute)Attribute.GetCustomAttribute(field, typeof(SynonymsAttribute));
                    if (synAttribute != null)
                    {
                        foreach (string synonym in synAttribute.Values)
                        {
                            if (synonym.Equals(description, StringComparison.OrdinalIgnoreCase))
                            {
                                return (T)field.GetValue(null);
                            }
                        }
                    }

                    DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attribute != null && attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return (T)field.GetValue(null);
                    }
                }
                throw new ArgumentException("Description not found", description);
            }
            throw new ArgumentException("T must be an enumerated type");
        }

        /// <summary>
        /// Checks the Enum Name, the DescriptionAttribute, and the SynonymsAttribute for the value passed in
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <param name="enumVal">If not found, this will be the default value of the Enum</param>
        /// <returns></returns>
        public static bool TryParse<T>(string description, out T enumVal) where T : struct, IConvertible
        {
            Type type = typeof(T);
            if (type.IsEnum)
            {
                enumVal = default(T);
                foreach (FieldInfo field in type.GetFields())
                {
                    if (field.Name.Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        enumVal = (T)field.GetValue(null);
                        return true;
                    }

                    SynonymsAttribute synAttribute = (SynonymsAttribute)Attribute.GetCustomAttribute(field, typeof(SynonymsAttribute));
                    if (synAttribute != null)
                    {
                        foreach (string synonym in synAttribute.Values)
                        {
                            if (synonym.Equals(description, StringComparison.OrdinalIgnoreCase))
                            {
                                enumVal = (T)field.GetValue(null);
                                return true;
                            }
                        }
                    }

                    DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if (attribute != null && attribute.Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        enumVal = (T)field.GetValue(null);
                        return true;
                    }
                }
                return false;
            }
            throw new ArgumentException("T must be an enumerated type");
        }

    }
}
