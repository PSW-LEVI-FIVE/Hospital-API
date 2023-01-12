using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Assert = NUnit.Framework.Assert;

namespace HospitalTests.E2E.ExaminationReport;

public class MovingExamination
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
        Navigate("http://localhost:4200/doctor/appointments/create");

        Click("patient-select");
        ReadOnlyCollection<IWebElement> patientOptions = FindAllByCssSelector(".mat-option-text");
        patientOptions[0].Click();
        Sleep(1000);

        Click("room-select");
        ReadOnlyCollection<IWebElement> roomOptions = FindAllByCssSelector(".mat-option-text");
        roomOptions[0].Click();
        Sleep(1000);
        
        
        Click("date-input");
        DeleteCharacters("date-input", 10);
        TypeInInput("date-input", "01/17/2022");
        Sleep(1000);
        
        Click("time-from");
        TypeInInput("time-from", "10");
        TypeInInput("time-from", "00");
        TypeInInput("time-from", "AM");
        
        Click("time-to");
        TypeInInput("time-to", "11");
        TypeInInput("time-to", "00");
        TypeInInput("time-to", "AM");
        
        Click("btn-create");
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