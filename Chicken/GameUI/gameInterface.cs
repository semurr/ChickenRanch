using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


//class that take care of all the interface design while playing the game
namespace Chicken.GameUI
{
    class gameInterface:UI.Container
    {
        //main barn panel for user interface
        private const int panelWidth = 1280;//1280Screen
        private const int panelHeight = 120;//720Screen
        private const int panelGap = 5;
        private const int buttonSpacing = 90;
        private inGameMenu menuPanel;
        public EndDaySummary summaryPanel;//change to private later
        private sunPanel sunPanel;
        public VictoryLossScreen winlossPanel;
        //private GameTime gameTime;
       // private SunPanel sunPanel; create a panel that holds animation for a sun/moon 2d graphics
        private int eggNumber;  //how many eggs have been collected
        private int brokenEggNumber; //how many eggs have been wasted/broken
        private int chickenNumber; //how many chickens you own
        private int roosterNumber;//how many roosters you own
        private int farmUpkeep; //the daily cost to feed your chickens
        private int totalMoney;//total money after selling eggs
        private int hitPoints; //character health
        private int daytimePast;//how many days/hours played
        public int goalAchievedT;//financial goal or time goal, how close to goal
        public int goalAchievedM;
        private int leftColumnMargin, middleLeftColumnMargin,middleRightColumnMargin, 
                            rightColumnMargin, verticalSpacing,textLabelWidth;
        private int buttonWidth = 120;
        private int buttonHeight = 30;
        public Texture2D characterPicture;
        private UI.ImageLabel characterImage;
        public UI.ImageLabel foxWarning;
        public int foxWarningCounter = 0;
        public bool wasWarned = false;
        //---------------------------------------------------------------------
        //variables to hold interface text
        UI.TextLabel characterHealthText, chickenNumberText, roosterNumberText, 
                            eggNumberText, moneyText, sunText, goalText,isEquippedText;
        public static gameInterface instance;
        public bool bootIsEquipped = false;
        
       
       
       
        //button for interface, causes the in-game menu to display 
       // private UI.PushButton menuButton;
       //button to hide this interface will be in the game-world. showInterface/hideInterface  
        //this will allow players to remove the interface if it is in their view of eggs
         

