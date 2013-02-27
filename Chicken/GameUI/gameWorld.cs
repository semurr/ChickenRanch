using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;

//class that loads all of the game world, object, animations
namespace Chicken.GameUI
{
    class gameWorld
    {
        //game world variables
        public static gameWorld instance;
        public bool isPaused = true;
        private Model myScene;
        private Model mySkyBox;

        private float aspectRatio;
        GraphicsDeviceManager graphics;
        ContentManager content;
        public VictoryConditionClass winLoss;

        //camera variables
        public Matrix cameraViewMatrix;
        public Matrix cameraProjectionMatrix;
        public Vector3 cameraLookAt = new Vector3(3500, 0, 3500);
        public Vector3 cameraPosition = new Vector3(3500.0f, 450.0f, 6000.0f); //initial camera position
        public float camRearOffset = 1000.0f; //default distance between camera and character
        public float camHeightOffset = 800f; //default distance between camera and ground
        public float maxCamHeight = 2000f;
        public float minCamHeight = 200f;
        public float maxCamOffset = 3000f;
        public float minCamOffset = 200f;

        //character variables
        public CharacterClass myCharacter;

        //rooster variables
        public RoosterClass[] roosterList = new RoosterClass[5];
        public int numRooster = 1; //initial number of roosters
        public int maxRooster = 5; //max number of roosters
        float roosterTime = 0; //tracks number of seconds since last rooster spawned
        float roostQueueTime = 7; //time between rooster spawning
        public int roosterQueue = 0; //number of roosters waiting to be spawned

        //chicken variables
        public ChickenClass[] chickenList = new ChickenClass[10];
        public int numChic = 1; //initial number of chickens
        public int maxChicken = 10; //max number of chickens
        float chickenTime = 0f; //tracks number of seconds since last chicken spawned
        float chicQueueTime = 7f; //time between chicken spawning
        public int chickenQueue = 0; //number of chickens waiting to be spawned

        //fox variables
        public FoxClass myFox;

        //egg variables
        public EggClass[] eggList = new EggClass[100];
        public int eggCount = 0; //number of eggs caught by Character
        public int numEgg = 0; //says if egg is laid

        //egg shadow variables
        EggShadowClass[] shadowList = new EggShadowClass[100];
        //public int numShadow = 0; //number of egg shadows present

        //broken egg variables
        BrokenEggClass[] brokenEggList = new BrokenEggClass[100];
        public int numBrokeEgg = 0; //number of broken eggs on ground
        public float brokenEggDisappear = 30.0f;  // time for broken egg to disappear
        public float brokenEggTime = 0.0f;        //timer for broken egg

        //timer variables
        public int days = 0;           //tracks number of total days
        public float dayTime = 0; //tracks number of seconds since day began
        public float endDay = 500; //day ends when dayTime equals endDay
        public int timedGoalEnd = 30;
        public int timedGoalCurrent = 0;
        public bool endOfDay = false;
        //public bool tutorial = false;

        //economics variables
        public EconomicsClass player;

        //boot variables
        public RubberBootClass[] myBootList = new RubberBootClass[2];
        public int numBoots = 0;
        public float bootTime = 0.0f; //tracks time since last boot spawned
        float bootWaitTime = 10.0f; //time between spawns (when bootSpawnTime equals bootWaitTime add boots)
        float bootDeleteTime = 30.0f; //time that boot will disappear
        public bool bootPresent = false; //shows if there is a boot spawned
        public bool bootsEquipped = false; // shows if player has picked up boots
        float equippedTime = 0.0f; //tracks time since boots were Equipped
        float maxEquipTime = 30.0f; //time boots are able to be used for

        //hotkey
        public bool buttonCheck = false;
        public bool buttonCheck2 = false;

        public GamePadState gamePadState;


