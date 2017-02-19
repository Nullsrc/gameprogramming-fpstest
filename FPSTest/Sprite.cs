using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FPSTest
{
    public class Sprite
    {
        private float x = 0;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y = 0;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float scale = 1;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float rotation = 0;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private int centerY;
        public int CenterY
        {
            get { return centerY; }
            set { centerY = value; }
        }

        private int centerX;
        public int CenterX
        {
            get { return centerX; }
            set { centerX = value; }
        }

        private Sprite parent = null;
        public Sprite Parent
        {
            get { return parent; }
        }
        public List<Sprite> children = new List<Sprite>();
        public void Add(Sprite s)
        {
            s.parent = this;
            children.Add(s);
        }

        public void Kill()
        {
            parent.children.Remove(this);
        }

        //methods
        public void Render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            Paint(g);
            foreach (Sprite s in children)
            {
                s.Render(g);
            }
            g.Transform = original;
        }

        public virtual void Paint(Graphics g)
        {
        }

        public virtual void Act()
        {
        }
    }
}
