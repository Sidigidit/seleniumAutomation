using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    public class TestComputer
    {
        private IWebDriver _driver;
        private string url;
        private string computer;
        private string existComp;
        private string introducedDate;
        private string discontinuedDate;
        private string company;

        [SetUp]
        public void StartBrowser()
        {

            url = "http://computer-database.gatling.io/computers";
            existComp = "ACE";
            computer = "x280";
            introducedDate = "2021-12-31";
            discontinuedDate = "2022-12-01";
            company = "Nintendo";

            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
        }

        [Test]
        public void TC1_Access()
        {
            Assert.That(_driver.FindElement(By.CssSelector("#main > h1")).Displayed, Is.True);
            Assert.That(_driver.FindElement(By.CssSelector("tbody")).Displayed, Is.True);
            Assert.That(_driver.FindElement(By.Id("searchsubmit")).Displayed, Is.True);
        }

        [Test]
        public void TC2_SearchComputer()
        {
            _driver.FindElement(By.Id("searchbox")).SendKeys(existComp);
            _driver.FindElement(By.Id("searchsubmit")).Click();
            Assert.That(_driver.FindElement(By.CssSelector("tbody")).Displayed, Is.True);
        }

        [Test]
        public void TC10_AddComputer()
        {
            _driver.FindElement(By.Id("add")).Click();
            _driver.FindElement(By.Id("name")).SendKeys(computer);
            _driver.FindElement(By.Id("introduced")).SendKeys(introducedDate);
            _driver.FindElement(By.Id("discontinued")).SendKeys(discontinuedDate);
            SelectElement selectElement = new SelectElement(_driver.FindElement(By.Id("company")));
            selectElement.SelectByText(company);
            _driver.FindElement(By.CssSelector(".btn.primary")).Submit();

            var notif = _driver.FindElement(By.CssSelector(".alert-message"));
            string strNotif = computer + " has been created";
            Assert.That(notif.Displayed, Is.True);
            Assert.That(notif.Text.Contains(strNotif), Is.True);
        }

        [Test]
        public void TC17_EditComputer()
        {
            _driver.FindElement(By.CssSelector("tr:nth-child(1) > td > a")).Click();
            _driver.FindElement(By.Id("name")).Clear();
            _driver.FindElement(By.Id("name")).SendKeys(computer);
            _driver.FindElement(By.Id("introduced")).SendKeys(introducedDate);
            _driver.FindElement(By.Id("discontinued")).SendKeys(discontinuedDate);
            SelectElement selectElement2 = new SelectElement(_driver.FindElement(By.Id("company")));
            selectElement2.SelectByText(company);
            _driver.FindElement(By.CssSelector(".btn.primary")).Submit();

            var notifEdit = _driver.FindElement(By.CssSelector(".alert-message"));
            string strNotifEdit = computer + " has been updated";
            Assert.That(notifEdit.Displayed, Is.True);
            Assert.That(notifEdit.Text.Contains(strNotifEdit), Is.True);
        }

        [Test]
        public void TC26_DeleteComputer()
        {
            string strDeletedcomputer = _driver.FindElement(By.CssSelector("tr:nth-child(1) > td > a")).Text;
            _driver.FindElement(By.CssSelector("tr:nth-child(1) > td > a")).Click();
            _driver.FindElement(By.CssSelector(".btn.danger")).Submit();

            var notifDelete = _driver.FindElement(By.CssSelector(".alert-message"));
            string strNotifDelete = strDeletedcomputer + " has been deleted";
            Assert.That(notifDelete.Displayed, Is.True);
            Assert.That(notifDelete.Text.Contains(strNotifDelete), Is.True);
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
