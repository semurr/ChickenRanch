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
    class NewGameMenu: UI.Container
    {
        int columnRight, columnLeft, verticalSpacing;
        float fontScale;
        public static NewGameMenu instance;

        //checked objects
        //UI.CheckBox windowedChecked_NG;
        //UI.CheckBox fullScreenChecked_NG;
        //UI.CheckBox musicChecked_NG;
        //UI.CheckBox soundEffectsChecked_NG;
        UI.CheckBox maleChecked;
        UI.CheckBox femaleChecked;
        UI.CheckBox timedChecked;
        UI.CheckBox monetaryChecked;

        public NewGameMenu(int w, int h, ContentManager content) 
            : base(0, 0, w, h)
        {
            instance = this;
            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankbuttonDarkGlow");

            Texture2D checkedImage = content.Load<Texture2D>("menuImages/checkbox2");
            Texture2D unCheckedImage = content.Load<Texture2D>("menuImages/checkbox");

            Point location;

            UI.ImageLabel backGroundImage, panel1,menuTitle;
        
            backGroundImage= (new UI.ImageLabel(0, 0, content.Load<Texture2D>("menuImages/barnSidewithGlow")));
            backGroundImage.resize(w, h);
            addComponent(backGroundImage);

            location = backGroundImage.getPos();
            columnLeft = (backGroundImage.getWidth() / 4);
            columnRight = location.X + (backGroundImage.getWidth() / 2+20);
            verticalSpacing = 100;
            fontScale = 0.5f;



            panel1 = (new UI.ImageLabel(columnLeft, backGroundImage.getPos().Y + 10,
                content.Load<Texture2D>("menuImages/tutorialtipbox")));
            panel1.resize((backGroundImage.getWidth() / 2), backGroundImage.getHeight() - 50);
            addComponent(panel1);
            //panel2 = (new UI.ImageLabel(columnRight, backGroundImage.getPos().Y + 10,
            //content.Load<Texture2D>("menuImages/menuPanelTrans85")));
            //panel2.resize((backGroundImage.getWidth()/2) - 100, backGroundImage.getHeight() - 50);
            //addComponent(panel2);

            menuTitle = (new UI.ImageLabel(columnLeft - 100, location.Y + (verticalSpacing / 10),
                content.Load<Texture2D>("menuImages/M_newGameMenuTitle")));
            menuTitle.scale(1);
            addComponent(menuTitle);

            UI.PushButton startButton = new UI.PushButton(((backGroundImage.getWidth() / 3) - (buttonUp.Width/3)),
                (backGroundImage.getHeight() - (buttonUp.Height * 2)), buttonUp, buttonDown, "");//return to start
            startButton.resize(185, 65);
            addComponent(startButton);
            startButton.setClickEventHandler(startButtonClicked);
            Point buttonlocation = startButton.getPos();
            ////load the start font onto the button
            UI.TextLabel startText = new UI.TextLabel(buttonlocation.X, buttonlocation.Y, startButton.getWidth(),
                                   startButton.getHeight(), "Return", 0.4f, Color.White);
            addComponent(startText);

            //button to load the tutorial
            UI.PushButton tutorialButton = new UI.PushButton((buttonlocation.X+startButton.getWidth()+20),
                (backGroundImage.getHeight() - (buttonUp.Height * 2)), buttonUp, buttonDown, "");//return to start
            tutorialButton.resize(185, 65);
            addComponent(tutorialButton);
            tutorialButton.setClickEventHandler(tutorialButtonClicked);
            buttonlocation = tutorialButton.getPos();
            ////load the start font onto the button
            UI.TextLabel tutorialText = new UI.TextLabel(buttonlocation.X, buttonlocation.Y, startButton.getWidth(),
                                   startButton.getHeight(), "Tutorial", 0.4f, Color.White);
            addComponent(tutorialText);

            UI.PushButton playButton = new UI.PushButton((buttonlocation.X + tutorialButton.getWidth() + 20),
                (backGroundImage.getHeight() - (buttonUp.Height * 2)), buttonUp, buttonDown, "");//return to start
            playButton.resize(185, 65);
            addComponent(playButton);
            playButton.setClickEventHandler(playButtonClicked);
             buttonlocation = playButton.getPos();
            ////load the start font onto the button
            UI.TextLabel playText = new UI.TextLabel(buttonlocation.X, buttonlocation.Y, startButton.getWidth(),
                                   startButton.getHeight(), "Continue", 0.4f, Color.White);
            addComponent(playText);
//------------------------------------OptionsScreenOptions---------------------------------------------------------------
         ////group 1 Screen Options
         //  ArrayList optionGroup1 = new ArrayList();
         //  windowedChecked_NG = new UI.CheckBox(columnLeft + 20, location.Y + verticalSpacing, checkedImage,
         //  unCheckedImage, "Windowed", true);
         //  windowedChecked_NG.changeFontScale(fontScale);
         //  addComponent(windowedChecked_NG);
         //  fullScreenChecked_NG = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 2), checkedImage,
         //  unCheckedImage, "Full Screen", false);
         //  fullScreenChecked_NG.changeFontScale(fontScale);
         //  addComponent(fullScreenChecked_NG);

         //  optionGroup1.Add(windowedChecked_NG);
         //  optionGroup1.Add(fullScreenChecked_NG);
         //  windowedChecked_NG.buttonGroup = optionGroup1;
         //  fullScreenChecked_NG.buttonGroup = optionGroup1;

            

         //   //group 2 Audio Options
         //  ArrayList optionGroup2 = new ArrayList();
         //  musicChecked_NG = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 3), checkedImage,
         //  unCheckedImage, "Music ON/OFF", true);
         //  musicChecked_NG.changeFontScale(fontScale);
         //  addComponent(musicChecked_NG);
         //  soundEffectsChecked_NG = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 4), checkedImage,
         //  unCheckedImage, "SFX ON/OFF", true);
         //  soundEffectsChecked_NG.changeFontScale(fontScale);
         //  addComponent(soundEffectsChecked_NG);
//------------------------------------EndOptionsScreenOptions----------------------------------------------------------
           //optionGroup2.Add(musicChecked);
           //optionGroup2.Add(soundEffectsChecked);
           //musicChecked.buttonGroup = optionGroup2;
           //soundEffectsChecked.buttonGroup = optionGroup2;

            //group 3 character Options
           ArrayList optionGroup3 = new ArrayList();
           maleChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 2), checkedImage, 
               unCheckedImage, "   Male Character", true);
           maleChecked.changeFontScale(fontScale);
           addComponent(maleChecked);
           femaleChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 3), checkedImage,
               unCheckedImage, "  Female Character", false);
           femaleChecked.changeFontScale(fontScale);
           addComponent(femaleChecked);

           optionGroup3.Add(maleChecked);
           optionGroup3.Add(femaleChecked);
           maleChecked.buttonGroup = optionGroup3;
           femaleChecked.buttonGroup = optionGroup3;

            //group 4 Goal/Victory Options
           ArrayList optionGroup4 = new ArrayList();
           timedChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 4), checkedImage,
               unCheckedImage, "Timed Game Victory", true);
           timedChecked.changeFontScale(fontScale);
           addComponent(timedChecked);
           monetaryChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 5), checkedImage,
               unCheckedImage, " Bank Payoff Victory", false);
           monetaryChecked.changeFontScale(fontScale);
           addComponent(monetaryChecked);

           optionGroup4.Add(timedChecked);
           optionGroup4.Add(monetaryChecked);
           timedChecked.buttonGroup = optionGroup4;
           monetaryChecked.buttonGroup = optionGroup4;


          }
        //public void newGameMenuUpdate()
        //{
        //    if (musicChecked_NG.isChecked)
        //    {
        //        Chicken.AudioManager.instance.resumeBackgroundSound();
        //    }
        //    else
        //    {
        //        Chicken.AudioManager.instance.pauseBackgroundSound();
        //    }
        //    if (soundEffectsChecked_NG.isChecked)
        //    {
        //        Chicken.AudioManager.instance.sFXOn = true;
        //    }
        //    else
        //    {
        //        //turn off all sound effects
        //        Chicken.AudioManager.instance.sFXOn = false;
        //    }
        //}
        public void tutorialButtonClicked()
        {
            //turn on tutorial
            //initialize tutorial
            //turn on tutorial tip boxes
            Game1.instance.tutLevel.initilizeTutorialLevel();
            Game1.instance.tutorial = true;
           // GameUI.tutorialScreen.instance.updateTutorialBox(10,2);//debugging
            Game1.instance.setGameState(Game1.GameState.tutorial);

            
        }
        public void startButtonClicked()//save settings
        {
            gameWorld.instance.initializeWorld();
            //add logic to determine option settings
            //---------------OptionsMenuOptions------------ //can remove this code if desired
            //if (windowedChecked_NG.isChecked)
            //{
            //    Game1.instance.graphics.IsFullScreen = false;
            //}
            //else
            //{
            //    Game1.instance.graphics.IsFullScreen = true;
            //}
            //if (musicChecked_NG.isChecked)
            //{
            //    Chicken.AudioManager.instance.musicOn = true;
            //}
            //else
            //{
            //    Chicken.AudioManager.instance.musicOn = false;
            //}
            //if (soundEffectsChecked_NG.isChecked)
            //{
            //    Chicken.AudioManager.instance.sFXOn = true;
            //}
            //else
            //{
            //    Chicken.AudioManager.instance.sFXOn = false;
            //}
            //-----------OptionsMenuOptions--------------------

            //-----------CharacterOptions-----------------------
            if (maleChecked.isChecked)
            {
                gameWorld.instance.myCharacter.InitializeCharacter("Models\\farmBoy[final2]");
                gameInterface.instance.characterPicture = Game1.instance.Content.Load<Texture2D>("MenuImages\\farmerBoyImage");
                gameInterface.instance.updateStats();
            }
            else if (femaleChecked.isChecked)
            {
                gameWorld.instance.myCharacter.InitializeCharacter("Models\\farmGirl[1]");
                gameInterface.instance.characterPicture = Game1.instance.Content.Load<Texture2D>("MenuImages\\girlImage");
                gameInterface.instance.updateStats();
            }
            else
            {
                //default
            }
            //----------VictoryConditionOptions-----------------
            if (timedChecked.isChecked)
            {
                gameWorld.instance.winLoss.initializeVictory(1);
                EndDaySummary.instance.summaryState = EndDaySummary.SummaryState.timedGoal;
            }
            if (monetaryChecked.isChecked)
            {
                gameWorld.instance.winLoss.initializeVictory(0);
                EndDaySummary.instance.summaryState = EndDaySummary.SummaryState.moneyGoal;
            }
            Game1.instance.loadGame();
            Game1.instance.setGameState(Game1.GameState.start);//go back to start menu after selecting options
        }
        public void playButtonClicked()//save settings
        {
            gameWorld.instance.initializeWorld();
            gameInterface.instance.visible = true;
            //Game1.instance.loadGame();
            //add logic to determine option settings
            //---------------OptionsMenuOptions------------ //can remove this code if desired
            //if (windowedChecked_NG.isChecked)
            //{
            //    Game1.instance.graphics.IsFullScreen = false;
            //}
            //else
            //{
            //    Game1.instance.graphics.IsFullScreen = true;
            //}
            //if (musicChecked_NG.isChecked)
            //{
            //    Chicken.AudioManager.instance.musicOn = true;
            //}
            //else
            //{
            //    Chicken.AudioManager.instance.musicOn = false;
            //}
            //if (soundEffectsChecked_NG.isChecked)
            //{
            //    Chicken.AudioManager.instance.sFXOn = true;
            //}
            //else
            //{
            //    Chicken.AudioManager.instance.sFXOn = false;
            //}
            //-----------OptionsMenuOptions--------------------

            //-----------CharacterOptions-----------------------
            if (maleChecked.isChecked)
            {
                gameWorld.instance.myCharacter.InitializeCharacter("Models\\farmBoy[final2]");
                gameInterface.instance.characterPicture = Game1.instance.Content.Load<Texture2D>("MenuImages\\farmerBoyImage");
                gameInterface.instance.updateStats();
            }
            else if (femaleChecked.isChecked)
            {
                gameWorld.instance.myCharacter.InitializeCharacter("Models\\farmGirl[1]");
                gameInterface.instance.characterPicture = Game1.instance.Content.Load<Texture2D>("MenuImages\\girlImage");
                gameInterface.instance.updateStats();
            }
            else
            {
                //default
            }
            //----------VictoryConditionOptions-----------------
            if (timedChecked.isChecked)
            {
                gameWorld.instance.winLoss.initializeVictory(1);
                EndDaySummary.instance.setSummaryState(EndDaySummary.SummaryState.timedGoal);
                EndDaySummary.instance.updateSummary();
            }
            if (monetaryChecked.isChecked)
            {
                gameWorld.instance.winLoss.initializeVictory(0);
                EndDaySummary.instance.setSummaryState(EndDaySummary.SummaryState.moneyGoal);
                EndDaySummary.instance.updateSummary();
            }
            Game1.instance.setGameState(Game1.GameState.game);//go to start of game
            gameInterface.instance.summaryPanel.visible = true;
            gameWorld.instance.isPaused = true;
        }
    }
}
    

