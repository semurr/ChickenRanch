using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Chicken.GameUI
{
    class splashScreen : UI.Container
    {
        public static splashScreen instance;
        private float time;
        private float totalTime= 3;
        public bool timerEnded = false;
        public bool gameStart;
        public bool enableKeypress = true;
        
      

         public splashScreen(int w, int h, ContentManager content)
            : base(0, 0, w, h)
        {
            instance = this;
            UI.ImageLabel splashLogo = new UI.ImageLabel(0, 0, content.Load<Texture2D>
                                      ("menuImages/splashScreen[3]"));
            splashLogo.resize(rect.Width, rect.Height);
            addComponent(splashLogo);

            UI.TextLabel keyText = new UI.TextLabel(rect.Width/2, rect.Height-50, 50, 20,
                "**Press any key to continue**", Color.White);
            keyText.changeFontScale(0.5f);
            addComponent(keyText);
            gameStart = true;
           
             
            //this.visible = true;
        }
         public void keyInputTimer()
         {
             if (gameStart == true && enableKeypress == true)
             {
                 if (Keyboard.GetState().GetPressedKeys().Length > 0 || 
                     GameUI.gameWorld.instance.gamePadState.IsButtonDown(Buttons.A))
                 {
                     Game1.instance.setGameState(Game1.GameState.start);
                     enableKeypress = false;
                     
                 }
             }
             else if (gameStart == false && enableKeypress == true)
             {
                 if (Keyboard.GetState().GetPressedKeys().Length > 0 || 
                     GameUI.gameWorld.instance.gamePadState.IsButtonDown(Buttons.A))
                 {
                     Game1.instance.Exit();
                 }
                 //key input exits game
             }

         }
         public void splashScreenUpdateTimer(GameTime gameTime)
         {
             time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
             if (time>=totalTime)
             {
                 //instance.visible = false;
                 Game1.instance.setGameState(Game1.GameState.start);
                 timerEnded = true;
             }

            
         }
    
    }
}
