
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Framework.Library.Extensions;

namespace Framework.Library
{
    /// <summary>
    /// Manager of the Test Execution Context
    /// </summary>
    public sealed class IntegrationTestContext
    {
        /*
        #region Private Members
        /// <summary>
        /// For Users who are authorized in multiple Customer Instances, this controls the active instance the test will run on
        /// </summary>
        private string activeInstanceCode;

        /// <summary>
        /// Current User for this Context
        /// </summary>
        private TestUser activeUser;

        /// <summary>
        /// Dictionary of config items related to a Browser
        /// </summary>
        private readonly Dictionary<string, string> browserConfigItems = new Dictionary<string, string>();

        /// <summary>
        /// Connection Strings
        /// </summary>
        private readonly Dictionary<string, string> connStrs = new Dictionary<string, string>();

        /// <summary>
        /// Serializable Config Object
        /// </summary>
        private readonly TestConfiguration instance;

        /// <summary>
        /// Dictionary of config items related to the JobSystem
        /// </summary>
        private readonly Dictionary<string, string> jobSystemConfigItems = new Dictionary<string, string>();

        /// <summary>
        /// Cache for loaded config files such that a config read once will no longer be read again
        /// </summary>
        private static readonly IDictionary<string, TestConfiguration> loadedConfigurationFiles = new Dictionary<string, TestConfiguration>();

        /// <summary>
        /// Singleton lock object
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// PlatformVersion, used for API calls (one of the headers)
        /// </summary>
        private static string platformVersion;

        /// <summary>
        /// URL Dictionary
        /// </summary>
        private readonly IDictionary<EndpointUrlType, string> urlMap;

        private readonly WebTestMethodType webTestMethod;
        #endregion

        #region GetInstance
        /// <summary>
        /// Multi threaded support for reading configuration items
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="configXmlFullPath"></param>
        /// <returns></returns>
        public static IntegrationTestContext GetInstance(string cacheKey, string configXmlFullPath)
        {
            if (string.IsNullOrWhiteSpace(configXmlFullPath) || !File.Exists(configXmlFullPath))
            {
                throw new TestConfigurationException("Invalid/Blank Configuration File ({0})", configXmlFullPath);
            }

            cacheKey = cacheKey.ToLowerInvariant();
            lock (locker)
            {
                if (!loadedConfigurationFiles.ContainsKey(cacheKey))
                {
                    loadedConfigurationFiles.Add(cacheKey, TestConfiguration.GetInstance(configXmlFullPath));
                }
            }
            return new IntegrationTestContext(loadedConfigurationFiles[cacheKey]);
        }
        #endregion

        public event ActiveUserChangedEventHandler ActiveUserChanged;

        public event ActiveInstanceChangedEventHandler ActiveInstanceChanged;

        #region Public Members
        /// <summary>
        /// For Users who are authorized in multiple Customer Instances, this controls the active instance the test will run on
        /// </summary>
        public string ActiveInstanceCode
        {
            get
            {
                return activeInstanceCode;
            }
            set
            {
                if (!value.Equals(activeInstanceCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    activeInstanceCode = value;
                    Controller.Log("Context: ActiveInstanceCode set to: {0}", activeInstanceCode);
                }
                onActiveInstanceChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Token of the current active customer instance the proxy will hit on
        /// </summary>
        public string ActiveInstanceToken { get; set; }

        /// <summary>
        /// Current User for this Context
        /// </summary>        
        public TestUser ActiveUser
        {
            get
            {
                if (activeUser == null)
                {
                    throw new TestConfigurationException("No ActiveUser configured.");
                }
                return activeUser;
            }
            set
            {
                if (value == null)
                {
                    throw new TestConfigurationException("ActiveUser cannot be set to null");
                }

                if (activeUser != null && value.UserName.Equals(activeUser.UserName, StringComparison.InvariantCultureIgnoreCase) && value.Type.Equals(activeUser.Type))
                {
                    return;
                }

                string currentInstanceCode = activeInstanceCode;
                activeUser = value;
                Controller.Log("Context: {0} set as Active User", value.UserName);

                onActiveUserChanged(EventArgs.Empty);
                if (!string.IsNullOrEmpty(currentInstanceCode) && !currentInstanceCode.Equals(ActiveInstanceCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    ActiveInstanceCode = currentInstanceCode;
                }
            }
        }

        public Dictionary<string, string> BrowserConfigItems
        {
            get
            {
                return browserConfigItems;
            }
        }

        public Dictionary<string, string> ConnectionStrings
        {
            get
            {
                return connStrs;
            }
        }

        /// <summary>
        /// Test Controller Instance
        /// </summary>
        public TestController Controller { get; private set; }

        /// <summary>
        /// Customer Instances to use per configuration
        /// </summary>
        public IDictionary<string, string> CustomerInstances { get; private set; }

        /// <summary>
        /// Directory where files will be written to if the test downloads a file
        /// </summary>
        public string DownloadDir
        {
            get
            {
                return instance.DownloadDir;
            }
        }

        /// <summary>
        /// Is the run configured to fail on Microbenchmark Performance failures?
        /// </summary>
        public bool IsPerformanceRun
        {
            get
            {
                return instance.IsPerformanceRun;
            }
        }

        public Dictionary<string, string> JobSystemConfigItems
        {
            get
            {
                return jobSystemConfigItems;
            }
        }

        /// <summary>
        /// Test Performance Helper Instance
        /// </summary>
        public TestPerformanceHelper PerformanceHelper { get; private set; }

        /// <summary>
        /// PlatformVersion
        /// </summary>
        public string PlatformVersion
        {
            get
            {
                return platformVersion;
            }
        }

        /// <summary>
        /// Where your test data root dir is
        /// </summary>
        public string TestDataDir
        {
            get
            {
                return instance.TestDataDir;
            }
        }

        /// <summary>
        /// Flag for whether or not the ActiveInstanceToken is used
        /// </summary>
        public bool UseInstanceToken { get; set; }

        /// <summary>
        /// Determines if you test using a browser or jus through API calls
        /// </summary>
        public WebTestMethodType WebTestMethod
        {
            get
            {
                return webTestMethod;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get a specific TestUser by TestUserType and Username
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="userName"></param>
        /// <param name="throwIfNoResult"></param>
        /// <returns></returns>
        public TestUser GetUser(TestUserType userType, string userName, bool throwIfNoResult = true)
        {
            TestUser result = null;
            TestUser[] userbase = instance.TestUserList.TestUsers.Where(user => user.Type == userType).ToArray();
            foreach (TestUser user in userbase)
            {
                if (user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    result = user;
                    break;
                }
            }

            if (result == null && throwIfNoResult)
            {
                throw new TestConfigurationException("Unable to find User of TestUserType {0} with UserName {1}", userType, userName);
            }
            return result;
        }

        /// <summary>
        /// List of all users of a certain type having specified UserAccessFlags enabled
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="requiredAccessFlags"></param>
        /// <param name="exactMatch"></param>
        /// <param name="throwIfNoResult">throw TestConfigurationException if not found</param>
        /// <returns></returns>
        public TestUser[] GetUsers(TestUserType userType, UserAccessFlags requiredAccessFlags, bool exactMatch = false, bool throwIfNoResult = true)
        {
            // Assume exactMatch is false
            TestUser[] result = instance.TestUserList.TestUsers.Where(user => (user.Type == userType && (user.Access & (int)requiredAccessFlags) == (int)requiredAccessFlags)).ToArray();
            if (exactMatch)
            {
                result = instance.TestUserList.TestUsers.Where(user => (user.Type == userType && user.Access == (int)requiredAccessFlags)).ToArray();
            }

            if (result.Length == 0)
            {
                if (throwIfNoResult)
                {
                    throw new TestConfigurationException("Unable to find Users with UserAccessFlags {0} enabled", requiredAccessFlags);
                }
                Controller.Warn("Context: Unable to find Users with UserAccessFlags {0} enabled", requiredAccessFlags);
            }
            else
            {
                Controller.Log("Context: Found {0} User(s) (UserAccessFlags: {1})", result.Length, requiredAccessFlags);
            }
            return result;
        }

        /// <summary>
        /// Gets the URL given the EndpointUrlType
        /// </summary>
        /// <param name="urlType">URL key</param>
        /// <param name="throwIfMissing">throw TestConfigurationException if not found</param>
        /// <returns>URL of the desired endpoint</returns>
        public string GetUrl(EndpointUrlType urlType, bool throwIfMissing = true)
        {
            if (urlMap.ContainsKey(urlType))
            {
                return urlMap[urlType];
            }

            if (throwIfMissing)
            {
                throw new TestConfigurationException("Cannot Find Url of type: {0}", urlType);
            }
            Controller.Warn("Context: Cannot Find Url of type: {0}", urlType);
            return null;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ActiveUserChangedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ActiveInstanceChangedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Private Constructor
        /// </summary>
        /// <param name="config">deserialized configuration object</param>
        private IntegrationTestContext(TestConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            Controller = new TestController();
            PerformanceHelper = new TestPerformanceHelper(Controller);
            instance = config;
            urlMap = new Dictionary<EndpointUrlType, string>();

            try
            {
                foreach (TestUrl url in instance.TestUrlList.TestUrls)
                {
                    urlMap.Add((EndpointUrlType)Enum.Parse(typeof(EndpointUrlType), url.Key), url.Url);
                    Controller.Log("Context: URL Retrieved (Key: {0}, Value: {1})", url.Key, url.Url);
                }
            }
            catch (Exception e)
            {
                throw new TestConfigurationException("Unable to properly deserialize endpoint URLs", e);
            }

            CustomerInstances = new Dictionary<string, string>();

            foreach (TestCustomer customer in instance.TestCustomerList.TestCustomers)
            {
                CustomerInstances.Add(customer.Key, customer.ShortCode);
                Controller.Log("Context: Customer Instance retrieved (Key: {0}, ShortCode: {1})", customer.Key, customer.ShortCode);
            }

            foreach (ConnStr connStr in instance.DatabaseConfig.ConnectionStrings)
            {
                connStrs.AddDistinct(connStr.Key, connStr.Value);
                Controller.Log("Context: Connection String retrieved (Key: {0}, Value: {1})", connStr.Key, connStr.Value);
            }

            foreach (ConfigItem item in instance.JobSystemConfig.ConfigItems)
            {
                jobSystemConfigItems.AddDistinct(item.Key, item.Value);
                Controller.Log("Context: ConfigItem retrieved (Key: {0}, Value: {1})", item.Key, item.Value);
            }

            foreach (ConfigItem item in instance.BrowserConfig.ConfigItems)
            {
                browserConfigItems.AddDistinct(item.Key, item.Value);
                Controller.Log("Context: ConfigItem retrieved (Key: {0}, Value: {1})", item.Key, item.Value);
            }

            platformVersion = instance.PlatformVersion;
            webTestMethod = instance.WebTestMethod.ToEnum<WebTestMethodType>(true);
        }

        private void onActiveUserChanged(EventArgs e)
        {
            if (ActiveUserChanged != null)
            {
                ActiveUserChanged(this, e);
            }
        }

        private void onActiveInstanceChanged(EventArgs e)
        {
            if (ActiveInstanceChanged != null)
            {
                ActiveInstanceChanged(this, e);
            }
        }
        */
    }
    

    /// <summary>
    /// Endpoints:
    /// </summary>
    [Serializable]
    public enum EndpointUrlType
    {
        C2rApiBase,
        DalServiceUri,
        DataFabricApiBase,
        EdsApiBase,
        GatewayApiBase,
        KeyServiceUri,
        JobMasterUri,
        JobRunnerUri,
        LoggingServiceUri,
        PersistenceApiBase,
        PhoneBookUri,
        QueryApiBase,
        RenewApiBase,
        RolesAndResourcesServiceUri,
        SecurityApiBase,
        SegmentApiBase,
        ScoutUiBase
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TestUserType
    {
        C2R,
        RA,
        Renew,
        Token
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    [Serializable]
    public enum UserAccessFlags
    {
        All = 0,
        Track = 1,
        Analyze = 2,
        Reports = 4,
        Programs = 8,
        Automation = 16,
        Users = 32,
        Hub = 64,
        MultiInstance = 128,
        FullAccess = 255
    }

    public enum WebTestMethodType
    {
        WebApi,
        WebUi
    }
}
