using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using ConsoleApp2;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;


public class FirstSelenium
{
    public static string DataCsvFile= System.IO.Directory.GetCurrentDirectory();

    static void Main()
    {

        var TestDtaList = ReadFile(DataCsvFile + "\\data\\data.csv");

        CreateReportDirectorys();

        IWebDriver driver = new ChromeDriver();

        ExtentReports extentReports = new ExtentReports();

        ExtentSparkReporter reportpath = new ExtentSparkReporter(@"D:\RepoortLocation\Report" + DateTime.Now.ToString("_dd/MM/yyyy_hhmmss") + ".html");

        extentReports.AttachReporter(reportpath);

        ExtentTest test = extentReports.CreateTest("Login Test", "This our first test Case");

        driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");

    



        test.Log(Status.Info, "Open browser");

        Console.WriteLine("Open Browser");

        driver.Manage().Window.Maximize();
        Console.WriteLine("Browser Maximize");

        foreach (var testdata in TestDtaList)
        {
            driver.FindElement(By.Id("username")).SendKeys(testdata.username);
            Console.WriteLine("Provide username: "+ testdata.username);
            test.Log(Status.Info, "Provide username: "+ testdata.username);


            driver.FindElement(By.Id("password")).SendKeys(testdata.password);
            Console.WriteLine("Provide Password:" + testdata.password);
            test.Log(Status.Info, "Provide Password: " + testdata.password);

        }



        driver.FindElement(By.Id("submit")).Click();
        Console.WriteLine("Hit Submit button");
        test.Log(Status.Info, "Hit Submit button");
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



    static List<Testdata> ReadFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return new List<Testdata>(csv.GetRecords<Testdata>());
        }
    }


}


    

