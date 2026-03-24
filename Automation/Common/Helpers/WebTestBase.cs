using System;
using System.Collections.Generic;
//using Automation.Click2Renew.Web.Api.Helpers;
//using Automation.Common.UI;
using Common.Tools;
using Framework.Library;
using NUnit.Framework;
//using OpenQA.Selenium;

namespace Automation.Common.Helpers
{
    public class WebTestBase : IntegrationTestBase
    {
        private bool eventsRegistered = false;


        //private ProxyClick2RenewApi click2RenewProxy = null;
        //private Click2RenewBaseMethods click2RenewBaseMethods = null;


        private static volatile WebTestBase instance;
        private static readonly object syncRoot = new object();

        //public List<BrowserObj> Browsers { get; private set; }
        //public BrowserObj CurrentBrowser { get; set; }

        /*
        public IWebDriver Driver
        {
            get
            {
                if (CurrentBrowser != null)
                {
                    return CurrentBrowser.Driver;
                }
                return null;
            }
        }
        */

        /*private ProxyClick2RenewApi click2RenewApiProxy
        {
            get
            {
                if (click2RenewProxy == null)
                {
                    click2RenewProxy = new ProxyClick2RenewApi(Context);
                }
                return click2RenewProxy;
            }
        }*/

        

        private WebTestBase(string cacheKey = null, string configFile = null)
            : base(cacheKey, configFile)
        {
        }

        public static WebTestBase GetInstance(string cacheKey = null, string configFile = null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new WebTestBase(cacheKey, configFile);
                    instance.SetUp();
                }
            }
            return instance;
        }

        public WebTestBase()
        {
            SetUp();
        }

/*
        public Click2RenewBaseMethods Click2Renew
        {
            get
            {
                if (click2RenewBaseMethods == null)
                {
                    click2RenewBaseMethods = new Click2RenewBaseMethods(Performance, click2RenewApiProxy);
                }
                return click2RenewBaseMethods;
            }
        }*/

        
        #region Public Methods
        /*
        public void AddBrowser(BrowserObj browserObj)
        {
            Browsers.Add(browserObj);
            if (CurrentBrowser == null)
            {
                CurrentBrowser = browserObj;
            }
        }
        */
        #endregion

        [SetUp]
        public override void SetUp()
        {
            //Browsers = new List<BrowserObj>();
            if (Context == null)
            {
                eventsRegistered = false;
                base.SetUp();
            }

            if (!eventsRegistered)
            {
                Context.ActiveInstanceChanged -= activeInstanceChanged;
                Context.ActiveUserChanged -= activeUserChanged;
                Context.ActiveInstanceChanged += activeInstanceChanged;
                Context.ActiveUserChanged += activeUserChanged;
                eventsRegistered = true;
            }

            Context.ActiveUser = Context.GetUsers(TestUserType.Renew, UserAccessFlags.FullAccess)[0];
            //Context.ActiveInstanceCode = Context.CustomerInstances["Default"];
            //if (securityApiProxy == null)
            //{
            //    setupSecurityProxy();
            //}
        }

        [TearDown]
        public override void TearDown()
        {
            //Browsers = new List<BrowserObj>();
            base.TearDown();
            eventsRegistered = false;
            //click2RenewProxy = null;
            //click2RenewBaseMethods = null;
        }

        public override void TestFixtureTearDown()
        {
            //CurrentBrowser = null;
            base.TestFixtureTearDown();
        }

        private void activeInstanceChanged(object sender, EventArgs eventArgs)
        {
            //Context.ActiveInstanceToken = securityApiProxy.GetInstanceToken(Context.ActiveInstanceCode);
        }

        private void activeUserChanged(object sender, EventArgs eventArgs)
        {
            //// If dealing with an RA user, re-authenticate automagically
            //if (Context.ActiveUser.Type.Equals(TestUserType.RA))
            //{
            //    securityApiProxy.Authenticate(Context.ActiveUser);
            //}
        }

    }
}
