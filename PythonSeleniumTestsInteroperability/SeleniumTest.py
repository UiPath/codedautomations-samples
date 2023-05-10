# Instead of print, we can use workflowUtils.Log to emit a log statement to the robot
workflowUtils.Log("Starting Selenium Python Test...")

from time import sleep
from selenium import webdriver  
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.by import By

try:
  # Open the form in Chrome
  driver = webdriver.Chrome()
  driver.maximize_window()
  driver.get("https://uipathtestappsto.blob.core.windows.net/testapps/formWithTitle.html")
  driver.find_element_by_id("name").send_keys(userName)
  driver.find_element_by_id("email").send_keys(email)

  # Submit the form
  driver.find_element_by_xpath('//input[@type="submit"]').click()

  # Retrieve the result
  result = driver.find_element_by_id("output").text

  windowTitle = driver.title

  # Use testingService to verify the form
  testingService.VerifyExpression(result == verificationText)

  # Sample workflow invocation from within Python 
  workflowUtils.RunWorkflow("Sequence.xaml")
finally:
  driver.quit()