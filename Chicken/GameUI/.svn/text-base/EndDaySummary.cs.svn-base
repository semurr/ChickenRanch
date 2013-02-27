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
    class EndDaySummary : UI.Container
    {

        public static EndDaySummary instance;
        //main barn panel for user interface
        //private const int panelWidth = 122;
        //private const int panelHeight = 204;
        private const int panelGap = 5;
        private const int buttonVerticalSpacing = 84;
        private const int buttonHorizontalSpacing = 80;
        private int leftColumnMargin, middleColumnMargin,rightColumnMargin;
        private int buttonWidth = 190;
        private int buttonHeight = 40;
        UI.TextLabel summaryTextItem, summaryTitle;
        public UI.TextLabel pausedText;
        private string summaryHeader = "Summary Title";
        private string summaryInfoOne = "Panel Summary";
        private string goal;
        public enum SummaryState {moneyGoal,timedGoal,endDaySummary};
        public SummaryState summaryState;
        bool timed = false;
        bool money = false;
        //private string summaryInfoTwo = "Amount";


        public EndDaySummary(int w, int h, ContentManager content, GraphicsDeviceManager _graphics)
            : base(0, 0, w, h)
        {

            instance = this;
            this.visible = false;
            //assign variables from the economics class
          

           
            UI.ImageLabel menuBackground = new UI.ImageLabel((w / 4), 80, content.Load<Texture2D>
                                                             ("menuImages/chickenCoopMenuPoster"));
            menuBackground.resize(rect.Width / 2, rect.Height - 200);
            addComponent(menuBackground);//add a background image for the menu system

            UI.ImageLabel inGameMenu = new UI.ImageLabel((w / 4) + 100, 130, content.Load<Texture2D>
                                        ("menuImages/tutorialtipbox"));
            inGameMenu.resize(menuBackground.getWidth() - 200, menuBackground.getHeight() - 100);
            addComponent(inGameMenu);//add a panel to the image for button placement

            leftColumnMargin = inGameMenu.getPos().X + 80;
            rightColumnMargin = inGameMenu.getPos().X + (inGameMenu.getWidth())-(buttonHorizontalSpacing*2);
            middleColumnMargin = (inGameMenu.getPos().X + (inGameMenu.getWidth() / 2));
            //world = new gameWorld(w, h, content,_graphics,gameTime);
            //addComponent(world);
            //add the gameworld object to the display list




            //add interactive buttons to the game screen 
            //purchase/buy on the Left, Sell on the right. Play at the top. Save/Start/Quit on the bottom row



            UI.PushButton returnToPlayButton = new UI.PushButton(middleColumnMargin - (buttonWidth / 2), 115,
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
            UI.TextLabel playText = new UI.TextLabel(location_L.X, location_L.Y, returnToPlayButton.getWidth(),
                                    returnToPlayButton.getHeight(), "Resume Game", 0.3f, Color.White);
            addComponent(playText);

            //a button to hide the menu system when X is clicked
            summaryTitle = new UI.TextLabel(middleColumnMargin, (location_R.Y + 55), 10, 10, summaryHeader, 
                                    0.35f, Color.White);
            addComponent(summaryTitle);
            summaryTextItem = new UI.TextLabel(leftColumnMargin+50, (location_R.Y + buttonVerticalSpacing), 
                                    100, 300, summaryInfoOne, 0.3f, Color.White);
            addComponent(summaryTextItem);
            //creates a seperate column for entering summary information
            //summaryTextAmount = new UI.TextLabel(rightColumnMargin, (location_R.Y + buttonVerticalSpacing), 
                                    //10, 10, summaryInfoTwo, 0.3f, Color.White);
            //addComponent(summaryTextAmount);


            UI.PushButton menuButton = new UI.PushButton(location_L.X, (location_L.Y + buttonVerticalSpacing*5),
                                     content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
                                     content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
                                     "");
            menuButton.resize(buttonWidth, buttonHeight);
            addComponent(menuButton);
            menuButton.setClickEventHandler(menuClicked);
            location_R = menuButton.getPos();
            UI.TextLabel menuText = new UI.TextLabel(location_R.X, location_R.Y, menuButton.getWidth(),
                                     menuButton.getHeight(), "Resource Menu", 0.3f, Color.White);
            addComponent(menuText); 



            pausedText = new UI.TextLabel(menuBackground.getPos().X + (menuBackground.getWidth() / 2) 
                                    - 150, menuBackground.getPos().Y - 100, menuBackground.getWidth() 
                                    / 2, 100, "***Game Paused***", Color.Red);
            addComponent(pausedText);

            setSummaryState(SummaryState.timedGoal);//default start summary is timed goal intro
            updateSummary();

            //---------------------DebugButton------------------------------------
            //UI.PushButton debugButton = new UI.PushButton(location_R.X-450, (location_L.Y + buttonVerticalSpacing*3),
            //                                              content.Load<Texture2D>("MenuImages/blankbuttonGlow"),
            //                                              content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"),
            //                                              "Debug");
           
            //addComponent(debugButton);
            //debugButton.setClickEventHandler(debugClicked);
            //---------------------DebugButton------------------------------------


        }

        //---------------------DebugClicked-----------------------------------
        //public void debugClicked()
        //{
        //    updateSummary();
        //}
        //---------------------DebugClicked-----------------------------------
        public void menuClicked()
        {
            this.visible = false;
            gameInterface.instance.inGameMenuClicked();
            //make the menu either go invisible or close
            //Game1.instance.setGameState(Game1.GameState.gameInterface);

        }

        public void returnToPlayClicked()
        {
            this.visible = false;
            GameUI.gameWorld.instance.isPaused = false;
            //Game1.instance.setGameState(Game1.GameState.game);//return to game, close menu, no game interface
           
            //if (summaryState == SummaryState.moneyGoal || summaryState == SummaryState.timedGoal)
            //{
            //    setSummaryState(SummaryState.endDaySummary);//change to endofdaysummary
            //    updateSummary();
            //}
           
            

        }
        public void setSummaryState(SummaryState _newState)
        {
            summaryState = _newState;
           
        }
        public void updateSummary()
        {
            
            switch (summaryState)
            {
                case SummaryState.moneyGoal://summaryInfoOne = moneyGoal Introduction, goal = money goal
                    summaryInfoOne = "              You are ready to prove your independence"
                                   + "\n             to your parents who run a thriving farm."
                                   + "\n             Your neighbor offered to sell a parcel of  "
                                   + "\n             his land for you to have as your own "
                                   + "\n             for $" + Convert.ToString(VictoryConditionClass.instance.moneyWin) + ". To help you out your parents"
                                   + "\n             made you a deal in which you were given a " 
                                   + "\n             plot small plot on their farm fenced off "
                                   + "\n             with a coop, one chicken and one rooster "
                                   + "\n             to get you started. All the assets you "
                                   + "\n             earn will be completely yours. This is  "
                                   + "\n             your chance to earn the money needed to "
                                   + "\n             buy the neighbors acres and start your "
                                   + "\n             independent life!";

                    summaryHeader = "Earn Money To Reach Your Goal: $" + 
                                   Convert.ToString(VictoryConditionClass.instance.moneyWin);
                    goal = "$ " + Convert.ToString(gameWorld.instance.player.moneyAquired) + "/" + 
                                   Convert.ToString(VictoryConditionClass.instance.moneyWin);
                    money = true;
                    break;
                case SummaryState.timedGoal://summaryInfoOne = timedGoal Introduction, goal = timed goal
                    summaryInfoOne = "               You are ready to prove your independence "
                                   + "\n               to your parents who run a thriving cattle "
                                   + "\n               ranch. They made you a deal in which you "
                                   + "\n               were given a plot of land fenced off with "
                                   + "\n               a coop, one chicken and one rooster to get "
                                   + "\n               you started. If you can prove you can "
                                   + "\n               manage the chicken Yard for " + Convert.ToString(VictoryConditionClass.instance.timeWin) + " days they will "
                                   + "\n               give you lucrative job managing the family's "
                                   + "\n               large ranch style farm. It's time to prove you "
                                   + "\n               are ready for their challenge!";
                    summaryHeader = "Prove You Can Run The Farm: " + 
                                    Convert.ToString(VictoryConditionClass.instance.timeWin)+" Days";
                    goal = "Days " + Convert.ToString(gameWorld.instance.timedGoalCurrent) + "/" + 
                                    Convert.ToString(VictoryConditionClass.instance.timeWin);
                    timed = true;
                    break;
                case SummaryState.endDaySummary://update end of day summary information
                    if (timed == true)
                    {
                        goal = "Days " + Convert.ToString(gameInterface.instance.goalAchievedT) + "/" + 
                                     Convert.ToString(VictoryConditionClass.instance.timeWin);
                    }
                    if(money == true)
                    {
                        goal = "$ " + Convert.ToString(gameInterface.instance.goalAchievedM) + "/" + 
                                     Convert.ToString(VictoryConditionClass.instance.moneyWin);
                    }
                    summaryInfoOne = "Eggs Collected: " + Convert.ToString(gameWorld.instance.player.eggsCollected)
                             + "\nEggs Eaten: " + Convert.ToString(gameWorld.instance.player.eggsEaten)
                             + "\nEggs Sold: " + Convert.ToString(gameWorld.instance.player.eggSold)
                             + "\nChickens Bought: " + Convert.ToString(gameWorld.instance.player.chickenBought)
                             + "\nChickens Sold: " + Convert.ToString(gameWorld.instance.player.chickenSold)
                             + "\nChickens Lost: " + Convert.ToString(gameWorld.instance.player.chickenEaten)
                             + "\nRoosters Bought: " + Convert.ToString(gameWorld.instance.player.roosterBought)
                             + "\nRoosters Sold: " + Convert.ToString(gameWorld.instance.player.roosterSold)
                             + "\nRoosters Lost: " + Convert.ToString(gameWorld.instance.player.roosterEaten)
                             + "\nMoney Earned: " + Convert.ToString(gameWorld.instance.player.moneyAquired)
                             + "\nGoal: " + goal;
                    summaryHeader = "Game Summary";
                    break;
                    
            }
            summaryTitle.changeText(summaryHeader);
            summaryTextItem.changeText(summaryInfoOne);
             
        }

    }
}
