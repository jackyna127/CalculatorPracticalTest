@WebCalculatorTest
Feature: Web Calculator Test Scenarios

@PositiveFlow
Scenario Outline: Calculator positive flows
	Given the user wants to see Scenario <Name>
	And he enters the left number <LeftNumber>
	And he chooses the operator <Operator>
	And he enters the right number <RightNumber>	
	When he clicks the Calculate button
	Then he will see the calculate <Result> on the web page

Examples: 
| Name                                                                         | LeftNumber | Operator | RightNumber | Result |
| the addition of 2 maximum numbers                                            | 999        | +        | 999         | 1998   |
| the addition of 1 positive number and 1 negative number                      | 1          | +        | -2          | -1     |
| the addition of 2 minimum numbers                                            | -99        | +        | -99         | -198   |
| the substraction of 2 positive numbers                                       | 1          | -        | 2           | -1     |
| the substraction of 2 negative numbers                                       | -1         | -        | -2          | 1      |
| the substraction of 1 maximum number for left and 1 minimum number for right | 999        | -        | -99         | 1098   |
| the substraction of 1 minimum number for left and 1 maxinum number for right | -99        | -        | 999         | -1098  |
| the multiplication of 2 positive numbers                                     | 1          | *        | 2           | 2      |
| the multiplication of 2 negative numbers                                     | -1         | *        | -2          | 2      |
| the multiplication of 1 positive number and 1 negative number                | 1          | *        | -2          | -2     |
| the multiplication of 2 maximum numbers                                      | 999        | *        | -99         | 98901  |
| the multiplication of 2 minimum numbers                                      | -99        | *        | -99         | 9801   |
| the divison of 2 positive numbers                                            | 1          | /        | 2           | 1      |
| the divison of 2 negative numbers                                            | -5         | /        | -2          | 2      |
| the divison of 1 positive number and 1 negative number                       | 2          | /        | -1          | -2     |
| the divison of 2 maximum numbers                                             | 999        | /        | -99         | 10     |
| the divison of 2 minimum numbers                                             | -99        | /        | -99         | 1      |


@NegativeFlow
Scenario Outline: Calculator negative flows
	Given the user accidentally triggers with Scenario <Name>
	And he enters the left number <LeftNumber>
	And he chooses the operator <Operator>
	And he enters the right number <RightNumber>
	When he clicks the Calculate button
	Then he will see the error <Message> on the web page

Examples: 
| Name                      | LeftNumber | Operator | RightNumber | Message       |
| Divivison by 0            | 10         | /        | 0           | Invalid input |
| Enter charactor O         | 1          | +        | O           | Invalid input |
| Enter special charactor # | #          | +        | 2           | Invalid input |
| No input for left number  |            | +        | 1           | Invalid input |
| No input for right number | 1          | +        |             | Invalid input |
| No input for right number | 1          | +        |             | Invalid input |
| No selection for operator | 1          |          | 1           | Invalid input |