        //constructor
        public gameWorld(ContentManager _content, GraphicsDeviceManager _graphics)
        {
            //takes in a content manager plus the designated widtch and height of the game - optional
            instance = this;
            graphics = _graphics;
            content = _content;

            myScene = content.Load<Model>("Models\\farmYardNoSky[3]");
           // mySkyBox = content.Load<Model>("Models\\skybox[4]");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

        }
        public void initializeWorld()
        {

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;


            // pausedText.changeFontSize(100);

            this.graphics.IsFullScreen = Game1.instance.chooseFullScreen;
            graphics.ApplyChanges();


            cameraViewMatrix = Matrix.CreateLookAt(
                cameraPosition,
                cameraLookAt,
                Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                10000.0f);

            // TODO: Add your initialization logic here
            //create rooster
            roosterList[0] = new RoosterClass(content, graphics);
            roosterList[0].InitializeRooster(13);

            //create multiple chickens
            //initialize chickens
            chickenList[0] = new ChickenClass(content, graphics);
            chickenList[0].InitializeChicken(0);

            //on button press/chicken buy selection create another chicken object
            //add graphics, and initialize then add to chickenlist

            //initialize character
            myCharacter = new CharacterClass(content, graphics);
            myCharacter.InitializeCharacter();

            //initialize broken egg
            brokenEggList[0] = new BrokenEggClass(content, graphics);
            brokenEggList[0].initializeBrokenEgg();

            //initialize egg
            eggList[0] = new EggClass(content, graphics);
            eggList[0].initializeEgg();

            //initialize egg shadow
            shadowList[0] = new EggShadowClass(content, graphics);
            shadowList[0].initializeEggShadow();

            //initilize fox
            myFox = new FoxClass(content, graphics);
            myFox.initializeFox(new Vector3(3600, 80, 3600), numChic);

            //economics
            player = new EconomicsClass();
            player.InitializeEconomic();

            //boots
            myBootList[0] = new RubberBootClass(content, graphics);
            myBootList[0].initializeBoots();

            //win/loss
            winLoss = new VictoryConditionClass();
            winLoss.initializeVictory();




            //initilize all data
            isPaused = true;

            //camera
            cameraPosition = new Vector3(3500.0f, 450.0f, 6000.0f); //initial camera position
            camRearOffset = 1000.0f; //default distance between camera and character
            camHeightOffset = 800f; //default distance between camera and ground
            maxCamHeight = 2000f;
            minCamHeight = 200f;
            maxCamOffset = 3000f;
            minCamOffset = 200f;

            //roosters
            numRooster = 1; //initial number of roosters
            maxRooster = 5; //max number of roosters
            roosterTime = 0; //tracks number of seconds since last rooster spawned
            roostQueueTime = 7; //time between rooster spawning
            roosterQueue = 0;

            //chicken
            numChic = 1; //initial number of chickens
            maxChicken = 10; //max number of chickens
            chickenTime = 0f; //tracks number of seconds since last chicken spawned
            chicQueueTime = 7f; //time between chicken spawning
            chickenQueue = 0;

            //egg values
            eggCount = 0; //number of eggs caught by Character
            numEgg = 0;

            //broken egg
            numBrokeEgg = 0; //number of broken eggs on ground
            brokenEggDisappear = 30.0f;  // time for broken egg to disappear
            brokenEggTime = 0.0f;

            //day timer
            days = 0;           //tracks number of total days5
            dayTime = 0; //tracks number of seconds since day began
            endDay = 500; //day ends when dayTime equals endDay /180 default value
            timedGoalEnd = 30;
            timedGoalCurrent = 0;
            endOfDay = false;

            //boots
            numBoots = 0;
            bootTime = 0.0f; //tracks time since last boot spawned
            bootWaitTime = 10.0f; //time between spawns (when bootSpawnTime equals bootWaitTime add boots)
            bootDeleteTime = 30.0f; //time that boot will disappear
            bootPresent = false; //shows if there is a boot spawned
            bootsEquipped = false; // shows if player has picked up boots
            equippedTime = 0.0f; //tracks time since boots were Equipped
            maxEquipTime = 30.0f;

            //hotkey
            buttonCheck = false;
            buttonCheck2 = false;


            // base.Initialize();
        }

