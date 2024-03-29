﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace HospitalTests.E2E.Rooms;

public class RoomEditTest
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

        Sleep(2000);

        ClickSvg("svgDiv", -300, -100);
        ClickSvg("svgDiv", 0, 150);
        ClickSvg("svgDiv", -350, -200);
        
        DeleteCharacters("roomNameInput", 10);
        TypeInInput("roomNameInput", "Pozdrav od selenijuma");
        Click("confirmRoomEdit");
        
        Assert.Pass();
    }

    private void ClickSvg(string id, int offsetX, int offsetY)
    {
        IWebElement svg = FindById(id);
        Actions actions = new Actions(Driver);
        actions.MoveToElement(svg, offsetX, offsetY).DoubleClick().Build().Perform();
        Sleep(2000);
    }
    
    private void Login()
    {
        TypeInInput("input-username", "fiki");
        TypeInInput("input-password", "123");
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
    
    private IWebElement FindById(string id)
    {
        return Driver.FindElement(By.Id(id));
    }

    private void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }
}