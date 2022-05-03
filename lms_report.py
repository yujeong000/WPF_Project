from selenium import webdriver
from bs4 import BeautifulSoup
import requests #아직 사용안하ㅁ

driver = webdriver.Chrome('\chromedriver_win32\\chromedriver.exe')
driver.implicitly_wait(3)

driver.get('https://ieilms.jbnu.ac.kr/login/login2.jsp') ##로그인 URL로 이동하기


driver.find_element_by_name('login').send_keys("202111680")
driver.find_element_by_name('passwd').send_keys("!!wlcks6084")

## 로그인 버튼을 클릭.
driver.find_element_by_xpath('/html/body/center/form/button').click()
#과목 클릭
driver.find_element_by_xpath('//*[@id="boardAbox"]/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]').click()
#레포트 클릭
driver.find_element_by_xpath('//*[@id="nav"]/li[5]/a').click()
#레포트 크롤링
report_ = driver.find_elements_by_css_selector(".btr")
for i in report_:
    rep = i.text
    print(rep)
