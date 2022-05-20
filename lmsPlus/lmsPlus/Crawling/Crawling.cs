using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace lmsPlus.Crawling
{
    class Crawling
    {
        string id;
        string pw;
        public Crawling(string id, string pw)
        {
            this.id = id;
            this.pw = pw;
        }
        IWebDriver driver;
        public string report_crwal()
        {
            string reportText = null;
            //레포트 클릭//*[@id="nav"]/li[5]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[5]/a")).Click();
            //기다려주는 요 코드가 없으면 값이 잘 안받아짐=> 아마 exception 빠진것같음 
            System.Threading.Thread.Sleep(3000);      //=> 따라서 지연시간추가
            var element = driver.FindElements(By.CssSelector(".b02"));

            foreach (var report in element)
            {
                reportText += report.Text;
                reportText += "\n";
            }
            return reportText;
        }
        public string Notice_crwal()
        {
            string notice = null;
            //공지사항 클릭       //*[@id='nav']/li[9]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[9]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            var element = driver.FindElements(By.CssSelector(".btr"));

            foreach (var report in element)
            {
                notice += report.Text;
                notice += "\n";
            }
            return notice;
        }
        public string lecture_cmt_crawl()
        {
            string lectureCmt = null;
            //강의대화 클릭       //*[@id='nav']/li[2]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[2]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            var element = driver.FindElements(By.CssSelector(".commText01"));

            foreach (var report in element)
            {
                lectureCmt += report.Text;
                lectureCmt += "\n";
            }
            return lectureCmt;
        }
        public string video_attendance_crawl()
        {
            string video = null;
            //출석부       //*[@id='nav']/li[7]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[7]/a")).Click();
            //영상출석부 클릭      //*[@id='center']/div/div/div[2]/div[2]/ul/li[2]/ul/b/li[1]/a
            driver.FindElement(By.XPath("//*[@id='center']/div/div/div[2]/div[2]/ul/li[2]/ul/b/li[1]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            var element = driver.FindElements(By.CssSelector(".tableThTd"));

            foreach (var report in element)
            {
                video += report.Text;
                video += "\n";
            }
            return video;
        }
        public void crawlingBase()
        {
            ChromeDriverService ser = ChromeDriverService.CreateDefaultService();
            ser.HideCommandPromptWindow = true;
            ChromeOptions opt = new ChromeOptions();
            opt.AddArgument("disable-gpu");
            //ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            //opt.AddArgument("--headless");
            driver = new ChromeDriver(ser, opt);
            driver.Url = "https://ieilms.jbnu.ac.kr/login/login2.jsp";

            driver.FindElement(By.Name("login")).SendKeys(id);
            driver.FindElement(By.Name("passwd")).SendKeys(pw);

            driver.FindElement(By.XPath("/html/body/center/form/button")).Click();

            //System.Threading.Thread.Sleep(3000);
        }
        //과목 선택
        //1번부터 과목 입력
        public void chooseSubject(int num)
        {
            switch (num)
            {
                case 1:
                    //과목 클릭(자바 프로그래밍)
                    driver.FindElement(By.XPath("//*[@id='boardAbox']/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]")).Click();
                    //초기 내강의 버튼
                    driver.FindElement(By.XPath("//*[@id='nav']/li[1]/a")).Click();
                    break;
            }
        }
        public void closedSite()
        {
            driver.Close();
        }
    }
}
