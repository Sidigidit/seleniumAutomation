using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    public class TestJSAlerts
    {
        private IWebDriver _driver;
        private string url;
        private string alertJS;
        private string confirmJS;
        private string promptJS;
        private string promptInput;

        [SetUp]
        public void StartBrowser()
        {

            url = "https://the-internet.herokuapp.com/javascript_alerts";
            alertJS = "I am a JS Alert";
            confirmJS = "I am a JS Confirm";
            promptJS = "I am a JS prompt";
            promptInput = "Test Prompt Sigit";

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
        }

        [Test]
        public void TC1_JSAlert()
        {
            _driver.FindElement(By.CssSelector("li:nth-child(1) > button")).Click();
            Assert.That(_driver.SwitchTo().Alert().Text, Is.EqualTo(alertJS));
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void TC6_JSConfirm()
        {
            _driver.FindElement(By.CssSelector("li:nth-child(2) > button")).Click();
            Assert.That(_driver.SwitchTo().Alert().Text, Is.EqualTo(confirmJS));
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void TC11_JSPrompt()
        {
            _driver.FindElement(By.CssSelector("li:nth-child(3) > button")).Click();
            Assert.That(_driver.SwitchTo().Alert().Text, Is.EqualTo(promptJS));
            _driver.SwitchTo().Alert().SendKeys(promptInput);
            _driver.SwitchTo().Alert().Accept();
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
