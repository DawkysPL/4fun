using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    //The game mode is REVERSE: You do not have access to the statement. You have to guess what to do by observing the following set of tests:
    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    public class Solution
    {
        /*
        public void Action()
        {
            var cases = prepereTestData();            
        }   
         */
        public void Action()
        {
            var cases = prepereTestData();

            foreach(var item in cases)
            {
                bool passed = true;
                for (int i = 0; i < item.Count; i++)
                {
                    int count = 0;
                    string s = item.Case[i];
                    foreach (var c in s)
                    {
                        if (c == '1') count++;
                    }
                    string result = count % 2 == 1 ? "1" : "0";

                    if(result != item.ResultCase[i])
                    {
                        Console.WriteLine($"Wynik zly: result: {result}, wynik docelowy: {item.ResultCase[i]}");
                        passed = false;
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }
                if (passed)
                {
                    Console.WriteLine("Test pomyslny");
                }
            }           
        }

        private List<UnitTest> prepereTestData()
        {
            var inputCase = System.IO.File.ReadAllLines(@"C:\Users\micha\Documents\testInput.txt").DeleteSpaces();
            var resultCase = System.IO.File.ReadAllLines(@"C:\Users\micha\Documents\testOutput.txt").DeleteSpaces();

            List<UnitTest> unitTests = new List<UnitTest>();

            bool flag = true;
            int count = 0;

            for(int i = 0; i < inputCase.Length; i++)
            {
                if(flag)
                {
                    var obj = new UnitTest();
                    obj.Count = Convert.ToInt32(inputCase[i]);
                    unitTests.Add(obj);
                    flag = false;
                    continue;
                }
                var item = unitTests[unitTests.Count - 1];
                if (count++ < item.Count)
                {
                    item.Case.Add(inputCase[i]);
                }
                else
                {
                    flag = true;
                    count = 0;
                    i--;
                }                   
            }
            count = 0;
            foreach(var item in unitTests)
            {
                for(int i = 0; i < item.Count; i++)
                {
                    item.ResultCase.Add(resultCase[count++]);
                }
            }
            return unitTests;
        }

        private class UnitTest
        {
            public UnitTest()
            {
                Case = new List<string>();
                ResultCase = new List<string>();
            }
            public int Count { get; set; }
            public List<string> Case { get; set; }

            public List<string> ResultCase { get; set; }
        }
    }

    public static class StringExtension
    {
        public static string[] DeleteSpaces(this string[] str)
        {
            List<string> ss = new List<string>();
            
            foreach(var s in str)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    ss.Add(s);
                }
            }
            var sss = new string[ss.Count];
            int count = 0;
            foreach(var s in ss)
            {
                sss[count++] = s;
            }
            return sss;
        }
    }
}
