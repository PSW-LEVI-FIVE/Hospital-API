using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Remote;

namespace HospitalTests.E2E.Consiliums;

public class CreateConsiliumTest
{
    IWebDriver Driver;

    [SetUp]
    public void StartBrowser()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("start-maximized");            // open Browser in maximized mode
        options.AddArguments("disable-infobars");           // disabling infobars
        options.AddArguments("--disable-extensions");       // disabling extensions
        options.AddArguments("--disable-gpu");              // applicable to windows os only
        options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
        options.AddArguments("--no-sandbox");               // Bypass OS security model
        options.AddArguments("--disable-notifications");    // disable notifications

        Driver = new ChromeDriver(options);
        Driver.Url = "http://localhost:4200/";
        Driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test()
    {
        
        IWebElement element = Driver.FindElement(By.Id("input-username"));
        element.SendKeys("doktor");
        
        Thread.Sleep(1000);

        element = Driver.FindElement(By.Id("input-password"));
        element.SendKeys("asdasd");
        Thread.Sleep(1000);
        
        element = Driver.FindElement(By.Id("submit-login"));
        element.Submit();
        Thread.Sleep(1000);

        element = Driver.FindElement(By.XPath("//*[contains(text(),'Create consilium')]"));
        element.Click();
        Thread.Sleep(1000);
        
        element = Driver.FindElement(By.Id("reason-consilium"));
        element.SendKeys("Some reason");
        Thread.Sleep(1000);
        
        element = Driver.FindElement(By.Id("from-consilium"));
        DeleteDate(element);
        element.SendKeys("12/25/2022");
        Thread.Sleep(1000);

        element = Driver.FindElement(By.Id("to-consilium"));
        DeleteDate(element);
        element.SendKeys("12/30/2022");
        Thread.Sleep(1000);
        
        element = Driver.FindElement(By.Id("duration-consilium"));
        element.Clear();
        element.SendKeys("30");
        Thread.Sleep(1000);

        element = Driver.FindElement(By.Id("speciality-consilium"));
        element.Click();
        Thread.Sleep(2000);
        element = Driver.FindElement(By.XPath("//*[contains(text(),'INTERNAL_MEDICINE')]"));
        element.Click();

        Thread.Sleep(3000);

        
        element = Driver.FindElement(By.Id("suggest-consilium"));
        element.Submit();
        Thread.Sleep(10000);
        
        element = Driver.FindElement(By.Id("create-consilium"));
        element.Submit();
        Thread.Sleep(3000);
        
        Assert.Pass();
    }

    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }

    private void DeleteDate(IWebElement element)
    {
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
        element.SendKeys(Keys.Backspace);
    }
}