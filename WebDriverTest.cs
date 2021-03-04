using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
 
namespace SeleniumTests
{
    [TestFixture]
    public class WdriverTest
    {
        private IWebDriver driver;
    
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            
        }
        [TearDown]
        public void TeardownTest()
        {
            try
            {
            driver.Quit();
            }
            catch (Exception)
            {
            // Ignore errors if unable to close the browser
            }
        }
        [Test, Property("Requirement", "CALC-1")]
        public void GoogleTest()
        {
            // Open Google search engine.
            driver.Navigate().GoToUrl("http://www.google.com/");

            int size = driver.FindElements(By.XPath(".//*[iframe]")).Count;

            driver.SwitchTo().Frame(0);
            Boolean isPresent = driver.FindElements(By.Id("introAgreeButton")).Count > 0;
            if(isPresent)
            {
                driver.FindElement(By.Id("introAgreeButton")).Click();
            }

            // Assert Title of page.
            Assert.AreEqual("Google", driver.Title);
            // Provide search term as "Selenium OpenQA"
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium OpenQA");
            query.Submit();
            Assert.IsTrue(driver.PageSource.Contains("www.selenium.dev"));
        }
    }
}