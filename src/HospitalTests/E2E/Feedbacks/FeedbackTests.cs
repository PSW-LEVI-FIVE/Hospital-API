using ceTe.DynamicPDF;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;

namespace HospitalTests.E2E.Feedbacks;

public class FeedbackTests
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
        Driver.Url = "http://localhost:4200/login";
        Driver.Manage().Window.Maximize();
    }
    [TearDown]
    public void CloseBrowser()
    {
        Driver.Quit();
    }
    
    [Test]
    public void Failed_login()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm3691");
        TypeInInput("password", "asdasd");

        Submit("login");
        Sleep(700);
        FindById("error").Text.ShouldBe("Username or password not valid!");
    }
    
    [Test]
    public void Failed_submit()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm369");
        TypeInInput("password", "asdasd");

        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/feedback/create");
        TypeInInput("feedbackContent", "");
        FindById("submitFeedback").Enabled.ShouldBe(false);
    }
    
    [Test]
    public void Success_private_anonymous_submit()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm369");
        TypeInInput("password", "asdasd");

        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/feedback/create");
        TypeInInput("feedbackContent", "Private anonymous feedback");
        Click("privateFlag");
        Click("anonymousFlag");
        FindById("submitFeedback").Enabled.ShouldBe(true);
    }
    
    [Test]
    public void Success_public_anonymous_submit()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm369");
        TypeInInput("password", "asdasd");
        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/feedback/create");
        TypeInInput("feedbackContent", "Public anonymous feedback");
        Click("anonymousFlag");
        FindById("submitFeedback").Enabled.ShouldBe(true);
    }
    
    [Test]
    public void Success_public_not_anonymous_submit()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm369");
        TypeInInput("password", "asdasd");
        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/feedback/create");
        TypeInInput("feedbackContent", "Public not anonymous feedback");
        FindById("submitFeedback").Enabled.ShouldBe(true);
    }
    
    [Test]
    public void Success_private_not_anonymous_submit()
    {
        Navigate("http://localhost:4200/login");
        
        TypeInInput("username", "pRoXm369");
        TypeInInput("password", "asdasd");

        Submit("login");
        Sleep(700);
        Navigate("http://localhost:4200/patient/feedback/create");
        TypeInInput("feedbackContent", "Private not anonymous feedback");
        Click("privateFlag");
        FindById("submitFeedback").Enabled.ShouldBe(true);
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

    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
}