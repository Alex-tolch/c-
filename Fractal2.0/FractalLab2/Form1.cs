using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FractalLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "YF+XF+Y";
            textBox2.Text = "XF-YF-X";
            AxiomText.Text = "YF";
            textBox4.Text = "60";
            AutoCompleteStringCollection source = new AutoCompleteStringCollection()
        {
            "10","20","30","40","50","60","70","80","90"
        };
            textBox4.AutoCompleteCustomSource = source;
            textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection source1 = new AutoCompleteStringCollection()
        {
            "X[-FFF][+FFF]FX","YF+XF+Y"
        };
            textBox1.AutoCompleteCustomSource = source1;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection source2 = new AutoCompleteStringCollection()
        {
            "YFX[+Y][-Y]","XF-YF-X"
        };
            textBox2.AutoCompleteCustomSource = source2;
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;


            AutoCompleteStringCollection source3 = new AutoCompleteStringCollection()
        {
            "XF-F+F-XF+F+XF-F+F-X"
        };
            textBox1.AutoCompleteCustomSource = source3;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        int steps = 0;


        /// <summary>
        /// Метод, выполняющий рисовку фрактала. Использует последовательность действий из TextBоx, поверхность, на которую наносится рисунок, коэффицент, задающий значения уменьшения рисуемой линии с увеличением кол-ва шагов и кисть.
        /// </summary>
        /// <param name="axioma"></param>
        /// <param name="q"></param>
        /// <param name="H"></param>
        /// <param name="brush"></param>
        public void DrawFractal(String axioma, Graphics q, double H,Pen brush)
        {
            Stack<string> stack = new Stack<string>();
            double L = 0;
            char[] DrawMap = axioma.ToCharArray();// перевод символов, обозначающих последовательность действий, в массив символов
            float x0 = field.Width/3+40;// начальная координата x
            float y0 = field.Height/3+150;// начальная координата y
            float x1 = x0;
            float y1 = y0;
            
            for (int i = 0; i < DrawMap.Length; i++)// проход всех символов из TextBox
            {
                brush.Color = Color.DarkViolet;
                switch (DrawMap[i])// выборка для каждого эл-та
                {
                    case 'F':
                        {
                            x1 += (float)(2 * H * Math.Cos((L * Math.PI) / 180));
                            y1 += (float)(2 * H * Math.Sin((L * Math.PI) / 180));
                            q.DrawLine(brush, x0, y0, x1, y1);
                            x0 = x1;
                            y0 = y1;
                            break;
                        }
                    case 'X':
                        {
                            break;
                        }
                    case 'Y':
                        {
                            break;
                        }
                    case '-':
                        {
                            L -= Convert.ToDouble(textBox4.Text);
                            break;
                        }
                    case '+':
                        {
                            L += Convert.ToDouble(textBox4.Text);
                            break;
                        }
                    
                    case '[':
                        {
                            stack.Push(Convert.ToString(x0));
                            stack.Push(Convert.ToString(y0));
                            stack.Push(Convert.ToString(L));
                            break;
                        }
                    case ']':
                        {
                            L = float.Parse(stack.Pop());
                            y0 = float.Parse(stack.Pop());
                            x0 = float.Parse(stack.Pop());
                            x1 = x0;
                            y1 = y0;
                            
                            break;
                        }
                }
            }   
        }

        public string ToTextFormula(string before, string X,string Y)
        {
            string Nextsteps = before;
            int k = 0;
            char[] DrawMap = Nextsteps.ToCharArray();
            for (int i = 0; i < DrawMap.Length; i++)
            {
                if (DrawMap[i] == 'X')
                {
                    Nextsteps = Nextsteps.Remove(k, 1);
                    Nextsteps = Nextsteps.Insert(k, X);
                    k += X.Length;
                    continue;
                }
                if (DrawMap[i] == 'Y')
                {
                    Nextsteps = Nextsteps.Remove(k, 1);
                    Nextsteps = Nextsteps.Insert(k, Y);
                    k += Y.Length;
                    continue;
                }
                k++;
            }
            return Nextsteps;
        }

        
        /// <summary>
        /// Основная часть программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(10);
            }
            
            steps++;
                label1.Text = Convert.ToString(steps);
                AxiomText.Text = ToTextFormula(AxiomText.Text, textBox1.Text, textBox2.Text);
                Graphics g = field.CreateGraphics();
                g.Clear(Color.White);
                Pen pen = new Pen(Color.DarkViolet, 2);
                DrawFractal(AxiomText.Text, g,  (35/Math.Pow(steps, 2)*1.5), pen);
            progressBar1.Value = 0;
        } 

        private void aboutbutton_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AxiomText.Text = "YF";
            Graphics g = field.CreateGraphics();
            g.Clear(Color.White);
            steps = 0;
            label1.Text = Convert.ToString('0');
        }

        private void AxiomText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
