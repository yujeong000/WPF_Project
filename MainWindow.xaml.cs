using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace lms_login
{
     public partial class MainWindow
    {

        //셀레니움 실행
        IWebDriver driver;
        public MainWindow()
        {
            InitializeComponent();

            //커맨드 창 숨김
            ChromeDriverService ser = ChromeDriverService.CreateDefaultService();//이 드라이버를 쓰겟다는 선언
            ser.HideCommandPromptWindow = true;
            //시크릿모드 옵션
            ChromeOptions opt = new ChromeOptions();//이 드라이버를 쓰겟다는 선언
            opt.AddArgument("disable-gpu");
            //여기까지 셀리니움 api가 제공
            driver = new ChromeDriver(ser, opt);
            //학교 lms 링크
            driver.Url = "https://ieilms.jbnu.ac.kr/login/login2.jsp";

            string id = "";
            string pw = "!!";
            //sendkey 에 아이디 비번 입력
            driver.FindElement(By.Name("login")).SendKeys(id);
            driver.FindElement(By.Name("passwd")).SendKeys(pw);

            driver.FindElement(By.XPath("/html/body/center/form/button")).Click();
            //한 가지 주의사항이 있습니다. 빠르게 웹 사이트 제어를 시도할 경우,
            //웹 페이지 다운로드가 끝나지도 않았는데 제어하는 경우가 발생합니다.
            //이 경우 Exception 으로 빠지게 됩니다. 이를 방지하기 위해 위 예제에도
            //포함되어 있는 Implicitwait 설정이나 WebDriverWait 객체를 사용하여
            //지연 시간을 추가할 수 있습니다.하지만 경험상 웹 페이지가 다운로드되지
            //않았는데도 시도하더군요.그래서 저는 아쉽지만 Thread.Sleep() 함수를 사용합니다. 
            
            System.Threading.Thread.Sleep(3000);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //@@@@  xpath 따올때 큰따옴표를 작은따옴표로 변경!!

            //과목 클릭(자바 프로그래밍)
            driver.FindElement(By.XPath("//*[@id='boardAbox']/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]")).Click();

            //레포트 클릭//*[@id="nav"]/li[5]/a
            driver.FindElement(By.XPath("//*[@id='nav']/li[5]/a")).Click();
            //기다려주는 요 코드가 없으면 값이 잘 안받아짐=> 아마 exception 빠진것같음 
            System.Threading.Thread.Sleep(3000);      //=> 따라서 지연시간추가
            var element = driver.FindElements(By.CssSelector(".b02"));
            //system.threading.thread.sleep(3000

            ////공지사항 클릭       //*[@id='nav']/li[9]/a
            //driver.FindElement(By.XPath("//*[@id='nav']/li[9]/a")).Click();
            //System.Threading.Thread.Sleep(3000);
            //var element = driver.FindElements(By.CssSelector(".btr"));

            ////강의대화 클릭       //*[@id='nav']/li[2]/a
            //driver.FindElement(By.XPath(" //*[@id='nav']/li[2]/a")).Click();
            //System.Threading.Thread.Sleep(3000);
            //var element = driver.FindElements(By.CssSelector(".commText01"));

            foreach (var report in element)
            {
                Notice.Content += report.Text;
                Notice.Content += "\n";
            }

        }
    }
}
