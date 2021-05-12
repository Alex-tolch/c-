using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawLaba3
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            // расширенное окно для выбора цвета
            // colorDialog1.FullOpen = true;
            // установка обработчика события Scroll
           
        }

        public void SimpleLine(double x, double y, double X, double Y, Bitmap bmp, Color color)
        {
            double x0;
            double x1;
            double y0;
            double y1;

            bool flag = false;

            if ((X - x) == 0)
            {
                y0 = (y < Y) ? y : Y;
                y1 = (y < Y) ? Y : y;
                for (int i = Convert.ToInt32(y0); i <= y1; i++)
                {
                    bmp.SetPixel(Convert.ToInt32(x), i, color);

                }
            }
            else
            {
                double a = (Y - y) / (X - x);
                double b = y - a * x;
                if (Math.Abs(X - x) < Math.Abs(Y - y))
                {
                    // Меняем координату у
                    x0 = y;
                    x1 = Y;
                    y0 = x;
                    y1 = X;
                    flag = true;
                }
                else
                {
                    // Меняем координату х
                    x0 = x;
                    x1 = X;
                    y0 = y;
                    y1 = Y;
                }

                if (x1 < x0)
                {
                    double v = x1; x1 = x0; x0 = v;
                }

                if (flag)
                {
                    for (int i = Convert.ToInt32(x0); i <= x1; i++) bmp.SetPixel(Convert.ToInt32((i - b) / a), i, color);
                }
                else
                    for (int i = Convert.ToInt32(x0); i <= x1; i++) bmp.SetPixel(i, Convert.ToInt32(i * a + b), color);

            }
        }

        public void BarezenhamRound(int x0, int y0, int R, Bitmap bmp, Color color)
        {
            int x = 0;
            int y = R;
            int d = 3 - 2 * R;
            while (y >= x)
            {
                bmp.SetPixel(x + x0, y + y0, color);
                bmp.SetPixel(x + x0, -y + y0, color);
                bmp.SetPixel(-x + x0, y + y0, color);
                bmp.SetPixel(-x + x0, -y + y0, color);
                bmp.SetPixel(y + x0, x + y0, color);
                bmp.SetPixel(y + x0, -x + y0, color);
                bmp.SetPixel(-y + x0, x + y0, color);
                bmp.SetPixel(-y + x0, -x + y0, color);
                if (d <= 0)
                {
                    d += 4 * x + 6;
                }
                else
                {
                    d += 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
        }
        public void ModifyPattern(int x0, int y0, Bitmap pic, Color color)
        {
            Color bgColor = pic.GetPixel(x0, y0);
            pic.SetPixel(x0, y0, color);
            if (pic.GetPixel(x0 + 1, y0) == bgColor)
            {
                ModifyPattern(x0 + 1, y0, pic, color);
            }
            if (pic.GetPixel(x0 - 1, y0) == bgColor)
            {
                ModifyPattern(x0 - 1, y0, pic, color);
            }
            if (pic.GetPixel(x0, y0 + 1) == bgColor)
            {
                ModifyPattern(x0, y0 + 1, pic, color);
            }
            if (pic.GetPixel(x0, y0 - 1) == bgColor)
            {
                ModifyPattern(x0, y0 - 1, pic, color);
            }
        }
        public void Pattern(int x, int y, Bitmap bmp, Color color)
        {
            Color backcolor = bmp.GetPixel(x, y);
            int xl = x;
            int xr = x + 1;
            while ((xl >= 0) && (bmp.GetPixel(xl, y) == backcolor))
            {
                bmp.SetPixel(xl, y, color);
                xl--;
            }
            xl++;
            while ((xr < bmp.Width - 1) && (bmp.GetPixel(xr, y) == backcolor))
            {
                bmp.SetPixel(xr, y, color);
                xr++;
            }
            xr--;

            int tmp_x = xl;
            while ((tmp_x <= xr) && (y != 0))
            {
               
                while ((tmp_x <= xr) && (bmp.GetPixel(tmp_x, y - 1) != backcolor))
                {
                    tmp_x++;
                }
                if (tmp_x <= xr) Pattern(tmp_x, y - 1, bmp, color);
                tmp_x++;
            }
            tmp_x = xl;
            while ((tmp_x <= xr) && (y + 1 != bmp.Height))
            {
                while ((tmp_x <= xr) && (bmp.GetPixel(tmp_x, y + 1) != backcolor))
                {
                    tmp_x++;
                }
                if (tmp_x <= xr) Pattern(tmp_x, y + 1, bmp, color);
                tmp_x++;
            }
        }

        public void PatternLines(int x, int y, Bitmap bmp, Color[,] colorZ, int w, int h)
        {
            Color backcolor = bmp.GetPixel(x, y);
            int xl = x;
            int xr = x + 1;
            while ((xl >= 0) && (bmp.GetPixel(xl, y) == backcolor))
            {
                bmp.SetPixel(xl, y, colorZ[xl % w, y % h]);
                xl--;
            }
            xl++;
            while ((xr < bmp.Width - 1) && (bmp.GetPixel(xr, y) == backcolor))
            {
                bmp.SetPixel(xr, y, colorZ[xr % w, y % h]);
                xr++;
            }
            xr--;

            int tmp_x = xl;
            while ((tmp_x <= xr) && (y != 0))
            {
                while ((tmp_x <= xr) && (bmp.GetPixel(tmp_x, y - 1) != backcolor))
                {
                    tmp_x++;
                }
                if (tmp_x <= xr) PatternLines(tmp_x, y - 1, bmp, colorZ, w, h);
                tmp_x++;
            }
            tmp_x = xl;
            while ((tmp_x <= xr) && (y + 1 != bmp.Height))
            {
                while ((tmp_x <= xr) && (bmp.GetPixel(tmp_x, y + 1) != backcolor))
                {
                    tmp_x++;
                }
                if (tmp_x <= xr) PatternLines(tmp_x, y + 1, bmp, colorZ, w, h);
                tmp_x++;
            }
        }
        public void BezierLine(PointF[] points, Bitmap bmp, Color color)
        {
            int n = points.Length;
            int countPoints = n<< 6;
            PointF[] P = new PointF[n];
            PointF[] drawingPoints = new PointF[countPoints + 1];
            float dt = 1f / countPoints;
            float t = 0f;
            for (int i = 0; i <= countPoints; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    P[j] = points[j];
                }

                for (int j = n - 1; j > 0; j--)
                {
                    for (int k = 0; k < j; k++)
                    {
                        P[k].X += t * (P[k + 1].X - P[k].X);
                        P[k].Y += t * (P[k + 1].Y - P[k].Y);
                    }
                }
                drawingPoints[i] = P[0];
                t += dt;
            }
            for (int i = 1; i < countPoints + 1; i++)
            {
                SimpleLine((int)drawingPoints[i - 1].X, (int)drawingPoints[i - 1].Y, (int)drawingPoints[i].X, (int)drawingPoints[i].Y, bmp, color);
            }
        }



        bool flag = false;
        int x, y, X, Y;
        Color color = Color.FromArgb(1, 1, 1);
        bool temp1 = false;
        bool p1, p2 = false;
        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            temp1 = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < pictureBox1.Width; i++)
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    map.SetPixel(i, j, Color.WhiteSmoke);
                    pictureBox1.Image = map;
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = true;
            colorDialog1.ShowHelp = true;


            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                groupBox1.BackColor = color;
            }

            //if (colorDialog1.ShowDialog() == DialogResult.OK)
            //    // установка цвета
            //    color = colorDialog1.Color;
            //    groupBox1.BackColor = color;
        }
        Bitmap bmp = new Bitmap(1376,762);
        PointF[] points = new PointF[3];
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        { try
            {
                Color[,] col = new Color[2, 2];
                col[0, 0] = Color.White;
                col[0, 1] = color;
                col[1, 0] = color;
                col[1, 1] = Color.White;
                int h = 2;
                int w = 1;
               
                x = e.X;
                y = e.Y;
                int x1, x2, y1, y2;
                ///Риование линии
                if (radioButton1.Checked)
                {
                    if (flag)
                    {
                        SimpleLine(x, y, X, Y, bmp, color);
                        pictureBox1.Image = bmp;
                        X = x;
                        Y = y;
                        flag = false;
                    }
                    else
                    {
                        X = x;
                        Y = y;
                        bmp.SetPixel(x, y, color);
                        pictureBox1.Image = bmp;
                        flag = true;
                    }

                }
                ///Рисование ломаной линии
                if (radioButton2.Checked)
                {
                    if (flag)
                    {
                        SimpleLine(x, y, X, Y, bmp, color);
                        pictureBox1.Image = bmp;
                        X = x;
                        Y = y;
                    }
                    else
                    {
                        X = x;
                        Y = y;
                        bmp.SetPixel(x, y, color);
                        pictureBox1.Image = bmp;
                        flag = true;
                    }

                }
                //круг
                if (radioButton3.Checked)
                {
                    if (flag)
                    {
                        BarezenhamRound(X, Y, Convert.ToInt32(Math.Abs(Math.Sqrt(Math.Pow((X - x), 2) + Math.Pow((Y - y), 2)))), bmp, color);
                        pictureBox1.Image = bmp;
                        bmp.SetPixel(X, Y, color);
                        pictureBox1.Image = bmp;
                        flag = false;
                    }
                    else
                    {
                        X = x;
                        Y = y;
                        bmp.SetPixel(x, y, color);
                        pictureBox1.Image = bmp;
                        flag = true;
                    }
                }
                //заливка
                if (radioButton4.Checked)
                    Pattern(x, y, bmp, color);
                pictureBox1.Image = bmp;
                //ручка
                if (radioButton5.Checked)
                    temp1 = true;
                //модиф заливка
                if (radioButton6.Checked)
                {
                    try
                    {
                        ModifyPattern(Convert.ToInt32(x), Convert.ToInt32(y), bmp, color);
                        pictureBox1.Image = bmp;

                    }
                    catch
                    {
                        MessageBox.Show("Стек, при закраске этим методом переполнен!");
                    }
                }
                //заливка узором
                if(radioButton7.Checked)
                {
                    PatternLines(x, y, bmp, col, w, h);
                    pictureBox1.Image = bmp;
                }
                //линия безье
                if (radioButton8.Checked)
                {
                    if (p2)
                    {
                        points[0] = new PointF(x, y);
                        BezierLine(points, bmp, color);
                        pictureBox1.Image = bmp;
                        p1 = false;
                        p2 = false;
                    }
                    else
                    {
                        if(p1)
                        {
                            x2 = x;
                            y2 = y;
                            points[1] = new PointF(x2, y2);
                            bmp.SetPixel(x2, y2,color);
                            pictureBox1.Image = bmp;
                            p2 = true;
                        }
                        else
                        {
                            x1 = x;
                            y1 = y;
                            points[2] = new PointF(x1, y1);
                            bmp.SetPixel(x1, y1, color);
                            pictureBox1.Image = bmp;
                            p1 = true;
                        }
                    }
                }
            }

            catch
            {
                MessageBox.Show("Ошибка!");
            }

            }
       
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
          
            label2.Text = String.Format("X: " + e.X);
            label3.Text = String.Format("Y: " + e.Y);
            if (temp1 == true)
            {
                if (radioButton5.Checked)
                {
                    bmp.SetPixel(e.X, e.Y,color);
                    pictureBox1.Image = bmp;
                }
            }
        }
    }
}

