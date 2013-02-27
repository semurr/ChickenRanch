using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UI
{
    class TextLabel:Component
    {
        private Color color;
        private string text;
        private float fontScale = 1.0f;
        public TextLabel(int x, int y, int w, int h, string _text, float _fontScale, Color _color)
            : base(x, y, w, h) 
        {
            text = _text;
            color = _color;
            fontScale = _fontScale;
        }
        public TextLabel(int x, int y, int w, int h, string _text, Color _color)
            : base(x, y, w, h)
        {
            text = _text;
            color = _color;
            
        }
        public void changeFontScale(float _fontScale)
        {
            fontScale = _fontScale;
        }
        public int getTextWidth()
        {
            return (int)font.MeasureString(text).X;
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 textPos = new Vector2(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            Vector2 textOrigin = font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, textPos, color, 0, textOrigin, fontScale, SpriteEffects.None, 0.5f);
        }
        public void changeText(string _text)
        {
            text = _text;
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
    }
}
