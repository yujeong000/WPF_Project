using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using lmsPlus;

namespace lmsPlus.Login
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void LoginCheckButton_Click(object sender, RoutedEventArgs e)
        {
            //아이디와 비밀번호가 맞을 때 (틀리면 MessageBox 오류 출력)
            Crawling.Crawling crl = new Crawling.Crawling(IDBox.Text.ToString(), passwordBox.Password.ToString());
            crl.crawlingBase();
            //정보 저장

            //크롬드라이버 종료
            crl.closedSite();
            //로그인 창 닫고, 기본 창 보이기.
            Application.Current.MainWindow.Height = 450;
            Application.Current.MainWindow.Width = 800;
            //visibility 바꾸기
            ((MainWindow)Application.Current.MainWindow).login.Visibility = Visibility.Collapsed;

            ((MainWindow)Application.Current.MainWindow).report.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).notice.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).calender.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).lecture.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).videoAttendance.Visibility = Visibility.Visible;
            //login창 닫기
            loginCheckButton.IsCancel = true;
        }
    }
}
