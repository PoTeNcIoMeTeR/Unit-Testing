using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace TestLab2.Hooks
{
    [Binding]
    public class WebDriverSupport
    {
        private readonly IObjectContainer _container;

        public WebDriverSupport(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            var driver = new ChromeDriver();
            _container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void CleanupWebDriver()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver.Quit();
        }
    }
}