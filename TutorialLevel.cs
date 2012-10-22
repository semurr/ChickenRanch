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


namespace Chicken
{
    public class TutorialLevel
    {
        GraphicsDeviceManager graphics;
        ContentManager content;
        private Model myScene;

        //camera
        //camera variables
        private float aspectRatio;
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

        //variable in all stages
        public int stage;
        GamePadState gamePadState;
        KeyboardState keyBoardState;
        
        //character
        CharacterClass Character;

        //chicken
        ChickenClass[] Chicken = new ChickenClass[1];
        public int layEgg;
        public int numCount;

        //rooster
        RoosterClass[] Rooster = new RoosterClass[1];
        public int numRooster = 1;

        //egg
        //egg variables
        EggClass[] eggList = new EggClass[100];
        public int numEgg;

        //broken egg
        BrokenEggClass[] brokenEggList = new BrokenEggClass[100];
        public int numBrokeEgg = 0; //number of broken eggs on ground

        //egg shadow variables
        EggShadowClass[] shadowList = new EggShadowClass[100];

        //fox variables
        FoxClass myFox;
        public Vector3 tmpPosition = new Vector3(0, 0, 0);

        
        //eggslip
        public float displacement = 0;
        public float speed = 15.0f;
        public float force = 2.0f;
        public bool moveBack = false;
        public bool slip = false;
        public bool noSlip = false;

        //boots
        RubberBootClass[] myBootList = new RubberBootClass[2];
        public int numBoots = 0;
        public bool bootsEquipped = false;
        public bool bootPresent = false;

        //which to draw/update
        public bool chickenB;
        public bool roosterB;
        public bool foxB;

        //stage 0 values
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        //stage 1 values
        public bool cUp;
        public bool cDown;
        public bool cIn;
        public bool cOut;
        public bool menustage;

        public bool buttonDown;
        public int buttonstage;


        //stage 2 automatic
        //stage 3 automatic