        public void updateWorld(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyBoardState = Keyboard.GetState();

            gameInterface.instance.updateStats(); //update the variables in gameInterface panel
            gameInterface.instance.summaryPanel.updateSummary();

            //open menu keyboard hotkey
            if (keyBoardState.IsKeyDown(Keys.M) && buttonCheck == false)
            {
                if (inGameMenu.instance.visible == true)
                {
                    gameInterface.instance.inGameMenuClicked();
                    inGameMenu.instance.visible = false;
                    isPaused = false;
                }
                else
                {
                    //open menu
                    gameInterface.instance.inGameMenuClicked();
                    inGameMenu.instance.visible = true;
                    gameInterface.instance.summaryPanel.visible = false;
                    isPaused = true;
                }
                buttonCheck = true;
            }

            //open menu gamepad
            if (gamePadState.IsButtonDown(Buttons.Start) && buttonCheck2 == false)
            {
                if (inGameMenu.instance.visible == true)
                {
                    inGameMenu.instance.visible = false;
                    isPaused = false;
                }
                else
                {
                    //open menu
                    inGameMenu.instance.visible = true;
                    //close summary if open
                    gameInterface.instance.summaryPanel.visible = false;
                    isPaused = true;
                }
                buttonCheck2 = true;
            }

            //test to see if hit multipe times
            if (keyBoardState.IsKeyUp(Keys.M) && buttonCheck == true)
            {
                buttonCheck = false;
            }
            if (gamePadState.IsButtonUp(Buttons.Start) && buttonCheck2 == true)
            {
                buttonCheck2 = false;
            }


            //fox warning counter update
            if (GameUI.gameInterface.instance.foxWarning.visible == true)
            {
                GameUI.gameInterface.instance.foxWarningCounter ++;
            }

            if (isPaused != true)
            {
                endOfDay = false;
                if (winLoss.checkVictory(player.money, days) == true)
                {

                    //you win do whatever
                    VictoryLossScreen.instance.enableKeypress = true;
                    VictoryLossScreen.instance.enableKeyPress();

                    VictoryLossScreen.instance.determineWinLossInfo(winLoss.checkVictory(player.money, days));
                    Game1.instance.setGameState(Game1.GameState.winloss);


                    isPaused = true;
                    initializeWorld();
                    player.InitializeEconomic();

                }
                else if (myCharacter.hitPoints <= 0 || player.money < 0)// for debugging, money<0
                {
                    //lose
                    VictoryLossScreen.instance.enableKeypress = true;
                    VictoryLossScreen.instance.enableKeyPress();
                    VictoryLossScreen.instance.determineWinLossInfo(false);
                    Game1.instance.setGameState(Game1.GameState.winloss);


                    isPaused = true;
                    initializeWorld();
                    player.InitializeEconomic();

                }
                //updates time of day
                dayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                
                //runs at the end of day
                if (dayTime >= endDay)
                {
                    
                    if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                    {
                        Chicken.AudioManager.instance.endDaySound.Play();
                    }
                    dayTime = 0;
                    if (Game1.instance.tutorial == true)
                    {
                        days = 0;
                        Game1.instance.tutorial = false;
                    }
                    else
                    {
                        days++;
                    }
                   
                    endOfDay = true;
                    //tutorial = false;//after the first day, turn tutorial tip boxes off
                  
                    player.feed(numChic, numRooster); //take upkeep cost out at end of day

                    roosterQueue = numRooster;
                    chickenQueue = numChic;
                    myFox.FoxReset();
                    player.money += eggCount * player.eggSellEnd;
                    eggCount = 0;
                    numEgg = 0;


                    isPaused = true;
                    timedGoalCurrent++;
                    //open up menu;
                    EndDaySummary.instance.setSummaryState(EndDaySummary.SummaryState.endDaySummary);
                    gameInterface.instance.summaryPanel.updateSummary();
                    gameInterface.instance.summaryPanel.visible = true; //end of day summary becomes visible
                    inGameMenu.instance.updateEggPrice(player.eggSellEnd);
                    //inGameMenu.instance.visible = true;

                    
                    
                }

                //camera always looking at character
                cameraLookAt = myCharacter.position;

                //camera position follows character movement
                cameraPosition.X = myCharacter.position.X - camRearOffset
                    * (float)Math.Sin(myCharacter.rotation);
                cameraPosition.Z = myCharacter.position.Z - camRearOffset
                    * (float)Math.Cos(myCharacter.rotation);

                //get input from Xbox controller for one player
                //GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

                //zoom camera position with Xbox controller 
                if (camRearOffset >= minCamOffset)
                {
                    if (camRearOffset <= maxCamOffset)
                    {
                        camRearOffset += gamePadState.ThumbSticks.Right.X * 10f;
                    }
                    else
                    {
                        camRearOffset = maxCamOffset;
                    }
                }
                else
                {
                    camRearOffset = minCamOffset;
                }

                cameraPosition.Y = camHeightOffset;

                //adjust camera height
                if (camHeightOffset <= maxCamHeight)
                {
                    if (camHeightOffset >= minCamHeight)
                    {
                        camHeightOffset += gamePadState.ThumbSticks.Right.Y * 10f;
                    }
                    else
                    {
                        camHeightOffset = minCamHeight;
                    }
                }
                else
                {
                    camHeightOffset = maxCamHeight;
                }

                //rotate camera position with keyboard input
                //KeyboardState keyBoardState = Keyboard.GetState();

                //rotate camera position with keyboard
                if (keyBoardState.IsKeyDown(Keys.W))
                {
                    if (camHeightOffset <= maxCamHeight)
                    {
                        //move camera higher
                        camHeightOffset += 10;
                    }
                    else
                    {
                        //set camera height to maximum
                        camHeightOffset = maxCamHeight;
                    }
                }
                if (keyBoardState.IsKeyDown(Keys.S))
                {
                    if (camHeightOffset >= minCamHeight)
                    {
                        //move camera lower
                        camHeightOffset -= 10;
                    }
                    else
                    {
                        //set camera height to minimum
                        camHeightOffset = minCamHeight;
                    }
                }
                if (keyBoardState.IsKeyDown(Keys.A))
                {
                    if (camRearOffset <= maxCamOffset)
                    {
                        //zoom out
                        camRearOffset += 10;
                    }
                    else
                    {
                        camRearOffset = maxCamOffset;
                    }
                }
                if (keyBoardState.IsKeyDown(Keys.D))
                {
                    if (camRearOffset >= minCamOffset)
                    {
                        //zoom in
                        camRearOffset -= 10;
                    }
                    else
                    {
                        camRearOffset = minCamOffset;
                    }
                }


                //updates each chicken
                for (int i = 0; i < numChic - chickenQueue; i++)
                {
                    chickenList[i].update(gameTime, numRooster, ref numEgg);
                }

                // update each rooster
                for (int i = 0; i < numRooster - roosterQueue; i++)
                {
                    roosterList[i].update(gameTime, myCharacter.position, ref myCharacter.hitPoints);
                }

                //updates each egg
                for (int i = 0; i < numEgg; i++)
                {
                    eggList[i].update(gameTime);

                    //checks to see if egg hits character
                    if (Vector3.Distance(eggList[i].position, myCharacter.position) < 100)
                    {
                        if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                        {
                            Chicken.AudioManager.instance.itemCollectSound.Play();
                        }
                        eggList[i] = null;
                        eggCount++;
                        player.eggsCollected++;
                        decrementEgg(i);
                        decrementEggShadow(i);
                        numEgg--;
                    }

                    //checks to see if egg hits the ground
                    else if (eggList[i].position.Y <= 185)
                    {
                        if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                        {
                            Chicken.AudioManager.instance.eggCrackSound.Play();
                        }
                        addBrokenEgg(eggList[i].position);
                        eggList[i] = null; //makes egg disappear
                        decrementEgg(i);
                        numEgg--;

                    }
                }


                //chicken queue add chickens
                if (chickenQueue > 0)
                {

                    //chicken timer, add chicken every so often
                    chickenTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //adds chicken every few seconds until chickens reach max number
                    //or until the number of chickens owned is reached
                    if (chickenTime > chicQueueTime && numChic - chickenQueue < maxChicken)
                    {
                        if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                        {
                            Chicken.AudioManager.instance.chickenSpawnSound.Play();
                        }
                        addChicken();
                        chickenTime = 0;
                        chickenQueue--;
                    }
                }

                //rooster queue add roosters
                if (roosterQueue > 0)
                {

                    //rooster timer, add rooster every so often
                    roosterTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //adds rooster every 10 seconds until chickens reach max number
                    if (roosterTime > roostQueueTime && numRooster - roosterQueue < maxRooster)
                    {
                        if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                        {
                            Chicken.AudioManager.instance.roosterSpawnSound.Play(0.5f, 0f, 0f);
                        }
                        addRooster();
                        roosterTime = 0;
                        roosterQueue--;
                    }
                }

                //add egg to Egg Queue if Chicken lays egg
                for (int i = 0; i < numChic - chickenQueue; i++)
                {
                    if (chickenList[i].eggLaid == true)
                    {
                        addEgg(chickenList[i].position);
                        if (Chicken.AudioManager.instance.sFXOn == true && isPaused != true)
                        {
                            Chicken.AudioManager.instance.chickenEggLaySound.Play();
                        }
                        //addEggShadow(eggList[i].position);

                    }
                }

                //update broken egg
                for (int i = 0; i < numBrokeEgg; i++)
                {
                    brokenEggList[i].update(gameTime);
                }

                myCharacter.update(gameTime, brokenEggList, numBrokeEgg, bootsEquipped,
                                    ref cameraPosition, ref cameraLookAt, ref cameraViewMatrix,
                                    player, ref eggCount);

                //update fox
                myFox.update(gameTime, ref chickenList, ref roosterList, ref numRooster,
                                    ref numChic, myCharacter.position, roosterQueue, chickenQueue, player);

                //broken egg remove
                if (numBrokeEgg > 0)
                {
                    brokenEggTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (brokenEggTime >= brokenEggDisappear)
                {
                    decrementBrokenEgg(0);
                    brokenEggTime = 0;
                }

            }
            else
            {
                //display "PAUSED" on the screen

            }

            //update boots
            bootTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            equippedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //add new boots           
            if (bootTime >= bootWaitTime && bootPresent == false)
            {
                // audio play when boots spawn
                addBoots();
                bootPresent = true;
                bootsEquipped = false;
            }

            //if boots have been on too long, remove them
            if (equippedTime >= maxEquipTime && bootsEquipped == true)
            {
                gameInterface.instance.bootIsEquipped = false;
                bootsEquipped = false;
                bootPresent = false;
                bootTime = 0.0f;
            }

            for (int i = 0; i < numBoots; i++)
            {
                //delete boots
                //checks to see if character picks up boots
                if (Vector3.Distance(myBootList[i].position, myCharacter.position) < 200)
                {
                    // audio play when boots are picked up
                    Chicken.AudioManager.instance.itemCollectSound.Play();
                    player.money += 5; //monetary reward for picking up the boots
                    gameInterface.instance.bootIsEquipped = true;
                    myBootList[i] = null;
                    bootsEquipped = true;
                    deleteBoots(i);
                    numBoots--;
                    //bootTime = 0.0f;
                    equippedTime = 0.0f;
                }
                //checks to see if boots have been out too long
                else if (bootTime >= bootDeleteTime && bootPresent == true)
                {

                    myBootList[i] = null;
                    deleteBoots(i);
                    bootTime = 0.0f;
                    numBoots--;
                    bootPresent = false;
                }

            }


            // base.Update(gameTime);
        }

        public void draw(GameTime gameTime)
        {
            cameraViewMatrix = Matrix.CreateLookAt(
                cameraPosition,
                cameraLookAt,
                Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                10000.0f*50);//10000.0f

            // draws each chicken
            for (int i = 0; i < numChic - chickenQueue; i++)
            {
                chickenList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            // copy any parent transforms
            Matrix[] transforms3 = new Matrix[myScene.Bones.Count];
            myScene.CopyAbsoluteBoneTransformsTo(transforms3);

            //draw positions for rooster
            for (int i = 0; i < numRooster - roosterQueue; i++)
            {
                roosterList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            //draw character
            myCharacter.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);

            //draw fox
            myFox.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);

            //draw broken egg
            for (int i = 0; i < numBrokeEgg; i++)
            {
                brokenEggList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            //draw egg
            for (int i = 0; i < numEgg; i++)
            {
                eggList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            //draw egg shadow
            for (int i = 0; i < numEgg; i++)
            {
                shadowList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            //draw boots
            for (int i = 0; i < numBoots; i++)
            {
                myBootList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            foreach (ModelMesh mesh3 in myScene.Meshes)
            {
                foreach (BasicEffect effect in mesh3.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms3[mesh3.ParentBone.Index] //*
                        //Matrix.CreateRotationY(modelRotation)
                        * Matrix.CreateTranslation(0, 0, 0);
                    //effect.View = Matrix.CreateLookAt(cameraPosition,
                    //    Vector3.Zero, Vector3.Up);
                    //effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                    //    MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                    effect.View = cameraViewMatrix;
                    effect.Projection = cameraProjectionMatrix;
                }
                mesh3.Draw();
            }
            //draw the skybox
            //foreach (ModelMesh mesh3 in mySkyBox.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh3.Effects)
            //    {
            //        //effect.World = transforms2[mesh2.ParentBone.Index] *
            //        //    Matrix.CreateScale(scale) *
            //        //    Matrix.CreateRotationY(rotation) *
            //        //Matrix.CreateTranslation(position);
            //        //effect.View = viewMatrix;
            //        //effect.Projection = projectionMatrix;

            //        effect.EnableDefaultLighting();
            //        effect.World = transforms3[mesh3.ParentBone.Index] *
            //            Matrix.CreateScale(20.0f) * Matrix.CreateRotationY(0)
            //            * Matrix.CreateTranslation(0, 0, 0);
            //        //effect.View = Matrix.CreateLookAt(cameraPosition,
            //        //    Vector3.Zero, Vector3.Up);
            //        //effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            //        //    MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            //        effect.View = cameraViewMatrix;
            //        effect.Projection = cameraProjectionMatrix;
            //    }
            //    mesh3.Draw();
            //}

        }
        //function to add chicken into the game
        public void addChicken()
        {
            chickenList[numChic - chickenQueue] = new ChickenClass(content, graphics);
            chickenList[numChic - chickenQueue].InitializeChicken(0);
            //numChic++;
        }

        //function to add rooster into the game
        public void addRooster()
        {
            roosterList[numRooster - roosterQueue] = new RoosterClass(content, graphics);
            roosterList[numRooster - roosterQueue].InitializeRooster(13);
            //numRooster++;
        }
        public void addEgg(Vector3 chickenPosition)
        {
            eggList[numEgg] = new EggClass(content, graphics);
            eggList[numEgg].position = chickenPosition;
            addEggShadow(chickenPosition);
            numEgg++;
        }
        public void addBrokenEgg(Vector3 eggPosition)
        {
            brokenEggList[numBrokeEgg] = new BrokenEggClass(content, graphics);
            brokenEggList[numBrokeEgg].position = eggPosition;
            numBrokeEgg++;
        }
        public void decrementEgg(int i)
        {
            for (int j = i; j < numEgg; j++)
            {
                //transfers egg data from element 2 to 1 and deletes element 2
                eggList[j] = eggList[j + 1];
                eggList[j + 1] = null;
            }
        }

        public void decrementBrokenEgg(int i)
        {
            for (int j = i; j < numBrokeEgg; j++)
            {
                //transfers egg data from element 2 to 1 and deletes element 2
                brokenEggList[j] = brokenEggList[j + 1];
                brokenEggList[j + 1] = null;
            }
            numBrokeEgg--;
        }
        public void addBoots()
        {
            myBootList[numBoots] = new RubberBootClass(content, graphics);
            myBootList[numBoots].initializeBoots();
            numBoots++;
        }
        public void deleteBoots(int i)
        {
            for (int j = i; j < numBoots; j++)
            {
                //transfers boot data from element 2 to 1 and deletes element 2
                myBootList[j] = myBootList[j + 1];
                myBootList[j + 1] = null;
            }
        }
        public void addEggShadow(Vector3 chickenPosition)
        {
            shadowList[numEgg] = new EggShadowClass(content, graphics);
            shadowList[numEgg].position = chickenPosition;
            shadowList[numEgg].position.Y = 185f;
        }
        public void decrementEggShadow(int i)
        {
            for (int j = i; j < numEgg; j++)
            {
                //transfers egg data from element 2 to 1 and deletes element 2
                shadowList[j] = shadowList[j + 1];
                shadowList[j + 1] = null;
            }
        }
    }
}

   

       
