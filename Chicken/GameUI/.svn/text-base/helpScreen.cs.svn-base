using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

//class to display tutorial and help tips
namespace Chicken.GameUI
{
    class helpScreen : UI.Container
    {
        private UI.TextLabel gameInfo; //use to display game information
        public string description ="";
        private int page = 1;
        private int maxPages = 3;
        private Point location;
        public UI.PushButton playButton;
        public UI.TextLabel playText;
        //public bool gameIsStarted = false;
        //private ArrayList helpInfo;

        public helpScreen(int w, int h, ContentManager content)
            : base(0, 0, w, h)
        {
            UI.ImageLabel helpScreen = new UI.ImageLabel(0, 0, content.Load<Texture2D>
                                ("menuImages/barnSidewithGlow"));
            helpScreen.resize(rect.Width, rect.Height);
            addComponent(helpScreen);

            gameInfo = new UI.TextLabel((rect.Width / 2)-175, (rect.Height / 2), 
                                helpScreen.getWidth() / 3, (int)(helpScreen.getHeight() * 0.10), 
                                "", 0.3f, Color.White);
            addComponent(gameInfo);

            displayHelp();

            //UI.TextLabel title2 = new UI.TextLabel((rect.Width / 2)-175, rect.Height / 3, 
                                //helpScreen.getWidth() / 3, (int)(helpScreen.getHeight() * 0.10), 
                                //"Tutorial", 1.0f, Color.White);
            //addComponent(title2);

            Texture2D buttonUp = content.Load<Texture2D>("menuImages/blankbuttonGlow");
            Texture2D buttonDown = content.Load<Texture2D>("menuImages/blankButtonDarkGlow");
            //Texture2D previousUp = content.Load<Texture2D>("menu/previousUp");
            //Texture2D previousDown = content.Load<Texture2D>("menu/previousDown");
            //Texture2D nextUp = content.Load<Texture2D>("menu/nextUp");
            //Texture2D nextDown = content.Load<Texture2D>("menu/nextDown");
            location = helpScreen.getPos();
            int verticalSpacing = 100;
            
            UI.ImageLabel menuTitle = (new UI.ImageLabel(location.X + (verticalSpacing*4), 
                                    location.Y-(verticalSpacing/2), 
                                    content.Load<Texture2D>("menuImages/M_helptitle")));
            menuTitle.scale(1);
            addComponent(menuTitle);

            UI.PushButton returnButton = new UI.PushButton((w / 2) - 150, (h / 2 + 275), 
                                    buttonUp, buttonDown, "");
           
            addComponent(returnButton);
            returnButton.setClickEventHandler(onButtonClicked);

            location = returnButton.getPos();
            //load the start font onto the button
            UI.TextLabel returnText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, 
                                    buttonUp.Height, "Return To Start", 0.4f, Color.White);
            
            addComponent(returnText);
            //arrow buttons to navigate the menu until character input is recognized
            Texture2D arrowUpLeft = content.Load<Texture2D>("menuImages/arrowLeft");
            Texture2D arrowDownLeft = content.Load<Texture2D>("menuImages/arrowLeftDown");
            Texture2D arrowUpRight = content.Load<Texture2D>("menuImages/arrowRight");
            Texture2D arrowDownRight = content.Load<Texture2D>("menuImages/arrowRightDown");

            
            UI.PushButton next = new UI.PushButton(location.X + arrowUpRight.Width + 30, 
                                    location.Y+6, arrowUpRight, arrowDownRight, "");
            // Next scroll through text pages
            next.resize(40, 40);
            addComponent(next);
            next.setClickEventHandler(onNextClicked);

            UI.PushButton previous = new UI.PushButton(location.X - 70, location.Y + 6, 
                                    arrowUpLeft, arrowDownLeft, ""); 
            //Previous scroll through text pages
            previous.resize(40, 40);
            addComponent(previous);
            previous.setClickEventHandler(onPreviousClicked);


            //playButton = new UI.PushButton((w / 2) - 150, (h / 2 - 350), buttonUp, buttonDown, "");

            //addComponent(playButton);
            //returnButton.setClickEventHandler(playClicked);

            //location = playButton.getPos();
            ////load the start font onto the button
            //playText = new UI.TextLabel(location.X, location.Y, buttonUp.Width, buttonUp.Height, 
                                        //"Resume Game", 0.4f, Color.White);

            //addComponent(playText);

