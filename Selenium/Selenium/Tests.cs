using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Selenium
{
    [TestClass]
    public class Tests
    {
        private IWebDriver driver;
        private string driverPath = Directory.GetCurrentDirectory();
        private const string page = @"https://lamp.ii.us.edu.pl/~mtdyd/zawody/";

        /// <summary>
        /// Open browser
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            driver = new ChromeDriver(driverPath);
            driver.Navigate()
                .GoToUrl(page);
        }

        /// <summary>
        /// Parametrized test
        /// </summary>
        /// <param name="name">First name</param>
        /// <param name="surname">Last name</param>
        /// <param name="age">Age</param>
        /// <param name="parents">Parents agreement</param>
        /// <param name="doctor">Doctor agreement</param>
        /// <param name="expected">Expected result</param>
        [TestMethod]
        [DataRow("Jan", "Kowalski", -9, true, true, "Brak kwalifikacji")]
        [DataRow("Jan", "Kowalski", -11, true, true, "Skrzat")]
        [DataRow("Jan", "Kowalski", -13, true, true, "Mlodzik")]
        [DataRow("Jan", "Kowalski", -15, true, true, "Junior")]
        [DataRow("Jan", "Kowalski", -19, false, false, "Dorosly")]
        [DataRow("Jan", "Kowalski", -66, false, true, "Senior")]
        [DataRow("", "Kowalski", -66, false, true, "First name must be filled out")]
        [DataRow("Jan", "", -66, false, true, "Nazwisko musi byc wypelnione")]
        [DataRow("Jan", "Kowalski", null, false, true, "Data urodzenia nie moze byc pusta")]
        [DataRow("Jan", "Kowalski", -11, false, true, "Blad danych")]
        [DataRow("Jan", "Kowalski", -11, true, false, "Blad danych")]
        [DataRow("Jan", "Kowalski", -66, false, false, "Blad danych")]
        public void Test(
            string name,
            string surname,
            int? age,
            bool parents,
            bool doctor,
            string expected)
        {
            // if age has value format date otherwise use empty string
            var date = age.HasValue ? DateTime.Now.AddYears(age.Value).ToString("dd-MM-yyyy") : string.Empty;

            // Extension methods used to test.
            var result = driver.TextInput("inputEmail3", name)
                .TextInput("inputPassword3", surname)
                .TextInput("dataU", date)
                .Checkbox("rodzice", parents)
                .Checkbox("lekarz", doctor)
                .Button("btn")
                .Alert();

            // Asserts
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Close driver after test
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}
