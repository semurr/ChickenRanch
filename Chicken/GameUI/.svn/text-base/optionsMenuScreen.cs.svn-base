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

    class optionsMenuScreen : UI.Container
    {
        int  columnLeft, verticalSpacing;
        float fontScale;
        public static optionsMenuScreen instance;
        //checked objects
        UI.CheckBox windowedChecked;
        UI.CheckBox fullScreenChecked;
        UI.CheckBox musicChecked;
        UI.CheckBox soundEffectsChecked;

        public optionsMenuScreen(int w, int h, ContentManager content) 
            : base(0, 0, w, h)
        {
            instance = this;
            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankbuttonDarkGlow");

            Texture2D checkedImage = content.Load<Texture2D>("menuImages/checkbox2");
            Texture2D unCheckedImage = content.Load<Texture2D>("menuImages/checkbox");

           

            Point location;

            UI.ImageLabel backGroundImage,panel1,menuTitle;

            backGroundImage = (new UI.ImageLabel(0, 0, content.Load<Texture2D>("menuImages/barnSidewithGlow")));
            //barnSidewithGlow/chickenCoopMenuPoster optional
            backGroundImage.resize(w, h);
            addComponent(backGroundImage);

            location = backGroundImage.getPos();
            columnLeft = (backGroundImage.getWidth()/4);
            //columnRight = location.X + (backGroundImage.getWidth() / 2+20);
            verticalSpacing = 100;
            fontScale = 0.5f;
            
            

            panel1 = (new UI.ImageLabel(columnLeft, backGroundImage.getPos().Y + 10,
                content.Load<Texture2D>("menuImages/tutorialtipbox")));
            panel1.resize((backGroundImage.getWidth()/2 ), backGroundImage.getHeight() - 50);
            addComponent(panel1);

            menuTitle = (new UI.ImageLabel(columnLeft-100, location.Y + (verticalSpacing/10), 
                content.Load<Texture2D>("menuImages/M_optionsmenutitle")));
            menuTitle.scale(1);
            addComponent(menuTitle);
           

           
           
           UI.PushButton startButton = new UI.PushButton(((backGroundImage.getWidth()/2)-(buttonUp.Width/2)),
               (backGroundImage.getHeight() - (buttonUp.Height*2)), buttonUp, buttonDown, "");//return to start
          
            addComponent(startButton);
           startButton.setClickEventHandler(startButtonClicked);
           Point startButtonlocation = startButton.getPos();
           ////load the start font onto the button
           UI.TextLabel startText = new UI.TextLabel(startButtonlocation.X, startButtonlocation.Y, startButton.getWidth(),
                                  startButton.getHeight(), "Return to Start", 0.5f, Color.White);
           addComponent(startText);

         //group 1 Screen Options
           ArrayList optionGroup1 = new ArrayList();
           windowedChecked = new UI.CheckBox(columnLeft+20, location.Y+(verticalSpacing*2), checkedImage, unCheckedImage,
               " Windowed Mode", true);
           windowedChecked.changeFontScale(fontScale);
           addComponent(windowedChecked);
           fullScreenChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 3), checkedImage,
               unCheckedImage, "Full Screen Mode", false);
           fullScreenChecked.changeFontScale(fontScale);
           addComponent(fullScreenChecked);

           optionGroup1.Add(windowedChecked);
           optionGroup1.Add(fullScreenChecked);
           windowedChecked.buttonGroup = optionGroup1;
           fullScreenChecked.buttonGroup = optionGroup1;

            

            //group 2 Audio Options
          // ArrayList optionGroup2 = new ArrayList();
           musicChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 4), checkedImage, unCheckedImage,
               "  Music ON/OFF", true);
           musicChecked.changeFontScale(fontScale);
           addComponent(musicChecked);
           soundEffectsChecked = new UI.CheckBox(columnLeft + 20, location.Y + (verticalSpacing * 5), checkedImage,
               unCheckedImage, "   SFX ON/OFF", true);
           soundEffectsChecked.changeFontScale(fontScale);
           addComponent(soundEffectsChecked);
           //--------------------------------------------------------------if we end up with 1 options menu-------------
           //optionGroup2.Add(musicChecked);
           //optionGroup2.Add(soundEffectsChecked);
           //musicChecked.buttonGroup = optionGroup2;
           //soundEffectsChecked.buttonGroup = optionGroup2;

            //group 3 character Options
           //ArrayList optionGroup3 = new ArrayList();
           //UI.CheckBox maleChecked = new UI.CheckBox(columnRight + 20, location.Y + (verticalSpacing ),
           //checkedImage, unCheckedImage, "Male Character", true);
           //maleChecked.changeFontScale(fontScale);
           //addComponent(maleChecked);
           //UI.CheckBox femaleChecked = new UI.CheckBox(columnRight + 20, location.Y + (verticalSpacing * 2),
           //checkedImage, unCheckedImage, "Female Character", false);
           //femaleChecked.changeFontScale(fontScale);
           //addComponent(femaleChecked);

           //optionGroup3.Add(maleChecked);
           //optionGroup3.Add(femaleChecked);
           //maleChecked.buttonGroup = optionGroup3;
           //femaleChecked.buttonGroup = optionGroup3;

           // //group 4 Goal/Victory Options
           //ArrayList optionGroup4 = new ArrayList();
           //UI.CheckBox timedChecked = new UI.CheckBox(columnRight + 20, location.Y + (verticalSpacing * 3), checkedImage,
           //unCheckedImage, "Timed Game Victory", true);
           //timedChecked.changeFontScale(fontScale);
           //addComponent(timedChecked);
           //UI.CheckBox monetaryChecked = new UI.CheckBox(columnRight + 20, location.Y + (verticalSpacing * 4), checkedImage,
           //unCheckedImage, "Bank Payoff Victory", false);
           //monetaryChecked.changeFontScale(fontScale);
           //addComponent(monetaryChecked);

           //optionGroup4.Add(timedChecked);
           //optionGroup4.Add(monetaryChecked);
           //timedChecked.buttonGroup = optionGroup4;
           //monetaryChecked.buttonGroup = optionGroup4;
           //-----------------------------------------------------------if we end up with 1 options menu-------------

      
          }
        public void optionsMenuUpdate()
        {
            if (musicChecked.isChecked)
            {
                Chicken.AudioManager.instance.resumeBackgroundSound();
               
            }
            else
            {
                Chicken.AudioManager.instance.pauseBackgroundSound();
            }
            if (soundEffectsChecked.isChecked)
            {
                Chicken.AudioManager.instance.sFXOn = true;
            }
            else
            {
                //turn off all sound effects
                Chicken.AudioManager.instance.sFXOn = false;
            }
        }

        public void startButtonClicked()//save settings
        {
            //add logic to determine option settings

            if (windowedChecked.isChecked)
            {
                Game1.instance.graphics.IsFullScreen = false;
            }
            if(fullScreenChecked.isChecked)
            {
                Game1.instance.chooseFullScreen = true;
            }

            if (musicChecked.isChecked)
            {
                Chicken.AudioManager.instance.musicOn = true;
            }
            else
            {
                Chicken.AudioManager.instance.musicOn = false;
                Chicken.AudioManager.instance.stopBackgroundSound();

            }
            if (soundEffectsChecked.isChecked)
            {
                Chicken.AudioManager.instance.sFXOn = true;
            }
            else
            {
                Chicken.AudioManager.instance.sFXOn = false;
            }
            Game1.instance.setGameState(Game1.GameState.start);//go back to start menu after selecting options
        }
    }
}


            //bool dynamic;
            //GameBoard.Difficulty difficulty;
            //if (dynamicChecked.isChecked)
            //    dynamic = true;
            //else
            //    dynamic = false;
            //if (easyChecked.isChecked)
            //    difficulty = GameBoard.Difficulty.easy;
            //else if (mediumChecked.isChecked)
            //    difficulty = GameBoard.Difficulty.medium;
            //else
            //    difficulty = GameBoard.Difficulty.hard;
            //GameBoard.instance.createMap(dynamic, difficulty);