        public gameInterface(int w, int h, ContentManager content,
                            GraphicsDeviceManager _graphics):base(0,0,w,h)
        {
            instance = this;
            //updateStats();

            characterPicture = content.Load<Texture2D>("menuImages/farmerBoyImage");
        
             
             //add an additional panel to the gameInterface that holds the in-game Menu features
            menuPanel = new inGameMenu(w , h , content,_graphics);
            addComponent(menuPanel);

            //add an additional panel that displays the end of day summary
            summaryPanel = new EndDaySummary(w, h, content, _graphics);
            addComponent(summaryPanel);

            //winlossPanel = new VictoryLossScreen(w, h, content);
            //addComponent(winlossPanel);

            

            //initialize default summary page to timed goal intro
            //summaryPanel.setSummaryState(EndDaySummary.SummaryState.timedGoal);

            //add an additional panel that displays tutorial tips

             //add an additional panel to the gameInterface that displays a sun 
                            //graphic to show time/day passing
            sunPanel = new sunPanel(w, h, content, _graphics);
            addComponent(sunPanel);
            
             //add the panels for the screen interfaces
            UI.ImageLabel gameInterfaceBackground = new UI.ImageLabel
                                (0, _graphics.PreferredBackBufferHeight-panelHeight, 
                                content.Load<Texture2D>("menuImages/gameInterfacePanel"));
            gameInterfaceBackground.resize(_graphics.PreferredBackBufferWidth, panelHeight);
            addComponent(gameInterfaceBackground);//dark panel

             //variables to set alignment positions for panel components
            verticalSpacing = gameInterfaceBackground.getPos().Y+70;
            leftColumnMargin = gameInterfaceBackground.getPos().X;
            middleLeftColumnMargin = gameInterfaceBackground.getPos().X + 
                                ((gameInterfaceBackground.getWidth()/2) -350);
            middleRightColumnMargin = gameInterfaceBackground.getPos().X + 
                                ((gameInterfaceBackground.getWidth()/2) -50);
            rightColumnMargin = gameInterfaceBackground.getPos().X + (gameInterfaceBackground.getWidth());
            textLabelWidth = 150;





             //image Labels for the interface Panel, will each have an associated txt label to display values
            characterImage = new UI.ImageLabel(leftColumnMargin + 10, verticalSpacing -30,
                                characterPicture);
            characterImage.resize(50,50);
            addComponent(characterImage);

            Point location = characterImage.getPos(); //keeps track of where to set the text Label

             isEquippedText = new UI.TextLabel(location.X + characterImage.getWidth(), location.Y+30, 
                                textLabelWidth, characterImage.getHeight(), "", 0.3f, Color.Yellow);
            addComponent(isEquippedText);
            //load the text next to the appropriate image
            characterHealthText = new UI.TextLabel(location.X + characterImage.getWidth(), 
                                location.Y, textLabelWidth, characterImage.getHeight(),
                                "Health "+Convert.ToString(hitPoints)+" / " + 
                                Convert.ToString(gameWorld.instance.myCharacter.maxHP), 
                                0.3f, Color.White);
            addComponent(characterHealthText);


            UI.ImageLabel chickenImage = new UI.ImageLabel(middleLeftColumnMargin, 
                                verticalSpacing, content.Load<Texture2D>("menuImages/chickenImage"));
            chickenImage.resize(40,40);
            addComponent(chickenImage);

            location = chickenImage.getPos();

            
            chickenNumberText = new UI.TextLabel(location.X + characterImage.getWidth(), 
                                location.Y, textLabelWidth, chickenImage.getHeight(),
                                "Chickens " + Convert.ToString(chickenNumber) + " / " + 
                                Convert.ToString(gameWorld.instance.maxChicken),
                                0.3f, Color.White);
            addComponent(chickenNumberText);

            UI.ImageLabel roosterImage = new UI.ImageLabel(middleLeftColumnMargin, 
                                verticalSpacing - 50, content.Load<Texture2D>
                                ("menuImages/RoosterImage"));
            roosterImage.resize(40,40);
            addComponent(roosterImage);

            location = roosterImage.getPos();


            roosterNumberText = new UI.TextLabel(location.X + characterImage.getWidth(), 
                                location.Y, textLabelWidth, chickenImage.getHeight(),
                                "Roosters " + Convert.ToString(roosterNumber) + " / " + 
                                Convert.ToString(gameWorld.instance.maxRooster),
                                0.3f, Color.White);
            addComponent(roosterNumberText);

            UI.ImageLabel eggImage = new UI.ImageLabel(middleRightColumnMargin, 
                                verticalSpacing -50, content.Load<Texture2D>
                                ("menuImages/eggImage"));
            eggImage.resize(30,30);
            addComponent(eggImage);

            location = eggImage.getPos();


            eggNumberText = new UI.TextLabel(location.X + characterImage.getWidth(), 
                                location.Y, textLabelWidth, chickenImage.getHeight(),
                                "Eggs " + Convert.ToString(eggNumber) + " / Broken " + 
                                Convert.ToString(brokenEggNumber), 0.3f, Color.White);
            addComponent(eggNumberText);

            UI.ImageLabel moneyImage = new UI.ImageLabel(middleRightColumnMargin, 
                                verticalSpacing, content.Load<Texture2D>
                                ("menuImages/dollarSignImage"));
            moneyImage.resize(40,40);
            addComponent(moneyImage);

            location = moneyImage.getPos();


            moneyText = new UI.TextLabel(location.X + characterImage.getWidth(), 
                                location.Y, textLabelWidth, chickenImage.getHeight(),
                                "  $ " + Convert.ToString(totalMoney) + " / Upkeep " + 
                                Convert.ToString(farmUpkeep), 0.3f, Color.White);
            addComponent(moneyText);

            UI.ImageLabel sunImage = new UI.ImageLabel(rightColumnMargin - 400, 
                                verticalSpacing - 50, content.Load<Texture2D>
                                ("menuImages/sunIcon"));
            sunImage.resize(40, 40);
            addComponent(sunImage);
            location = sunImage.getPos();


            sunText = new UI.TextLabel(location.X + characterImage.getWidth(), location.Y, 
                                textLabelWidth, chickenImage.getHeight(), "Day light " + 
                                Convert.ToString(daytimePast) + " / " + Convert.ToString
                                (gameWorld.instance.endDay), 0.3f, Color.White);
            addComponent(sunText);

            UI.ImageLabel goalImage = new UI.ImageLabel(rightColumnMargin -400, verticalSpacing, 
                                content.Load<Texture2D>("menuImages/goalImage"));
            goalImage.resize(40, 40);
            addComponent(goalImage);

            location = goalImage.getPos();


            goalText = new UI.TextLabel(location.X + characterImage.getWidth(), location.Y, 
                                textLabelWidth, chickenImage.getHeight(), "Goal Timer " + 
                                Convert.ToString(goalAchievedT) + " / " + Convert.ToString
                                (gameWorld.instance.winLoss.timeWin), 0.3f, Color.White);
            addComponent(goalText);
            
             


            //add interactive buttons to the game screen 
            UI.PushButton inGameMenuButton = new UI.PushButton(rightColumnMargin - 
                                (buttonWidth)-20, verticalSpacing - 
                                gameInterfaceBackground.getHeight()/2,
                                content.Load<Texture2D>("MenuImages/blankbuttonGlow"), 
                                content.Load<Texture2D>("MenuImages/blankbuttonDarkGlow"), 
                                "");
            inGameMenuButton.resize(buttonWidth, buttonHeight);
            addComponent(inGameMenuButton);
            inGameMenuButton.setClickEventHandler(inGameMenuClicked);

             //local variable to set button positions
            Point location_L = inGameMenuButton.getPos(); //keeps track of spacing on the Left side of the menu
            Point location_R = inGameMenuButton.getPos();//keeps track of spacing on the Right side of the menu

             //load the text onto the button so the size is appropriate for the button 
             //activate this menu with M or assigned xbox control
            UI.TextLabel inGameMenuText = new UI.TextLabel(location_L.X - 10, location_L.Y, 
                                inGameMenuButton.getWidth(), inGameMenuButton.getHeight(), 
                                "  Menu<M>", 0.3f, Color.White);
             addComponent(inGameMenuText);

             foxWarning = new UI.ImageLabel(_graphics.PreferredBackBufferWidth/2-50, _graphics.PreferredBackBufferHeight/10
                                            , content.Load<Texture2D>("menuImages/foxWarning"));
             foxWarning.resize(100, 100);
             addComponent(foxWarning);
             foxWarning.visible = false;
            

            
        }
       
