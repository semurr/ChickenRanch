using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace UI
{
    class ImageLabel:Component
    {
        protected Texture2D labeltexture;
        
        public ImageLabel(int x, int y,Texture2D image)
            : base(x, y, image.Width, image.Height)
        {
            labeltexture = image;
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(labeltexture, rect, Color.White);
        }
        public void changeImage(Texture2D _image)
        {
            labeltexture = _image;
        }
    }
}
