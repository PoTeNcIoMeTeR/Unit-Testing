using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestLab2.PageObjects
{
    public class CustomersPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By _customersTab = By.CssSelector("button[ng-click='showCust()']");
        private readonly By _searchInput = By.CssSelector("input[ng-model='searchCustomer']");

        public CustomersPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void GoToCustomersTab()
        {
            _wait.Until(d => d.FindElement(_customersTab)).Click();
        }
        public void SearchByText(string searchText)
        {
            var searchField = _wait.Until(d => d.FindElement(_searchInput));
            searchField.Clear();
            searchField.SendKeys(searchText);
        }
        public string GetFirstRowAccountNumbers()
        {
            var firstRowAccountCell = By.CssSelector("tbody tr:first-child td:nth-child(4)");
            return _wait.Until(d => d.FindElement(firstRowAccountCell)).Text;
        }
    }
}