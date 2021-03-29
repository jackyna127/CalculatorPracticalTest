using System;
using System.Collections.Generic;
using System.Text;

namespace APICalculatorTest.Models
{
    public class CalculatorAPIRequest
    {
        public int LeftNumber;
        public int RightNumber;
        public string Operator;
    }
    public class CalculatorAPIResponse
    {
        public int calculateResult;
    }

}
