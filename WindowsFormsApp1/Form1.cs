using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WMPLib;


namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        public string xml = "";
        private List<string> pods = new List<string>();
        WMPLib.WindowsMediaPlayer Player;
        System.Xml.XmlDocument dom = new System.Xml.XmlDocument();
        private Stream ms = new MemoryStream();

        //List<Pod> allaPoddar = new List<Pod>();

    public Form1()

        {

            if (!File.Exists(@"C:\lista.xml"))
            {
                addPods.serializePods();
            }

            InitializeComponent();
            addPods.deSerializePods();
            
            comboBox2.Items.AddRange(addPods.fyllComboBoxMedUrl().ToArray());
            comboBox1.Items.AddRange(addPods.fyllComboBoxMedKategori().ToArray());
           





            //Stream stream = File.Open("PodData.dat", FileMode.Create);
            //BinaryFormatter bf = new BinaryFormatter();

            //bf.Serialize(stream, allaPoddar);
            //stream.Close();

            //stream = File.Open("PodData.dat", FileMode.Open);
            //bf = new BinaryFormatter();
        }

        public Dictionary<String, String> categorys = new Dictionary<String, String>();
        public List<KeyValuePair<String, String>> kategorier = new List<KeyValuePair<String, String>>();

        public List<string> Pods { get => Pods1; set => Pods1 = value; }
        public List<string> Pods1 { get => pods; set => pods = value; }
        public List<string> Pods2 { get => pods; set => pods = value; }

        private class Hejsan : addPods
        {

        }

    private void button1_Click(object sender, EventArgs e)
        {
            //Pod varjePodcast = new Pod(textBox1.Text,textBox3.Text , int.Parse(textBox4.Text));
         addPods.addList(textBox1.Text, textBox3.Text, int.Parse(textBox4.Text));
            //allaPoddar.Add(varjePodcast);
            
            using (var client = new System.Net.WebClient())
            {
                client.Encoding = Encoding.UTF8;
                xml = client.DownloadString(textBox1.Text); 
            }
            dom.LoadXml(xml);

            foreach (System.Xml.XmlNode item
               in dom.DocumentElement.SelectNodes("channel/item"))
            {
                //Skriv ut dess titel.
                var title = item.SelectSingleNode("title");
                Pods.Add(item.SelectSingleNode("title").InnerText);
                listBox1.Text = title.InnerText;
                Console.WriteLine(title.InnerText);
            }
            
                if (listBox1.Items.ToString() != textBox3.Text)
                {
                    listBox1.Items.Add(textBox3.Text);
                }

            


            setCategory(textBox3.Text);
            comboBox1.Text = textBox3.Text;
            addPods.serializePods();

        }
        private void visaLista()
        {
            var valtItem = comboBox1.SelectedItem.ToString();
            listBox2.Items.Add(categorys.ContainsKey(valtItem));
        }
        //private void serializePods()
        //{

        //    using (Stream fs = new FileStream(@"C:\lista.xml", FileMode.Create, FileAccess.Write, FileShare.None))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof
        //                (List<Pod>));
        //        serializer.Serialize(fs, allaPoddar);

        //    }
        //    //XmlSerializer serializer2 = new XmlSerializer(typeof
        //    //    ())
        //}
        //private void deSerializePods()
        //{
        //    XmlSerializer serializer2 = new XmlSerializer(typeof(List<Pod>));

        //    using (FileStream fs2 = File.OpenRead(@"C:\lista.xml"))
        //    {
        //        allaPoddar = (List<Pod>)serializer2.Deserialize(fs2);
        //    }
        //}
        public void setCategory(string category)
        {
            //category = textBox3.Text.ToString();
            //var choosenPod = listBox1.SelectedItem.ToString();
            //kategorier.Add(new KeyValuePair<String, String>(category, choosenPod));
            ////varför inte katergorier.Key?
            //var item = new KeyValuePair<String, String>(category, choosenPod);
            //if (comboBox1.Items.Contains(item.Key) == false)
            //{
            //    comboBox1.Items.Add(item.Key);
            //}


        }
        private void fillListWithObjects()
        {
            listBox2.Items.Clear();
 
            foreach (var item in kategorier)
            {
                if (comboBox1.SelectedItem.ToString() == item.Key)
                {
                    listBox2.Items.Add(item.Value);
                }
                
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string curItem = listBox1.SelectedItem.ToString();
            //textBox3.Text = curItem;

        }


        private void button2_Click(object sender, EventArgs e)
        {

            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(addPods.fyllListboxMedFeeds(comboBox1.SelectedItem.ToString()).ToArray());


        }

        private void button3_Click(object sender, EventArgs e)
        {
            fillListWithObjects();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Encoding = Encoding.UTF8;
                xml = client.DownloadString(textBox1.Text);
            }

            //Skapa en objektrepresentation.
            var dom = new System.Xml.XmlDocument();
            dom.LoadXml(xml);

            //Iterera igenom elementet item.
            foreach (System.Xml.XmlNode item
               in dom.DocumentElement.SelectNodes("channel/item"))
            {
                if (item.SelectSingleNode("title").InnerText == listBox2.SelectedItem.ToString())
                {
                    MessageBox.Show(item.SelectSingleNode("description").InnerText);
                }
                //Skriv ut dess titel.
                
                
                
            }
     
        }
        //private void createXmlFile()
        //{
        //    if (!File.Exists("lista.xml"))
        //    {
        //        using (Stream fs = new FileStream(@"C:\lista.xml", FileMode.Create, FileAccess.Write, FileShare.None))
        //        {
        //            XmlSerializer serializer = new XmlSerializer(typeof
        //                    (List<Pod>));
        //            serializer.Serialize(fs, allaPoddar);

        //        }
        //    }
        //}

        private void button5_Click(object sender, EventArgs e)
        {
            var filePath = "";
            dom.LoadXml(xml);
            foreach (System.Xml.XmlNode item
               in dom.DocumentElement.SelectNodes("channel/item"))
            {
                if (item.SelectSingleNode("title").InnerText == listBox2.SelectedItem.ToString())
                {
                    
                    filePath = item.SelectSingleNode("enclosure/@url").InnerText;
                   
                   Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + filePath);
                }

            
          
        }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            addPods.changeFeedUrl(comboBox2.SelectedItem.ToString(), textBox5.Text, textBox6.Text, textBox7.Text);
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(addPods.fyllComboBoxMedUrl().ToArray());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            addPods.removeFeed(comboBox2.SelectedItem.ToString());
        }
    }
    }
