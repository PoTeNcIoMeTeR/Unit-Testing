using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TestLab2.PageObjects; 

namespace TestLab2.StepDefinitions 
{
    [Binding]
    public class OpenAccountSteps
    {
        private readonly IWebDriver _driver;
        private readonly MainPage _mainPage;
        private readonly ManagerPage _managerPage;
        private readonly CustomersPage _customersPage;

        private string _newlyCreatedAccountNumber;

        public OpenAccountSteps(IWebDriver driver)
        {
            _driver = driver;
            _mainPage = new MainPage(_driver);
            _managerPage = new ManagerPage(_driver);
            _customersPage = new CustomersPage(_driver);
        }

        [Given(@"the bank manager is on the main banking page")]
        public void GivenTheBankManagerIsOnTheMainBankingPage()
        {
            _mainPage.NavigateTo();
        }

        [When(@"he clicks on the ""(.*)"" button")]
        public void WhenHeClicksOnTheButton(string buttonName)
        {
            _mainPage.ClickBankManagerLogin();
        }

        [When(@"he navigates to the ""(.*)"" tab")]
        public void WhenHeNavigatesToTheTab(string tabName)
        {
            _managerPage.GoToOpenAccountTab();
        }

        [When(@"he selects the customer ""(.*)"" from the list")]
        public void WhenHeSelectsTheCustomerFromTheList(string customerName)
        {
            _managerPage.SelectCustomer(customerName);
        }

        [When(@"he selects the currency ""(.*)""")]
        public void WhenHeSelectsTheCurrency(string currency)
        {
            _managerPage.SelectCurrency(currency);
        }

        [When(@"he clicks the ""(.*)"" button")]
        public void WhenHeClicksTheButton(string buttonName)
        {
            _managerPage.ClickProcess();
        }

       

        [Then(@"he should see an alert with the text about successful account creation")]
        public void ThenHeShouldSeeAnAlertWithTheTextAboutSuccessfulAccountCreation()
        {
            string alertText = _managerPage.GetAlertText();
       
            Assert.IsTrue(alertText.Contains("Account created successfully"),
                $"Expected text was not found in the alert. Actual text: {alertText}");

           
            _newlyCreatedAccountNumber = alertText.Split(':').Last().Trim();
        }

        [Then(@"the new account should be visible in the customer list for ""(.*)""")]
        public void ThenTheNewAccountShouldBeVisibleInTheCustomerListFor(string customerName)
        {

            _customersPage.GoToCustomersTab();

            _customersPage.SearchByText(_newlyCreatedAccountNumber);

            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
                wait.Until(d => false);
            }
            catch (WebDriverTimeoutException)
            {
                
            }
            string accountNumbersOnPage = _customersPage.GetFirstRowAccountNumbers();
            Assert.IsTrue(accountNumbersOnPage.Contains(_newlyCreatedAccountNumber),
                $"Searched for account '{_newlyCreatedAccountNumber}' but it was not found. Found accounts: '{accountNumbersOnPage}'");
        }
    }
}