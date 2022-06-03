using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace lmsPlus.Crawling
{
    struct Rep       //① 구조체 선언
    {
        public string title;
        public string date;
        public string check;
    }
    struct Noti       //① 구조체 선언
    {
        public string title;
        public string date;


    }
    struct Cmt       //① 구조체 선언
    {
        public string title;
        public string date;
        public string name;
    }
    struct Video_attendance       //① 구조체 선언
    {
        public string title;
        public string date;
        public string view_time;
        public string ratings;
        public string check;
    }
    class Crawling
    {
        IWebDriver driver;
        string id;
        string pw;

        public Crawling(string id, string pw)
        {
            this.id = id;
            this.pw = pw;
        }
        public void initial_screen()
        {
            //초기 내강의 버튼
            driver.FindElement(By.XPath("//*[@id='nav']/li[1]/a")).Click();
        }
        public void report_crwal()
        {
            //레포트 클릭//*[@id="nav"]/li[5]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[5]/a")).Click();
            //기다려주는 요 코드가 없으면 값이 잘 안받아짐=> 아마 exception 빠진것같음 
            System.Threading.Thread.Sleep(3000);      //=> 따라서 지연시간추가
            List<IWebElement> list = driver.FindElements(By.CssSelector(".b02")).ToList();
            Rep[] reps = new Rep[50];
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 5 == 0)
                        reps[i].title = list[i].Text;
                    else if (i % 5 == 1)
                        reps[i].date = list[i].Text;
                    else if (i % 5 == 2)
                        reps[i].check = list[i].Text;
                }
            }
            catch { }


        }
        public void Notice_crwal()
        {
            //공지사항 클릭       //*[@id='nav']/li[9]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[9]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            List<IWebElement> x = driver.FindElements(By.CssSelector(".btr")).ToList();
            Noti[] notis = new Noti[50];
            for (int i = 0; i < x.Count; i++)
            {

                notis[i].title = x[i].FindElement(By.TagName("a")).GetAttribute("title");
                notis[i].date = x[i].FindElement(By.ClassName("b03")).Text;
            }

        }
        public void lecture_cmt_crawl()
        {
            //강의대화 클릭       //*[@id='nav']/li[2]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[2]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            List<IWebElement> element = driver.FindElements(By.CssSelector(".commText01")).ToList();
            Cmt[] cmts = new Cmt[50];
            for (int i = 0; i < element.Count; i++)
            {
                cmts[i].name = element[i].FindElement(By.TagName("a")).GetAttribute("title");
                cmts[i].title = element[i].FindElement(By.TagName("pre")).Text;
                cmts[i].date = element[i].FindElement(By.ClassName("comcom")).FindElement(By.TagName("span")).Text;

            }

        }
        public void video_attendance_crawl()
        {
            //출석부       //*[@id='nav']/li[7]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[7]/a")).Click();
            //영상출석부 클릭      //*[@id='center']/div/div/div[2]/div[2]/ul/li[2]/ul/b/li[1]/a
            driver.FindElement(By.XPath("//*[@id='center']/div/div/div[2]/div[2]/ul/li[2]/ul/b/li[1]/a")).Click();
            System.Threading.Thread.Sleep(3000);
            //List<IWebElement> list = driver.FindElements(By.CssSelector("body center div div div b div div div table tbody tr td")).ToList();
            List<IWebElement> x = driver.FindElements(By.CssSelector(".tableThTd")).ToList();
            
            
            Video_attendance[] videos = new Video_attendance[60];//@@@@@@@@@@@크기 고려!!!!!!!!!!!!!!!!!!!!!!

            List<IWebElement> a = driver.FindElement(By.XPath("//*[@id='dataBox']/table/tbody")).FindElements(By.TagName("tr")).ToList();


            for (int i = 0; i < a.Count; i++)
            {
                List<IWebElement> list = a[i].FindElements(By.TagName("td")).ToList();
                videos[i].title = list[1].Text;
                videos[i].date = list[2].Text;
                videos[i].view_time = list[3].Text;
                videos[i].ratings = list[4].Text;
                videos[i].check = list[5].Text;

                //for (int j = 0; j < list.Count; j++)
                //{
                //    if (j == 1)
                //        videos[i].title = list[j].Text;
                //    else if (j == 2)
                //        videos[i].date = list[j].Text;
                //    else if (j == 3)
                //        videos[i].view_time = list[j].Text;
                //    else if (j == 4)
                //        videos[i].ratings = list[j].Text;
                //    else if (j == 5)
                //        videos[i].check = list[j].Text;

                //}
            }

        }
        public void crawlingBase()
        {
            try
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
            }
            catch
            {
                MessageBox.Show("로그인 실패");
            }

            //System.Threading.Thread.Sleep(3000);
        }
       public void crawl()
        {
            report_crwal();
            initial_screen();
            Notice_crwal();
            initial_screen();
            lecture_cmt_crawl();
            initial_screen();
            video_attendance_crawl();
        }
        public void chooseSubject(int num)
        {
            //        //*[@id='boardAbox']/form/table/tbody/tr[ 2 ]/td[ 1 ]/div/table/tbody/tr/td[2]/div[1]
            string url1 = "//*[@id='boardAbox']/form/table/tbody/tr[";
            string url2 = "]/td[";
            string url3 = "]/div/table/tbody/tr/td[2]/div[1]";
            string url4 = "]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b";//이름 xpath
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    /*과목이름 따로 저장
                    //*[@id="boardAbox"]/form/table/tbody/tr[1]/td[2]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b
                    //*[@id="boardAbox"]/form/table/tbody/tr[1]/td[3]/div/table/tbody/tr/td[2]/div[2]/a/div[1]/b
                    
                    과목 이름 =>  url1 + i + url2 + j + url4
                    
                    과목 이름 =  driver.FindElement(By.XPath(url1 + i + url2 + j + url3)).Text
                     */

                    driver.FindElement(By.XPath(url1 + i + url2 + j + url3)).Click();
                    
                    crawl();

                }
            }
            switch (num)
            {
                case 1:
                    //과목 클릭(자바 프로그래밍) (2,1)
                    driver.FindElement(By.XPath("//*[@id='boardAbox']/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]")).Click();

                    break;
                case 2://윈프 xpath     (1,4)
                       //*[@id="boardAbox"]/form/table/tbody/tr[1]/td[2]/div/table/tbody/tr/td[2]/div[1]
                    break;
                case 3:
                    //*[@id="boardAbox"]/form/table/tbody/tr[1]/td[4]/div/table/tbody/tr/td[2]/div[1]

                    break;
                case 4://데이터통신xpath     (1,2)
                    driver.FindElement(By.XPath("//*[@id='boardAbox']/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]")).Click();
                    break;
                case 5://데이터통신xpath     (1,2)
                    //*[@id="boardAbox"]/form/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td[2]/div[1]
                    break;
                case 6:
                    //*[@id="boardAbox"]/form/table/tbody/tr[2]/td[3]/div/table/tbody/tr/td[2]/div[1]
                    break;
                case 7:
                    //*[@id="boardAbox"]/form/table/tbody/tr[2]/td[4]/div/table/tbody/tr/td[2]/div[1]
                    break;
                case 8://사회학의 초대 xpath  (3,0)
                    //*[@id="boardAbox"]/form/table/tbody/tr[3]/td/div/table/tbody/tr/td[2]/div[1]

                    break;
            }
        }
        public void closedSite()
        {
            driver.Quit();
        }
    }
}
