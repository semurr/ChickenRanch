using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace UI
{
    class PushButton:Component
    {
        private ImageLabel upImage,downImage; //button texture or background
        private ImageLabel disabledImage;
        protected Component label;
        protected Point labelOffset;
        private bool mouseWasDown = false;
        private bool trackingMouse = false;
        private bool mouseWasInside = false;
        public delegate void ButtonEventHandler();
        private ButtonEventHandler clickEventHandler = null;
        private ButtonEventHandler hoverInEventHandler = null;
        private ButtonEventHandler hoverOutEventHandler = null;
        public static Texture2D disabledTexture;
        private bool isDisabled= false;

       

        public PushButton(int x, int y, Texture2D _upImage, Texture2D _downImage, string _text)
            : base(x, y, _upImage.Width,_upImage.Height)
        {
            upImage = new ImageLabel(0, 0, _upImage);
            downImage = new ImageLabel(0, 0, _downImage);
            labelOffset = new Point(0, 0);
            label = new TextLabel(0, 0, rect.Width, rect.Height, _text, Color.Beige);
            createDisabledImage();
        }
        //constuctor to allow button resizing instead of fitting to texture size
        public PushButton(int x, int y, Texture2D _upImage, Texture2D _downImage, string _text,int _width,int _height)
            : base(x, y, _width, _height)
        {
            upImage = new ImageLabel(0, 0, _upImage);
            downImage = new ImageLabel(0, 0, _downImage);
            labelOffset = new Point(0, 0);
            label = new TextLabel(0, 0, rect.Width, rect.Height, _text, Color.Beige);
            disabledImage.resize(rect.Width, rect.Height);
            createDisabledImage();
        }
        public PushButton(int x, int y, Texture2D _upImage, Texture2D _downImage, Texture2D _image)
            : base(x, y, _upImage.Width, _upImage.Height)
        {
            upImage = new ImageLabel(0, 0, _upImage);
            downImage = new ImageLabel(0, 0, _downImage);
            labelOffset = new Point((rect.Width - _image.Width) / 2, (rect.Height - _image.Height) / 2); 
            label = new ImageLabel(0, 0, _image);
            createDisabledImage();
        }
        public PushButton(int x, int y, int w, int h, string _text)
            : base(x, y, w, h)
        {
            labelOffset = new Point(0, 0);
            label = new TextLabel(0, 0, rect.Width, rect.Height, _text, Color.Beige);
            createDisabledImage();
        }
        private void createDisabledImage()
        {
            disabledImage = new ImageLabel(0, 0, disabledTexture);
            disabledImage.resize(rect.Width, rect.Height);
        }
        public void setClickEventHandler(ButtonEventHandler _clickEventHandler)
        {
            clickEventHandler += _clickEventHandler;
        }
        public void setHoverEventHandlers(ButtonEventHandler _hoverInEventHandler, ButtonEventHandler _hoverOutEventHandler)
        {
            hoverInEventHandler += _hoverInEventHandler;
            hoverOutEventHandler += _hoverOutEventHandler;
        }
        public void enable(bool _isEnabled)
        {
            if (_isEnabled != !isDisabled)
            {
                if (!_isEnabled)
                {
                    trackingMouse = mouseWasDown = mouseWasInside = false;
                }
                isDisabled = !_isEnabled;
            }
        }
        public bool mouseInside()
        {
            return rect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y));
            
        }
        public bool mouseDown()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            return (Mouse.GetState().LeftButton == ButtonState.Pressed) ||
                (gamePadState.Buttons.A == ButtonState.Pressed);
            //xbox controls
            
        }
        public override void resize(int w, int h)
        {
            upImage.resize(w, h);
            downImage.resize(w, h);
            base.resize(w, h);
        }
        protected void updateState()
        {
            if (mouseInside() && !mouseWasInside)
            {
                mouseWasInside = true;
                if (hoverInEventHandler != null)
                {
                    hoverInEventHandler();
                }
            }
            if (!mouseInside() && mouseWasInside)
            {
                mouseWasInside = false;
                if (hoverOutEventHandler != null)
                {
                    hoverOutEventHandler();
                }
            }
            if (mouseDown() && !mouseWasDown)
            {
                mouseWasDown = true;
                if (mouseInside())
                {
                    trackingMouse = true;
                }
            }
            if (!mouseDown() && mouseWasDown)
            {
                if (mouseInside() && trackingMouse)
                {
                    if (clickEventHandler != null)
                    {
                        // play audio for button click
                        if (Chicken.AudioManager.instance.sFXOn == true)
                        {
                            Chicken.AudioManager.instance.buttonClickSound.Play(0.3f, 0.0f, 0.0f);
                        }
                        clickEventHandler();
                    }
                }
          
                mouseWasDown = false;
                trackingMouse = false;
            }
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isDisabled)
            {
                updateState();
            }
            
            if (trackingMouse && mouseInside())
            {
                downImage.move(rect.X + 2, rect.Y + 2); 
                downImage.draw(gameTime, spriteBatch);

                label.move(rect.X + labelOffset.X, rect.Y + labelOffset.Y);
                label.draw(gameTime, spriteBatch);
            }
            else
            {
                upImage.move(rect.X, rect.Y);
                upImage.draw(gameTime, spriteBatch);

                label.move(rect.X + labelOffset.X - 2, rect.Y + labelOffset.Y - 2);
                label.draw(gameTime, spriteBatch);
            }


            if (isDisabled)
            {
                disabledImage.move(rect.X, rect.Y);
                disabledImage.draw(gameTime, spriteBatch);
            }
        }
    }

}
