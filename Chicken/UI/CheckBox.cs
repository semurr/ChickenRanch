using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UI
{
    class CheckBox:PushButton
    {
        private ImageLabel checkedImage;
        private ImageLabel unCheckedImage;
        public ArrayList buttonGroup = null;
        public bool isChecked;
  
        public CheckBox(int x, int y, Texture2D _checkedImage,Texture2D _unCheckedImage, string _text, bool _isChecked)
            : base(x, y, 0, 0, _text) 
        {
            isChecked = _isChecked;
            checkedImage = new ImageLabel(0, 0, _checkedImage);
            unCheckedImage = new ImageLabel(0, 0, _unCheckedImage);
            setClickEventHandler(onClicked);
            label.resize(((TextLabel)label).getTextWidth(), checkedImage.getHeight());
            labelOffset.X = _checkedImage.Width + 5;
            rect.Width = labelOffset.X + ((TextLabel)label).getTextWidth();
            rect.Height = _checkedImage.Height;
        }
        public void changeFontScale(float _fontScale)
        {
            ((TextLabel)label).changeFontScale(_fontScale);
        }
        private void onClicked()
        {
            if (buttonGroup != null) // radio button
            {
                if (!isChecked)
                {
                    isChecked = true;
                    foreach (CheckBox c in buttonGroup)
                    {
                        if (c != this)
                            c.isChecked = false;
                    }
                }
            }
            else
            {
                isChecked = !isChecked;
            }
        }
        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            updateState();

            if (isChecked)
            {
                checkedImage.move(rect.X, rect.Y);
                checkedImage.draw(gameTime, spriteBatch);
            }
            else
            {
                unCheckedImage.move(rect.X, rect.Y);
                unCheckedImage.draw(gameTime, spriteBatch);
            }

            label.move(rect.X + labelOffset.X, rect.Y + labelOffset.Y);
            label.draw(gameTime, spriteBatch);
        }
    }
}
