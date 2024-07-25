using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumExercise
{
    public class SummatorPagePOM
    {
        private readonly AndroidDriver _driver;

        public SummatorPagePOM(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FirstField => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));

        public IWebElement SecondFIeld => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));

        public IWebElement ResultField => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

        public IWebElement CalcButton => _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));

        public string SumNumbers(string firstNum, string secondNum)
        {
            ClearFields();
            FirstField.SendKeys(firstNum);            
            SecondFIeld.SendKeys(secondNum);
            CalcButton.Click();
            return ResultField.Text;
        }

        public void ClearFields()
        {
            FirstField.Clear();
            SecondFIeld.Clear();
        }
    }
}
