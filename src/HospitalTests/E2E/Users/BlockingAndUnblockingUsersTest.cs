using System.Collections.ObjectModel;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HospitalTests.E2E.Users;

public class BlockingAndUnblockingUsersTest
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
        Driver.Url = "http://localhost:4200";
        Driver.Manage().Window.Maximize();
    }
    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }
    
    [Test]
    public void Success_block_user()
    {
        Navigate("http://localhost:4200");
        TypeInInput("input-username", "pRoXm369");
        TypeInInput("input-password", "asdasd123");
        Submit("submit-login");
        Sleep(2000);
        Navigate("http://localhost:4200/manager/malicious-patients");
        
        foreach (IWebElement element in FindByClass("checkB"))
        {
            element.Click();
            FindById("confirmButtonn").Click();
            Sleep(10000);
        }
        
        driver.FindElement(By.Id("confirmButtonn")).Click();
        Sleep(3000);
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