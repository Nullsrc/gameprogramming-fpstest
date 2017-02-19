using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FPSTest
{
    class SineSprite : Sprite
    {
        public SineSprite(Image image, float x, float y, int timer)
        {
            this.image = image;
            this.CenterY = image.Height / 2;
            this.CenterX = image.Width / 2;
            this.Y = y;
            this.X = x;
            this.Timer = timer;
        }

        private Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private int timer;
        public int Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        public override void Act()
        {
            this.Timer += 1;
            this.Y = this.CenterY + (float)(100 * Math.Cos(timer / 10.0));
        }

        public override void Paint(Graphics g)
        {
            g.DrawImage(image, this.X, this.Y);
        }
    }
}
