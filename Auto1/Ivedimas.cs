using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;        // visi šie using yra bibliotekos!
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoPaskaitos.Auto1
{
    class Ivedimas
    {
        class IvedimoLaukai
        {
            [Test]  // testo atributas, bet dar nepabaigtas testas
            public void RodykZinute() // Testo pavadinimas
            {
                IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(); // sukuriama sasaja tarp Visual Studio ir Chromo. Jei tik sitas butu, atidarutu Chrome ir vel uzdarytu
                driver.Url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html"; // Jis nunaviguoja į konkretų puslapi, kurį nurime nurodyti. Ir atidaro maža langa
                driver.Manage().Window.Maximize(); // Padidina atidarytą langą
                // implicity wait turi buti naudojamas prieš ieškant elementą. ir jis galioja visam testui

                string irasomastekstas = "Testas";
                driver.FindElement(By.Id("at-cv-lightbox-close")).Click(); // paspaudžia ant kryželio, ir išjungia pop up (reklamą).
                driver.FindElement(By.Id("user-message")).SendKeys(irasomastekstas);
                /* ieškai elemento tik vieno todėl FindElement (o ne Elements) - butu elements, jei daug elementų rasti
                   kad surastų konkretų mygtuką ir įrašo žodį į langa "Tekstas"
                */
                driver.FindElement(By.CssSelector("#get-input button")).Click(); // kad paspaustų mygtuką įvedus tekstą # dedamas prie id ir tik su CssSelector tipu

                Assert.AreEqual(irasomastekstas, driver.FindElement(By.Id("display")).Text); // išsilups tekstą iš svetaines ir panaudos jį kaip string.
                // assert - tikrinimas, ar tas lygus tam.


            }


            [Test]

            public void ApskaiciuokSuma()
            {
                IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(); 
                driver.Url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html"; 
                driver.Manage().Window.Maximize(); 
                driver.FindElement(By.Id("at-cv-lightbox-close")).Click();
                driver.FindElement(By.Id("sum1")).SendKeys("10");
                driver.FindElement(By.Id("sum2")).SendKeys("5");
                driver.FindElement(By.CssSelector("#gettotal button")).Click();
                Assert.AreEqual("15", driver.FindElement(By.Id("displayvalue")).Text);


            }

            [Test]

            public void Uzkrove100()
            {
                IWebDriver driver = new ChromeDriver();
                driver.Url = "https://www.seleniumeasy.com/test/bootstrap-download-progress-demo.html";
                driver.Manage().Window.Maximize();
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);

                driver.FindElement(By.Id("cricle-btn")).Click(); // viena karta paspaudzia, jei norim daugiau reikia nukopijuoti sita teksta
                driver.FindElement(By.Id("cricle-btn")).Click(); // kiek nukopijuosi  tiek ir bus


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(
                    ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("percenttext")), "100%")); // tai reiskia, kad klase dar galioja, bet gali nebegalioti po puses metu

                Assert.AreEqual("100%", driver.FindElement(By.ClassName("percenttext")).Text);

            }

            /*
                1) Thread.Sleep(milisekundes);
                2) Implicitly wait -> driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120); tikrina, tikrina, tikrina
                3) Explicitly wait ->WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30)); - nori kazkokios konkrecios dalygos, pvz, kad pasirodytu 100 proc.
            wait.Until(  - pvz gali buti naudojamas su mokejimais pvz.: mokejimas įvykdytas sekmingai.  
                ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("percenttext")),"100%"));
               4) Fluent Wait - cia gali nurodyti kas kiek laiko tikrinti, bet tam reikia programeriu pagalbos.


            */

            [Test]
            public void LangelisCheckbox()
            {
                IWebDriver driver = new ChromeDriver();
                driver.Url = "https://www.seleniumeasy.com/test/basic-checkbox-demo.html"; // užžymi viena checkboxą
                driver.Manage().Window.Maximize();

                driver.FindElement(By.Id("isAgeSelected")).Click();

            }

            [Test]
            public void PatikrinkArVisiCheckboxPazymeti()
            {
                IWebDriver driver = new ChromeDriver();
                driver.Url = "https://www.seleniumeasy.com/test/basic-checkbox-demo.html";
                driver.Manage().Window.Maximize();
                // driver.FindElement(By.Id("at-cv-lightbox-close")).Click();

                var checkboxai = driver.FindElements(By.ClassName("cb1-element")); // kai parašoma var, jis pats nusprendžia kosk  tipoas: int, string ir pan.
                var checkboxas = driver.FindElement(By.ClassName("cb1-element")); 

                // Be to, jei norime panaudoti masyvą ar cikla privaloma turėt kintamuosius, nes kitaip neveiks. 


                foreach (var checkboxElementas in checkboxai)
                {
                    checkboxElementas.Click();      // čia sužymi visus pasirinkimus. Tam reikia ciklo
                    Thread.Sleep(2000);
                }

                checkboxas.Click(); // cia viena varianta atzymi


                foreach (var checkboxElementas in checkboxai)
                {
                    Assert.IsTrue(checkboxElementas.Selected);  // o cia tikrina ar visi pazymeti pasirinkimai. Siuo atveju nepraeis testo, nes vienas langelis bus atžymėtas
                }


            }



        }

    }
    }
    

