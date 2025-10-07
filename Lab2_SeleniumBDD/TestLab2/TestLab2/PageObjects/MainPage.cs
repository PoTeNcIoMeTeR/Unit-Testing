using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestLab2.PageObjects 
{
    public class MainPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _managerLoginButton = By.CssSelector("button[ng-click='manager()']");

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void ClickBankManagerLogin()
        {
            
            _wait.Until(d => d.FindElement(_managerLoginButton)).Click();
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login");
        }
    }
}