using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumExercise
{
    public class SummatorAppTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _service;
        private SummatorPagePOM _pom;

        [OneTimeSetUp]
        public void Setup()
        {
            _service = new AppiumServiceBuilder()
            .WithIPAddress("127.0.0.1")
            .UsingPort(4723)
            .WithStartUpTimeOut(TimeSpan.FromSeconds(10))
            .Build();

            _service.Start();

            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";
            options.PlatformVersion = "14";
            options.DeviceName = "Pixel7";
            options.App = @"C:\Users\missie\Desktop\com.example.androidappsummator.apk";

            _driver = new AndroidDriver(_service, options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _pom = new SummatorPagePOM(_driver);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _service?.Dispose();
        }

        [Test]
        public void TestWithValidNummbers()
        {
            var result = _pom.SumNumbers("2", "2");
            Assert.That(result, Is.EqualTo("4"));

        }

        [Test]
        [TestCase("", "2", "error")]
        [TestCase("2", "", "error")]
        [TestCase("-2", "", "error")]
        [TestCase("", "-2", "error")]
        [TestCase("", "", "error")]
        [TestCase("string", "2", "error")]
        [TestCase("2", "string", "error")]
        [TestCase("string", "string", "error")]
        public void TestWithInvalidNummbers(string firstNumber, string secondNumber, string expected)
        {
           var result = _pom.SumNumbers(firstNumber, secondNumber);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}