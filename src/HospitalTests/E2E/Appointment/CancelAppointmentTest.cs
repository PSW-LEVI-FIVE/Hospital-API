using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Shouldly;
using Assert = NUnit.Framework.Assert;

namespace HospitalTests.E2E.Appointment;

public class CancelAppointmentTest
{
     IWebDriver Driver;
     WebDriver driver = new ChromeDriver();

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
        Driver.Url = "http://localhost:4200/login";
        Driver.Manage().Window.Maximize();
    }
    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }

    [Test]
    public void Success_cancel_appointment()
    {
        Navigate("http://localhost:4200/login");
        TypeInInput("username", "pRoXm369");
<<<<<<< HEAD
        TypeInInput("password", "asdasd");
=======
        TypeInInput("password", "asdasd123");
>>>>>>> 2837106c45bde0d4e09111a8c3586fe3ab34d24b
        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/myAppointments");

        foreach (IWebElement element in FindByClass("inner_button"))
        {
            element.Click();
            Sleep(5000);

        }
        Sleep(5000);
        Assert.Pass();

    }
    private void Navigate(string url)
    {
        Driver.Url = url;
        Sleep(4000);
    }
    
    private void Click(string id)
    {
        var element = FindById(id);
        element.Click();
        Sleep(1000);
    }

    private void TypeInInput(string id, string text, int sleep=300)
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
    private IWebElement FindById(string id)
    {
        return Driver.FindElement(By.Id(id));
    }

    private ReadOnlyCollection<IWebElement> FindByClass(string id)
    {
        return Driver.FindElements(By.ClassName(id));
    }

    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
}