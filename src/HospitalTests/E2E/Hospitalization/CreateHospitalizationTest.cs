using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework;
namespace HospitalTests.E2E.Hospitalization;

public class CreateHospitalizationTest
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
        Login();
        Navigate("http://localhost:4200/doctor/records");

        Click("create-button");
        
        Click("patientBox");
        Click("patientOption");
        
        Click("roomBox");
        Click("roomOption");
        
        Click("bedBox");
        Click("bedOption");

        var DateTime = FindById("dateTimeBox");
        DateTime.SendKeys("12");
        DateTime.SendKeys("30");
        DateTime.SendKeys("2022");
        DateTime.SendKeys(Keys.Tab);
        DateTime.SendKeys("10");
        DateTime.SendKeys("10");
        DateTime.SendKeys("AM");
        Sleep(1000);
        
        Click("save-button");
        
        Sleep(3000);
        
        Assert.Pass();
    }
    
    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
    
    private void Navigate(string url)
    {
        Driver.Url = url;
        Sleep(3000);
    }
    
    private void Click(string id)
    {
        var element = FindById(id);
        element.Click();
        Sleep(1000);
    }
    
    private void Login()
    {
        TypeInInput("input-username", "doktor");
        TypeInInput("input-password", "asdasd123");
        Submit("submit-login");
    }

    private void Submit(string id)
    {
        var element = FindById(id);
        element.Submit();
        Sleep(1000);
    }

    private void TypeInInput(string id, string text, int sleep=1000)
    {
        var element = FindById(id);
        element.SendKeys(text);
        Sleep(sleep);
    }
    
    private IWebElement FindById(string id)
    {
        return Driver.FindElement(By.Id(id));
    }

    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }
    
    
}