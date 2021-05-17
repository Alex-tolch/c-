using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> recipients = new List<string>(); 
        List<string> files = new List<string>();

        private void button3_Click(object sender, EventArgs e) 
        {
            //label9.ForeColor = Color.Green; label9.Text = "Отправлено";

            string message = richTextBox1.Text; 
            string smtpServer = textBox1.Text; 
            string from = textBox3.Text; 
            string password = textBox4.Text; 
            string caption = textBox5.Text; 

            for (int i = 0; i < recipients.Count; i++)
            {
                Email.sendingMessage(recipients[i], smtpServer, from, password, caption, message, files);
            }
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            OpenFileDialog fileDialog = new OpenFileDialog(); 

            if (fileDialog.ShowDialog() == DialogResult.OK && fileDialog.FileName != "")
            {
                files.Add(fileDialog.FileName);
                listBox2.Items.Add(fileDialog.FileName);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            recipients.Add(textBox2.Text);
            listBox1.Items.Add(textBox2.Text);
 
            textBox2.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
