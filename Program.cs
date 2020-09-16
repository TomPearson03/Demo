using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using Flurl;
using System.Threading.Tasks;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ExaminingStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What text do you want to view in other formats?");
            StringChange sc = new StringChange(Console.ReadLine());
            Console.WriteLine(sc.ToGerman());
            
        }
    }
    class StringChange
    {
        private string subject;
        public StringChange(string anyString)
        {
            subject = anyString;
        }
        public string ToAlphabetPositions()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;//Finds where the app path is
            string filePath = "alphabet.txt";//Name of file containing alphabet info
            StreamReader sr = new StreamReader(Path.Combine(appPath,filePath));//Opens file with alphabet info
            string line,result = null;
            IDictionary<char, int> alphabetNumbers = new Dictionary<char, int>();//Creates dictionary with letter and position
            List<int> positions = new List<int>();
            while((line = sr.ReadLine())!= null)//Reads each line until a line is empty
            {
                char letter = line[0];//Assigns the first character which is a letter to a character
                string postitionString = line[1].ToString()+line[2].ToString();//Assigns the next two characters to a string with them both together
                int position = Int16.Parse(postitionString);//Creates an integer with the last two values of string
                alphabetNumbers.Add(letter, position);//Adds the letter and position to dictionary
            }
            sr.Close();
            subject = subject.ToLower();//Ensures all letters are lower case
            foreach(char character in subject)
            {

                if (Char.IsLetter(character))
                {
                    positions.Add(alphabetNumbers[character]);
                }
            }
            foreach(var item in positions)
            {
                result += item + " ";
            }
            return (result);

        }
        public string ToBinary()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var character in subject.ToCharArray())
            {
                sb.Append(Convert.ToString(character, 2).PadLeft(8, '0'));
            }
            return(sb.ToString());
        }
        public string ToGerman()
        {

            var url = @"https://translate.google.co.uk/#view=home&op=translate&sl=en&tl=de";
            IWebDriver webDriver = new FirefoxDriver();
            webDriver.Url = url;
            IWebElement input = webDriver.FindElement(By.Id("source"));
            input.SendKeys(subject);
            IWebElement output = webDriver.FindElement(By.XPath("//span[@class='tlid-translation translation']"));
            string result = output.Text;
            webDriver.Close();
            return (result);
        }
        

        
    }
}
