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
    class VictoryLossScreen:UI.Container
    {
        public static VictoryLossScreen instance;
       // private ArrayList gameEndFrames;
       // UI.Animation gameEnd;
        //UI.Animation gameWinLoss;
        private UI.ImageLabel victoryloss;
        Texture2D titleImage;
        UI.ImageLabel menuTitle;
        private Point location;
        private string info;
        UI.TextLabel title2;
        ContentManager content;
        public bool enableKeypress = false;
       

        public VictoryLossScreen(int w, int h, ContentManager _content)
            : base(0, 0, w, h)
        {
            instance = this;
            content = _content;
            int verticalSpacing = 100;
            victoryloss = new UI.ImageLabel(0, 0, content.Load<Texture2D>("menuImages/splashScreen"));
            addComponent(victoryloss);
            victoryloss.resize(rect.Width, rect.Height);
            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankButtonDarkGlow");

            UI.ImageLabel background = (new UI.ImageLabel(location.X + (verticalSpacing * 4)-20,
                location.Y + 100, content.Load<Texture2D>("menuImages/tutorialtipbox")));
            background.resize((victoryloss.getWidth() / 2) - 100, victoryloss.getHeight() - 200);
            addComponent(background);

            info = "Win/Loss";

            //UI.TextLabel title1 = new UI.TextLabel((rect.Width / 2) - 175, (rect.Height / 6),
            //this.getWidth() / 3, (int)(this.getHeight() * 0.10), "Game Credits", 1.0f, Color.White);
            //addComponent(title1);
            location = victoryloss.getPos();
            

            menuTitle = (new UI.ImageLabel(location.X + (verticalSpacing * 3)-20, location.Y - (verticalSpacing / 5),
                content.Load<Texture2D>("menuImages/M_wintitle")));
            menuTitle.scale(1);
            addComponent(menuTitle);
            title2 = new UI.TextLabel((rect.Width / 2) - 210, rect.Height / 3, this.getWidth() / 3, 
                (int)(this.getHeight() * 0.10), info, 0.5f, Color.White);
            addComponent(title2);

            //UI.TextLabel keyText = new UI.TextLabel(rect.Width / 2, rect.Height - 50, 50, 15,
            //"Press any key to continue", Color.White);
            //addComponent(keyText);

            UI.PushButton myButton = new UI.PushButton((w/2) -( buttonUp.Width/2), (h - buttonUp.Height) 
                / 2 + 300, buttonUp, buttonDown, "");// play again return to start
            addComponent(myButton);
            myButton.setClickEventHandler(onButtonClicked);

            location = myButton.getPos();
            //load the start font onto the button
            UI.TextLabel returnText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, buttonUp.Height,
                "Continue", 0.5f, Color.White);
            addComponent(returnText);

            //gameEndFrames = new ArrayList();
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIconC"));//default torches
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon2C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon3C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon4C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon5C"));

            //gameEnd = new UI.Animation(50, gameEndFrames, true);
            //gameEnd.move(w / 2 -120,100);
            //addComponent(gameEnd);

          
        }

        public void onButtonClicked() //return to start
        {
            //splashScreen.instance.gameStart = false;
            //Game1.instance.setGameState(Game1.GameState.splash);
            Game1.instance.setGameState(Game1.GameState.credits); //start a new game 
        }
        public void playAgainClicked()//return to start, re-initialize the world
        {
            Game1.instance.setGameState(Game1.GameState.start); //start a new game 
            gameWorld.instance.initializeWorld();
        }
        public void createGameEndFramesList(ArrayList _winloss)
        {
            //assigns a list of images to create animation for win/loss
            //if (_winloss != null)
            //    gameEnd.setFrames(_winloss);
              
            //else
            //    gameEnd.setFrames(gameEndFrames);
        }
        public void enableKeyPress()
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0 ||
                GameUI.gameWorld.instance.gamePadState.IsButtonDown(Buttons.A))
            {
                Game1.instance.setGameState(Game1.GameState.credits);
                enableKeypress = false;

            }
            
        }
        public void determineWinLossInfo(bool win)
        {
            if (win == true)
            {
                titleImage = content.Load<Texture2D>("menuImages/M_wintitle");
                info = "You've met your goal!";         
                title2.changeText(info);
                menuTitle.changeImage(titleImage);
            }
            else if (win == false)
            {
                titleImage = content.Load<Texture2D>("menuImages/M_losetitle");
                info = "   You were unsuccessful,\n   maybe you can try again?";
                title2.changeText(info);
                menuTitle.changeImage(titleImage);
            }
        }
    
    

    }
}
