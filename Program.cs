﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
public class FirstSelenium
{
    static void Main()
    {
        CreateReportDirectorys();

        IWebDriver driver = new ChromeDriver();

        ExtentReports extentReports = new ExtentReports();

        ExtentSparkReporter reportpath = new ExtentSparkReporter(@"D:\RepoortLocation\Report"+DateTime.Now.ToString("_dd/MM/yyyy_hhmmss")+".html");

        extentReports.AttachReporter(reportpath);

        ExtentTest test = extentReports.CreateTest("Login Test", "This our first test Case");

        driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");

        test.Log(Status.Info, "Open browser");

        Console.WriteLine("Open Browser");

        driver.Manage().Window.Maximize();
        Console.WriteLine("Browser Maximize");

        driver.FindElement(By.Id("username")).SendKeys("students");
        Console.WriteLine("Provide username");

        driver.FindElement(By.Id("password")).SendKeys("Password123");
        Console.WriteLine("Provide Password");

        driver.FindElement(By.Id("submit")).Click();
        Console.WriteLine("Hit Submit button");
        try
        {
            driver.FindElement(By.CssSelector(".wp-block-button__link")).Click();
            test.Log(Status.Pass, "Login Successfully");
        }
        catch
        {
            Console.WriteLine("Failed Login");
            test.Log(Status.Fail, "Login Unsuccessful");
        }
        driver.Quit();
        extentReports.Flush();

    }
        private static void CreateReportDirectorys()
    {
        string ReportPath = @"D:\RepoortLocation\";
        if (!Directory.Exists(ReportPath))
        {
            Directory.CreateDirectory(ReportPath);
        } 
    }
}
