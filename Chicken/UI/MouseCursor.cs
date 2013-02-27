using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UI
{
    class MouseCursor:ImageLabel
    {
        public static MouseCursor instance;
        
        //TextLabel mouseDebug;  //debugging purposes
        ImageLabel dragIcon = null;
        private Point hotSpot;
       
        public MouseCursor(int hotSpotX, int hotSpotY,Texture2D image)
            : base(0, 0, image) 
        {
            instance = this;
           // mouseDebug = new TextLabel(500, 500, 100, 10, "", Color.Beige); //debugging purposes
            hotSpot = new Point(hotSpotX,hotSpotY);
           

           
           
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            //debug code for mouse position
            //mouseDebug.Text = rect.X.ToString() + ", " + rect.Y.ToString();
            //mouseDebug.draw(gameTime, spriteBatch);

            

            if (gamePadState.ThumbSticks.Left.Y < 0)
            {
                rect.Y += + 3;
                
                Mouse.SetPosition(rect.X, rect.Y);
            }
            if (gamePadState.ThumbSticks.Left.Y > 0)
            {
                rect.Y +=  - 3;
               
                Mouse.SetPosition(rect.X, rect.Y);
            }
            if (gamePadState.ThumbSticks.Left.X > 0)
            {
                rect.X +=  + 3;
                
                Mouse.SetPosition(rect.X, rect.Y);
            }
            if (gamePadState.ThumbSticks.Left.X < 0)
            {
                rect.X +=  - 3;
               
                Mouse.SetPosition(rect.X, rect.Y);
            }
           
            move(Mouse.GetState().X, Mouse.GetState().Y);
           

            if (dragIcon != null)
            {
                dragIcon.move(rect.X+ hotSpot.X, rect.Y+hotSpot.Y);
                dragIcon.draw(gameTime, spriteBatch);
            }

            base.draw(gameTime, spriteBatch);
        }

        public void beginDrag(Texture2D _image)
        {
            dragIcon = new ImageLabel(0, 0, _image);

        }
        public void endDrag()
        {
            dragIcon = null;
        }
    }
}
