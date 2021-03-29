# CalculatorPracticalTest
Automation Framework for API and UI for calculator practical test

There are 2 projects in the solution:
1. WebCalculatorTest: it is an BBD automated framework project for Web testing with multiple browser, specflow, selenium web driver and MsTest in Visual Studio 2019. 
2. APICalculatorTest: it is an automated framework project for REST API about RestSharp and MsTest in Visual Studio 2019. 

Reminder for executing automation testing：
When execute WebCalculatorTest locally, need choose run.settings file (Test -> Configure Run Settings ->Select Solution Wide runsettings file) in Visual Studio 2019, otherswise, might get error due to no selected run.settings file.(using run.settings file to select Browser)

Test Scenarios:
1.Web & API: Test all left number and right number in all range（Web: -99 to 999, API： from int minvalue to int maximum)
2.Web & API: Test all operators("+","-","*","/")
3.Web & API: Test possible invalid input including out of range, string, special characters, and no number entering etc.
4.API:No AuthToken for API calculater, wrong josn request body and etc.

Observation:
1. Web Calculator:
   * It should consider default operator on the UI, if user use the default operator at the first time (no select operator on web with correct left and right number), there is no action after clicking "Calculate" button.
   * It does not consider the negative number scenarios, for example: 1+(-2), expected result: -1, actual result:3, same as other operators("-","*","/").
   * It does not consider the maximum result from server: 999*999, expected result:998001, actual result:99800.
   * It does not return an error message when there is invalid input,for example: 1/0, expected result:error message, actual result:0.
 
2. API Calculator:
   * when there is invalid json request, it should return "Bad request", for example: no operator in the request body, expected result:Bad request(400), actual result:Not found(404)
   * It does not consider the negative number scenarios, for example: 1+(-2), expected result: -1, actual result:3, same as other operators("-","*","/").
   * It does not return anything when number divided by 0, example: 1/0, expected result: Bad request, actual result: nothing returns from server,.   
   
   
   
