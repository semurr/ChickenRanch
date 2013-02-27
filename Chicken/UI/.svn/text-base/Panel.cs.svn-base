using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace UI
{
    class Panel:Container
    {
        private const int offsetData = 40;
        
        private UI.TextLabel description;
        private UI.TextLabel title = null;

        public Panel(int x, int y, Texture2D image)
            :base(x,y,image.Width,image.Height)
        {
            addComponent(new ImageLabel(x,y,image));
        }
        public Panel(int x, int y, Texture2D image,string _title)
            : base(x, y, image.Width, image.Height)
        {
            addComponent(new ImageLabel(x, y, image));
            title = new TextLabel(x, y + 10, image.Width, fontSize, _title, Color.Beige);
            addComponent(title); //change 20 to be font height
            description = new TextLabel(x, y + offsetData, image.Width, fontSize*4, "", Color.Beige);
            addComponent(description);
        }
        public string Description
        {
            set
            {
                description.Text = value;
            }
            get
            {
                return description.Text;
            }
        }
        
        public string Title
        {
            set
            {
                if (title != null)
                {
                    title.Text = value;
                }
            }
        }
    }
}
