using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestLab2.PageObjects
{
    public class ManagerPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _openAccountTab = By.CssSelector("button[ng-click='openAccount()']");
        private readonly By _customerDropdown = By.Id("userSelect");
        private readonly By _currencyDropdown = By.Id("currency");
        private readonly By _processButton = By.CssSelector("button[type='submit']");

        public ManagerPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToOpenAccountTab()
        {
            _wait.Until(d => d.FindElement(_openAccountTab)).Click();
        }

        public void SelectCustomer(string customerName)
        {
            var customerElement = _wait.Until(d => d.FindElement(_customerDropdown));
            var customerSelect = new SelectElement(customerElement);
            customerSelect.SelectByText(customerName);
        }

        public void SelectCurrency(string currency)
        {
            var currencyElement = _wait.Until(d => d.FindElement(_currencyDropdown));
            var currencySelect = new SelectElement(currencyElement);
            currencySelect.SelectByText(currency);
        }

        public void ClickProcess()
        {
            _wait.Until(d => d.FindElement(_processButton)).Click();
        }

        public string GetAlertText()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(drv => drv.SwitchTo().Alert());

            var alert = _driver.SwitchTo().Alert();
            string text = alert.Text;
            alert.Accept();
            return text;
        }
    }
}