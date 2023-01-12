using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = NUnit.Framework.Assert;
namespace HospitalTests.E2E.ExaminationReport;

public class ExaminationReportTest
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
        Navigate("http://localhost:4200/doctor/examination/5/report");
        
        TypeInInput("input-symptoms", "Blood", 2000);
        FindAllByCssSelector(".mat-option-text")[0].Click();
        Sleep(1000);
        DeleteCharacters("input-symptoms", 5);
        Click("symptoms-next");
        
        TypeInInput("report", "This is test for report");
        Click("report-next");
        
        
        TypeInInput("input-medicine", "Paracetamol", 5000);
        FindAllByCssSelector(".mat-option-text")[0].Click();
        Sleep(1000);
        
        FindAllByCssSelector(".prescription input")[0].SendKeys("3x3");
        Click("prescriptions-next");
        
        Click("done");
        
        Sleep(10000);
        
        Assert.Pass();
    }
    
    
    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }

    private void Navigate(string url)
    {
        Driver.Url = url;
        Sleep(4000);
    }
    
    private void Login()
    {
        TypeInInput("input-username", "doktor");
        TypeInInput("input-password", "asdasd123");
        Submit("submit-login");
    }

    private void Click(string id)
    {
        var element = FindById(id);
        element.Click();
        Sleep(1000);
    }
    
    private void TypeInInput(string id, string text, int sleep=1000)
    {
        var element = FindById(id);
        element.SendKeys(text);
        Sleep(sleep);
    }

    private void Submit(string id)
    {
        var element = FindById(id);
        element.Submit();
        Sleep(1000);
    }

    private void DeleteCharacters(string id, int characters)
    {
        var element = FindById(id);
        for(var i = 0; i < characters; i++) 
        {
            element.SendKeys(Keys.Backspace);    
        }
        Sleep(1000);
    }

    private ReadOnlyCollection<IWebElement> FindAllByCssSelector(string selector)
    {
        return Driver.FindElements(By.CssSelector(selector));
    }

    private IWebElement FindById(string id)
    {
        return Driver.FindElement(By.Id(id));
    }

    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

}