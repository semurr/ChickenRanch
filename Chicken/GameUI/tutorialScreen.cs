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

namespace Chicken.GameUI
{
    class tutorialScreen:UI.Container
    {
        public static tutorialScreen instance;
        private const int panelWidth = 1280;//1280Screen
        private const int panelHeight = 80;//720Screen
        private const int panelGap = 5;
        private const int buttonSpacing = 90;
        UI.TextLabel tutorialText;
        UI.ImageLabel tutorialBoxBackground;
        UI.ImageLabel tutorialBoxHeader;
        UI.TextLabel tutorialHeaderText;
        int graphicsH, graphicsW;
        public string tutorialHeader = "Howdy, farmer! "
                                  +"\nThis tutorial is to let you learn"
                                  +"\nhow to properly run your ranch";

        public string tutorialInfo = "";
        public inGameMenu menuPanelTutorial;

         public tutorialScreen(int w, int h, ContentManager content,
                            GraphicsDeviceManager _graphics):base(0,0,w,h)
        {
            instance = this;
            graphicsH = _graphics.PreferredBackBufferHeight;
            graphicsW = _graphics.PreferredBackBufferWidth;
            //add the panels for the screen interfaces
            UI.ImageLabel tutorialBackground = new UI.ImageLabel
                                (0, _graphics.PreferredBackBufferHeight - panelHeight,
                                content.Load<Texture2D>("menuImages/gameInterfacePanel"));
            tutorialBackground.resize(_graphics.PreferredBackBufferWidth, panelHeight);
            addComponent(tutorialBackground);//panel across the bottom that says tutorial

             Point location = tutorialBackground.getPos();

             UI.ImageLabel tutorialTitle = new UI.ImageLabel(location.X, location.Y, content.Load<Texture2D>("menuImages/tutorialLeveltext"));
             addComponent(tutorialTitle);

             //tutorial header
             tutorialBoxHeader = new UI.ImageLabel((_graphics.PreferredBackBufferWidth / 4)+20, 20, content.Load<Texture2D>("menuImages/tutorialtipbox"));
             //tutorialBoxBackground.resize(panelWidth / 4, panelWidth / 3);
             tutorialBoxHeader.resize(panelWidth / 4, panelWidth / 15);
             addComponent(tutorialBoxHeader);

             location = tutorialBoxHeader.getPos();

             tutorialHeaderText = new UI.TextLabel(location.X, location.Y, tutorialBoxHeader.getWidth(), tutorialBoxHeader.getHeight(),tutorialHeader, Color.White);
             tutorialHeaderText.changeFontScale(0.3f);
             addComponent(tutorialHeaderText);

             //UI.TextLabel tutorialTitle = new UI.TextLabel(location.X, location.Y+20, 1280, 20, "Tutorial Level", Color.White);
             //tutorialTitle.changeFontScale(1);
             //addComponent(tutorialTitle);
             tutorialBoxBackground = new UI.ImageLabel(20, 20,content.Load<Texture2D>("menuImages/tutorialtipbox"));
             //tutorialBoxBackground.resize(panelWidth / 4, panelWidth / 3);
             tutorialBoxBackground.resize(panelWidth / 4, panelWidth / 8);
             addComponent(tutorialBoxBackground);

             location = tutorialBoxBackground.getPos();

            tutorialText = new UI.TextLabel(location.X,location.Y,tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight(),tutorialInfo, Color.White);
            tutorialText.changeFontScale(0.3f);
             addComponent(tutorialText);

             menuPanelTutorial = new inGameMenu(w, h, content, _graphics);
             addComponent(menuPanelTutorial);
          

        }
         public void updateTutorialBox(int _stage,int _phase)
         {
             //public string tutorialInfo = "Howdy, farmer! "
             //                     +"\nThis tutorial is to let you "
             //                     +"\nlearn how to properly run "
             //                     +"\nyour ranch. Press <A> on the "
             //                     +"\nXbox controller or the "
             //                     +"\n<space bar> on your keyboard"
             //                     +"\nwhen you are ready to move on.";

             switch (_stage)
             {
                 case 0: // stage 0 character movement
                     tutorialInfo = "CHARACTER MOVEMENT"
                                + "\nFirst thing you need to know"
                                + "\nis how to move your character."
                                + "\nIf you are using the Xbox "
                                + "\ncontroller, you will push the"
                                + "\nleft joystick up and down to "
                                + "\nmove your character forward "
                                + "\nand backward. Pushing the left"
                                + "\njoystick right and left will "
                                + "\nchange the direction your "
                                + "\ncharacter is facing. If you"
                                + "\nare using a keyboard, you "
                                + "\nwill use the arrow keys to"
                                + "\ncontrol your movement. Up "
                                + "\nand Down will move your "
                                + "\ncharacter forward and "
                                + "\nbackward, while right and "
                                + "\nleft will change your "
                                + "\ndirection. Test this out. "
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar'"
                                + "\non your keyboard when you "
                                + "\nare ready to move on.";

                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 2)-60);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = true;
                     tutorialHeaderText.visible = true;
                     
