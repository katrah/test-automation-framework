using Automation.Common.Helpers;
using Automation.Common.Reqnroll;
using Reqnroll;

namespace Automation.Common.SpecFlow
{
    [Binding]
    public class Hooks
    {
        private static WebTestBase wtb = null;
        private static Storage storage = null;

        #region Web
        [BeforeFeature("Web")]
        public static void BeforeWebFeature(FeatureContext featureContext)
        {
            storage = Storage.GetInstance(featureContext);
            wtb = WebTestBase.GetInstance();
            wtb.TestFixtureSetUp();
        }

        [BeforeScenario("Web")]
        public static void BeforeWebScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage = Storage.GetInstance(scenarioContext);
            wtb = WebTestBase.GetInstance();
            wtb.SetUp();
            storage.SetWebTestMethod(wtb.Context.WebTestMethod);
        }

        [AfterScenario("Web")]
        public static void AfterWebScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage.DisposeFeatureContext();
            storage = Storage.GetInstance(scenarioContext);
            storage.DisposeScenarioContext();
            wtb = WebTestBase.GetInstance();
            wtb.TearDown();
        }

        [AfterFeature("Web")]
        public static void AfterWebFeature(FeatureContext featureContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage.DisposeFeatureContext();
            wtb = WebTestBase.GetInstance();
            wtb.TearDown();
            wtb.TestFixtureTearDown();
        }
        #endregion

        #region UI
        [BeforeFeature("UI")]
        public static void BeforeUiFeature(FeatureContext featureContext)
        {
            storage = Storage.GetInstance(featureContext);
            wtb = WebTestBase.GetInstance();
            wtb.TestFixtureSetUp();
        }

        [BeforeScenario("Ui")]
        public static void BeforeUiScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage = Storage.GetInstance(scenarioContext);
            wtb = WebTestBase.GetInstance();
            wtb.SetUp();
            storage.SetWebTestMethod(wtb.Context.WebTestMethod);
        }

        [AfterScenario("Ui")]
        public static void AfterUiScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage.DisposeFeatureContext();
            storage = Storage.GetInstance(scenarioContext);
            storage.DisposeScenarioContext();
            wtb = WebTestBase.GetInstance();
            wtb.TearDown();
        }

        [AfterFeature("Ui")]
        public static void AfterUiFeature(FeatureContext featureContext)
        {
            storage = Storage.GetInstance(featureContext);
            storage.DisposeFeatureContext();
            wtb = WebTestBase.GetInstance();
            wtb.TearDown();
            wtb.TestFixtureTearDown();
        }
        #endregion
    }
}
