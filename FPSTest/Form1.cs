using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using System.VisualStudio.Modeling.

namespace FPSTest
{
    public partial class Form1 : Form
    {
        public static Form form;
        public static Thread thread;
        public static Thread thread2;
        public static int s = 100;
        public static int fps = 30;
        public static double running_fps = 30.0;
        public static int numOfPoints = 0;
        public static int baseX;
        public static int baseY;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            thread = new Thread(new ThreadStart(run));
            thread.Start();
            thread2 = new Thread(new ThreadStart(otherRun));
            thread2.Start();
        }

        public static void run()
        {
            DateTime last = DateTime.Now;
            DateTime now = last;
            TimeSpan frameTime = new TimeSpan(10000000 / fps);
            while (true)
            {
                DateTime temp = DateTime.Now;
                running_fps = .9 * running_fps + .1 * 1000.0 / (temp - now).TotalMilliseconds;
                now = temp;
                TimeSpan diff = now - last;
                if (diff.TotalMilliseconds < frameTime.TotalMilliseconds)
                    Thread.Sleep((frameTime - diff).Milliseconds);
                last = DateTime.Now;
                form.Invoke(new MethodInvoker(form.Refresh));
            }
        }

        public static void otherRun()
        {
            while(true)
            {
                s++;
                Thread.Sleep(1000 / fps);
            }
        }

    private void UpdateSize()
        {
            baseX = 0;
            baseY = form.ClientSize.Height /2;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            thread.Abort();
            thread2.Abort();
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateSize();
            running_fps = fps;
            Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            running_fps = fps;
            if (e.KeyCode == Keys.W) numOfPoints += 100;
            else if (e.KeyCode == Keys.S) numOfPoints -= 100;
            else if (e.KeyCode == Keys.R) numOfPoints = 0; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Font font = new System.Drawing.Font("Courier New", 48);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            for (int i = 0; i < numOfPoints; i++)
            {
                e.Graphics.FillRectangle(Brushes.White, 100, 100, i % form.ClientSize.Width, 100);
            }
            e.Graphics.DrawString(((int)running_fps).ToString(), font, Brushes.Black, 0, 0);
            for (int i = 0; i < numOfPoints; i++)
            {
                double t = 100 * Math.Sin(s / (200.0 / (i / 10.0)));
                double v = 5 * Math.Cos(s / (200.0 / (i / 10.0)));
                Brush dullBrush = new System.Drawing.SolidBrush(new ColorDemo.HSLColor((double)i % 960.0 / 4, 105.0 + 9 * v, 120.0));
            if (v < 0)
                {
                    e.Graphics.FillEllipse(dullBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t), (int)v + 15, (int)v + 15);
                    e.Graphics.FillEllipse(dullBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t) - 250, (int)v + 15, (int)v + 15);
                    e.Graphics.FillEllipse(dullBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t) + 250, (int)v + 15, (int)v + 15);
                }
            }
            for (int i = 0; i < numOfPoints; i++)
            {
                double t = 100 * Math.Sin(s / (200.0 / (i / 10.0)));
                double v = 5 * Math.Cos(s / (200.0 / (i / 10.0)));
                Brush brightBrush = new System.Drawing.SolidBrush(new ColorDemo.HSLColor(((double)i % 960.0) / 4, 195.0 + 9*v, 120.0));
                if (v >= 0)
                {
                    e.Graphics.FillEllipse(brightBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t), (int)v + 15, (int)v + 15);
                    e.Graphics.FillEllipse(brightBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t) - 250, (int)v + 15, (int)v + 15);
                    e.Graphics.FillEllipse(brightBrush, baseX + (int)(i * 2) % form.ClientSize.Width, (baseY + (float)t) + 250, (int)v + 15, (int)v + 15);
                }
            }
            e.Graphics.FillEllipse(Brushes.Black, form.ClientSize.Width / 2 + (float)(form.ClientSize.Width / 2 * Math.Sin(s / 10.0)) - 5, form.ClientSize.Height - 10, 10, 10);
        }
    }
}
