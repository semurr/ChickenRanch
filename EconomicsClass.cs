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
    class EconomicsClass
    {
        public int feedPerChicken;
        public int feedPerRooster;
        public int money;
        public int chickenCost, roosterCost, chickenSell, roosterSell, eggSell, eggSellEnd,eggEatBonus;
        
        //values for end of day report start values
        public int startEggs;
        public int startChickens;
        public int startRoosters;
        public int startmoney;

        //values aquired throughout the day
        public int eggsCollected;
        public int chickenBought;
        public int roosterBought;
        public int eggsEaten;
        public int eggSold;
        public int chickenSold;
        public int roosterSold;
        public int chickenEaten;
        public int roosterEaten;
        public int moneyAquired;

        //intiilize all data
        public void InitializeEconomic()
        {
            //feed 10 per chicken 20 per rooster
            feedPerChicken = 10;
            feedPerRooster = 20;
            money = 200;
            //initialize prices for economics
            chickenCost = 100;
            roosterCost = 50;
            chickenSell = chickenCost / 2;
            roosterSell = roosterCost / 2;
            eggSell = 10;
            eggSellEnd = 15;
            eggEatBonus = 10;

            startEggs = 0;
            startChickens = 1;
            startRoosters = 1;
            startmoney = money;

            //values aquired throughout the day
            eggsCollected = 0;
            chickenBought = 0;
            roosterBought = 0;
            eggsEaten = 0;
            eggSold = 0;
            chickenSold = 0;
            roosterSold = 0;
            chickenEaten = 0;
            roosterEaten = 0;
            moneyAquired = 0;
        }

        //sell eggs during day
        public void sellEgg(ref int eggs, bool endofDay)
        {
            if (endofDay != true)
            {
                GameUI.inGameMenu.instance.updateEggPrice(eggSell);
                if (eggs != 0)
                {
                    money += eggSell;
                    moneyAquired += eggSell;
                    eggs--;
                    eggSold++;
                    
                }
            }
            else
            {
                GameUI.inGameMenu.instance.updateEggPrice(eggSellEnd);
                if (eggs != 0)
                {
                    money += eggSellEnd;
                    moneyAquired += eggSellEnd;
                    eggs--;
                    eggSold++;
                }
            }
            //end of day 10
        }


        //eat eggs
        public void eatEgg(ref int HP, int maxHP, ref int eggs)
        {
            if (eggs != 0 && HP < maxHP)
            {
                HP += eggEatBonus;
                eggs--;
                eggsEaten++;
            }
        }

        //buy chicken
        public void buyChicken(int maxChicken, ref int numChickens, ref int chickenQueue)
        {
            //make sure they have enough money
            if (money >= chickenCost)
            {
                //check for max chickens
                if (numChickens < maxChicken)
                {
                    chickenQueue++;
                    numChickens++;
                    chickenBought++;
                    money -= chickenCost;
                }
            }
        }

        //sell chicken
        public void sellChicken(ref int numChickens, ref ChickenClass[] chickenLst)
        {
            //check to make sure you have chickens
            if (numChickens > 1)
            {
                chickenLst[numChickens - 1] = null;
                numChickens--;
                chickenSell++;
                money += chickenSell;
                moneyAquired += chickenSell;
            }

        }

        //buy rooster
        public void buyRooster(int maxRooster, ref int numRoosters, ref int RoosterQueue)
        {
            //make sure they have enough money
            if (money >= roosterCost)
            {
                //check for max Roosters
                if (numRoosters < maxRooster)
                {
                    RoosterQueue++;
                    numRoosters++;
                    roosterBought++;
                    money -= roosterCost;

                }
            }
        }

        //sell rooster
        public void sellRooster(ref int numRoosters, ref RoosterClass[] roosterLst)
        {
            //check to make sure you have chickens
            if (numRoosters > 1)
            {
                roosterLst[numRoosters - 1] = null;
                numRoosters--;
                roosterSold++;
                money += roosterSell;
                moneyAquired += roosterSell;
            }
        }

        //end of day feed cost
        public void feed(int chickens, int roosters)
        {
            money -= (chickens * feedPerChicken) + (roosters * feedPerRooster);
        }

        //end of day summary update
        public void summaryUpdate()
        {
            startEggs += eggsCollected - eggsEaten - eggSold;
            startChickens += chickenBought - chickenSold - chickenEaten;
            startRoosters += roosterBought - roosterSold - roosterEaten;
            startmoney = money;

            //values aquired throughout the day
            eggsCollected = 0;
            chickenBought = 0;
            roosterBought = 0;
            eggsEaten = 0;
            chickenSold = 0;
            roosterSold = 0;
            chickenEaten = 0;
            roosterEaten = 0;
            moneyAquired = 0;

        }
    }
}