        public TutorialLevel(ContentManager _content, GraphicsDeviceManager _graphics)
        {
            graphics = _graphics;
            content = _content;
        }
        public void initilizeTutorialLevel()
        {
            myScene = content.Load<Model>("Models\\farmScene[8]");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            //camera 

            cameraViewMatrix = Matrix.CreateLookAt(
               cameraPosition,
               cameraLookAt,
               Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                10000.0f * 50);
           


            //intilize all values
            stage = 0;
            buttonDown = false;
            //stage0
            up = false;
            down = false;
            left = false;
            right = false;
            //stage1
            cUp = false;
            cDown = false;
            cIn = false;
            cOut = false;
            menustage = false;

            //character
            Character = new CharacterClass(content, graphics);
            Character.InitializeCharacter();
            layEgg = 0;
            numCount = 0;

            //chicken
            Chicken[0] = new ChickenClass(content, graphics);
            Chicken[0].InitializeChicken(0);

            //Rooster
            Rooster[0] = new RoosterClass(content, graphics);
            Rooster[0].InitializeRooster(13);
            numRooster = 1;

            //egg
            eggList[0] = new EggClass(content, graphics);
            eggList[0].initializeEgg();
            numEgg = 0;

            //borken egg
            numBrokeEgg = 0;
            brokenEggList[0] = new BrokenEggClass(content, graphics);
            brokenEggList[0].initializeBrokenEgg();

            //boots
            myBootList[0] = new RubberBootClass(content, graphics);
            myBootList[0].initializeBoots();

            //initilize fox
            myFox = new FoxClass(content, graphics);
            myFox.initializeFox(new Vector3(3600, 80, 3600), 1);

            //eggslip
            displacement = 0;
            speed = 15.0f;
            force = 2.0f;
            moveBack = false;
            slip = false;
            noSlip = false;
            buttonstage = 0;

            //which to draw/update
            chickenB = false;
            roosterB = false;
            foxB = false;
            tmpPosition = new Vector3(0, 0, 0);

            //camera
            cameraLookAt = new Vector3(3500, 0, 3500);
            cameraPosition = new Vector3(3500.0f, 450.0f, 6000.0f); //initial camera position
            camRearOffset = 1000.0f; //default distance between camera and character
            camHeightOffset = 800f; //default distance between camera and ground
            maxCamHeight = 2000f;
            minCamHeight = 200f;
            maxCamOffset = 3000f;
            minCamOffset = 200f;
        }
        public void updateTutorialLevel(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            keyBoardState = Keyboard.GetState();
            cameraLookAt = Character.position;

            //camera position follows character movement
            cameraPosition.X = Character.position.X - camRearOffset
                * (float)Math.Sin(Character.rotation);
            cameraPosition.Z = Character.position.Z - camRearOffset
                * (float)Math.Cos(Character.rotation);

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

            if (chickenB == true)
            {
                Chicken[0].update(gameTime, layEgg, ref GameUI.gameWorld.instance.eggCount);
            }

            if (roosterB == true)
            {
                Rooster[0].update(gameTime, Character.position, ref GameUI.gameWorld.instance.myCharacter.hitPoints);
            }

            if (foxB == true)
            {
                myFox.update(gameTime, ref Chicken, ref Rooster,
                    ref numRooster, ref numCount, tmpPosition,
                    0, 0, GameUI.gameWorld.instance.player);
            }
            
            switch (stage)
            {
                case 0: // stage 0 character movement
                    stage0();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 1: // stage 1 camera movement
                    stage1();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);      
                    break;
                case 2: // stage 2 introduce chicken into the farm
                    stage2();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 3: // stage 3 introduce rooster, non chasing
                    stage3();
                    if (menustage == false)
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    }
                    else
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 2); //for phase 2 within tutorial level code
                    }
                    break;
                case 4: // stage 4 catch a falling egg
                    stage4();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 5: // stage 5 broken egg fall
                    stage5();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 6: // boots walk over broken eggs
                    stage6();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 7: //rooster attack character
                    stage7();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 8: // eat eggs to replenish life
                    stage8();
                    GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    break;
                case 9: // fox introduced and attack chicken and rooster
                    stage9();
                    if (menustage == false)
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    }
                    else
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 2);
                    }
                    //updateTutorialBox( stage, 2) for phase 2 within tutorial level code
                    break;
                case 10: //resource menu
                    GameUI.tutorialScreen.instance.menuPanelTutorial.visible = true;
                    stage10();
                    if (menustage == false)
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 1);
                    }
                    else
                    {
                        GameUI.tutorialScreen.instance.updateTutorialBox(stage, 2);
                    }
                   //updateTutorialBox( stage, 2) for phase 2 within tutorial level code
                    break;
                case 11: // tutorial level finish
                    Game1.instance.tutorial = false;
                    Game1.instance.setGameState(Game1.GameState.start);
                    break;
                default: //error
                    break;
            }

            //updates each egg
            for (int i = 0; i < numEgg; i++)
            {
                eggList[i].update(gameTime);

                //checks to see if egg hits character
                if (Vector3.Distance(eggList[i].position, Character.position) < 100 && stage == 4)
                {
                    eggList[i] = null;
                    GameUI.gameWorld.instance.eggCount++;
                    GameUI.gameWorld.instance.player.eggsCollected++;
                    decrementEgg(i);
                    //decrementEggShadow(i);
                    numEgg--;
                }
                //checks to see if egg hits the ground
                else if (eggList[i].position.Y <= 185)
                {
                    if (AudioManager.instance.sFXOn == true)
                    {
                        AudioManager.instance.eggCrackSound.Play();
                    }
                    addBrokenEgg(eggList[i].position);
                    eggList[i] = null; //makes egg disappear
                    decrementEgg(i);
                    numEgg--;

                }
            }

            //update broken egg
            for (int i = 0; i < numBrokeEgg; i++)
            {
                brokenEggList[i].update(gameTime);
            }
            
        }
        //function to draw
        public void drawTutorialLevel(GameTime gameTime)
        {
           
            cameraViewMatrix = Matrix.CreateLookAt(
                cameraPosition,
                cameraLookAt,
                Vector3.Up);

            cameraProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                10000.0f*50);

            Character.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);

            if (chickenB == true)
            {
                Chicken[0].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            if (roosterB == true)
            {
                Rooster[0].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }

            if (foxB == true)
            {
                myFox.draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
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

            //draw broken egg
            for (int i = 0; i < numBrokeEgg; i++)
            {
                brokenEggList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }
            //draw boots
            for (int i = 0; i < numBoots; i++)
            {
                myBootList[i].draw(gameTime, cameraViewMatrix, cameraProjectionMatrix);
            }





            Matrix[] transforms3 = new Matrix[myScene.Bones.Count];
            myScene.CopyAbsoluteBoneTransformsTo(transforms3);

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

        }
        //stage 0
        public void stage0()
        {
         //text for intro
            // character movement
            //character movement with Xbox controller input

            Character.moveNormal(cameraPosition, ref cameraViewMatrix);

            if (keyBoardState.IsKeyDown(Keys.Up) || gamePadState.ThumbSticks.Left.Y > 0)
            {
                up = true;
            }
            if (keyBoardState.IsKeyDown(Keys.Down) || gamePadState.ThumbSticks.Left.Y < 0)
            {
                down = true;
            }

            if (keyBoardState.IsKeyDown(Keys.Left) || gamePadState.ThumbSticks.Left.X > 0)
            {
                left = true;
            }

            if (keyBoardState.IsKeyDown(Keys.Right) || gamePadState.ThumbSticks.Left.X < 0)
            {
                right = true;
            }

            if (up == true && down == true && left == true && right == true)
            {
                stage++;
            }               
        }

        public void stage1()
        {
            Character.moveNormal(cameraPosition, ref cameraViewMatrix);

            if (keyBoardState.IsKeyDown(Keys.W) || gamePadState.ThumbSticks.Right.Y > 0)
            {
                cUp = true;
            }
            if (keyBoardState.IsKeyDown(Keys.S) || gamePadState.ThumbSticks.Right.Y < 0)
            {
                cDown = true;
            }

            if (keyBoardState.IsKeyDown(Keys.A) || gamePadState.ThumbSticks.Right.X > 0)
            {
                cIn = true;
            }

            if (keyBoardState.IsKeyDown(Keys.D) || gamePadState.ThumbSticks.Right.X < 0)
            {
                cOut = true;
            }

            if (cUp == true && cDown == true && cIn == true && cOut == true)
            {
                stage++;
            }
        }

        public void stage2()
        {
            //text to explain chicken

            //draw and update chicken
            chickenB = true;
            cameraLookAt = Chicken[0].position;

            //keyboard
            if(keyBoardState.IsKeyDown(Keys.Space))
            {
                buttonDown = true;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true)
            {
                buttonDown = false;
                stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A))
            {
                buttonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true)
            {
                buttonDown = false;
                stage++;
            }

        }

        public void stage3()
        {
            roosterB = true;
            cameraLookAt = Rooster[0].position;
            Rooster[0].state = 1;

            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space) && buttonstage == 0)
            {
                menustage = true;
                buttonDown = true;
                
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true && buttonstage ==0)
            {
                buttonDown = false;
               // stage++;
                buttonstage = 1;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A))
            {
                buttonDown = true;
                //buttonstage = 1;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true)
            {
                buttonDown = false;
                buttonstage = 1;
                //stage++;
            }

            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space) && buttonstage == 1)
            {
                menustage = true;
                buttonDown = true;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true && buttonstage == 1)
            {
                buttonDown = false;
                buttonstage = 0;
                stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A) && buttonstage == 1)
            {
                buttonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true && buttonstage == 1)
            {
                buttonstage = 0;
                buttonDown = false;
                stage++;
            }
        }

        public void stage4()
        {
            
            Character.moveNormal(cameraPosition, ref cameraViewMatrix);

            cameraLookAt = Character.position;
            Rooster[0].state = 1;
            layEgg = 1;

            if (Chicken[0].eggLaid == true)
            {
                addEgg(Chicken[0].position);
                if (AudioManager.instance.sFXOn == true)
                {
                    AudioManager.instance.chickenEggLaySound.Play();
                }
            }

            if (GameUI.gameWorld.instance.eggCount > 0)
            {
                stage++;
            }

        }

        public void stage5()
        {
            Character.moveNormal(cameraPosition, ref cameraViewMatrix);

            cameraLookAt = Character.position;
            Rooster[0].state = 1;
            layEgg = 1;

            if (Chicken[0].eggLaid == true)
            {
                addEgg(Chicken[0].position);
                if (AudioManager.instance.sFXOn == true)
                {
                    AudioManager.instance.chickenEggLaySound.Play();
                }
            }

            //if there are broken eggs on ground
            for (int i = 0; i < numBrokeEgg; i++)
            {
                //test to see if character is walking on broken eggs
                //if on eggs, slide until off
                if (Vector3.Distance(Character.position, brokenEggList[i].position) <= 250)
                {
                    //slide code
                    AudioManager.instance.setSlipEffectInstance(AudioManager.instance.eggSlipSound);
                    if (AudioManager.instance.sFXOn == true && AudioManager.instance.sFXeggSlipInstance.State
                        != SoundState.Playing && GameUI.gameWorld.instance.isPaused != true)
                    {

                        AudioManager.instance.sFXeggSlipInstance.Volume = 0.1f;
                        AudioManager.instance.sFXeggSlipInstance.Play();
                    }
                    displacement = ((speed * speed) / 2) / force;
                    if (moveBack == true)
                    {
                        Character.position.X -= displacement * (float)Math.Sin(Character.rotation);
                        Character.position.Z -= displacement * (float)Math.Cos(Character.rotation);
                        slip = true;
                    }
                    else
                    {
                        Character.position.X += displacement * (float)Math.Sin(Character.rotation);
                        Character.position.Z += displacement * (float)Math.Cos(Character.rotation);
                        slip = true;
                    }
                    if (slip == true && Vector3.Distance(Character.position, brokenEggList[i].position) > 250)
                    {
                        stage++;
                    }
                }
            }

        }

        public void stage6()
        {
            Character.moveNormal(cameraPosition, ref cameraViewMatrix);
            Rooster[0].state = 1;


            if (Chicken[0].eggLaid == true)
            {
                addEgg(Chicken[0].position);
                if (AudioManager.instance.sFXOn == true)
                {
                    AudioManager.instance.chickenEggLaySound.Play();
                }
            }
            if (bootPresent == false)
            {
                addBoots();
                bootPresent = true;
            }

            for (int i = 0; i < numBoots; i++)
            {
                //delete boots
                //checks to see if character picks up boots
                if (Vector3.Distance(myBootList[i].position, Character.position) < 200)
                {
                    // audio play when boots are picked up
                    AudioManager.instance.itemCollectSound.Play();
                    GameUI.gameInterface.instance.bootIsEquipped = true;
                    myBootList[i] = null;
                    bootsEquipped = true;
                    deleteBoots(i);
                    numBoots--;
                }
            }

            for (int i = 0; i < numBrokeEgg; i++)
            {
                if (Vector3.Distance(Character.position, brokenEggList[i].position) <= 250 && bootsEquipped == true)
                {
                    noSlip = true;
                    
                }

                if (Vector3.Distance(Character.position, brokenEggList[i].position) > 250 && noSlip == true)
                {
                    stage++;
                }
            }




        }
        public void stage7()
        {
            if (Vector3.Distance(Rooster[0].position, Character.position) < 100)
            {
                Rooster[0].state = 3;
            }
            else
            {
                Rooster[0].state = 2;
            }

            if (GameUI.gameWorld.instance.myCharacter.hitPoints < Character.maxHP)
            {
                stage++;
            }
        }
        public void stage8()
        {
            Rooster[0].state = 1;

            if (keyBoardState.IsKeyDown(Keys.E) || gamePadState.IsButtonDown(Buttons.Y))
            {
                stage++;
                GameUI.gameWorld.instance.myCharacter.hitPoints += 10;
            }

        }
        public void stage9()
        {
            Character.moveNormal(cameraPosition, ref cameraViewMatrix);
            foxB = true;


            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space))
            {
                menustage = true;
                buttonDown = true;
                buttonstage = 1;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true)
            {
                buttonDown = false;
                //stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A))
            {
                buttonDown = true;
                buttonstage = 1;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true)
            {
                buttonDown = false;
                //stage++;
            }

            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space) && buttonstage == 1)
            {
                menustage = true;
                buttonDown = true;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true && buttonstage == 1)
            {
                buttonDown = false;
                buttonstage = 0;
                //stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A) && buttonstage == 1)
            {
                buttonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true && buttonstage == 1)
            {
                buttonstage = 0;
                buttonDown = false;
                //stage++;
            }

            if (numRooster == 0)
            {
                stage++;
                roosterB = false;
            }
            else
            {
                Rooster[0].state = 1;
            }

        }

        public void stage10()
        {
            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space) && buttonstage == 0)
            {
                menustage = true;
                buttonDown = true;
                buttonstage = 1;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true && buttonstage == 0)
            {
                buttonDown = false;
                //stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A) && buttonstage == 0)
            {
                buttonDown = true;
                buttonstage = 1;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true && buttonstage == 0)
            {
                buttonDown = false;
                //stage++;
            }

            //keyboard
            if (keyBoardState.IsKeyDown(Keys.Space) && buttonstage == 1)
            {
                menustage = true;
                buttonDown = true;
            }

            if (keyBoardState.IsKeyUp(Keys.Space) && buttonDown == true && buttonstage == 1)
            {
                buttonDown = false;
                buttonstage = 0;
                stage++;
            }

            //gamepad
            if (gamePadState.IsButtonDown(Buttons.A) && buttonstage == 1)
            {
                buttonDown = true;
            }

            if (gamePadState.IsButtonDown(Buttons.A) && buttonDown == true && buttonstage == 1)
            {
                buttonstage = 0;
                buttonDown = false;
                stage++;
            }

            if (numRooster > 0)
            {
                stage++;
            }
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
        public void addEgg(Vector3 chickenPosition)
        {
            eggList[numEgg] = new EggClass(content, graphics);
            eggList[numEgg].position = chickenPosition;
            addEggShadow(chickenPosition);
            numEgg++;
        }
        public void addEggShadow(Vector3 chickenPosition)
        {
            shadowList[numEgg] = new EggShadowClass(content, graphics);
            shadowList[numEgg].position = chickenPosition;
            shadowList[numEgg].position.Y = 185f;
        }
        public void addBrokenEgg(Vector3 eggPosition)
        {
            brokenEggList[numBrokeEgg] = new BrokenEggClass(content, graphics);
            brokenEggList[numBrokeEgg].position = eggPosition;
            numBrokeEgg++;
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


    }
}