        public void inGameMenuClicked()
        {
            if (Chicken.AudioManager.instance.menuSFXInstance != null)
            {
                Chicken.AudioManager.instance.menuSFXInstance.Stop();
            }

            //open and close the in-game menu
            if (menuPanel.visible != true)
            {
                Chicken.AudioManager.instance.menuSFXInstance = 
                                    Chicken.AudioManager.instance.chickenNoises.CreateInstance();
                if (Chicken.AudioManager.instance.sFXOn == true && 
                                    Chicken.AudioManager.instance.menuSFXInstance.State != 
                                    SoundState.Playing)
                {
                    Chicken.AudioManager.instance.menuSFXInstance.Play();
                }
                menuPanel.visible = true;
                gameWorld.instance.isPaused = true;
            }
            else
            {
                //chicken noises stop
                if (Chicken.AudioManager.instance.menuSFXInstance != null)
                {
                    Chicken.AudioManager.instance.menuSFXInstance.Stop();
                }
                menuPanel.visible = false;
                gameWorld.instance.isPaused = false;
               
            }
            
           
        }
        public void eatEggClicked() // E or assigned xbox to also control this button
        {
            gameWorld.instance.player.eatEgg(ref gameWorld.instance.myCharacter.hitPoints, 
                                gameWorld.instance.myCharacter.maxHP, ref gameWorld.instance.eggCount);
            updateStats();
        }
        public void updateStats()
        {
            //re-assign the text variables
            eggNumber = gameWorld.instance.eggCount;  //how many eggs have been collected
            brokenEggNumber = gameWorld.instance.numBrokeEgg;
            chickenNumber = gameWorld.instance.numChic; //how many chickens you own
            roosterNumber = gameWorld.instance.numRooster;//how many roosters you own
            //the daily cost to feed your chickens
            farmUpkeep = (gameWorld.instance.player.feedPerChicken * gameWorld.instance.numChic) + 
                                (gameWorld.instance.player.feedPerRooster * gameWorld.instance.numRooster); 
            totalMoney = gameWorld.instance.player.money;//total money after selling eggs
            hitPoints = gameWorld.instance.myCharacter.hitPoints; //character health
            daytimePast = (int)(gameWorld.instance.dayTime);//how many days/hours played

            // determines what this is set to: either X/30 days or X/$$ earned
            goalAchievedT = gameWorld.instance.timedGoalCurrent;//financial goal or time goal, how close to goal
            goalAchievedM = totalMoney;


            characterImage.changeImage(characterPicture);
            characterHealthText.changeText("Health "+Convert.ToString(hitPoints)+" / " + 
                                Convert.ToString(gameWorld.instance.myCharacter.maxHP));
            chickenNumberText.changeText("Chickens " + Convert.ToString(chickenNumber) + " / " + 
                                Convert.ToString(gameWorld.instance.maxChicken));
            eggNumberText.changeText("Eggs " + Convert.ToString(eggNumber) + " / Broken " + 
                                Convert.ToString(brokenEggNumber));
            moneyText.changeText("  $ " + Convert.ToString(totalMoney) + " / Upkeep " + 
                                Convert.ToString(farmUpkeep));
            sunText.changeText("Day light " + Convert.ToString(daytimePast) + " / " + 
                                Convert.ToString(gameWorld.instance.endDay));
            roosterNumberText.changeText("Roosters " + Convert.ToString(roosterNumber) + " / " + 
                                Convert.ToString(gameWorld.instance.maxRooster));
            if (VictoryConditionClass.instance.gameMode == 0)//money mode
            {
                goalText.changeText("Goal $" + Convert.ToString(goalAchievedM) + " / " + 
                                Convert.ToString(gameWorld.instance.winLoss.moneyWin));
            }
            if (VictoryConditionClass.instance.gameMode == 1)//time mode
            {
                goalText.changeText("Goal Time " + Convert.ToString(goalAchievedT) + " / " + 
                                Convert.ToString(gameWorld.instance.winLoss.timeWin));
            }
                

            if (bootIsEquipped == true)
            {
                isEquippedText.changeText("Boots Equipped");
                
            }
            else
            {
                isEquippedText.changeText("");

            }

            
        }


        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            updateStats();
            base.draw(gameTime, spriteBatch);
        }
        //public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    //base.Draw(gameTime, spriteBatch);
        //    //world.checkWinLossStatus();//check for win/loss

        //}
    }
    
}
