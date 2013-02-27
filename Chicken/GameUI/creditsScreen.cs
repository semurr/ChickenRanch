using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Chicken.GameUI
{
    class creditsScreen:UI.Container
    {
    
        public static creditsScreen instance;
        private ArrayList gameEndFrames;
        UI.Animation gameEnd;
        //UI.Animation gameWinLoss;
        private UI.ImageLabel credits;
        private Point location;
        private string creditsInfo;
        public creditsScreen(int w, int h, ContentManager content)
            : base(0, 0, w, h)
        {
            instance = this;
            credits = new UI.ImageLabel(0, 0, content.Load<Texture2D>("menuImages/splashScreen"));
            addComponent(credits);
            credits.resize(rect.Width, rect.Height);
            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankButtonDarkGlow");

            creditsInfo = "TEAM HENPECKED"
                          + "\n\nKiera Valnes: Project Manager"
                          + "\nVanessa Lind: Assets Manager"
                          + "\nStephan Murray: Lead Programmer"
                          + "\n\nSPECIAL THANKS TO:"
                          + "\n\nWEBSITE CREATOR:"
                          + "\nKimara Lind"
                          + "\n\nADVISORS:"
                          + "\nProf. Duncan, Prof. Bunge, "
                          + "Prof. Bahrt, Dean Thomas"
                          + "\n\nPRIMARY TESTERS:"
                          + "\nCody Dixon, AJ Hanson, Ben Siems";
                          

            //UI.TextLabel title1 = new UI.TextLabel((rect.Width / 2) - 175, (rect.Height / 6), 
                                    //this.getWidth() / 3, (int)(this.getHeight() * 0.10), 
                                    //"Game Credits", 1.0f, Color.White);
            //addComponent(title1);
            location = credits.getPos();
            int verticalSpacing = 100;

            UI.ImageLabel background = (new UI.ImageLabel(location.X +(verticalSpacing * 4), 
                                    location.Y+100, content.Load<Texture2D>("menuImages/tutorialtipbox")));
            background.resize((credits.getWidth()/2)-100, credits.getHeight()- 200);
            addComponent(background);
            UI.ImageLabel menuTitle = (new UI.ImageLabel(location.X + (verticalSpacing * 3), 
                                    location.Y - (verticalSpacing / 5), content.Load<Texture2D>
                                    ("menuImages/M_creditstitle")));
            menuTitle.scale(1);
            addComponent(menuTitle);
            UI.TextLabel title2 = new UI.TextLabel((rect.Width / 2) - 175, (rect.Height / 2)-20, 
                                    this.getWidth() / 3, (int)(this.getHeight() * 0.10), creditsInfo, 
                                    0.3f, Color.White);
            addComponent(title2);

            UI.PushButton returnButton = new UI.PushButton((w/2) - (buttonUp.Width+50), 
                                    (h - buttonUp.Height) / 2 + 300, buttonUp, buttonDown, "");
            addComponent(returnButton);
            returnButton.setClickEventHandler(playAgainClicked);// play again return to start

            location = returnButton.getPos();
            //load the start font onto the button
            UI.TextLabel returnText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, 
                                    buttonUp.Height, "Return To Start", 0.5f, Color.White);
            addComponent(returnText);

            UI.PushButton quitButton = new UI.PushButton((w / 2) + (buttonUp.Width/2), 
                                    (h - buttonUp.Height) / 2 + 300, buttonUp, buttonDown, "");
            addComponent(quitButton);
            quitButton.setClickEventHandler(quitClicked);

            location = quitButton.getPos();
            //load the start font onto the button
            UI.TextLabel quitText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, 
                                    buttonUp.Height, "Quit Game", 0.5f, Color.White);
            addComponent(quitText);

            //gameEndFrames = new ArrayList();
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIconC"));//default torches
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon2C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon3C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon4C"));
            //gameEndFrames.Add(content.Load<Texture2D>("menu/torchIcon5C"));

            //gameEnd = new UI.Animation(50, gameEndFrames, true);
            //gameEnd.move(w / 2 -120,100);
            //addComponent(gameEnd);

            //credits = new UI.ImageLabel(300,200,content.Load<Texture2D>("menu/creditsText"));
            //addComponent(credits);
        }

        public void quitClicked() 
        {
            splashScreen.instance.gameStart = false;
            Game1.instance.setGameState(Game1.GameState.splash);
            splashScreen.instance.enableKeypress = true;
           // Game1.instance.setGameState(Game1.GameState.start); //start a new game 
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
    }
    
}
