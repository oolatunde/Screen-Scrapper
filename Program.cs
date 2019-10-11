using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace Scrapper
{
    class Program
    {
        static string url = "http://thinkingbig.net/about/";
        static void Main(string[] args)
        {
            exec();
            var web = new HtmlWeb();
            HtmlDocument document = web.Load(url);


            //Names of Thinking big executives
            //1. Getting the location of the nodes by XPath
            var headNames = document.DocumentNode.SelectNodes("//div[@class='executive']/h2").ToList();
            foreach (var name in headNames)
            {
                Console.WriteLine(name.InnerText);
                Compare(name.InnerText);
            }
            

        }

        //Method that screen scrapes the Thinking big executives and their role
        public static void exec()
        {
            var web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            //2. Getting the nodes by descendants
            var firstSection = document.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("executive")).ToList();

            for (int i = 0; i < firstSection.Count; i++)
            {
                var newfirst = firstSection[i].Descendants("h4").ToList();
                foreach (var position in newfirst)
                {
                    Console.WriteLine("--------------{0}--------------", (i + 1));
                    Console.WriteLine(position.InnerText);
                    Console.WriteLine(firstSection[i].Descendants("h2").FirstOrDefault().InnerText);
                }
                Console.WriteLine();
            }
        }


        //Method that compares the output value from scraping with the actual names.
        public static void Compare(string val)
        {
            List<string> newNames = new List<string>();
            newNames.Add("Joeanne Thomson");
            newNames.Add("Chris Weeks");
            newNames.Add("Ron Myers");
            newNames.Add("Luke Rooney");
            newNames.Add("Devin Bruce");
            newNames.Add("Paul Lopes");

            foreach (var executive in newNames)
            {
                if (executive.Equals(val))
                {
                    Console.WriteLine("Correct scrapper");
                }
            }
            
        }
    }
}
