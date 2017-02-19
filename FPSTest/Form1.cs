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
        public static Thread render;
        public static Thread update;
        public static int s = 0;
        public static int fps = 30;
        public static double running_fps = 30.0;
        SineSprite sprite = new SineSprite(FPSTest.Properties.Resources.sprite, 0, 0, s);
        public int formHeight;
        public int formWidth;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            form = this;
            UpdateSize();

            render = new Thread(new ThreadStart(Render));
            render.Start();
            update = new Thread(new ThreadStart(Update));
            update.Start();
        }

        public static void Render()
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

        public static void Update()
        {
            while(true)
            {
                s++;
                Thread.Sleep(1000 / fps);
            }
        }

        private void UpdateSize()
        {
            formHeight = form.ClientSize.Height;
            formWidth = form.ClientSize.Width;
            sprite.X = (formWidth / 4) - (sprite.Image.Width / 4);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            render.Abort();
            update.Abort();
        }

        protected override void OnResize(EventArgs e)
        {
            running_fps = fps;
            Refresh();
            UpdateSize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            sprite.Act();
            sprite.Render(e.Graphics);
        }
    }
}
