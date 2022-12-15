using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using System.Xml.Linq;
using Assert = NUnit.Framework.Assert;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace HospitalTests.E2E.AnnualLeaves;

public class ReviewRequestTests
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
        Driver.Url = "http://localhost:4200";
        Driver.Manage().Window.Maximize();
    }
    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }
    [Test]
    public void Success_review_request()
    {
        Login();
        Navigate("http://localhost:4200/manager/annual-leave");
        Sleep(700);
        IWebElement element = FindAllByTagName("button")[0];
        element.Click();
        Sleep(200);
        element = Driver.FindElement(By.TagName("mat-select"));
        element.Click();
        IWebElement matOption = Driver.FindElement(By.XPath("//mat-option[@value='APPROVED']"));
        matOption.Click();
        element = Driver.FindElement(By.TagName("button"));
        element.Click();
        Sleep(200);
        Assert.AreEqual(Driver.SwitchTo().Alert().Text, "Review successfully sent!");
    }
    [Test]
    public void Fail_didnt_choose_option()
    {
        Login();
        Navigate("http://localhost:4200/manager/annual-leave");
        Sleep(700);
        IWebElement element = FindAllByTagName("button")[0];
        element.Click();
        Sleep(200);
        element = Driver.FindElement(By.TagName("button"));
        element.Click(); 
        Sleep(200);
        element = Driver.FindElement(By.ClassName("err"));
        element.Text.ShouldBe("You didn't choose review option");
    }

    [Test]
    public void Fail_didnt_enter_reason()
    {
        Login();
        Navigate("http://localhost:4200/manager/annual-leave");
        Sleep(700);
        IWebElement element = FindAllByTagName("button")[0];
        element.Click();
        Sleep(200);
        element = Driver.FindElement(By.TagName("mat-select"));
        element.Click();
        IWebElement matOption = Driver.FindElement(By.XPath("//mat-option[@value='CANCELED']"));
        matOption.Click();
        element = Driver.FindElement(By.TagName("button"));
        element.Click();
        Sleep(200);
        element = Driver.FindElement(By.ClassName("err"));
        element.Text.ShouldBe("You didn't enter rejection reason");
    }

    private void Login()
    {
        TypeInInput("input-username", "fikiSigma");
        TypeInInput("input-password", "123");
        Submit("submit-login");
        Sleep(2000);
    } 
    private void TypeInInput(string id, string text, int sleep = 300)
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
    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
    private void Navigate(string url)
    {
        Driver.Url = url;
        Sleep(4000);
    }
    private ReadOnlyCollection<IWebElement> FindAllByTagName(string tagName)
    {
        return Driver.FindElements(By.TagName(tagName));
    }
}
