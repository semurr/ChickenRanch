using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UI
{
    class Animation:ImageLabel
    {
        private ArrayList imageFrames;
        private int currentFrameIndex = 0;
        private Timer timer;

        public Animation(int _timeBetweenFrames, ArrayList _imageFrames, bool _looping)
            : base(0, 0, (Texture2D)_imageFrames[0]) 
        {
            imageFrames = _imageFrames;
            timer = new Timer(_timeBetweenFrames, _looping ? -1 : _imageFrames.Count);
        }
        public bool isFinished()
        {
            return timer.finishedTicking();
        }
        public void setFrames(ArrayList _imageFrames)
        {
            currentFrameIndex = 0;
            imageFrames = _imageFrames;
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(timer.update(gameTime))
            {
                currentFrameIndex++;
                if (currentFrameIndex == imageFrames.Count)
                    currentFrameIndex = 0;
            }
            labeltexture = (Texture2D)imageFrames[currentFrameIndex];
            base.draw(gameTime, spriteBatch);
        }
    }
}
