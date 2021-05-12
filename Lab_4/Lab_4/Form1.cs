using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_4
{
    public partial class Form1 : Form
    {
        MyClass obj = new MyClass();
        LinkedList<Bitmap> list = new LinkedList<Bitmap>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("C:\\Users\\Bird\\Desktop\\Лабораторная работа 4\\Изображения\\3.jpg");
            pictureBox1.Image = bmp;
            list.AddFirst(bmp);
            pictureBox2.Image = list.First();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj.Gray(pictureBox1, list);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            obj.Contrast(pictureBox1, list);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            obj.Clear(pictureBox1, list);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Альгашев Геннадий, 6203");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выполнить линейное контрастирование малоконтрастного изображения");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj.OpenFile(pictureBox1, openFileDialog1, list);
            pictureBox2.Image = list.First();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            obj.Cancel(pictureBox1, list);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

    }
}
