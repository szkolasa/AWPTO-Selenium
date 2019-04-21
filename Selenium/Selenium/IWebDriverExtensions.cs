using OpenQA.Selenium;

namespace Selenium
{
    static class IWebDriverExtensions
    {
        public static IWebDriver TextInput(this IWebDriver driver, string id, string value)
        {
            driver.FindElement(By.Id(id))
                .SendKeys(value);

            return driver;
        }

        public static IWebDriver Checkbox(this IWebDriver driver, string id, bool value)
        {
            if (value)
            {
                driver.FindElement(By.Id(id))
                    .Click();
            }

            return driver;
        }

        public static IWebDriver Button(this IWebDriver driver, string className)
        {
            driver.FindElement(By.ClassName(className))
                .Click();

            return driver;
        }

        public static string Alert(this IWebDriver driver)
        {
            var alert = driver.SwitchTo()
                .Alert();

            if (alert.Text.StartsWith("Roznica"))
                alert.Accept();

            alert = driver.SwitchTo().Alert();

            var result = alert.Text;
            alert.Accept();

            return result;
        }
    }
}
