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

//saveload
using System.IO;
using System.Xml.Serialization;

using System.Text.RegularExpressions;


namespace Chicken
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum GameState { start, options, help, game, credits, gameInterface, newGame, splash,winloss,tutorial };//gameMenu

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        //game ui
        GameUI.gameInterface gameInterfaceScreen;
        GameUI.startMenuScreen startScreen;
        GameUI.optionsMenuScreen optionsScreen;
        GameUI.NewGameMenu newGameScreen;
        GameUI.helpScreen helpScreen;
        GameUI.creditsScreen creditsScreen;
        GameUI.splashScreen splashScreen;
        GameUI.VictoryLossScreen winlossScreen;
        GameUI.tutorialScreen tutorialScreen;
        UI.MouseCursor mouseCursor;
        // GameUI.inGameMenu inGameMenuScreen;
        private GameState gameState;
        public static Game1 instance;
        private GameUI.gameWorld world;
        public bool chooseFullScreen = false;
        public TutorialLevel tutLevel;
        public bool tutorial = false;

        //save load variable
        public IAsyncResult result;
        public StorageDevice device;


        //Matrix cameraViewMatrix;
        //Matrix cameraProjectionMatrix;
        //Vector3 cameraLookAt = new Vector3(3500, 50, 3500);
        //Vector3 cameraPosition = new Vector3(3500.0f, 450.0f, 6000.0f);
        //RoosterClass myRooster;
        //ChickenClass myChicken;
        //CharacterClass myCharacter;
        //int numChic = 1;
        //ArrayList chickenList = new ArrayList();
        List<RoosterClass> Rooster = new List<RoosterClass>();


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;



        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.graphics.PreferredBackBufferWidth = 1280;
            //this.graphics.PreferredBackBufferHeight = 720;

            this.graphics.IsFullScreen = chooseFullScreen; //default is false
            this.graphics.ApplyChanges();
            world = new GameUI.gameWorld(Content, graphics);
            world.initializeWorld();
            tutLevel = new TutorialLevel(Content, graphics);
            tutLevel.initilizeTutorialLevel();

            //world.addComponent(world);


            //cameraViewMatrix = Matrix.CreateLookAt(
            //    cameraPosition,
            //    cameraLookAt,
            //    Vector3.Up);

            //cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            //    MathHelper.ToRadians(45.0f),
            //    graphics.GraphicsDevice.Viewport.AspectRatio,
            //    1.0f,
            //    10000.0f);

            //// TODO: Add your initialization logic here
            //myRooster = new RoosterClass(Content, graphics);
            //myRooster.InitializeRooster();

            ////create multiple chickens
            //ChickenClass myChicken = new ChickenClass(Content, graphics);
            //myChicken.InitializeChicken();

            //chickenList.Add(myChicken);
            ////on button press/chicken buy selection create another chicken object
            ////add graphics, and initialize then add to chickenlist
            //chickenList.Add(myChicken);



            //myCharacter = new CharacterClass(Content, graphics);
            //myCharacter.InitializeCharacter();

            //saveload
            result = Guide.BeginShowStorageDeviceSelector(PlayerIndex.One, null, null);
            device = Guide.EndShowStorageDeviceSelector(result);
            CheckWhetherExists(device);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        //test the scene Model
        //Model myScene;

        ////the aspect ratio determines how to scale 3d to 3d projection
        //float aspectRatio;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load your game content

            //myScene = Content.Load<Model>("Models\\chickenScene");
            //aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            new AudioManager(Content);
            UI.Component.font = Content.Load<SpriteFont>("UIFont");
            UI.PushButton.disabledTexture = Content.Load<Texture2D>("MenuImages/darkMenuPaneltrans50");

            //load the menu screens
            startScreen = new GameUI.startMenuScreen(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content);
            gameInterfaceScreen = new GameUI.gameInterface(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content, graphics);
            helpScreen = new GameUI.helpScreen(graphics.GraphicsDevice.Viewport.Width, 
                graphics.GraphicsDevice.Viewport.Height, Content);
            optionsScreen = new GameUI.optionsMenuScreen(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content);
            creditsScreen = new GameUI.creditsScreen(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content);
            newGameScreen = new GameUI.NewGameMenu(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content);
            splashScreen = new GameUI.splashScreen(graphics.GraphicsDevice.Viewport.Width, 
                graphics.GraphicsDevice.Viewport.Height, Content);
            winlossScreen = new GameUI.VictoryLossScreen(graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height, Content);
            tutorialScreen = new GameUI.tutorialScreen(graphics.GraphicsDevice.Viewport.Width,
               graphics.GraphicsDevice.Viewport.Height, Content, graphics);

            mouseCursor = new UI.MouseCursor(0, 0, Content.Load<Texture2D>("MenuImages/triangleIcon"));
            mouseCursor.resize(40, 40);
            startScreen.addComponent(mouseCursor);

            helpScreen.addComponent(mouseCursor);
            optionsScreen.addComponent(mouseCursor);
            creditsScreen.addComponent(mouseCursor);
            newGameScreen.addComponent(mouseCursor);
            winlossScreen.addComponent(mouseCursor);
            gameInterfaceScreen.addComponent(mouseCursor);
            GameUI.tutorialScreen.instance.menuPanelTutorial.addComponent(mouseCursor);
            setGameState(GameState.splash);
            Chicken.AudioManager.instance.roosterSpawnSound.Play(0.3f,0f,0f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //change to full screen
            if (chooseFullScreen == true)
            {
                this.graphics.IsFullScreen = true;
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyBoardState = Keyboard.GetState();

            if (keyBoardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (keyBoardState.IsKeyDown(Keys.F1))
            {
                Save(device, world.numChic, world.chickenQueue, world.myCharacter, world.numRooster, world.roosterQueue, 
                    world.chickenList, world.roosterList, world.player, world.eggCount, world.numEgg, world.eggList,
                    world.days, world.dayTime, world.endOfDay, world.numBoots, world.myBootList, world.bootTime,
                    world.bootPresent, world.bootsEquipped, world.myFox);
            }
            if (keyBoardState.IsKeyDown(Keys.F2))
            {
                LoadData(device,ref world.numChic, ref world.chickenQueue, ref world.numRooster,
                    ref world.roosterQueue, ref world.myCharacter, ref world.chickenList, ref world.roosterList,
                    ref world.player, ref world.eggCount, ref world.numEgg, ref world.eggList, ref world.days,
                    ref world.dayTime, ref world.endOfDay, ref world.numBoots, ref world.myBootList, ref world.bootTime,
                    ref world.bootPresent, ref world.bootsEquipped, ref world.myFox);
            }

            //get input from Xbox controller for one player
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            //add rotation to the model
            //modelRotation += (float)gameTime.ElapsedGameTime.TotalMilliseconds
            //    * MathHelper.ToRadians(0.1f);

            //update the rooster position
            //myRooster.update(gameTime,myCharacter.position, ref myCharacter.hitPoints);
            //foreach (RoosterClass r in Rooster)
            //{
            //    r.update(gameTime,myCharacter.position, ref myCharacter.hitPoints);
            //}
            //foreach (ChickenClass c in chickenList)
            //{
            //    c.update(gameTime);
            //}
            // myChicken.update(gameTime);
            //myCharacter.update(gameTime);

            //test to see if character has no hit points
            //if (world.myCharacter.hitPoints == 0) ///this code should be in the character class or gameworld under
            //win/loss conditions
            //{
            //    gameState = GameState.start;
            //    world.isPaused = true;
            //    world.initializeWorld();
            //}
            //if (gameState == GameState.splash)
            //{
            //    splashScreen.keyInputTimer();
            //}
            splashScreen.keyInputTimer();
           // winlossScreen.enableKeyPress();

           
            //if(splashScreen.timerEnded!= true) //code for auto-update
            //{
            //    splashScreen.splashScreenUpdateTimer(gameTime);
            //}
            if (tutorial == true)
            {
                tutLevel.updateTutorialLevel(gameTime);
            }
            else
            {
                world.updateWorld(gameTime);
            }

            base.Update(gameTime);
        }

        //method to select the game state
        public void setGameState(GameState _newState)
        {
            gameState = _newState;
            if (Chicken.AudioManager.instance.musicOn == true)
            {
                AudioManager.instance.playBackgroundSound(gameState);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        //set the position of the model in world space, and set the rotation
        //Vector3 modelPosition = Vector3.Zero;
        //Vector3 modelPositionTwo = Vector3.Down;
        //Vector3 modelPositionThree = Vector3.Zero;
        //float modelRotation = 0.0f;

        //Vector3 temp = new Vector3(0, 50, 0);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //re-enable the zbuffer so 3d will work correctly
            GraphicsDevice.RenderState.DepthBufferEnable = true;
            GraphicsDevice.RenderState.DepthBufferWriteEnable = true;

            if (tutorial == true)
            {
                tutLevel.drawTutorialLevel(gameTime);
            }
            else
            {
                world.draw(gameTime);
            }

            //cameraViewMatrix = Matrix.CreateLookAt(
            //    cameraPosition,
            //    cameraLookAt,
            //    Vector3.Up);

            //cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
            //    MathHelper.ToRadians(45.0f),
            //    graphics.GraphicsDevice.Viewport.AspectRatio,
            //    1.0f,
            //    10000.0f);

            //// copy any parent transforms
            ////Matrix[] transforms = new Matrix[myModel.Bones.Count];
            ////Matrix[] transforms2 = new Matrix[myRooster.Bones.Count];
            //Matrix[] transforms3 = new Matrix[myScene.Bones.Count];
            ////myModel.CopyAbsoluteBoneTransformsTo(transforms);
            ////myRooster.CopyAbsoluteBoneTransformsTo(transforms2);
            //myScene.CopyAbsoluteBoneTransformsTo(transforms3);

            //initial position for Character and AI
            //foreach (RoosterClass r in Rooster)
            //{
            //    r.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            //}
            //myRooster.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);

            //foreach (ChickenClass c in chickenList)
            //{
            //    c.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            //}
            ////myChicken.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            //myCharacter.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);

            //foreach (ModelMesh mesh3 in myScene.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh3.Effects)
            //    {
            //        effect.EnableDefaultLighting();
            //        effect.World = transforms3[mesh3.ParentBone.Index] //*
            //            //Matrix.CreateRotationY(modelRotation)
            //            * Matrix.CreateTranslation(0,0,0);
            //        //effect.View = Matrix.CreateLookAt(cameraPosition,
            //        //    Vector3.Zero, Vector3.Up);
            //        //effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            //        //    MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            //        effect.View = cameraViewMatrix;
            //        effect.Projection = cameraProjectionMatrix;
            //    }
            //    mesh3.Draw();
            //}
            //quick code to make control invisible whenever the menu system is closed,hit M to open menu system and
            //control appears
            if (GameUI.gameWorld.instance.isPaused == true)
            {
                mouseCursor.visible = true;
            }
            else
            {
                mouseCursor.visible = false;
            }
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.splash:
                    splashScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.start:
                    startScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.options:
                    optionsScreen.draw(gameTime, spriteBatch);
                    optionsScreen.optionsMenuUpdate();
                    break;
                case GameState.newGame:
                    newGameScreen.draw(gameTime, spriteBatch);
                    //newGameScreen.newGameMenuUpdate();
                    break;
                case GameState.help:
                    helpScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.game:
                    gameInterfaceScreen.draw(gameTime, spriteBatch);

                    break;
                case GameState.credits:
                    creditsScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.gameInterface:
                    gameInterfaceScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.winloss:
                    winlossScreen.draw(gameTime, spriteBatch);
                    break;
                case GameState.tutorial:
                    tutLevel.drawTutorialLevel(gameTime);
                    tutorialScreen.draw(gameTime, spriteBatch);
                 //use this until tutorial boxes are created
                    break;

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        //----------------------------------------------------------------------------------------------------------------------
        //public call to save game
        public void saveGame()
        {
            Save(device, world.numChic, world.chickenQueue, world.myCharacter, world.numRooster, world.roosterQueue,
                    world.chickenList, world.roosterList, world.player, world.eggCount, world.numEgg, world.eggList,
                    world.days, world.dayTime, world.endOfDay, world.numBoots, world.myBootList, world.bootTime,
                    world.bootPresent, world.bootsEquipped, world.myFox);
        }
        //public call to load game
        public void loadGame()
        {
            LoadData(device, ref world.numChic, ref world.chickenQueue, ref world.numRooster,
                    ref world.roosterQueue, ref world.myCharacter, ref world.chickenList, ref world.roosterList,
                    ref world.player, ref world.eggCount, ref world.numEgg, ref world.eggList, ref world.days,
                    ref world.dayTime, ref world.endOfDay, ref world.numBoots, ref world.myBootList, ref world.bootTime,
                    ref world.bootPresent, ref world.bootsEquipped, ref world.myFox);
        }

        //save load functions
        [Serializable]
        public struct DataToSave
        {
            //character save
            public Vector3 cPosition;
            public int hitpoints;
            public float cRotation;
            //chicken save
            public int numChickens;
            public int ChicQueue;

            //chicken1
            public float chic1time;
            public int chic1state;
            public int chic1ground;
            public int chic1InitNode;
            public int chic1PreviousNode;
            public int chic1CurrentNode;
            public int chic1NextNode;
            public Vector3 chic1Position;
            public float chic1Rotation;
            public Vector3 chic1RiseRun;
            public bool chic1EggLaid;

            //chicken2
            public float chic2time;
            public int chic2state;
            public int chic2ground;
            public int chic2InitNode;
            public int chic2PreviousNode;
            public int chic2CurrentNode;
            public int chic2NextNode;
            public Vector3 chic2Position;
            public float chic2Rotation;
            public Vector3 chic2RiseRun;
            public bool chic2EggLaid;

            //chicken3
            public float chic3time;
            public int chic3state;
            public int chic3ground;
            public int chic3InitNode;
            public int chic3PreviousNode;
            public int chic3CurrentNode;
            public int chic3NextNode;
            public Vector3 chic3Position;
            public float chic3Rotation;
            public Vector3 chic3RiseRun;
            public bool chic3EggLaid;

            //chicken4
            public float chic4time;
            public int chic4state;
            public int chic4ground;
            public int chic4InitNode;
            public int chic4PreviousNode;
            public int chic4CurrentNode;
            public int chic4NextNode;
            public Vector3 chic4Position;
            public float chic4Rotation;
            public Vector3 chic4RiseRun;
            public bool chic4EggLaid;

            //chicken5
            public float chic5time;
            public int chic5state;
            public int chic5ground;
            public int chic5InitNode;
            public int chic5PreviousNode;
            public int chic5CurrentNode;
            public int chic5NextNode;
            public Vector3 chic5Position;
            public float chic5Rotation;
            public Vector3 chic5RiseRun;
            public bool chic5EggLaid;

            //chicken6
            public float chic6time;
            public int chic6state;
            public int chic6ground;
            public int chic6InitNode;
            public int chic6PreviousNode;
            public int chic6CurrentNode;
            public int chic6NextNode;
            public Vector3 chic6Position;
            public float chic6Rotation;
            public Vector3 chic6RiseRun;
            public bool chic6EggLaid;

            //chicken7
            public float chic7time;
            public int chic7state;
            public int chic7ground;
            public int chic7InitNode;
            public int chic7PreviousNode;
            public int chic7CurrentNode;
            public int chic7NextNode;
            public Vector3 chic7Position;
            public float chic7Rotation;
            public Vector3 chic7RiseRun;
            public bool chic7EggLaid;

            //chicken8
            public float chic8time;
            public int chic8state;
            public int chic8ground;
            public int chic8InitNode;
            public int chic8PreviousNode;
            public int chic8CurrentNode;
            public int chic8NextNode;
            public Vector3 chic8Position;
            public float chic8Rotation;
            public Vector3 chic8RiseRun;
            public bool chic8EggLaid;

            //chicken9
            public float chic9time;
            public int chic9state;
            public int chic9ground;
            public int chic9InitNode;
            public int chic9PreviousNode;
            public int chic9CurrentNode;
            public int chic9NextNode;
            public Vector3 chic9Position;
            public float chic9Rotation;
            public Vector3 chic9RiseRun;
            public bool chic9EggLaid;

            //chicken10
            public float chic10time;
            public int chic10state;
            public int chic10ground;
            public int chic10InitNode;
            public int chic10PreviousNode;
            public int chic10CurrentNode;
            public int chic10NextNode;
            public Vector3 chic10Position;
            public float chic10Rotation;
            public Vector3 chic10RiseRun;
            public bool chic10EggLaid;

            //economic save
            public int money;
            public int startEggs;
            public int startChickens;
            public int startRoosters;
            public int startmoney;
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

            //rooster save
            public int numRooster;
            public int roosterQueue;

            //rooster1
            public int rooster1state;
            public int rooster1InitNode;
            public int rooster1PreviousNode;
            public int rooster1CurrentNode;
            public int rooster1NextNode;
            public Vector3 rooster1Position;
            public float rooster1Rotation;
            public Vector3 rooster1RiseRun;
            public Vector3 rooster1RiseRun2;
            public bool rooster1Start;

            //rooster2
            public int rooster2state;
            public int rooster2InitNode;
            public int rooster2PreviousNode;
            public int rooster2CurrentNode;
            public int rooster2NextNode;
            public Vector3 rooster2Position;
            public float rooster2Rotation;
            public Vector3 rooster2RiseRun;
            public Vector3 rooster2RiseRun2;
            public bool rooster2Start;

            //rooster3
            public int rooster3state;
            public int rooster3InitNode;
            public int rooster3PreviousNode;
            public int rooster3CurrentNode;
            public int rooster3NextNode;
            public Vector3 rooster3Position;
            public float rooster3Rotation;
            public Vector3 rooster3RiseRun;
            public Vector3 rooster3RiseRun2;
            public bool rooster3Start;

            //rooster4
            public int rooster4state;
            public int rooster4InitNode;
            public int rooster4PreviousNode;
            public int rooster4CurrentNode;
            public int rooster4NextNode;
            public Vector3 rooster4Position;
            public float rooster4Rotation;
            public Vector3 rooster4RiseRun;
            public Vector3 rooster4RiseRun2;
            public bool rooster4Start;

            //rooster5
            public int rooster5state;
            public int rooster5InitNode;
            public int rooster5PreviousNode;
            public int rooster5CurrentNode;
            public int rooster5NextNode;
            public Vector3 rooster5Position;
            public float rooster5Rotation;
            public Vector3 rooster5RiseRun;
            public Vector3 rooster5RiseRun2;
            public bool rooster5Start;

            //egg
            public int eggCount;
            public int numEgg;
            public Vector3 egg1;
            public Vector3 egg2;
            public Vector3 egg3;
            public Vector3 egg4;
            public Vector3 egg5;
            public Vector3 egg6;
            public Vector3 egg7;
            public Vector3 egg8;
            public Vector3 egg9;
            public Vector3 egg10;
            public Vector3 egg11;
            public Vector3 egg12;
            public Vector3 egg13;
            public Vector3 egg14;
            public Vector3 egg15;
            public Vector3 egg16;
            public Vector3 egg17;
            public Vector3 egg18;
            public Vector3 egg19;
            public Vector3 egg20;

            //timer variables
            public int days;       
            public float dayTime; 
            public bool endOfDay;

            //boots
            public int numBoots;
            public Vector3 boot1;
            public Vector3 boot2;
            public float bootTime;
            public bool bootPresent;
            public bool bootEquipped;

            //fox
            public bool foxWandering;
            public int foxInitNode;
            public int foxPreviousNode;
            public int foxCurrentNode;
            public int foxNextNode;
            public Vector3 foxPosition;
            public Vector3 foxPositionOld;
            public float foxRotation;
            public Vector3 foxRiseRun;
            public Vector3 foxRiseRun2;
            public int foxChasing;
            public bool foxAvoiding;
            public float foxTimer;
            public float foxChaseTime;
            public float foxTimer2;
            public bool foxHome;
            public bool foxStart;
            public bool foxTemp1;



        }

        public void CheckWhetherExists(StorageDevice storageDevice)
        {
            StorageContainer myContainer = storageDevice.OpenContainer("Chicken_Game");

            string nameOfFile = Path.Combine(myContainer.Path, "ChickenGameSave.sav");
            if (File.Exists(nameOfFile))
            {
                //LoadData(device, ref world.myCharacter.position);
            }
            myContainer.Dispose();
        }

        private static void Save(StorageDevice storageDevice,int numChics, int chicQueue, CharacterClass player,
            int numRooster, int roosterQueue, ChickenClass[] chickens, RoosterClass[] roosters, EconomicsClass eco, 
            int eggCount, int numEgg, EggClass[] egg, int days, float dayTime, bool endOfDay, int numBoots,
            RubberBootClass[] boots,
            float bootTime, bool bootPresent, bool bootEquipped, FoxClass fox)
        {

            // Pass the information into the data objects
            DataToSave dataToSave = new DataToSave();

            
            //save character
            dataToSave.cPosition = player.position;
            dataToSave.cRotation = player.rotation;
            dataToSave.hitpoints = player.hitPoints;

            //save Chickens
            dataToSave.numChickens = numChics;
            dataToSave.ChicQueue = chicQueue;
            //save chicken1
            if (chickens[0].time != null)
            {
                dataToSave.chic1time = chickens[0].time;
                dataToSave.chic1state = chickens[0].state;
                dataToSave.chic1ground = chickens[0].ground;
                dataToSave.chic1InitNode = chickens[0].ChickenInitNode;
                dataToSave.chic1PreviousNode = chickens[0].ChickenPreviousNode;
                dataToSave.chic1CurrentNode = chickens[0].ChickenCurrentNode;
                dataToSave.chic1NextNode = (int)chickens[0].chickenNextNode;
                dataToSave.chic1Position = chickens[0].position;
                dataToSave.chic1Rotation = chickens[0].rotation;
                dataToSave.chic1RiseRun = chickens[0].riseRun;
                dataToSave.chic1EggLaid = chickens[0].eggLaid;
            }
            else
            {
                dataToSave.chic1time = -1;
            }
            //save chicken2
            if (chickens[1] != null)
            {
                dataToSave.chic2time = chickens[1].time;
                dataToSave.chic2state = chickens[1].state;
                dataToSave.chic2ground = chickens[1].ground;
                dataToSave.chic2InitNode = chickens[1].ChickenInitNode;
                dataToSave.chic2PreviousNode = chickens[1].ChickenPreviousNode;
                dataToSave.chic2CurrentNode = chickens[1].ChickenCurrentNode;
                dataToSave.chic2NextNode = (int)chickens[1].chickenNextNode;
                dataToSave.chic2Position = chickens[1].position;
                dataToSave.chic2Rotation = chickens[1].rotation;
                dataToSave.chic2RiseRun = chickens[1].riseRun;
                dataToSave.chic2EggLaid = chickens[1].eggLaid;
            }
            else
            {
                dataToSave.chic2time = -1;
            }
            //save chicken3
            if (chickens[2] != null)
            {
                dataToSave.chic3time = chickens[2].time;
                dataToSave.chic3state = chickens[2].state;
                dataToSave.chic3ground = chickens[2].ground;
                dataToSave.chic3InitNode = chickens[2].ChickenInitNode;
                dataToSave.chic3PreviousNode = chickens[2].ChickenPreviousNode;
                dataToSave.chic3CurrentNode = chickens[2].ChickenCurrentNode;
                dataToSave.chic3NextNode = (int)chickens[2].chickenNextNode;
                dataToSave.chic3Position = chickens[2].position;
                dataToSave.chic3Rotation = chickens[2].rotation;
                dataToSave.chic3RiseRun = chickens[2].riseRun;
                dataToSave.chic3EggLaid = chickens[2].eggLaid;
            }
            else
            {
                dataToSave.chic3time = -1;
            }
            //save chicken4
            if (chickens[3] != null)
            {
                dataToSave.chic4time = chickens[3].time;
                dataToSave.chic4state = chickens[3].state;
                dataToSave.chic4ground = chickens[3].ground;
                dataToSave.chic4InitNode = chickens[3].ChickenInitNode;
                dataToSave.chic4PreviousNode = chickens[3].ChickenPreviousNode;
                dataToSave.chic4CurrentNode = chickens[3].ChickenCurrentNode;
                dataToSave.chic4NextNode = (int)chickens[3].chickenNextNode;
                dataToSave.chic4Position = chickens[3].position;
                dataToSave.chic4Rotation = chickens[3].rotation;
                dataToSave.chic4RiseRun = chickens[3].riseRun;
                dataToSave.chic4EggLaid = chickens[3].eggLaid;
            }
            else
            {
                dataToSave.chic4time = -1;
            }
            //save chicken5
            if (chickens[4] != null)
            {
                dataToSave.chic5time = chickens[4].time;
                dataToSave.chic5state = chickens[4].state;
                dataToSave.chic5ground = chickens[4].ground;
                dataToSave.chic5InitNode = chickens[4].ChickenInitNode;
                dataToSave.chic5PreviousNode = chickens[4].ChickenPreviousNode;
                dataToSave.chic5CurrentNode = chickens[4].ChickenCurrentNode;
                dataToSave.chic5NextNode = (int)chickens[4].chickenNextNode;
                dataToSave.chic5Position = chickens[4].position;
                dataToSave.chic5Rotation = chickens[4].rotation;
                dataToSave.chic5RiseRun = chickens[4].riseRun;
                dataToSave.chic5EggLaid = chickens[4].eggLaid;
            }
            else
            {
                dataToSave.chic5time = -1;
            }
            //save chicken6
            if (chickens[5] != null)
            {
                dataToSave.chic6time = chickens[5].time;
                dataToSave.chic6state = chickens[5].state;
                dataToSave.chic6ground = chickens[5].ground;
                dataToSave.chic6InitNode = chickens[5].ChickenInitNode;
                dataToSave.chic6PreviousNode = chickens[5].ChickenPreviousNode;
                dataToSave.chic6CurrentNode = chickens[5].ChickenCurrentNode;
                dataToSave.chic6NextNode = (int)chickens[5].chickenNextNode;
                dataToSave.chic6Position = chickens[5].position;
                dataToSave.chic6Rotation = chickens[5].rotation;
                dataToSave.chic6RiseRun = chickens[5].riseRun;
                dataToSave.chic6EggLaid = chickens[5].eggLaid;
            }
            else
            {
                dataToSave.chic6time = -1;
            }
            //save chicken7
            if (chickens[6] != null)
            {
                dataToSave.chic7time = chickens[6].time;
                dataToSave.chic7state = chickens[6].state;
                dataToSave.chic7ground = chickens[6].ground;
                dataToSave.chic7InitNode = chickens[6].ChickenInitNode;
                dataToSave.chic7PreviousNode = chickens[6].ChickenPreviousNode;
                dataToSave.chic7CurrentNode = chickens[6].ChickenCurrentNode;
                dataToSave.chic7NextNode = (int)chickens[6].chickenNextNode;
                dataToSave.chic7Position = chickens[6].position;
                dataToSave.chic7Rotation = chickens[6].rotation;
                dataToSave.chic7RiseRun = chickens[6].riseRun;
                dataToSave.chic7EggLaid = chickens[6].eggLaid;
            }
            else
            {
                dataToSave.chic7time = -1;
            }
            //save chicken8
            if (chickens[7] != null)
            {
                dataToSave.chic8time = chickens[7].time;
                dataToSave.chic8state = chickens[7].state;
                dataToSave.chic8ground = chickens[7].ground;
                dataToSave.chic8InitNode = chickens[7].ChickenInitNode;
                dataToSave.chic8PreviousNode = chickens[7].ChickenPreviousNode;
                dataToSave.chic8CurrentNode = chickens[7].ChickenCurrentNode;
                dataToSave.chic8NextNode = (int)chickens[7].chickenNextNode;
                dataToSave.chic8Position = chickens[7].position;
                dataToSave.chic8Rotation = chickens[7].rotation;
                dataToSave.chic8RiseRun = chickens[7].riseRun;
                dataToSave.chic8EggLaid = chickens[7].eggLaid;
            }
            else
            {
                dataToSave.chic8time = -1;
            }
            //save chicken9
            if (chickens[8] != null)
            {
                dataToSave.chic9time = chickens[8].time;
                dataToSave.chic9state = chickens[8].state;
                dataToSave.chic9ground = chickens[8].ground;
                dataToSave.chic9InitNode = chickens[8].ChickenInitNode;
                dataToSave.chic9PreviousNode = chickens[8].ChickenPreviousNode;
                dataToSave.chic9CurrentNode = chickens[8].ChickenCurrentNode;
                dataToSave.chic9NextNode = (int)chickens[8].chickenNextNode;
                dataToSave.chic9Position = chickens[8].position;
                dataToSave.chic9Rotation = chickens[8].rotation;
                dataToSave.chic9RiseRun = chickens[8].riseRun;
                dataToSave.chic9EggLaid = chickens[8].eggLaid;
            }
            else
            {
                dataToSave.chic9time = -1;
            }
            //save chicken10
            if (chickens[9] != null)
            {
                dataToSave.chic10time = chickens[9].time;
                dataToSave.chic10state = chickens[9].state;
                dataToSave.chic10ground = chickens[9].ground;
                dataToSave.chic10InitNode = chickens[9].ChickenInitNode;
                dataToSave.chic10PreviousNode = chickens[9].ChickenPreviousNode;
                dataToSave.chic10CurrentNode = chickens[9].ChickenCurrentNode;
                dataToSave.chic10NextNode = (int)chickens[9].chickenNextNode;
                dataToSave.chic10Position = chickens[9].position;
                dataToSave.chic10Rotation = chickens[9].rotation;
                dataToSave.chic10RiseRun = chickens[9].riseRun;
                dataToSave.chic10EggLaid = chickens[9].eggLaid;
            }
            else
            {
                dataToSave.chic10time = -1;
            }

            //economics save
            dataToSave.money = eco.money;
            dataToSave.startEggs = eco.startEggs;
            dataToSave.startChickens = eco.startChickens;
            dataToSave.startRoosters = eco.startRoosters;
            dataToSave.startmoney = eco.startmoney;
            dataToSave.eggsCollected = eco.eggsCollected;
            dataToSave.chickenBought = eco.chickenBought;
            dataToSave.roosterBought = eco.roosterBought;
            dataToSave.eggsEaten = eco.eggsEaten;
            dataToSave.eggSold = eco.eggSold;
            dataToSave.chickenSold = eco.chickenSold;
            dataToSave.roosterSold = eco.roosterSold;
            dataToSave.chickenEaten = eco.chickenEaten;
            dataToSave.roosterEaten = eco.roosterEaten;
            dataToSave.moneyAquired = eco.moneyAquired;


            //rooster save
            dataToSave.numRooster = numRooster;
            dataToSave.roosterQueue = roosterQueue;

            //rooster1 save
            if (roosters[0] != null)
            {
                dataToSave.rooster1state = roosters[0].state;
                dataToSave.rooster1InitNode = roosters[0].roosterInitNode;
                dataToSave.rooster1PreviousNode = roosters[0].roosterPreviousNode;
                dataToSave.rooster1CurrentNode = roosters[0].roosterCurrentNode;
                dataToSave.rooster1NextNode = roosters[0].roosterNextNode;
                dataToSave.rooster1Position = roosters[0].position;
                dataToSave.rooster1Rotation = roosters[0].rotation;
                dataToSave.rooster1RiseRun = roosters[0].riseRun;
                dataToSave.rooster1RiseRun2 = roosters[0].riseRun2;
                dataToSave.rooster1Start = roosters[0].start;
            }
            else
            {
                dataToSave.rooster1state = -1;
            }

            //rooster2 save
            if (roosters[1] != null)
            {
                dataToSave.rooster2state = roosters[1].state;
                dataToSave.rooster2InitNode = roosters[1].roosterInitNode;
                dataToSave.rooster2PreviousNode = roosters[1].roosterPreviousNode;
                dataToSave.rooster2CurrentNode = roosters[1].roosterCurrentNode;
                dataToSave.rooster2NextNode = roosters[1].roosterNextNode;
                dataToSave.rooster2Position = roosters[1].position;
                dataToSave.rooster2Rotation = roosters[1].rotation;
                dataToSave.rooster2RiseRun = roosters[1].riseRun;
                dataToSave.rooster2RiseRun2 = roosters[1].riseRun2;
                dataToSave.rooster2Start = roosters[1].start;
            }
            else
            {
                dataToSave.rooster2state = -1;
            }

            //rooster3 save
            if (roosters[2] != null)
            {
                dataToSave.rooster3state = roosters[2].state;
                dataToSave.rooster3InitNode = roosters[2].roosterInitNode;
                dataToSave.rooster3PreviousNode = roosters[2].roosterPreviousNode;
                dataToSave.rooster3CurrentNode = roosters[2].roosterCurrentNode;
                dataToSave.rooster3NextNode = roosters[2].roosterNextNode;
                dataToSave.rooster3Position = roosters[2].position;
                dataToSave.rooster3Rotation = roosters[2].rotation;
                dataToSave.rooster3RiseRun = roosters[2].riseRun;
                dataToSave.rooster3RiseRun2 = roosters[2].riseRun2;
                dataToSave.rooster3Start = roosters[2].start;
            }
            else
            {
                dataToSave.rooster3state = -1;
            }

            //rooster4 save
            if (roosters[3] != null)
            {
                dataToSave.rooster4state = roosters[3].state;
                dataToSave.rooster4InitNode = roosters[3].roosterInitNode;
                dataToSave.rooster4PreviousNode = roosters[3].roosterPreviousNode;
                dataToSave.rooster4CurrentNode = roosters[3].roosterCurrentNode;
                dataToSave.rooster4NextNode = roosters[3].roosterNextNode;
                dataToSave.rooster4Position = roosters[3].position;
                dataToSave.rooster4Rotation = roosters[3].rotation;
                dataToSave.rooster4RiseRun = roosters[3].riseRun;
                dataToSave.rooster4RiseRun2 = roosters[3].riseRun2;
                dataToSave.rooster4Start = roosters[3].start;
            }
            else
            {
                dataToSave.rooster4state = -1;
            }

            //rooster5 save
            if (roosters[4] != null)
            {
                dataToSave.rooster5state = roosters[4].state;
                dataToSave.rooster5InitNode = roosters[4].roosterInitNode;
                dataToSave.rooster5PreviousNode = roosters[4].roosterPreviousNode;
                dataToSave.rooster5CurrentNode = roosters[4].roosterCurrentNode;
                dataToSave.rooster5NextNode = roosters[4].roosterNextNode;
                dataToSave.rooster5Position = roosters[4].position;
                dataToSave.rooster5Rotation = roosters[4].rotation;
                dataToSave.rooster5RiseRun = roosters[4].riseRun;
                dataToSave.rooster5RiseRun2 = roosters[4].riseRun2;
                dataToSave.rooster5Start = roosters[4].start;
            }
            else
            {
                dataToSave.rooster5state = -1;
            }

            //eggclass
            dataToSave.eggCount = eggCount;
            dataToSave.numEgg = numEgg;
            if (numEgg > 0)
            {
                dataToSave.egg1 = egg[0].position;
            }
            if (numEgg > 1)
            {
                dataToSave.egg2 = egg[1].position;
            }
            if (numEgg > 2)
            {
                dataToSave.egg3 = egg[2].position;
            }
            if (numEgg > 3)
            {
                dataToSave.egg4 = egg[3].position;
            }
            if (numEgg > 4)
            {
                dataToSave.egg5 = egg[4].position;
            }
            if (numEgg > 5)
            {
                dataToSave.egg6 = egg[5].position;
            }
            if (numEgg > 6)
            {
                dataToSave.egg7 = egg[6].position;
            }
            if (numEgg > 7)
            {
                dataToSave.egg8 = egg[7].position;
            }
            if (numEgg > 8)
            {
                dataToSave.egg9 = egg[8].position;
            }
            if (numEgg > 9)
            {
                dataToSave.egg10 = egg[9].position;
            }
            if (numEgg > 10)
            {
                dataToSave.egg11 = egg[10].position;
            }
            if (numEgg > 11)
            {
                dataToSave.egg12 = egg[11].position;
            }
            if (numEgg > 12)
            {
                dataToSave.egg13 = egg[12].position;
            }
            if (numEgg > 13)
            {
                dataToSave.egg14 = egg[13].position;
            }
            if (numEgg > 14)
            {
                dataToSave.egg15 = egg[14].position;
            }
            if (numEgg > 15)
            {
                dataToSave.egg16 = egg[15].position;
            }
            if (numEgg > 16)
            {
                dataToSave.egg17 = egg[16].position;
            }
            if (numEgg > 17)
            {
                dataToSave.egg18 = egg[17].position;
            }
            if (numEgg > 18)
            {
                dataToSave.egg19 = egg[18].position;
            }
            if (numEgg > 19)
            {
                dataToSave.egg20 = egg[19].position;
            }

            
            dataToSave.days = days;           //tracks number of total days
            dataToSave.dayTime = dayTime; //tracks number of seconds since day began
            dataToSave.endOfDay = endOfDay;

            //boots
            dataToSave.numBoots = numBoots;
            if (numBoots > 0)
            {
                dataToSave.boot1 = boots[0].position;
            }
            if (numBoots > 1)
            {
                dataToSave.boot2 = boots[1].position;
            }
            dataToSave.bootTime = bootTime;
            dataToSave.bootPresent = bootPresent;
            dataToSave.bootEquipped = bootEquipped;


            //fox
            dataToSave.foxWandering = fox.wandering;
            dataToSave.foxInitNode = fox.foxInitNode;
            dataToSave.foxPreviousNode = fox.foxPreviousNode;
            dataToSave.foxCurrentNode = fox.foxCurrentNode;
            dataToSave.foxNextNode = fox.foxNextNode;
            dataToSave.foxPosition = fox.position;
            dataToSave.foxPositionOld = fox.positionOld;
            dataToSave.foxRotation = fox.rotation;
            dataToSave.foxRiseRun = fox.riseRun;
            dataToSave.foxRiseRun2 = fox.riseRun2;
            dataToSave.foxChasing = fox.chasing;
            dataToSave.foxAvoiding = fox.avoiding;
            dataToSave.foxTimer = fox.timer;
            dataToSave.foxChaseTime = fox.chaseTime;
            dataToSave.foxTimer2 = fox.timer2;
            dataToSave.foxHome = fox.home;
            dataToSave.foxStart = fox.start;
            dataToSave.foxTemp1 = fox.temp1;

            StorageContainer myContainer = storageDevice.OpenContainer("Chicken_Game");
            // Here is the path to where you want to save your data
            string nameOfFile = Path.Combine(myContainer.Path, "ChickenGameSave.sav");
            // If the file doesn't exist, it will be created, if it does, it will be replaced
            FileStream fileStream = File.Open(nameOfFile, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(DataToSave));
            serializer.Serialize(fileStream, dataToSave);
            //close this file
            fileStream.Close();
            myContainer.Dispose();

        }

        private void LoadData(StorageDevice storageDevice,ref int numChic, ref int chicQueue, ref int numRooster, 
            ref int roosterQueue, ref CharacterClass player, ref ChickenClass[] chickens, ref RoosterClass[] roosters,
            ref EconomicsClass eco, ref int eggCount, ref int numEgg, ref EggClass[] egg, ref int days, ref float dayTime,
            ref bool endOfDay, ref int numBoots, ref RubberBootClass[] boots, ref float bootTime, ref bool bootPresent,
            ref bool bootEquipped, ref FoxClass fox)
        {
            StorageContainer myContainer = storageDevice.OpenContainer("Chicken_Game");
            string filename = Path.Combine(myContainer.Path, "ChickenGameSave.sav");
            if (!File.Exists(filename))
                return;
            FileStream fileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Read);
            XmlSerializer serializer = new XmlSerializer(typeof(DataToSave));
            DataToSave dataToSave = (DataToSave)serializer.Deserialize(fileStream);
            fileStream.Close();
            myContainer.Dispose();

            
            //load character
            player.position = dataToSave.cPosition;
            player.rotation = dataToSave.cRotation;
            player.hitPoints = dataToSave.hitpoints;

            //load chickens
            numChic = dataToSave.numChickens;
            chicQueue = dataToSave.ChicQueue;

            //load chicken1
            if (dataToSave.chic1time != -1)
            {
                chickens[0] = new ChickenClass(Content, graphics);
                chickens[0].InitializeChicken(0);
                chickens[0].time = dataToSave.chic1time;
                chickens[0].state = dataToSave.chic1state;
                chickens[0].ground = dataToSave.chic1ground;
                chickens[0].ChickenInitNode = dataToSave.chic1InitNode;
                chickens[0].ChickenPreviousNode = dataToSave.chic1PreviousNode;
                chickens[0].ChickenCurrentNode = dataToSave.chic1CurrentNode;
                chickens[0].chickenNextNode = dataToSave.chic1NextNode;
                chickens[0].position = dataToSave.chic1Position;
                chickens[0].rotation = dataToSave.chic1Rotation;
                chickens[0].riseRun = dataToSave.chic1RiseRun;
                chickens[0].eggLaid = dataToSave.chic1EggLaid;
            }
            //load chicken2
            if (dataToSave.chic2time != -1)
            {
                chickens[1] = new ChickenClass(Content, graphics);
                chickens[1].InitializeChicken(0);
                chickens[1].time = dataToSave.chic2time;
                chickens[1].state = dataToSave.chic2state;
                chickens[1].ground = dataToSave.chic2ground;
                chickens[1].ChickenInitNode = dataToSave.chic2InitNode;
                chickens[1].ChickenPreviousNode = dataToSave.chic2PreviousNode;
                chickens[1].ChickenCurrentNode = dataToSave.chic2CurrentNode;
                chickens[1].chickenNextNode = dataToSave.chic2NextNode;
                chickens[1].position = dataToSave.chic2Position;
                chickens[1].rotation = dataToSave.chic2Rotation;
                chickens[1].riseRun = dataToSave.chic2RiseRun;
                chickens[1].eggLaid = dataToSave.chic2EggLaid;
            }
            //load chicken3
            if (dataToSave.chic3time != -1)
            {
                chickens[2] = new ChickenClass(Content, graphics);
                chickens[2].InitializeChicken(0);
                chickens[2].time = dataToSave.chic3time;
                chickens[2].state = dataToSave.chic3state;
                chickens[2].ground = dataToSave.chic3ground;
                chickens[2].ChickenInitNode = dataToSave.chic3InitNode;
                chickens[2].ChickenPreviousNode = dataToSave.chic3PreviousNode;
                chickens[2].ChickenCurrentNode = dataToSave.chic3CurrentNode;
                chickens[2].chickenNextNode = dataToSave.chic3NextNode;
                chickens[2].position = dataToSave.chic3Position;
                chickens[2].rotation = dataToSave.chic3Rotation;
                chickens[2].riseRun = dataToSave.chic3RiseRun;
                chickens[2].eggLaid = dataToSave.chic3EggLaid;
            }
            //load chicken4
            if (dataToSave.chic4time != -1)
            {
                chickens[3] = new ChickenClass(Content, graphics);
                chickens[3].InitializeChicken(0);
                chickens[3].time = dataToSave.chic4time;
                chickens[3].state = dataToSave.chic4state;
                chickens[3].ground = dataToSave.chic4ground;
                chickens[3].ChickenInitNode = dataToSave.chic4InitNode;
                chickens[3].ChickenPreviousNode = dataToSave.chic4PreviousNode;
                chickens[3].ChickenCurrentNode = dataToSave.chic4CurrentNode;
                chickens[3].chickenNextNode = dataToSave.chic4NextNode;
                chickens[3].position = dataToSave.chic4Position;
                chickens[3].rotation = dataToSave.chic4Rotation;
                chickens[3].riseRun = dataToSave.chic4RiseRun;
                chickens[3].eggLaid = dataToSave.chic4EggLaid;
            }
            //load chicken5
            if (dataToSave.chic5time != -1)
            {
                chickens[4] = new ChickenClass(Content, graphics);
                chickens[4].InitializeChicken(0);
                chickens[4].time = dataToSave.chic5time;
                chickens[4].state = dataToSave.chic5state;
                chickens[4].ground = dataToSave.chic5ground;
                chickens[4].ChickenInitNode = dataToSave.chic5InitNode;
                chickens[4].ChickenPreviousNode = dataToSave.chic5PreviousNode;
                chickens[4].ChickenCurrentNode = dataToSave.chic5CurrentNode;
                chickens[4].chickenNextNode = dataToSave.chic5NextNode;
                chickens[4].position = dataToSave.chic5Position;
                chickens[4].rotation = dataToSave.chic5Rotation;
                chickens[4].riseRun = dataToSave.chic5RiseRun;
                chickens[4].eggLaid = dataToSave.chic5EggLaid;
            }
            //load chicken6
            if (dataToSave.chic6time != -1)
            {
                chickens[5] = new ChickenClass(Content, graphics);
                chickens[5].InitializeChicken(0);
                chickens[5].time = dataToSave.chic6time;
                chickens[5].state = dataToSave.chic6state;
                chickens[5].ground = dataToSave.chic6ground;
                chickens[5].ChickenInitNode = dataToSave.chic6InitNode;
                chickens[5].ChickenPreviousNode = dataToSave.chic6PreviousNode;
                chickens[5].ChickenCurrentNode = dataToSave.chic6CurrentNode;
                chickens[5].chickenNextNode = dataToSave.chic6NextNode;
                chickens[5].position = dataToSave.chic6Position;
                chickens[5].rotation = dataToSave.chic6Rotation;
                chickens[5].riseRun = dataToSave.chic6RiseRun;
                chickens[5].eggLaid = dataToSave.chic6EggLaid;
            }
            //load chicken7
            if (dataToSave.chic7time != -1)
            {
                chickens[6] = new ChickenClass(Content, graphics);
                chickens[6].InitializeChicken(0);
                chickens[6].time = dataToSave.chic7time;
                chickens[6].state = dataToSave.chic7state;
                chickens[6].ground = dataToSave.chic7ground;
                chickens[6].ChickenInitNode = dataToSave.chic7InitNode;
                chickens[6].ChickenPreviousNode = dataToSave.chic7PreviousNode;
                chickens[6].ChickenCurrentNode = dataToSave.chic7CurrentNode;
                chickens[6].chickenNextNode = dataToSave.chic7NextNode;
                chickens[6].position = dataToSave.chic7Position;
                chickens[6].rotation = dataToSave.chic7Rotation;
                chickens[6].riseRun = dataToSave.chic7RiseRun;
                chickens[6].eggLaid = dataToSave.chic7EggLaid;
            }
            //load chicken8
            if (dataToSave.chic8time != -1)
            {
                chickens[7] = new ChickenClass(Content, graphics);
                chickens[7].InitializeChicken(0);
                chickens[7].time = dataToSave.chic8time;
                chickens[7].state = dataToSave.chic8state;
                chickens[7].ground = dataToSave.chic8ground;
                chickens[7].ChickenInitNode = dataToSave.chic8InitNode;
                chickens[7].ChickenPreviousNode = dataToSave.chic8PreviousNode;
                chickens[7].ChickenCurrentNode = dataToSave.chic8CurrentNode;
                chickens[7].chickenNextNode = dataToSave.chic8NextNode;
                chickens[7].position = dataToSave.chic8Position;
                chickens[7].rotation = dataToSave.chic8Rotation;
                chickens[7].riseRun = dataToSave.chic8RiseRun;
                chickens[7].eggLaid = dataToSave.chic8EggLaid;
            }
            //load chicken9
            if (dataToSave.chic9time != -1)
            {
                chickens[8] = new ChickenClass(Content, graphics);
                chickens[8].InitializeChicken(0);
                chickens[8].time = dataToSave.chic9time;
                chickens[8].state = dataToSave.chic9state;
                chickens[8].ground = dataToSave.chic9ground;
                chickens[8].ChickenInitNode = dataToSave.chic9InitNode;
                chickens[8].ChickenPreviousNode = dataToSave.chic9PreviousNode;
                chickens[8].ChickenCurrentNode = dataToSave.chic9CurrentNode;
                chickens[8].chickenNextNode = dataToSave.chic9NextNode;
                chickens[8].position = dataToSave.chic9Position;
                chickens[8].rotation = dataToSave.chic9Rotation;
                chickens[8].riseRun = dataToSave.chic9RiseRun;
                chickens[8].eggLaid = dataToSave.chic9EggLaid;
            }
            //load chicken1
            if (dataToSave.chic10time != -1)
            {
                chickens[9] = new ChickenClass(Content, graphics);
                chickens[9].InitializeChicken(0);
                chickens[9].time = dataToSave.chic10time;
                chickens[9].state = dataToSave.chic10state;
                chickens[9].ground = dataToSave.chic10ground;
                chickens[9].ChickenInitNode = dataToSave.chic10InitNode;
                chickens[9].ChickenPreviousNode = dataToSave.chic10PreviousNode;
                chickens[9].ChickenCurrentNode = dataToSave.chic10CurrentNode;
                chickens[9].chickenNextNode = dataToSave.chic10NextNode;
                chickens[9].position = dataToSave.chic10Position;
                chickens[9].rotation = dataToSave.chic10Rotation;
                chickens[9].riseRun = dataToSave.chic10RiseRun;
                chickens[9].eggLaid = dataToSave.chic10EggLaid;
            }

            //economics save
            eco.money = dataToSave.money;
            eco.startEggs = dataToSave.startEggs;
            eco.startChickens = dataToSave.startChickens;
            eco.startRoosters = dataToSave.startRoosters;
            eco.startmoney = dataToSave.startmoney;
            eco.eggsCollected = dataToSave.eggsCollected;
            eco.chickenBought = dataToSave.chickenBought;
            eco.roosterBought = dataToSave.roosterBought;
            eco.eggsEaten = dataToSave.eggsEaten;
            eco.eggSold = dataToSave.eggSold;
            eco.chickenSold = dataToSave.chickenSold;
            eco.roosterSold = dataToSave.roosterSold;
            eco.chickenEaten = dataToSave.chickenEaten;
            eco.roosterEaten = dataToSave.roosterEaten;
            eco.moneyAquired = dataToSave.moneyAquired;

            //load roosters
            numRooster = dataToSave.numRooster;
            roosterQueue = dataToSave.roosterQueue;

            //rooster1 load
            if (dataToSave.rooster1state != -1)
            {
                roosters[0] = new RoosterClass(Content, graphics);
                roosters[0].InitializeRooster(13);
                roosters[0].state = dataToSave.rooster1state;
                roosters[0].roosterInitNode = dataToSave.rooster1InitNode;
                roosters[0].roosterPreviousNode = dataToSave.rooster1PreviousNode;
                roosters[0].roosterCurrentNode = dataToSave.rooster1CurrentNode;
                roosters[0].roosterNextNode = dataToSave.rooster1NextNode;
                roosters[0].position = dataToSave.rooster1Position;
                roosters[0].rotation = dataToSave.rooster1Rotation;
                roosters[0].riseRun = dataToSave.rooster1RiseRun;
                roosters[0].riseRun2 = dataToSave.rooster1RiseRun2;
                roosters[0].start = dataToSave.rooster1Start;
            }

            //rooster2 load
            if (dataToSave.rooster2state != -1)
            {
                roosters[1] = new RoosterClass(Content, graphics);
                roosters[1].InitializeRooster(13);
                roosters[1].state = dataToSave.rooster2state;
                roosters[1].roosterInitNode = dataToSave.rooster2InitNode;
                roosters[1].roosterPreviousNode = dataToSave.rooster2PreviousNode;
       
                roosters[1].roosterCurrentNode = dataToSave.rooster2CurrentNode;
                roosters[1].roosterNextNode = dataToSave.rooster2NextNode;
                roosters[1].position = dataToSave.rooster2Position;
                roosters[1].rotation = dataToSave.rooster2Rotation;
                roosters[1].riseRun = dataToSave.rooster2RiseRun;
                roosters[1].riseRun2 = dataToSave.rooster2RiseRun2;
                roosters[1].start = dataToSave.rooster2Start;
            }

            //rooster3 load
            if (dataToSave.rooster3state != -1)
            {
                roosters[2] = new RoosterClass(Content, graphics);
                roosters[2].InitializeRooster(13);
                roosters[2].state = dataToSave.rooster3state;
                roosters[2].roosterInitNode = dataToSave.rooster3InitNode;
                roosters[2].roosterPreviousNode = dataToSave.rooster3PreviousNode;
                roosters[2].roosterCurrentNode = dataToSave.rooster3CurrentNode;
                roosters[2].roosterNextNode = dataToSave.rooster3NextNode;
                roosters[2].position = dataToSave.rooster3Position;
                roosters[2].rotation = dataToSave.rooster3Rotation;
                roosters[2].riseRun = dataToSave.rooster3RiseRun;
                roosters[2].riseRun2 = dataToSave.rooster3RiseRun2;
                roosters[2].start = dataToSave.rooster3Start;
            }

            //rooster4 load
            if (dataToSave.rooster4state != -1)
            {
                roosters[3] = new RoosterClass(Content, graphics);
                roosters[3].InitializeRooster(13);
                roosters[3].state = dataToSave.rooster4state;
                roosters[3].roosterInitNode = dataToSave.rooster4InitNode;
                roosters[3].roosterPreviousNode = dataToSave.rooster4PreviousNode;
                roosters[3].roosterCurrentNode = dataToSave.rooster4CurrentNode;
                roosters[3].roosterNextNode = dataToSave.rooster4NextNode;
                roosters[3].position = dataToSave.rooster4Position;
                roosters[3].rotation = dataToSave.rooster4Rotation;
                roosters[3].riseRun = dataToSave.rooster4RiseRun;
                roosters[3].riseRun2 = dataToSave.rooster4RiseRun2;
                roosters[3].start = dataToSave.rooster4Start;
            }

            //rooster5 load
            if (dataToSave.rooster5state != -1)
            {
                roosters[4] = new RoosterClass(Content, graphics);
                roosters[4].InitializeRooster(13);
                roosters[4].state = dataToSave.rooster5state;
                roosters[4].roosterInitNode = dataToSave.rooster5InitNode;
                roosters[4].roosterPreviousNode = dataToSave.rooster5PreviousNode;
                roosters[4].roosterCurrentNode = dataToSave.rooster5CurrentNode;
                roosters[4].roosterNextNode = dataToSave.rooster5NextNode;
                roosters[4].position = dataToSave.rooster5Position;
                roosters[4].rotation = dataToSave.rooster5Rotation;
                roosters[4].riseRun = dataToSave.rooster5RiseRun;
                roosters[4].riseRun2 = dataToSave.rooster5RiseRun2;
                roosters[4].start = dataToSave.rooster5Start;
            }

            //eggclass
            eggCount = dataToSave.eggCount;
            numEgg = dataToSave.numEgg;
            if (numEgg > 0)
            {
                egg[0].position = new Vector3();
                egg[0].position = new Vector3(dataToSave.egg1.X, dataToSave.egg1.Y, dataToSave.egg1.Z);
            }
            if (numEgg > 1)
            {
                egg[1].position = new Vector3();
                egg[1].position = new Vector3(dataToSave.egg2.X, dataToSave.egg2.Y, dataToSave.egg2.Z);
            }
            if (numEgg > 2)
            {
                egg[2].position = new Vector3();
                egg[2].position = new Vector3(dataToSave.egg3.X,dataToSave.egg3.Y,dataToSave.egg3.Z);
            }
            if (numEgg > 3)
            {
                egg[3].position = new Vector3();
                egg[3].position = new Vector3(dataToSave.egg4.X,dataToSave.egg4.Y, dataToSave.egg4.Z);
            }
            if (numEgg > 4)
            {
                egg[4].position = new Vector3();
                egg[4].position = new Vector3(dataToSave.egg5.X, dataToSave.egg5.Y, dataToSave.egg5.Z);
            }
            if (numEgg > 5)
            {
                egg[5].position = new Vector3();
                egg[5].position = new Vector3(dataToSave.egg6.X, dataToSave.egg6.Y, dataToSave.egg6.Z);
            }
            if (numEgg > 6)
            {
                egg[6].position = new Vector3();
                egg[6].position = new Vector3(dataToSave.egg7.X, dataToSave.egg7.Y, dataToSave.egg7.Z);
            }
            if (numEgg > 7)
            {
                egg[7].position = new Vector3();
                egg[7].position = new Vector3(dataToSave.egg8.X, dataToSave.egg8.Y, dataToSave.egg8.Z);
            }
            if (numEgg > 8)
            {
                egg[8].position = new Vector3();
                egg[8].position = new Vector3(dataToSave.egg9.X, dataToSave.egg9.Y, dataToSave.egg9.Z);
            }
            if (numEgg > 9)
            {
                egg[9].position = new Vector3();
                egg[9].position = new Vector3(dataToSave.egg10.X, dataToSave.egg10.Y, dataToSave.egg10.Z);
            }
            if (numEgg > 10)
            {
                egg[10].position = new Vector3();
                egg[10].position = new Vector3(dataToSave.egg11.X, dataToSave.egg11.Y, dataToSave.egg11.Z);
            }
            if (numEgg > 11)
            {
                egg[11].position = new Vector3();
                egg[11].position = new Vector3(dataToSave.egg12.X, dataToSave.egg12.Y, dataToSave.egg12.Z);
            }
            if (numEgg > 12)
            {
                egg[12].position = new Vector3();
                egg[12].position = new Vector3(dataToSave.egg13.X, dataToSave.egg13.Y, dataToSave.egg13.Z);
            }
            if (numEgg > 13)
            {
                egg[13].position = new Vector3();
                egg[13].position = new Vector3(dataToSave.egg14.X, dataToSave.egg14.Y, dataToSave.egg14.Z);
            }
            if (numEgg > 14)
            {
                egg[14].position = new Vector3();
                egg[14].position = new Vector3(dataToSave.egg15.X, dataToSave.egg15.Y, dataToSave.egg15.Z);
            }
            if (numEgg > 15)
            {
                egg[15].position = new Vector3();
                egg[15].position = new Vector3(dataToSave.egg16.X, dataToSave.egg16.Y, dataToSave.egg16.Z);
            }
            if (numEgg > 16)
            {
                egg[16].position = new Vector3();
                egg[16].position = new Vector3(dataToSave.egg17.X, dataToSave.egg17.Y, dataToSave.egg17.Z);
            }
            if (numEgg > 17)
            {
                egg[17].position = new Vector3();
                egg[17].position = new Vector3(dataToSave.egg18.X, dataToSave.egg18.Y, dataToSave.egg18.Z);
            }
            if (numEgg > 18)
            {
                egg[18].position = new Vector3();
                egg[18].position = new Vector3(dataToSave.egg19.X, dataToSave.egg19.Y, dataToSave.egg19.Z);
            }
            if (numEgg > 19)
            {
                egg[19].position = new Vector3();
                egg[19].position = new Vector3(dataToSave.egg20.X, dataToSave.egg20.Y, dataToSave.egg20.Z);
            }

            days = dataToSave.days;
            dayTime = dataToSave.dayTime; 
            endOfDay = dataToSave.endOfDay;

            //boots
            numBoots = dataToSave.numBoots;
            if (numBoots > 0)
            {
                boots[0] = new RubberBootClass(Content, graphics);
                boots[0].position = dataToSave.boot1; 
            }
            if (numBoots > 1)
            {
                boots[1] = new RubberBootClass(Content, graphics);
                boots[1].position = dataToSave.boot2;
            }
            bootTime = dataToSave.bootTime;
            bootPresent = dataToSave.bootPresent;
            bootEquipped = dataToSave.bootEquipped;

            //fox
            fox.wandering = dataToSave.foxWandering;
            fox.foxInitNode = dataToSave.foxInitNode;
            fox.foxPreviousNode = dataToSave.foxPreviousNode;
            fox.foxCurrentNode = dataToSave.foxCurrentNode;
            fox.foxNextNode = dataToSave.foxNextNode;
            fox.position = dataToSave.foxPosition;
            fox.positionOld = dataToSave.foxPositionOld;
            fox.rotation = dataToSave.foxRotation;
            fox.riseRun = dataToSave.foxRiseRun;
            fox.riseRun2 = dataToSave.foxRiseRun2;
            fox.chasing = dataToSave.foxChasing;
            fox.avoiding = dataToSave.foxAvoiding;
            fox.timer = dataToSave.foxTimer;
            fox.chaseTime = dataToSave.foxChaseTime;
            fox.timer2 = dataToSave.foxTimer2;
            fox.home = dataToSave.foxHome;
            fox.start = dataToSave.foxStart;
            fox.temp1 = dataToSave.foxTemp1;



        }
    }
}