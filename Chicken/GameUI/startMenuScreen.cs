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
    class startMenuScreen : UI.Container
    {
         public static startMenuScreen instance;
         //add frame animation for title
         UI.Animation titleText;
         private ArrayList titleTextFrames;

         public startMenuScreen(int w, int h, ContentManager content)
            : base(0, 0, w, h)
        {
            instance = this;
            int verticalSpacing, buttonStartY;
            UI.ImageLabel startMenu = new UI.ImageLabel(0, 0, content.Load<Texture2D>
                                        ("menuImages/menuBackgroundTransTest5 copy"));
            startMenu.resize(rect.Width, rect.Height);
            addComponent(startMenu);
             //add title text
            //UI.TextLabel title1 = new UI.TextLabel((int)(rect.Width*0.10),rect.Height - 
                                       // (rect.Height-30), startMenu.getWidth()/3,(int)
                                       // (startMenu.getHeight()*0.10), "Chicken", 1.0f, Color.White);
            //addComponent(title1);
            //UI.TextLabel title2 = new UI.TextLabel((int)(rect.Width * 0.5), rect.Height - 
                                        //(rect.Height - 30), startMenu.getWidth() / 3, (int)
                                        //(startMenu.getHeight() * 0.10), "Rancher", 1.0f, Color.White);
            //addComponent(title2);

             //load 2d textures for title animation
            titleTextFrames = new ArrayList();
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleText"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleText"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextSmall"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextBig"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleText"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleText"));
            
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextSmall"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextBig"));
            
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell0"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell1"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell2"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell3"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell4"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell5"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell6"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell7"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell8"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell9"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell10"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell11"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell12"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleSpell13"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextSmall"));
            titleTextFrames.Add(content.Load<Texture2D>("MenuImages/titleTextBig"));
            
            //add buttons
            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            // UI.TextLabel startText = new UI.Text
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankbuttonDarkGlow");
            verticalSpacing = 100;
            buttonStartY = (h - buttonUp.Height) / 4;

            UI.PushButton newGameButton = new UI.PushButton((w - buttonUp.Width - 75), buttonStartY,
                                 buttonUp, buttonDown, "");//options button
            addComponent(newGameButton);
            newGameButton.setClickEventHandler(newGameClicked);
            //load the options font onto the button
           Point location = newGameButton.getPos();
            UI.TextLabel newGameText = new UI.TextLabel(location.X, location.Y, buttonUp.Width,
                                    buttonUp.Height, "New Game", 0.75f, Color.White);
            addComponent(newGameText);

             //load the start button and event
            UI.PushButton startButton = new UI.PushButton((w - buttonUp.Width - 75),
                                    buttonStartY + verticalSpacing, buttonUp, buttonDown, "");
            addComponent(startButton);
            startButton.setClickEventHandler(onButtonClicked);
            
             //load a temp location variable
              location = startButton.getPos();
             //load the start font onto the button
             UI.TextLabel startText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, 
                                    buttonUp.Height, "Continue", 0.75f, Color.White);
             addComponent(startText);

             //load the NewGame button and event
            

             //load the options button and event
             UI.PushButton optionsButton = new UI.PushButton((w - buttonUp.Width - 75), buttonStartY + (verticalSpacing*2),
                                    buttonUp, buttonDown, "");//options button
            addComponent(optionsButton);
            optionsButton.setClickEventHandler(optionsClicked);
            //load the options font onto the button
            location = optionsButton.getPos();
            UI.TextLabel optionsText = new UI.TextLabel(location.X, location.Y, buttonUp.Width,
                                    buttonUp.Height, "Options", 0.75f, Color.White);
            addComponent(optionsText);

             //load the help button and event
            UI.PushButton helpButton = new UI.PushButton((w - buttonUp.Width) - 75,  buttonStartY + (verticalSpacing*3),
                                    buttonUp, buttonDown, "");//help button
            addComponent(helpButton);
            helpButton.setClickEventHandler(helpClicked);
            //load the help font onto the button
            location = helpButton.getPos();
            UI.TextLabel helpText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, buttonUp.Height, 
                                    "Help", 0.75f, Color.White);
            addComponent(helpText);

            UI.PushButton exitButton = new UI.PushButton((w - buttonUp.Width) - 75, buttonStartY + (verticalSpacing * 4),
                                buttonUp, buttonDown, "");//credits button
            addComponent(exitButton);
            exitButton.setClickEventHandler(exitClicked);
            //load the help font onto the button
            location = exitButton.getPos();
            UI.TextLabel exitText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, buttonUp.Height,
                                    "Exit", 0.75f, Color.White);
            addComponent(exitText);

            UI.PushButton creditsButton = new UI.PushButton((w - buttonUp.Width) - 75, buttonStartY + (verticalSpacing * 5),
                                   buttonUp, buttonDown, "");//credits button
            addComponent(creditsButton);
            creditsButton.setClickEventHandler(creditsClicked);
            //load the help font onto the button
            location = creditsButton.getPos();
            UI.TextLabel creditsText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, buttonUp.Height,
                                    "Credits", 0.75f, Color.White);
            addComponent(creditsText);
           

             //--------------------------------------------------------Debug
            // //debug test for other menu screens
            //UI.PushButton debugButton = new UI.PushButton((w - buttonUp.Width - 500), (h - buttonUp.Height) / 2,
            //                      buttonUp, buttonDown, "debug");
            //addComponent(debugButton);
            //debugButton.setClickEventHandler(debugClicked);
            // //--------------------------------------------------------Debug

            titleText = new UI.Animation(200, titleTextFrames, true);
            titleText.move((int)(rect.Width * 0.10), rect.Height - (rect.Height - 30));
            addComponent(titleText);
        }

        //----------------------------------------------------Debug
         //public void debugClicked()
         //{
         //    // gameWorld.instance.initializeWorld();
         //    //gameWorld.instance.initializeWorld();
         //    //Game1.instance.setGameState(Game1.GameState.gameMenu);
         //    Game1.instance.setGameState(Game1.GameState.winloss);
          
         //    //initialize all variables to default  
         //    GameUI.gameWorld.instance.isPaused = true;

         //}
        //-----------------------------------------------------Debug

        public void onButtonClicked()
        {
            EndDaySummary.instance.updateSummary();
            gameInterface.instance.summaryPanel.visible = true; 
            //could use an if statement if we don't want this to always appear
           // gameWorld.instance.initializeWorld();
            Game1.instance.setGameState(Game1.GameState.game);
            Game1.instance.loadGame();//load the previously saved game
            gameWorld.instance.isPaused = true;
        }
        public void newGameClicked()
        {
            Game1.instance.setGameState(Game1.GameState.newGame);
        }
        public void optionsClicked()
        {
            Game1.instance.setGameState(Game1.GameState.options);
        }
        public void helpClicked()
        {
            Game1.instance.setGameState(Game1.GameState.help);
        }
        public void creditsClicked()
        {
            Game1.instance.setGameState(Game1.GameState.credits);
        }
        public void exitClicked()
        {
            splashScreen.instance.gameStart = false;//splash has been accessed once, will default now to exit on keypress
            Game1.instance.setGameState(Game1.GameState.splash);
        }
    }
}


