using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string url = "https://www.jobsearch.az/vacancies";

        new DriverManager().SetUpDriver(new ChromeConfig());

        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Search input elementini bul ve arama kelimesini gir
            IWebElement searchInput = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("search__input")));
            searchInput.SendKeys("javascript");


            // list__item__logo sınıfına sahip tüm öğeleri bul
            var listItems = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("list__item")));

            foreach (var item in listItems)
            {
                var anchor = item.FindElement(By.ClassName("list__item__text"));
                if (anchor == null)
                    continue;

                Console.WriteLine(anchor.Text + " -> " + anchor.GetAttribute("href"));
            }
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine("An error occurred while navigating to the URL: " + ex.Message);
        }
        finally
        {
            // Tarayıcıyı kapatın
            driver.Quit();
        }
    }
}