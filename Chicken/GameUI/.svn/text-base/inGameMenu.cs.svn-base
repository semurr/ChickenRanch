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
    class inGameMenu: UI.Container
    {
        public static inGameMenu instance;
        //main barn panel for user interface
        //private const int panelWidth = 122;
        //private const int panelHeight = 204;
        private const int panelGap = 5;
        private const int buttonVerticalSpacing = 84;
        private const int buttonHorizontalSpacing = 80;
        private int leftColumnMargin,middleColumnMargin;
        private int buttonWidth = 190;
        private int buttonHeight= 40;
        private int chickenCost, roosterCost, chickenSell, roosterSell, eggSell, eggSellEnd;
        UI.TextLabel sellEggsText;
        public UI.TextLabel pausedText;

     

         public inGameMenu(int w, int h, ContentManager content,
                            GraphicsDeviceManager _graphics):base(0,0,w,h)
        {

            instance = this;
            this.visible = false;
             //assign variables from the economics class
            chickenCost = gameWorld.instance.player.chickenCost;
            chickenSell = gameWorld.instance.player.chickenSell;
            roosterCost = gameWorld.instance.player.roosterCost;
            roosterSell = gameWorld.instance.player.roosterSell;
            eggSell = gameWorld.instance.player.eggSell;
            eggSellEnd = gameWorld.instance.player.eggSellEnd;
            UI.ImageLabel menuBackground = new UI.ImageLabel((w/4), 80, 
                            content.Load<Texture2D>("menuImages/chickenCoopMenuPoster"));
            menuBackground.resize(rect.Width/2, rect.Height-200);
            addComponent(menuBackground);//add a background image for the menu system

            UI.ImageLabel inGameMenu = new UI.ImageLabel((w/4)+100, 130, 
                                content.Load<Texture2D>("menuImages/tutorialtipbox"));
            inGameMenu.resize(menuBackground.getWidth()-200,menuBackground.getHeight()-100);
            addComponent(inGameMenu);//add a panel to the image for button placement

            leftColumnMargin = inGameMenu.getPos().X +(buttonHeight/2);
            //rightColumnMargin = (inGameMenu.getPos().X + buttonHorizontalSpacing;
            middleColumnMargin = (inGameMenu.getPos().X + (inGameMenu.getWidth() / 2));
            //world = new gameWorld(w, h, content,_graphics,gameTime);
            //addComponent(world);
             //add the gameworld object to the display list
           
             


             //add interactive buttons to the game screen 
             //purchase/buy on the Left, Sell on the right. Play at the top. Save/Start/Quit on the bottom row

           
          
            UI.PushButton returnToPlayButton = new UI.PushButton(middleColumnMargin-
                            (buttonWidth/2), 115, 
                            content.Load<Texture2D>("MenuImages/blankbuttonGlow"), 
                            content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"), 
                                                                 "");
            returnToPlayButton.resize(buttonWidth, buttonHeight);
            addComponent(returnToPlayButton);
            returnToPlayButton.setClickEventHandler(returnToPlayClicked);

             //local variable to set button positions
            Point location_L = returnToPlayButton.getPos(); //keeps track of spacing on the Left side of the menu
             Point location_R = returnToPlayButton.getPos();//keeps track of spacing on the Right side of the menu

             //load the text onto the button so the size is appropriate for the button
             UI.TextLabel playText = new UI.TextLabel(location_L.X, location_L.Y, 
                                returnToPlayButton.getWidth(),
                                returnToPlayButton.getHeight(), "Resume Game", 0.3f, Color.White);
             addComponent(playText);

             //a button to hide the menu system when X is clicked
             UI.PushButton hideMenuButton = new UI.PushButton((inGameMenu.getPos().X + 
                                inGameMenu.getWidth()) - (buttonHeight), inGameMenu.getPos().Y 
                                - (buttonHeight / 2),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
             hideMenuButton.resize(buttonHeight,buttonHeight);
             //addComponent(hideMenuButton);
             hideMenuButton.setClickEventHandler(hideMenuClicked);
             location_R = hideMenuButton.getPos();
             UI.TextLabel hideMenuText = new UI.TextLabel(location_R.X, location_R.Y, 
                                hideMenuButton.getWidth(), hideMenuButton.getHeight(), 
                                "X", 0.3f, Color.White);
            // addComponent(hideMenuText); remove this button

            
             //v2.0 speckled rooster addition
            //UI.PushButton buySpeckledRoosterButton = new UI.PushButton(menuBackground.getWidth() 
            //              / 2 - (buttonHorizontalSpacing), menuBackground.getHeight() + (panelGap * 2),
            //              content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
            //              content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
            //              "Buy Rooster");
            //buySpeckledRoosterButton.resize(buttonWidth, buttonHeight);
            //addComponent(buySpeckledRoosterButton);
            //buySpeckledRoosterButton.setClickEventHandler(buySpeckledRoosterClicked);
             UI.PushButton eatEggButton = new UI.PushButton(leftColumnMargin, 
                                (location_R.Y + buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
             eatEggButton.resize(buttonWidth, buttonHeight);
             addComponent(eatEggButton);
             eatEggButton.setClickEventHandler(eatEggClicked);

             //local variable to set button positions
             location_L = eatEggButton.getPos(); //keeps track of spacing on the Left side of the menu
             

             //load the text onto the button so the size is appropriate for the button
             UI.TextLabel eatEggText = new UI.TextLabel(location_L.X, location_L.Y, 
                            eatEggButton.getWidth(),
                            eatEggButton.getHeight(), "Eat Egg", 0.3f, Color.White);
             addComponent(eatEggText);


           

            UI.PushButton sellEggsButton = new UI.PushButton(middleColumnMargin, 
                            (location_R.Y+buttonVerticalSpacing) ,
                             content.Load<Texture2D>("MenuImages/blankbuttonGlow"), 
                             content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"), 
                             "");
            sellEggsButton.resize(buttonWidth, buttonHeight);
            addComponent(sellEggsButton);
            sellEggsButton.setClickEventHandler(sellEggClicked);
            location_R = sellEggsButton.getPos();
            sellEggsText = new UI.TextLabel(location_R.X, location_R.Y, sellEggsButton.getWidth(),
                             sellEggsButton.getHeight(), "Sell Eggs $" + Convert.ToString(eggSell), 
                             0.3f, Color.White);
            addComponent(sellEggsText);

            UI.PushButton buyChickenButton = new UI.PushButton(leftColumnMargin, 
                            (location_R.Y + buttonVerticalSpacing),
                             content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                             content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                             "");
            buyChickenButton.resize(buttonWidth, buttonHeight);
            addComponent(buyChickenButton);
            buyChickenButton.setClickEventHandler(buyChickenClicked);

            location_L = buyChickenButton.getPos();

            UI.TextLabel buyChickenText = new UI.TextLabel(location_L.X, location_L.Y, 
                             buyChickenButton.getWidth(),
                             buyChickenButton.getHeight(), "Buy Chicken $" + 
                             Convert.ToString(chickenCost), 0.3f, Color.White);
            addComponent(buyChickenText);
            //v2.0 variety chicken colors for purchase
            //UI.PushButton buyRedChickenButton = new UI.PushButton(menuBackground.getWidth() 
            //               / 2 - (buttonHorizontalSpacing), menuBackground.getHeight() + (panelGap * 2),
            //               content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
            //               content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
            //               "Buy Chicken");
            //buyRedChickenButton.resize(buttonWidth, buttonHeight);
            //addComponent(buyRedChickenButton);
            //buyRedChickenButton.setClickEventHandler(buyRedChickenClicked);


            //UI.PushButton buyGoldChickenButton = new UI.PushButton(menuBackground.getWidth() 
            //               / 2 - (buttonHorizontalSpacing), menuBackground.getHeight() + (panelGap * 2),
            //               content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
            //               content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
            //               "Buy Chicken");
            //buyGoldChickenButton.resize(buttonWidth, buttonHeight);
            //addComponent(buyGoldChickenButton);
            //buyChickenButton.setClickEventHandler(buyGoldChickenClicked);

            UI.PushButton buyRoosterButton = new UI.PushButton(leftColumnMargin, 
                                (location_L.Y + buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            buyRoosterButton.resize(buttonWidth, buttonHeight);
            addComponent(buyRoosterButton);
            buyRoosterButton.setClickEventHandler(buyRoosterClicked);

            location_L = buyRoosterButton.getPos();
            UI.TextLabel buyRoosterText = new UI.TextLabel(location_L.X, location_L.Y, 
                                buyRoosterButton.getWidth(),
                                buyRoosterButton.getHeight(), "Buy Rooster $" + 
                                Convert.ToString(roosterCost), 0.3f, Color.White);
            addComponent(buyRoosterText); 

            UI.PushButton sellChickenButton = new UI.PushButton(middleColumnMargin, 
                                (location_R.Y+buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            sellChickenButton.resize(buttonWidth, buttonHeight);
            addComponent(sellChickenButton);
            sellChickenButton.setClickEventHandler(sellChickenClicked);
            location_R = sellChickenButton.getPos();
            UI.TextLabel sellChickenText = new UI.TextLabel(location_R.X, location_R.Y, 
                                sellChickenButton.getWidth(),
                                sellChickenButton.getHeight(), "Sell Chicken $" + 
                                Convert.ToString(chickenSell), 0.3f, Color.White);
            addComponent(sellChickenText); 

            UI.PushButton sellRoosterButton = new UI.PushButton(middleColumnMargin, 
                                (location_R.Y+buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            sellRoosterButton.resize(buttonWidth, buttonHeight);
            addComponent(sellRoosterButton);
            sellRoosterButton.setClickEventHandler(sellRoosterClicked);
            location_R = sellRoosterButton.getPos();
            UI.TextLabel sellRoosterText = new UI.TextLabel(location_R.X, location_R.Y, 
                                sellRoosterButton.getWidth(),
                                sellRoosterButton.getHeight(), "Sell Rooster $" + 
                                Convert.ToString(roosterSell), 0.3f, Color.White);
            addComponent(sellRoosterText); 

           

            UI.PushButton saveGameButton = new UI.PushButton(leftColumnMargin,
                                (location_R.Y+buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            saveGameButton.resize(buttonWidth, buttonHeight);
            //addComponent(saveGameButton);
            saveGameButton.setClickEventHandler(saveClicked);
            location_R = saveGameButton.getPos();
            UI.TextLabel saveGameText = new UI.TextLabel(location_R.X, location_R.Y, 
                                saveGameButton.getWidth(),
                                saveGameButton.getHeight(), "Save Game", 0.3f, Color.White);
            //addComponent(saveGameText); 


            UI.PushButton returnToStartButton = new UI.PushButton(middleColumnMargin, location_R.Y,
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            returnToStartButton.resize(buttonWidth, buttonHeight);
            //addComponent(returnToStartButton);
            returnToStartButton.setClickEventHandler(returnToStartClicked);

            location_R = returnToStartButton.getPos();

            UI.TextLabel returnToStartText = new UI.TextLabel(location_R.X, location_R.Y, 
                                returnToStartButton.getWidth(),
                                returnToStartButton.getHeight(), "Return To Start", 0.3f, 
                                Color.White);
           // addComponent(returnToStartText);

            UI.PushButton quitGameButton = new UI.PushButton(middleColumnMargin - 
                                (buttonWidth / 2), (location_R.Y+buttonVerticalSpacing),
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                "");
            quitGameButton.resize(buttonWidth, buttonHeight);
            addComponent(quitGameButton);
            quitGameButton.setClickEventHandler(quitClicked);
            location_R = quitGameButton.getPos();
            UI.TextLabel quitGameText = new UI.TextLabel(location_R.X, location_R.Y, 
                                quitGameButton.getWidth(),
                                quitGameButton.getHeight(), "Save / Quit Game", 0.3f, 
                                Color.White);
            addComponent(quitGameText); 
            //statPanel = new StatsPanel(w - panelWidth - panelGap, h - panelHeight - panelGap, content);
            //addComponent(statPanel);
            pausedText = new UI.TextLabel(menuBackground.getPos().X+ 
                                (menuBackground.getWidth()/2)-150, menuBackground.getPos().Y - 100, 
                                menuBackground.getWidth() / 2, 100, "***Game Paused***", Color.Red);
            addComponent(pausedText);

            
        }
         public void hideMenuClicked()
         {
             this.visible = false;
             gameWorld.instance.isPaused = false;
             //make the menu either go invisible or close
             //Game1.instance.setGameState(Game1.GameState.gameInterface);

         }
       
        public void returnToPlayClicked()
        {
            //Game1.instance.setGameState(Game1.GameState.game);//return to game, close menu, no game interface
            GameUI.gameWorld.instance.isPaused = false;
            this.visible = false;
            updateEggPrice(gameWorld.instance.player.eggSell);
           
        }
        public void returnToStartClicked()
        {
            Game1.instance.setGameState(Game1.GameState.start);//return to game, close menu
            this.visible = false;
            //reinitialize world
            //warning that this button will re-start the game without saving
           
        }
        public void eatEggClicked() // E or assigned xbox to also control this button
        {
            gameWorld.instance.player.eatEgg(ref gameWorld.instance.myCharacter.hitPoints, 
                                gameWorld.instance.myCharacter.maxHP, 
                                ref gameWorld.instance.eggCount);
            gameInterface.instance.updateStats();
        }
        public void sellEggClicked()
        {
            gameWorld.instance.player.sellEgg(ref gameWorld.instance.eggCount, 
                                gameWorld.instance.endOfDay);
            gameInterface.instance.updateStats();
            //code to decrement # of eggs collected, increase total cash
            
        }
        public void sellChickenClicked()
        {

            //code to decrement # of chickens displayed, increase total cash, remove a chicken from list
            gameWorld.instance.player.sellChicken( ref gameWorld.instance.numChic, 
                                ref gameWorld.instance.chickenList);
            gameInterface.instance.updateStats();

        }
        public void sellRoosterClicked()
        {
            //code to decrement # roosters, increase total cash, remove rooster from list. 
            //check to ensure it is not the last rooster. Player can not sell their last rooster
            gameWorld.instance.player.sellRooster(ref gameWorld.instance.numRooster, 
                                ref gameWorld.instance.roosterList);
            gameInterface.instance.updateStats();
        }
        public void buyChickenClicked()
        {
            gameWorld.instance.player.buyChicken(gameWorld.instance.maxChicken, 
                                ref gameWorld.instance.numChic, ref gameWorld.instance.chickenQueue);
            gameInterface.instance.updateStats();
            //code to increase # of chickens displayed, decrement total cash, add a chicken to list
            //check to ensure player has adequate cash for purchase
        }
        //v2.0 variety chicken colors
        //public void buyRedChickenClicked()
        //{
        //    //code to increase # of chickens displayed, decrement total cash, add a red chicken to list
        //    //check to ensure player has adequate cash for purchase
        //}
        //public void buyGoldChickenClicked()
        //{
        //    //code to increase # of chickens displayed, decrement total cash, add a gold chicken to list
        //    //check to ensure player has adequate cash for purchase
        //}
        public void buyRoosterClicked()
        {
            //code to increase # roosters, decrement total cash, add rooster from list. 
            //check to ensure player has adequate cash for purchase
            gameWorld.instance.player.buyRooster(gameWorld.instance.maxRooster, 
                                ref gameWorld.instance.numRooster,
                                ref gameWorld.instance.roosterQueue);
            gameInterface.instance.updateStats();
        }
        //v2.0 speckled rooster option
        //public void buySpeckledRoosterClicked()  
        //{
        //    //code to increase # roosters, decrement total cash, add speckled rooster to list. 
        //    //check to ensure player has adequate cash for purchase

        //}
        public void saveClicked()
        {
            Game1.instance.saveGame();
            //code to save character stat variables. (# of chickens,# of roosters,boy/girl, health,$$)

        }
        public void quitClicked()//save and quit button
        {
           //save game and go back to start menu
            Game1.instance.setGameState(Game1.GameState.start);
            GameUI.gameWorld.instance.isPaused = true;
            Game1.instance.saveGame();
            if (Chicken.AudioManager.instance.backgroundInstance != null)
            {
                Chicken.AudioManager.instance.stopBackgroundSound();
                Chicken.AudioManager.instance.backgroundInstance = 
                            Chicken.AudioManager.instance.backgroundTitle.CreateInstance();
                Chicken.AudioManager.instance.backgroundInstance.Play();
            }
            if (Chicken.AudioManager.instance.menuSFXInstance != null)
            {
                Chicken.AudioManager.instance.menuSFXInstance.Stop();
            }
            this.visible = false;
               // Game1.instance.Exit();

        }
        public void updateEggPrice(int eggPrice)
        {
            sellEggsText.changeText("Sell Eggs $" + Convert.ToString(eggPrice));
        }
      
        
        //public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    //base.Draw(gameTime, spriteBatch);
        //    //world.checkWinLossStatus();//check for win/loss

        //}
    }
    
}

    

