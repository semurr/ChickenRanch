using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace UI
{
    public abstract class Component
    {
        public static SpriteFont font;
       // public static SpriteFont fontBig;
        //protected const int fontSize = 20;
        
        public int largefontSize = 60;
        public int mediumfontSize = 40;
        public int fontSize = 80;
        protected Rectangle rect; //size and positon of component
        public bool visible = true;

        public Component(int x, int y, int w, int h)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = w;
            rect.Height = h;
        }
        public virtual void move(int x,int y) //move components
        {
            rect.X = x;
            rect.Y = y;
        }
        public Point getPos()
        {
            return new Point(rect.X, rect.Y);
        }

        public virtual void resize(int w, int h) //change component size
        {
            rect.Width = w;
            rect.Height = h;
        }
        public virtual void scale(int _scale)
        {
            rect.Width *= _scale;
            rect.Height *= _scale;
        }
        public virtual int getWidth()
        {
            return rect.Width;
        }
        public virtual int getHeight()
        {
            return rect.Height;
        }
        //public virtual void changeFontSize(int _size)
        //{
        //    fontSize = _size;
        //}
        public abstract void draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
