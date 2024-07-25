using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadAppium
{
    public class NotepadPOM
    {
        private readonly AndroidDriver _driver;

        public NotepadPOM(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement SkipButton => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));

        public IWebElement CreateNoteButton => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));

        public IWebElement SelectTextButton => _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));

        public IWebElement TextField => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));

        public IWebElement AddCreatedAndBackButton => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));

        public IWebElement CreatedNoteElement => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

        public IWebElement EditNoteButton => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));

        public IWebElement NoteDotMenu => _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));

        public IWebElement DeleteButton => _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));

        public IWebElement AndroidOKButton => _driver.FindElement(MobileBy.Id("android:id/button1"));

        public IReadOnlyCollection<IWebElement> AllNotesList => _driver.FindElements(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/note_list"));


        public void CreateNote(string text)
        {
            CreateNoteButton.Click();
            SelectTextButton.Click();
            TextField.SendKeys(text);
            AddCreatedAndBackButton.Click();
            AddCreatedAndBackButton.Click();
        }

        public void EditCreatedNote(string text)
        {
            var createdNote = CreatedNoteElement;
            createdNote.Click();
            EditNoteButton.Click();
            TextField.Clear();
            TextField.SendKeys(text);
            AddCreatedAndBackButton.Click();
            AddCreatedAndBackButton.Click();
        }

        public void DeleteCreatedNote()
        {
            var createdNote = CreatedNoteElement;
            createdNote.Click();
            NoteDotMenu.Click();
            DeleteButton.Click();
            AndroidOKButton.Click();
        }
    }
}
