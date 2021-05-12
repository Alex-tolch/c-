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
    class MyClass
    {
        public MyClass ()
        {

        }

        public void Gray(PictureBox pb,  LinkedList<Bitmap> list)
        {
            int brg;
            Bitmap bmp = new Bitmap(pb.Image);
            for (int i = 1; i < bmp.Width; i++)
            {
                for (int j = 1; j < bmp.Height; j++)
                {
                    brg = (int)(0.3 * bmp.GetPixel(i, j).R + 0.59 * bmp.GetPixel(i, j).G + 0.11 * bmp.GetPixel(i, j).B);
                    bmp.SetPixel(i, j, Color.FromArgb(brg, brg, brg));
                }
            }
            pb.Image = bmp;
            list.AddLast(bmp);
        }

        public void Contrast(PictureBox pb, LinkedList<Bitmap> list)
        {
            Bitmap bmp = new Bitmap(pb.Image);
            int R;
            int G;
            int B;
            double brg;
            double fMax = (int)(0.3 * bmp.GetPixel(1, 1).R + 0.59 * bmp.GetPixel(1, 1).G + 0.11 * bmp.GetPixel(1, 1).B);
            double fMin = fMax;
            double gMax;
            double gMin;
            double g;
            double f;
            double a;
            double b;
            int x = 50;
            for (int i = 1; i < bmp.Width; i++)
            {
                for (int j = 1; j < bmp.Height; j++)
                {
                    brg = (int)(0.3 * bmp.GetPixel(i, j).R + 0.59 * bmp.GetPixel(i, j).G + 0.11 * bmp.GetPixel(i, j).B);
                    if (brg > fMax)
                    {
                        fMax = brg;
                    }
                    if (brg < fMin)
                    {
                        fMin = brg;
                    }
                }
            }
            gMax = 255;
            gMin = 0;
            a = (gMax - gMin) / (fMax - fMin);
            b = (gMin * fMin - gMax * fMin) / (fMax - fMin);
            for (int i = 1; i < bmp.Width; i++)
            {
                for (int j = 1; j < bmp.Height; j++)
                {
                    brg = (int)(0.3 * bmp.GetPixel(i, j).R + 0.59 * bmp.GetPixel(i, j).G + 0.11 * bmp.GetPixel(i, j).B);
                    g = a * brg + b;
                    f = g / brg; ;
                    if (bmp.GetPixel(i, j).R * f <= 255)
                    {
                        R = (int)(bmp.GetPixel(i, j).R * f);
                    }
                    else
                    {
                        R = (int)(bmp.GetPixel(i, j).R);
                    }
                    if (bmp.GetPixel(i, j).G * f <= 255)
                    {
                        G = (int)(bmp.GetPixel(i, j).G * f);
                    }
                    else
                    {
                        G = (int)(bmp.GetPixel(i, j).G);
                    }
                    if (bmp.GetPixel(i, j).B * f <= 255)
                    {
                        B = (int)(bmp.GetPixel(i, j).B * f);
                    }
                    else
                    {
                        B = (int)(bmp.GetPixel(i, j).B);
                    }
                    bmp.SetPixel(i, j, Color.FromArgb((R), (G), (B)));
                }
            }
            pb.Image = bmp;
            list.AddLast(bmp);
        }

        public void OpenFile (PictureBox pb, OpenFileDialog ofd, LinkedList<Bitmap> list)
        {
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                Bitmap bmp = new Bitmap(ofd.FileName);
                pb.Image = bmp;
                list.Clear();
                list.AddFirst(bmp);
                ofd.FileName = "";
            }
        }

        public void Cancel (PictureBox pb, LinkedList<Bitmap> list)
        {
            if (list.Count != 1)
            {
                list.RemoveLast();
                pb.Image = list.Last();
            }
        }

        public void Clear (PictureBox pb, LinkedList<Bitmap> list)
        {
            pb.Image = list.First();
        }
    }
}
