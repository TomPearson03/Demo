using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime;


namespace ExaminingStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            StringChange sc = new StringChange("Works");
            Console.WriteLine(sc.IntoAlphabetPositions());
        }
    }
    class StringChange
    {
        private string subject;
        public StringChange(string anyString)
        {
            subject = anyString;
        }
        public string IntoAlphabetPositions()
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
        
    }
}
