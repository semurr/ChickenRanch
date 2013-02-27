using System;
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

namespace Chicken
{
    class VictoryConditionClass
    {
        //data for win and loss conditions
        public int gameMode;  // 0 = money   1 = time/days
        public int moneyWin = 500; //1000
        public  int timeWin = 30;//30 days
        int timeCustom;//what is this variable?
        public static VictoryConditionClass instance;


        public VictoryConditionClass()
        {
            instance = this;
            gameMode = 1; //default is Money conditation
        }

        //standard
        public void initializeVictory()
        {
            //set data to win loss
            gameMode = 1;
            //moneyWin = 200;//1000
            //timeWin = 2;//30 days
            timeCustom = 10;
        }

        //custom
        public void initializeVictory(int Mode)
        {
            //set data to win loss
            gameMode = Mode;
            //moneyWin = 200;//1000
            //timeWin = 2;//5 or 30 days
            timeCustom = 10;
        }

        public bool checkVictory(int money, int time)
        {
            if (gameMode == 0) //win by money
            {
                if (money >= moneyWin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else  //win by time
            {
                if (time >= timeWin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
