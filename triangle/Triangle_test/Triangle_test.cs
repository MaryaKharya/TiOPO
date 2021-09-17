using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Linq;
using System.IO;
using triangle = Triangle.Triangle;

namespace Triangle_test
{
    [TestClass]
    public class Triangle_test
    {
        [TestMethod]
        public void TestActualAndExpectedResultFromFile()
        {
                FileStream fileIn = new FileStream("../../../tests.txt", FileMode.Open);
                FileStream fileOut = new FileStream("../../../tests_results.txt", FileMode.Create);
                StreamReader reader = new StreamReader(fileIn);
                StreamWriter writer = new StreamWriter(fileOut);
                int i = 1;
                while (!reader.EndOfStream)
                {
                    string str = reader.ReadLine();
                    string[] args = str.Split(' ');
                    triangle.Main(args);
                    str = reader.ReadLine();
                    if (str == triangle.result)
                    {
                        writer.WriteLine("{0} success", i);
                    }
                    else
                    {
                        writer.WriteLine("{0} error", i);
                    }
                    i++;
                }
                reader.Close();
                writer.Close();
        }
    }
}