            //playButton.visible = gameIsStarted;
            //playText.visible = gameIsStarted;

           
            //gameInfo = new UI.TextLabel(0, 200, w, 50, "Click Next to Begin Instructions", Color.Beige);
            //addComponent(gameInfo);
        }
        public void updateHelpButtons(bool gameStatus)
        {
            playButton.visible = gameStatus;
            playText.visible = gameStatus;
        }
        public void onNextClicked()
        {
            if (page < maxPages)
                page++;
            else page = 1;
            //change the text displayed when next is clicked. It loops around to the start if end is reached.
            displayHelp();
        }
        public void onPreviousClicked()
        {
            if (page > 1)
                page--;
            else page = maxPages;
            displayHelp();
        }
        public void keyInputTimer()
        {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    if (page < maxPages)
                        page++;
                    else page = 1;
                    //change the text displayed when next is clicked. It loops around to the start 
                                                //if end is reached.
                    displayHelp();
                }
        }
        private void displayHelp()
        {
            switch (page)
            {
                case 1:
                    //basic game info for gameplay
                    gameInfo.Text = "How to play: \nChicken Rancher will require plenty of hand-eye " 
                                                +"coordination, multi-tasking, and resource management. "
                                                +"\nTo Play, simply catch eggs falling from the "
                                                +"rafters while avoiding roosters and keeping the fox "
                                                +"\naway from your poultry. "

                                                +"\n\nYou must keep chickens happy and laying eggs. "
                                                +"This means you need at least one rooster for your "
                                                +"\nchickens to feel secure enough to lay eggs. Keep "
                                                +"in mind there is an upkeep cost per day to keep "
                                                +"\nyour chickens well fed. "
                                                
                                                +"\n\nYou can sell eggs to pay for the purchase of "
                                                +"more chickens or roosters, or eat the eggs to "
                                                +"\nrecover your hitpoints. "
                                                
                                                +"\n\nYou have two gameplay mode options to choose from. "
                                                +"\n\nThe choices are:"
                                                +"\nBank Payoff, which means you must win the game by "
                                                +"earning enough money to pay the bank morgage. "
                                                +"\nTimed Goal, which means you must prove you can keep "
                                                +"the farm afloat for a set mount of time."
                                                
                                                +"\n\nYou will lose the game if you allow your health to "
                                                +"reach zero, or you run out of funds to pay"
                                                +"\nfor your daily upkeep. Keep yourself healthy and don't "
                                                +"starve your poultry! "
                                                +"\n\n\n\n\n";
                    break;
                case 2:
                    //helpful tips
                    gameInfo.Text = "Some helpful tips to remember:"
                                    +"\nThe fox is hungry and will eat any chicken or rooster he can reach on"
                                    +"\nthe ground. To keep him at bay simply run towards him until you scare"
                                    +"\nhim out of the chicken yard. Don't forget, he will try to sneak back in"
                                    +"\nwhen you are not looking!"
                                    
                                    +"\n\nRoosters are grouchy fellows and will peck you if you get too close."
                                    +"\nUnfortunately, they keep your hens happy, and the more roosters there "
                                    +"\nare the more eggs your girls will lay."
                                    
                                    +"\n\nYour farm upkeep will go up for each chicken or rooster you purchase."
                                    +"\nRoosters will also eat twice as much feed as your chickens so it is up "
                                    +"\nto you to balance this cost versus the increase in eggs they create."
                                    
                                    +"\n\nEggs that are not caught will break when they reach the ground. These"
                                    +"\nbroken eggs are slippery! Obtaining the rubber boots will keep you from"
                                    +"\nslipping while you are wearing them."
                                    
                                    +"\n\nEggs collected can be sold or eaten at any time. However, they will be"
                                    +"\n worth a higher price if you wait until the end of the day when they can"
                                    +"\nbe carted off to the farmers market."
                                    + "\n\n\n\n\n";
                    break;
                case 3:
                   // game interaction, controls
                    gameInfo.Text = "Some Basics:"
                                     +"\nTo move the camera:"
                                     +"\nKeyboard Control-"
                                     +"\n<A> moves the camera away from your character giving a wider "
                                     +"view of the farmyard. "
                                     +"\n<D> moves the camera closer to the character."
                                     +"\n<W> moves the camera higher above the ground for top-down feel "
                                     +"of the farmyard. "
                                     +"\n<S> moves the camera closer to the ground."
                                     +"\nXbox Control-"
                                     +"\nCamera movement is controlled on the right joystick of the Xbox "
                                     +"controller."
                                     +"\n\nTo move the character:"
                                     +"\nKeyboard Control-"
                                     +"\nThe up and down arrows move the character forward and backward "
                                     +"in the direction he/she is facing."
                                     +"\nThe left right arrows change the direction the character is facing."
                                     +"\nXbox Control-"
                                     +"\nCharacter movement is controlled by the left joystick."
                                     +"\n\nTo navigate the menu system:"
                                     +"\nMouse Input can be used to navigate the menu system. Left click "
                                     +"to press a button."
                                     +"\nIf an Xbox controller is detected, the yellow triangle can be "
                                     +"moved using the left joystick."
                                     +"\nButtons are clicked by pressing the <A> button."
                                     +"\n Keyboard Hotkeys -"
                                     +"\n<E> to eat eggs, <M> to open the resource/pause menu while playing. "
                                     +"\nXbox Hotkeys -"
                                     +"\n<Y> to eat eggs, <start> to bring up the resource/pause menu "
                                     +"while playing, <back> to exit the game."
                                     + "\n\n\n\n\n";
                    break;
               
            }
        }

        public void onButtonClicked()
        {
            Game1.instance.setGameState(Game1.GameState.start); //return to the start screen
        }
        public void playClicked()
        {
            Game1.instance.setGameState(Game1.GameState.game); //return to the game
        }
    }
}

