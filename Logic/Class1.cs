using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using static Data.Pods;

namespace Logic 
{

    
    public class addPods
    {
        private Stream ms = new MemoryStream();
       static List<Pod> allaPoddar = new List<Pod>();
        
        public addPods()
        {
            Stream stream = File.Open("PodData.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, allaPoddar);
            stream.Close();

            stream = File.Open("PodData.dat", FileMode.Open);
            bf = new BinaryFormatter();
        }
        
        public static void addList(string url,string kategori, double intervall)
        {
            Pod varjePodcast = new Pod(url,kategori,intervall);
            allaPoddar.Add(varjePodcast);
        }
        public static void serializePods()
        {

            using (Stream fs = new FileStream(@"C:\lista.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof
                        (List<Pod>));
                serializer.Serialize(fs, allaPoddar);

            }
          
        }
        public static void deSerializePods()
        {
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Pod>));

            using (FileStream fs2 = File.OpenRead(@"C:\lista.xml"))
            {
                allaPoddar = (List<Pod>)serializer2.Deserialize(fs2);
            }
        }

        public static void changeFeedUrl(string url, string newUrl, string newCategory, string newUpdateIntervall)
        {
            //var doc = XElement.Load(@"C:\lista.xml");
            //var saveGame = doc
            //     .Element("ArrayOfPod")
            //     .Elements("Pod")
            //     .Where(e => e.Element("Url").Value == "http://api.sr.se/api/rss/pod/3966")
            //     .FirstOrDefault();

            //saveGame.Element("UpdateIntervall").Value = "50";

            //doc.Save(@"C:\lista.xml");

            //var xdoc = XDocument.Load(@"C:\lista.xml");

            ////xdoc.XPathSelectElement($"//Pod/Url[text()='{category}']/../UpdateIntervall").Value = "333333";
            //xdoc.XPathSelectElements($"//ArrayOfPod/Pod").Remove();
            //xdoc.Save(@"C:\lista.xml");

            //var xDoc = XDocument.Load(@"C:\lista.xml");
            //string xPath = "//ArrayOfPod/Pod/D";
            ////or string xPath = "//B2/C3/D4[@id='1']";

            //var eleList = xDoc.XPathSelectElements(xPath).ToList();
            //foreach (var xElement in eleList)
            //{
            //    Debug.WriteLine(xElement);
            //}

            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Url").Value == url
                        select item;

            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("Url", newUrl);
                itemElement.SetElementValue("Category", newCategory);
                itemElement.SetElementValue("UpdateIntervall", newUpdateIntervall);
            }

            xmlDoc.Save(@"C:\lista.xml");
        }

        public static void changeFeedCategory(string url, string newCategory)
        {
         
            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Url").Value == url
                        select item;

            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("Category", newCategory);
            }

            xmlDoc.Save(@"C:\lista.xml");
        }

        public static void changeFeedUpdateInteval(string url, string newInterval)
        {

            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Url").Value == url
                        select item;

            foreach (XElement itemElement in items)
            {
                itemElement.SetElementValue("UpdateIntervall", newInterval);
            }

            xmlDoc.Save(@"C:\lista.xml");
        }

        public static List<string> fyllComboBoxMedUrl()
        {

            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Url").Value != null
                        select item.Element("Url").Value;
            List<string> kategorier = new List<string>();
            foreach (string itemElement in items)
            {
               kategorier.Add(itemElement);
            }

            return kategorier;
        }

        public static List<string> fyllComboBoxMedKategori()
        {

            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Category").Value != null
                        select item.Element("Category").Value;
            List<string> kategorier = new List<string>();
            string hej = items.ToString();
            foreach (string itemElement in items)
            {
                if (!kategorier.Contains(itemElement.ToString()))
                {
                    kategorier.Add(itemElement);
                }
                
            }

            return kategorier;
        }

        public static List<string> fyllListboxMedFeeds(string category)
        {

            var xmlDoc = XDocument.Load(@"C:\lista.xml");
            //XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            var items = from item in xmlDoc.Descendants("Pod")
                        where item.Element("Category").Value == category
                        select item.Element("Url").Value;
            List<string> feeds = new List<string>();
            foreach (string itemElement in items)
            {
               
                    feeds.Add(itemElement);
            }

            return feeds;
        }

        public static void removeFeed(string url)
        {
            //var xmlDoc = XDocument.Load(@"C:\lista.xml");
            ////XDocument xmlDoc = XDocument.Parse(@"C:\lista.xml");

            //var items = from item in xmlDoc.Descendants("Pod")
            //            where item.Element("Url").Value == url
            //            select item;

            //foreach (XElement itemElement in items)
            //{
            //    itemElement.Remove();
            //}

            //xmlDoc.Save(@"C:\lista.xml");
            var xdoc = XDocument.Load(@"C:\lista.xml");

            //xdoc.XPathSelectElement($"//Pod/Url[text()='{url}']").Remove();
            xdoc.XPathSelectElements($"//ArrayOfPod/Pod").Where(x => (string)x.Element("Url") == url).Remove();
            xdoc.Save(@"C:\lista.xml");
        }
    }
}

