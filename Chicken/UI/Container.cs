using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UI
{
    class Container:Component
    {
        private ArrayList children;
        public Container(int x, int y, int w, int h):base(x, y, w, h)
        {
            children = new ArrayList();
        }
        public void addComponent(Component child)
        {
            children.Add(child);
        }
        public void removeComponent(Component child)
        {
            children.Remove(child);
        }
        public override void move(int x, int y) //move components
        {
            Point offset = new Point(x - rect.X, y - rect.Y);
            foreach (Component child in children)
            {
                Point oldPos = child.getPos();
                child.move(oldPos.X + offset.X, oldPos.Y + offset.Y);
            }
 
            base.move(x, y);
        }
 
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Component child in children)
            {
                if (child.visible)
                {
                    child.draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
