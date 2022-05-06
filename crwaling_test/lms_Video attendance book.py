from selenium import webdriver
from bs4 import BeautifulSoup
import requests #아직 사용안하ㅁ

driver = webdriver.Chrome('\chromedriver_win32\\chromedriver.exe')
driver.implicitly_wait(3)

driver.get('https://ieilms.jbnu.ac.kr/login/login2.jsp') ##로그인 URL로 이동하기

#send_key 에 아이디 비번 입력
driver.find_element_by_name('login').send_keys("")
driver.find_element_by_name('passwd').send_keys("")

## 로그인 버튼을 클릭.
driver.find_element_by_xpath('/html/body/center/form/button').click()
#과목 클릭
driver.find_element_by_xpath('//*[@id="boardAbox"]/form/table/tbody/tr[2]/td[1]/div/table/tbody/tr/td[2]/div[1]').click()
#공지사항  클릭
driver.find_element_by_xpath('//*[@id="nav"]/li[9]/a').click()
#공지사항 크롤링
Notice = driver.find_elements_by_css_selector(".btr")

for i in Notice:
    notice = i.text
    print(notice)
