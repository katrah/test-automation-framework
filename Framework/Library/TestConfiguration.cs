using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Framework.Library
{
    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestConfiguration
    {
        /// <summary>
        /// Loads specified xml file to a ScoutTestConfiguration object
        /// </summary>
        /// <param name="configurationFilePath">Location of config xml</param>
        /// <returns>Configuration object to be used in test execution</returns>
        public static TestConfiguration GetInstance(string configurationFilePath)
        {
            if (string.IsNullOrEmpty(configurationFilePath))
            {
                throw new TestConfigurationException(string.Format("Unable to deserialize {0}", configurationFilePath), new ArgumentNullException("configurationFilePath"));
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestConfiguration));
                using (FileStream reader = new FileStream(configurationFilePath, FileMode.Open))
                {
                    return (TestConfiguration)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new TestConfigurationException(string.Format("Unable to deserialize {0}", configurationFilePath), ex);
            }
        }

        /// <summary>
        /// The name of the config file used for this TestConfiguration.  Relative or full path.
        /// </summary>
        public string ConfigFile { get; set; }

        [XmlElement]
        public DatabaseConfig DatabaseConfig { get; set; }

        [XmlElement]
        public JobSystemConfig JobSystemConfig { get; set; }

        [XmlElement]
        public string DownloadDir { get; set; }

        [XmlElement]
        public bool IsPerformanceRun { get; set; }

        [XmlElement]
        public string PlatformVersion { get; set; }

        [XmlElement("CustomerInstances")]
        public TestCustomerList TestCustomerList { get; set; }

        [XmlElement]
        public string TestDataDir { get; set; }

        [XmlElement]
        public TestUrlList TestUrlList { get; set; }

        [XmlElement]
        public TestUserList TestUserList { get; set; }

        [XmlElement]
        public BrowserConfig BrowserConfig { get; set; }

        [XmlElement]
        public string WebTestMethod { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class BrowserConfig
    {
        [XmlElement("ConfigItem")]
        public ConfigItem[] ConfigItems { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class ConnStr
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class DatabaseConfig
    {
        [XmlElement("ConnStr")]
        public ConnStr[] ConnectionStrings { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class JobSystemConfig
    {
        [XmlElement("ConfigItem")]
        public ConfigItem[] ConfigItems { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class ConfigItem
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestUserList
    {
        [XmlElement("TestUser")]
        public TestUser[] TestUsers { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public class TestUser
    {
        [JsonIgnore]
        [XmlElement]
        public int Access { get; set; }

        [JsonProperty]
        [XmlElement]
        public string Password { get; set; }
        
        [JsonProperty]
        [XmlElement]
        public TestUserType Type { get; set; }

        [JsonProperty]
        [XmlElement]
        public string UserName { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestUrlList
    {
        [XmlElement("TestUrl")]
        public TestUrl[] TestUrls { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestUrl
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Url { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestCustomerList
    {
        [XmlElement("CustomerInstance")]
        public TestCustomer[] TestCustomers { get; set; }
    }

    /// <summary>
    /// These classes represent the serializable hierarchy of the test configuration settings xml
    /// </summary>
    [Serializable]
    public sealed class TestCustomer
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string ShortCode { get; set; }
    }
}
