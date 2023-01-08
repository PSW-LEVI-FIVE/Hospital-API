using HospitalLibrary.Shared.Exceptions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;


namespace HospitalTests.E2E.RelocationAndRenovation;

public class RelocationTest
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
        element.SendKeys("aljosa");
        Thread.Sleep(1000);
        element = Driver.FindElement(By.Id("input-password"));
        element.SendKeys("aljosa");
        Thread.Sleep(1000);
        element = Driver.FindElement(By.Id("submit-login"));
        element.Submit();   
        Thread.Sleep(1000);
        Navigate("http://localhost:4200/manager/room-schedule/1");
        Thread.Sleep(1000);
        element = Driver.FindElement(By.Id("mat-tab-label-0-1"));
        Thread.Sleep(1000);
        element.Click();
        Actions _action = new Actions(Driver);
        element = Driver.FindElement(By.Id("object-1"));
        _action.MoveToElement(element).Click().Perform();
        
        Thread.Sleep(10000);
        Assert.Pass();
        

    }
    
    [Test]
    public void Test_1()
    {
        IWebElement element = Driver.FindElement(By.Id("input-username"));
        element.SendKeys("aljosa");
        Thread.Sleep(1000);
        element = Driver.FindElement(By.Id("input-password"));
        element.SendKeys("aljosa");
        Thread.Sleep(1000);
        
        element = Driver.FindElement(By.Id("submit-login"));
        element.Submit();   
        Thread.Sleep(1000);
        Navigate("http://localhost:4200/manager/room-schedule/1");
        Thread.Sleep(1000);
        element = Driver.FindElement(By.Id("mat-tab-label-0-1"));
        Thread.Sleep(1000);
        element.Click();
        Actions _action = new Actions(Driver);
        element = Driver.FindElement(By.Id("object-2"));
        _action.MoveToElement(element).Click().Perform();
        Thread.Sleep(1000);
        Assert.Pass();
        

    }
    
    private void Navigate(string url)
    {
        Driver.Url = url;
        Thread.Sleep(4000);
    }
}