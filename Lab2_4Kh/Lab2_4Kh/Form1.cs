using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.IO;
using System.Net.Sockets;

namespace Lab2_4Kh
{
    public partial class Form1 : Form
    {
        private string[] rf = { ".pdf", ".doc", ".docx", ".rtf", ".css", ".rar" };

        private static List<Uri> FindLinks(string responseData, Uri linkForResponse)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseData);
            List<Uri> urlList = new List<Uri>();
            if (doc.DocumentNode?.SelectNodes(@"//a[@href]") == null) return urlList;
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes(@"//a[@href]"))
            {
                try
                {
                    HtmlAttribute att = link.Attributes["href"];
                    if (att == null) continue;
                    string href = att.Value;
                    if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;
                    Uri urlNext = new Uri(href, UriKind.RelativeOrAbsolute);
                    urlNext = new Uri(linkForResponse, urlNext);
                    if (!urlList.Contains(urlNext))
                    {
                        urlList.Add(urlNext);
                    }
                }
                catch (Exception e)
                {
                }
            }
            return urlList;
        }
        private bool CheckExp(string str)
        {
            string s = "";
            int j = str.Length - 1;
            while (!str[j].Equals('.'))
            {
                j--;
            }
            s = str.Substring(j, str.Length - j);
            for (int i = 0; i < rf.Length; i++)
            {
                if (s.Equals(rf[i]))
                {
                    return true;
                }
            }

            return false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private static Socket GetSocket(string host, int port)
        {
            Socket s = null;
            var tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
           ProtocolType.Tcp);
            try
            {
                tempSocket.Connect(host, port);
            }
            catch (SocketException ex)
            {
                return null;
            }
            if (tempSocket.Connected)
                s = tempSocket;
            return s;
        }
        private static string GetPage(string host, int port, string pages)
        {
            string request = pages != ""
        ? "GET " + pages + " HTTP/1.1\r\n" + "Host: " + host + "\r\nConnection: Close\r\n\r\n"
        : "GET / HTTP/1.1\r\nHost: " + host + "\r\nConnection: Close\r\n\r\n";
            Byte[] bytesSent = Encoding.Default.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];
            Socket s = GetSocket(host, port);
            if (s == null)
                return "Connection failed";
            s.Send(bytesSent, bytesSent.Length, 0);
            int bytes;
            string page = "";
            do
            {
                bytes = s.Receive(bytesReceived, 0, bytesReceived.Length, 0);
                page = page + Encoding.Default.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);
            return page;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = true;
            listBox2.HorizontalScrollbar = true;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox1.UseWaitCursor = true;
            int size = 0;
            int lеvеlCount = (int)numericUpDown1.Value;
            int k = 0;
            List<Uri> urlsList = new List<Uri>();
            bool result = Uri.TryCreate(textBox1.Text, UriKind.Absolute, out var
           uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

            urlsList.Add(uriResult);
            for (int i = 0; i < lеvеlCount; i++)
            {
                k = 0;

                List<Uri> alllinks = new List<Uri>();
                List<Uri> filelinks = new List<Uri>();
                foreach (var uri in urlsList)
                {
                    string page = GetPage(uri.Host, 80, uri.LocalPath);
                    List<Uri> LinksOnPage = FindLinks(page, uri);
                    foreach (var findedUri in LinksOnPage)
                    {
                        if (!alllinks.Contains(findedUri) && findedUri.Host == uriResult.Host)
                        {
                            alllinks.Add(findedUri);
                            string s = findedUri.ToString();
                            size += page.Length;

                            if (CheckExp(s))
                            {
                                filelinks.Add(findedUri);
                                k++;
                            }
                        }
                    }
                }
                for (int j = 0; j < alllinks.Count; j++)
                {
                    listBox1.Items.Add(alllinks[j]);
                }
                for (int j = 0; j < filelinks.Count; j++)
                {
                    listBox2.Items.Add(filelinks[j]);
                }
                listBox1.UseWaitCursor = false;
                urlsList = alllinks;
            }
            label7.Text = "" + k;
            label8.Text = "" + (size / 1024 / 1024) + " Mb";
        }
    }
}