                     break;
                 case 1: // stage 1 camera movement
                     tutorialInfo = "CAMERA MOVEMENT"
                                 +"\nThe next thing you need to"
                                 +"\nknow is how to adjust your "
                                 +"\ncamera. If you are using the"
                                 +"\nXbox controller, you will use"
                                 +"\nthe right joystick to control "
                                 +"\nyour camera. Up and down will "
                                 +"\nmove your camera further away "
                                 +"\nor closer to the ground. "
                                 +"\nRight and left will bring your"
                                 +"\nfurther away or closer to your"
                                 +"\ncharacter. If you are using the"
                                 +"\nkeyboard, the 'W' will move the"
                                 +"\ncamera away from the ground. "
                                 + "\n'S' will move it closer to ."
                                 + "\nthe ground. 'A' will move "
                                 + "\nthe camera away from the "
                                 +"\ncharacter. 'D' will move it "
                                 +"\ncloser to the character. "
                                 +"\nTest this out."
                                 + "\n\nPress 'A' on the"
                                 +"\nXbox controller or the "
                                 + "\n'space bar' on your keyboard  "
                                 + "\nwhen you are ready to move on.";
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 2) - 40);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 2: // stage 2 introduce chicken into the farm
                     tutorialInfo = "THE CHICKEN"
                                + "\nHere comes your chicken. "
                                + "\nShe will run around on the "
                                + "\nrafters and the ground. She"
                                + "\nwill lay eggs when she is on "
                                + "\nthe rafters. However, she only"
                                + "\nlays them when she is happy."
                                + "\nTo keep her happy, you need to"
                                + "\nhave at least one rooster and"
                                + "\nyou need to feed her every day."
                                + "\nThe more roosters you have, "
                                + "\nthe happier the chickens will"
                                + "\n be and the more eggs they "
                                +"\nwill lay."
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                +"\nready to move on.";
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3)+30);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 3: // stage 3 introduce rooster, non chasing
                     if (_phase == 1)//first set of text 3a
                     {
                         tutorialInfo = "THE ROOSTER"
                                    + "\nHere comes a rooster. He does"
                                    + "\nnot like to invade the chicken's"
                                    + "\nterritory, so he will only walk "
                                    + "\non the ground and not the "
                                    + "\nrafters."
                                    + "\n\nPress 'A' on the Xbox "
                                    + "\ncontroller or the 'space bar' on "
                                    + "\nyour keyboard when you are"
                                    + "\nready to move on.";
                         tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 5)+30);
                         tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     }
                     if (_phase == 2)//second set of text, 3b
                     {
                         tutorialInfo = "LAYING EGGS"
                                   + "\nNow that you have a chicken"
                                   + "\nand a rooster, your chicken"
                                   + "\nis happy and wants to lay eggs."
                                   + "\nWhen a chicken is ready to lay"
                                   + "\nan egg, she will stop moving "
                                   + "\nfor a little while to prepare "
                                   + "\nherself for laying the egg. When"
                                   + "\nshe is done preparing, she will "
                                   + "\nmake a loud noise to let you "
                                   + "\nknow that she has laid an egg  "
                                   + "\nand she will wander off. "
                                   +"\n\nPress 'A' on the Xbox "
                                   + "\ncontroller or the 'space bar' on "
                                   + "\nyour keyboard when you are"
                                   + "\nready to move on.";
                         tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3));
                         tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     }
                    
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 4: // stage 4 catch a falling egg
                     tutorialInfo = "CATCHING EGGS"
                                + "\nOnce the chicken has laid an "
                                + "\negg, an egg will fall from the"
                                + "\nrafters where the chicken was. "
                                + "\nA shadow will appear on the "
                                + "\nground to let you know where "
                                + "\nto stand to collect it. You "
                                + "\nmust catch it before it hits"
                                + "\nthe ground. Collect an egg. "
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                + "\nready to move on."; 
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3) - 60);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 5: // stage 5 broken egg fall
                     tutorialInfo = "BROKEN EGGS"
                                + "\nIf the chicken lays an egg and"
                                + "\nyou cannot catch it before it "
                                + "\nhits the ground, the egg will "
                                + "\nsplatter. Splattered eggs are "
                                + "\nslippery. If you were to step "
                                + "\non one, you will slide until "
                                + "\nyou are no longer on the egg. "
                                + "\nYou can change the direction "
                                + "\nyou are sliding. However, you "
                                + "\nwill be unable to stop your "
                                + "\nmomentum until you aren't on "
                                + "\nthe egg anymore. Let an egg "
                                + "\nsplatter and step on it. "
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                + "\nready to move on.";
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3)+30);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 6: // boots walk over broken eggs
                     tutorialInfo = "BOOTS & SLIPPERY EGGS"
                                + "\nThe only way to not slide on the"
                                + "\nsplattered egg is to grab a pair"
                                + "\nof rubber boots. They will "
                                + "\nappear randomly while you are "
                                + "\nin the farmyard. If you pick "
                                + "\nup a pair  of boots, you will "
                                + "\nsee a 'Boots Equipped' sign "
                                + "\nappear on the left side of the "
                                + "\nstatus bar at the bottom of  "
                                + "\nyour screen. While  you have  "
                                + "\nthe boots on, you will be able "
                                + "\nto move normally over  broken "
                                + "\neggs. however, they  will only "
                                + "\nlast for a little while, so "
                                + "\nkeep an eye on them. Pick up "
                                + "\na pair of boots and run over "
                                +"\na splattered egg."
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on"
                                + "\nyour keyboard when you are"
                                + "\nready to move on."; 
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 2) - 70);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 7: //rooster attack character
                     tutorialInfo = "DANGEROUS ROOSTERS"
                                + "\nA farmyard can be a  "
                                + "\ndangerous place. Roosters"
                                + "\n are very  territorial  "
                                + "\nand they do not like  "
                                + "\nfarmers messing with  "
                                + "\ntheir chickens. If you get  "
                                + "\nclose to a rooster, it will"
                                + "\nstart chasing you. If it "
                                + "\ncatches you, it will attack "
                                + "\n you and your hit points  "
                                + "\nwill go down. You cannot   "
                                + "\nhurt the rooster because  "
                                + "\nthen your chickens will be "
                                + "\n unhappy, so you have to "
                                +"\nevade them."
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                + "\nready to move on.";
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3) +90);
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 8: // eat eggs to replenish life
                     tutorialInfo = "REPLENISH LIFE"
                                + "\nIf your character loses hit "
                                + "\npoints, you want to replenish"
                                + "\nthem before you run out. You "
                                + "\nheal yourself by eating eggs. "
                                + "\nTo eat an egg, press 'E' if you"
                                + "\nare using the keyboard or 'Y' "
                                + "\nif you are using the Xbox "
                                + "\ncontroller. You can also eat an"
                                + "\negg from inside the Resource "
                                + "\nMenu. This will be explained  "
                                + "\nlater on. Eat an egg. "
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                + "\nready to move on.";
                     tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3) );
                     tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 9: // fox introduced and attack chicken and rooster
                     if (_phase == 1)
                     {
                         tutorialInfo = "THE FOX"
                                + "\nFoxes are hungry and will eat"
                                + "\nyour livestock if you are not"
                                + "\ncareful. You can drive the fox"
                                + "\naway by getting close to it. "
                                + "\nThe fox never truly leaves, he "
                                + "\nmerely bides his time at the "
                                + "\nedge of the farm, waiting for "
                                + "\na chance to get another meal. "
                                + "\n\nPress 'A' on the Xbox "
                                + "\ncontroller or the 'space bar' on "
                                + "\nyour keyboard when you are"
                                + "\nready to move on.";
                         tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3)-70);
                         tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     }
                     if (_phase == 2)
                     {
                         tutorialInfo = "REPLACING YOUR POULTRY"
                                    + "\nIf the fox eats your chicken "
                                    + "\nor rooster, and you need to "
                                    + "\nget a new one, open your menu "
                                    + "\nby pushing 'M' on the keyboard "
                                    + "\nor 'Start' on the Xbox "
                                    + "\ncontroller. Open your menu. "
                                    + "\n\nPress 'A' on the Xbox "
                                    + "\ncontroller or the 'space bar' on "
                                    + "\nyour keyboard when you are"
                                    + "\nready to move on.";
                         tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 4)-10 );
                         tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                     }
                     tutorialBoxHeader.visible = false;
                     tutorialHeaderText.visible = false;
                     break;
                 case 10: //resource menu
                     if (_phase == 1)
                     {
                         tutorialInfo = "RESOURCE MENU"
                                    + "\nInside your resource menu the"
                                    + "\ngame is paused. From this "
                                    + "\nmenu you can buy or sell  "
                                    + "\nchickens and roosters,  "
                                    + "\nsell or eat eggs, or save and"
                                    + "\nexit the game. You cannot sell"
                                    + "\nyour last chicken or rooster  "
                                    + "\nand you cannot buy own more "
                                    + "\nthan 10 chickens and 5 roosters"
                                    + "\nat a time. Buy a rooster and "
                                    + "\nclick 'Continue' to resume  "
                                    + "\nplaying your game."
                                    + "\n\nPress 'A' on the Xbox "
                                    + "\ncontroller or the 'space bar' on"
                                    + "\nyour keyboard when you are"
                                    + "\nready to move on.";
                         tutorialBoxBackground.resize(panelWidth / 4, (panelWidth / 3)+10 );
                         tutorialText.resize(tutorialBoxBackground.getWidth(), tutorialBoxBackground.getHeight());
                         tutorialBoxHeader.visible = false;
                         tutorialHeaderText.visible = false;
                     }
                     if (_phase == 2)
                     {
                         tutorialHeader = "Congratulations farmer!"
                                      + "\nYou have now completed "
                                      + "\nthe tutorial level. Press"
                                      + "\n'A' on the Xbox controller "
                                      + "\nor the 'space bar' on your "
                                      + "\nkeyboard when you are ready "
                                      + "\nto begin a new game.";
                         tutorialBoxHeader.move((graphicsW / 2) - (tutorialBoxHeader.getWidth() / 2), graphicsH / 9);
                         tutorialHeaderText.move((graphicsW / 2) - (tutorialBoxHeader.getWidth() / 2), graphicsH / 9);
                          tutorialHeaderText.changeText(tutorialHeader);
                          tutorialBoxHeader.resize(panelWidth / 4, panelWidth / 7);
                          tutorialHeaderText.resize(tutorialBoxHeader.getWidth(), tutorialBoxHeader.getHeight());
                          tutorialBoxHeader.visible = true;//enable to smaller box
                          tutorialHeaderText.visible = true;
                          tutorialBoxBackground.visible = false;
                          tutorialText.visible = false;
                         
                     }
                     
                     break;
                 default: //error
                     break;
       
             }
             
             tutorialText.changeText(tutorialInfo);
         }
    }
}
