using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Internal;

namespace NotepadAppium
{
    public class NotepadTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _service;
        private NotepadPOM _pom;

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
            options.DeviceName = "Pixel7";
            options.AutomationName = "UiAutomator2";
            options.PlatformVersion = "14";
            options.PlatformName = "Android";
            options.App = @"D:\SoftUni\Front-End\Front-End-Automation\Appium-Exercise\Notepad.apk";
            options.AddAdditionalAppiumOption("autoGrantPermissions", true);

            _driver = new AndroidDriver(_service, options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _pom = new NotepadPOM(_driver);

            try
            {
                _pom.SkipButton.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Skip button is not visible on the screen.");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _service?.Dispose();
        }

        [Test, Order(1)]
        public void CreateNote()
        {
            _pom.CreateNote("DemoNote");

            Assert.That(_pom.CreatedNoteElement.Text, Is.EqualTo("DemoNote"));
        }

        [Test, Order(2)]
        public void Edit_CreatedNote()
        {
            _pom.EditCreatedNote("Edited");

            Assert.That(_pom.CreatedNoteElement.Text, Is.EqualTo("Edited"));
        }

        [Test, Order(3)]
        public void Delete_CreatedNote()
        {
            _pom.DeleteCreatedNote();

            var deletedNote = _pom.AllNotesList.FirstOrDefault(x => x.Text == "Edited");

            Assert.That(deletedNote, Is.Null);
        }
    }
}