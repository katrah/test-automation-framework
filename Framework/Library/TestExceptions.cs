using System;

namespace Framework.Library
{
    /// <summary>
    /// Wrapper Exception for Test Configuration Related Errors
    /// </summary>
    [Serializable]
    public class TestConfigurationException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="args">arguments to construct error message</param>    
        public TestConfigurationException(string message, params object[] args) :
            base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">actual exception thrown</param>
        public TestConfigurationException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Wrapper Exception for Test Framework Related Errors
    /// </summary>
    [Serializable]
    public class TestFrameworkException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="args">arguments to construct error message</param>   
        public TestFrameworkException(string message, params object[] args) :
            base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">actual exception thrown</param>
        public TestFrameworkException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Wrapper Exception for Test Proxy Related Errors
    /// </summary>
    [Serializable]
    public class TestProxyException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="args">arguments to construct error message</param>   
        public TestProxyException(string message, params object[] args) :
            base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">actual exception thrown</param>
        public TestProxyException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Wrapper Exception for the framework discovering test failures
    /// </summary>
    [Serializable]
    public class TestFailureException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="args">arguments to construct error message</param>   
        public TestFailureException(string message, params object[] args) :
            base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">actual exception thrown</param>
        public TestFailureException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
