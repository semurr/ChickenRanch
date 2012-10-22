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
    class CharacterClass
    {
        ContentManager content;
        //basic character stats
        public Model myCharacter;
        public float scale = 1.0f;
        float aspectRatio;
        public float elapsed = 0.0f;
        public float pi = 3.14f;
        public float radius = 5.0f;

        //character hit point variables
        public int hitPoints = 200;
        public int maxHP = 200;

        //character movement variables
        public Vector3 position = new Vector3(1500, 363, 5700);//1500,360,5700
        public float rotation = 2.968f; //rotation in radians
        public float rotationDeg = 0.0f; //rotation in degrees
        public float speed = 15.0f;
        public float force = 2.0f;
        public float displacement = 0.0f;
        public float strafRotation = 0.0f;
        public bool moveBack = false;
        
        //farmyard wall position
        public float leftBorder = 625f;
        public float rightBorder = 5400f;
        public float topBorder = 375f;
        public float bottomBorder = 6000f;

        //barn wall positions
        public float barnTop = 1600f;
        public float barnTop2 = 1700f;
        public float barnBottom = 2650f;
        public float barnBottom2 = 2550f;
        public float barnLeftSide = 2000f;
        public float barnLeft2 = 2100f;
        public float barnRightSide = 3000f;
        public float barnRight2 = 2900f;

        //beam1 positions
        public float beam1Top = 2050;
        public float beam1Top2 = 2100;
        public float beam1Bottom = 2250;
        public float beam1Bottom2 = 2200;
        public float beam1Right = 4150;
        public float beam1Right2 = 4100;
        public float beam1Left = 3875;
        public float beam1Left2 = 3925;
        
        //beam2 positions
        public float beam2Top = 4100;
        public float beam2Top2 = 4150;
        public float beam2Bottom = 4325;
        public float beam2Bottom2 = 4275;
        public float beam2Right = 4150;
        public float beam2Right2 = 4100;
        public float beam2Left = 3925;
        public float beam2Left2 = 4025;

        //beam3 positions
        public float beam3Top = 3050;
        public float beam3Top2 = 3100;
        public float beam3Bottom = 3250;
        public float beam3Bottom2 = 3200;
        public float beam3Right = 3650;
        public float beam3Right2 = 3600;
        public float beam3Left = 3200;
        public float beam3Left2 = 3250;

        //beam4 positions
        public float beam4Top = 4100;
        public float beam4Top2 = 4150;
        public float beam4Bottom = 4325;
        public float beam4Bottom2 = 4275;
        public float beam4Right = 2800;
        public float beam4Right2 = 2750;
        public float beam4Left = 2250;
        public float beam4Left2 = 2300;

        //character hotkey
        bool buttonDown = false;
        bool buttonDown2 = false;

        public CharacterClass(ContentManager _content, GraphicsDeviceManager graphics)
        {
            //load my rooster
            content = _content;
            myCharacter = content.Load<Model>("Models\\farmBoy[final2]");//default
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        }

        public void InitializeCharacter(string _modelName)
        {
            myCharacter = content.Load<Model>(_modelName);//default
            hitPoints = 200;
        }
        public void InitializeCharacter()
        {
        }

        public void update(GameTime gameTime, BrokenEggClass[] brokenEggLst,
                            int numBrokeEgg, bool bootEquipped, ref Vector3 cameraPosition,
                            ref Vector3 cameraLookAt, ref Matrix cameraViewMatrix, EconomicsClass player, 
                            ref int totalEggs)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveNormal(cameraPosition, ref cameraViewMatrix);
           
            //if there are broken eggs on ground
            for (int i = 0; i < numBrokeEgg; i++)
            {
                //test to see if character is walking on broken eggs
                //if on eggs, slide until off
                if (Vector3.Distance(position, brokenEggLst[i].position) <= 250 && bootEquipped == false)
                {
                    //slide code
                    Chicken.AudioManager.instance.setSlipEffectInstance(Chicken.AudioManager.instance.eggSlipSound);
                    if (Chicken.AudioManager.instance.sFXOn == true && Chicken.AudioManager.instance.
                        sFXeggSlipInstance.State != SoundState.Playing && GameUI.gameWorld.instance.isPaused != true)
                    {

                        Chicken.AudioManager.instance.sFXeggSlipInstance.Volume = 0.1f;
                        Chicken.AudioManager.instance.sFXeggSlipInstance.Play();
                    }
                    displacement = ((speed * speed) / 2) / force;
                    if (moveBack == true)
                    {
                        position.X -= displacement * (float)Math.Sin(rotation);
                        position.Z -= displacement * (float)Math.Cos(rotation);
                    }
                    else
                    {
                        position.X += displacement * (float)Math.Sin(rotation);
                        position.Z += displacement * (float)Math.Cos(rotation);
                    }
                }
            }

            //hotkey for eat egg
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyBoardState = Keyboard.GetState();

            //eat egg gamepad hotkey
            if (gamePadState.IsButtonDown(Buttons.Y) && buttonDown == false)
            {
                //eat egg
                player.eatEgg(ref hitPoints, maxHP, ref totalEggs);
                buttonDown = true;
            }
            if (gamePadState.IsButtonUp(Buttons.Y) && buttonDown == true)
            {
                buttonDown = false;
            }


            //eat egg keyboard hotkey
            if (keyBoardState.IsKeyDown(Keys.E) && buttonDown2 == false)
            {
                //eat egg
                player.eatEgg(ref hitPoints, maxHP, ref totalEggs);
                buttonDown2 = true;
            }

            if (keyBoardState.IsKeyUp(Keys.E) && buttonDown2 == true)
            {
                buttonDown2 = false;
            }

        }


        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            //draw character
            Matrix[] transforms2 = new Matrix[myCharacter.Bones.Count];
            myCharacter.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myCharacter.Meshes)
            {
                foreach (BasicEffect effect in mesh2.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = transforms2[mesh2.ParentBone.Index] *
                         Matrix.CreateScale(scale) *
                         Matrix.CreateRotationY(rotation) *
                    Matrix.CreateTranslation(position);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh2.Draw();
            }
            // draw(gameTime);
        }

        public void moveNormal(Vector3 cameraPosition, ref Matrix cameraViewMatrix)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyBoardState = Keyboard.GetState();

            Console.WriteLine(rotation);
            
            radiansToDegrees();

            //character movement with Xbox controller input
            if (gamePadState.ThumbSticks.Left.Y > 0)
            {
                position.X += speed * (float)Math.Sin(rotation);
                position.Z += speed * (float)Math.Cos(rotation);
                moveBack = false;
            }
            else if (gamePadState.ThumbSticks.Left.Y < 0)
            {
                position.X -= speed * (float)Math.Sin(rotation);
                position.Z -= speed * (float)Math.Cos(rotation);
                moveBack = true;
            }

            //changes the direction the character is facing
            rotation -= gamePadState.ThumbSticks.Left.X * 0.02f;

            //character movement with keyboard input
            if (keyBoardState.IsKeyDown(Keys.Down))
            {
                //moves character backward
                position.X -= speed * (float)Math.Sin(rotation);
                position.Z -= speed * (float)Math.Cos(rotation);
                moveBack = true;
            }
            if (keyBoardState.IsKeyDown(Keys.Up))
            {
                //moves character forward
                position.X += speed * (float)Math.Sin(rotation);
                position.Z += speed * (float)Math.Cos(rotation);
                moveBack = false;
            }
            if (keyBoardState.IsKeyDown(Keys.Left))
            {
                //turns character left
                rotation += 0.02f;
            }
            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                //turns character right
                rotation -= 0.02f;
            }
            //if (keyBoardState.IsKeyDown(Keys.Q))
            //{
            //    //slides character left while facing same direction
            //    strafRotation = rotationDeg + 90;
            //    position.X += speed * (float)Math.Sin(strafRotation);
            //    position.Z += speed * (float)Math.Cos(strafRotation);
            //}
            //if (keyBoardState.IsKeyDown(Keys.E))
            //{
            //    //slides character right while facing same direction
            //    strafRotation = rotationDeg - 90;
            //    position.X += speed * (float)Math.Sin(strafRotation);
            //    position.Z += speed * (float)Math.Cos(strafRotation);
            //}
            getMovementState();
        }

        public void radiansToDegrees()
        {
            rotationDeg = MathHelper.ToDegrees(rotation);

            while (rotationDeg < 0)
            {
                rotationDeg += 360;
            }

            while (rotationDeg >= 360)
            {
                rotationDeg -= 360;
            }
            rotation = MathHelper.ToRadians(rotationDeg);
        }

        public void getMovementState()
        {
            //tests to see if character is leaving farmyard
            if (position.X < leftBorder)
            {
                position.X = leftBorder;
            }
            if (position.X > rightBorder)
            {
                position.X = rightBorder;
            }
            if (position.Z < topBorder)
            {
                position.Z = topBorder;
            }
            if (position.Z > bottomBorder)
            {
                position.Z = bottomBorder;
            }

            radiansToDegrees();
            Console.WriteLine(rotationDeg);
            

            //tests to see if character is going inside barn
            //test left side of barn
            if (position.X > barnLeftSide && position.X < barnLeft2 &&
                position.Z > barnTop && position.Z < barnBottom)
            {
                if (rotationDeg > 0 && rotationDeg < 180 || rotationDeg < 360 && rotationDeg > 180) 
                {
                    position.X = barnLeftSide;
                }
            }
            //test right side of barn
            if (position.X < barnRightSide && position.X >= barnRight2 &&
             position.Z > barnTop && position.Z < barnBottom) 
            {
                if (rotationDeg < 360 && rotationDeg > 180 || rotationDeg == 0 || 
                    rotationDeg < 180 && rotationDeg > 0) 
                {
                    position.X = barnRightSide;
                }
            }
            //test top side of barn
            if (position.Z > barnTop && position.Z < barnTop2 &&
                position.X > barnLeftSide && position.X < barnRightSide)
            {
                if (rotationDeg > 270 && rotationDeg < 360 || rotationDeg >= 0 && rotationDeg < 90
                    || rotationDeg > 90 && rotationDeg < 270) 
                {
                    position.Z = barnTop;
                }
            }
            //test bottom side of barn
            if (position.Z < barnBottom && position.Z >= barnBottom2 &&
                position.X > barnLeftSide && position.X < barnRightSide)
            {
                if (rotationDeg > 90 && rotationDeg < 270 || rotationDeg > 270 && rotationDeg < 360 ||
                    rotationDeg >= 0 && rotationDeg < 90) 
                {
                    position.Z = barnBottom;
                }
            }
            //tests to see if character is going inside beam1
            //test left side of beam1
            if (position.X > beam1Left && position.X < beam1Left2 &&
                position.Z > beam1Top && position.Z < beam1Bottom)
            {
                if (rotationDeg > 0 && rotationDeg < 180 || rotationDeg < 360 && rotationDeg > 180)
                {
                    position.X = beam1Left;
                }
            }
            //test right side of beam1
            if (position.X < beam1Right && position.X >= beam1Right2 &&
             position.Z > beam1Top && position.Z < beam1Bottom)
            {
                if (rotationDeg < 360 && rotationDeg > 180 || rotationDeg == 0 ||
                    rotationDeg < 180 && rotationDeg > 0)
                {
                    position.X = beam1Right;
                }
            }
            //test top side of beam1
            if (position.Z > beam1Top && position.Z < beam1Top2 &&
                position.X > beam1Left && position.X < beam1Right)
            {
                if (rotationDeg > 270 && rotationDeg < 360 || rotationDeg >= 0 && rotationDeg < 90
                    || rotationDeg > 90 && rotationDeg < 270)
                {
                    position.Z = beam1Top;
                }
            }
            //test bottom side of beam1
            if (position.Z < beam1Bottom && position.Z >= beam1Bottom2 &&
                position.X > beam1Left && position.X < beam1Right)
            {
                if (rotationDeg > 90 && rotationDeg < 270 || rotationDeg > 270 && rotationDeg < 360 ||
                    rotationDeg >= 0 && rotationDeg < 90)
                {
                    position.Z = beam1Bottom;
                }
            }
            //tests to see if character is going inside beam2
            //test left side of beam2
            if (position.X > beam2Left && position.X < beam2Left2 &&
                position.Z > beam2Top && position.Z < beam2Bottom)
            {
                if (rotationDeg > 0 && rotationDeg < 180 || rotationDeg < 360 && rotationDeg > 180)
                {
                    position.X = beam2Left;
                }
            }
            //test right side of beam2
            if (position.X < beam2Right && position.X >= beam2Right2 &&
             position.Z > beam2Top && position.Z < beam2Bottom)
            {
                if (rotationDeg < 360 && rotationDeg > 180 || rotationDeg == 0 ||
                    rotationDeg < 180 && rotationDeg > 0)
                {
                    position.X = beam2Right;
                }
            }
            //test top side of beam2
            if (position.Z > beam2Top && position.Z < beam2Top2 &&
                position.X > beam2Left && position.X < beam2Right)
            {
                if (rotationDeg > 270 && rotationDeg < 360 || rotationDeg >= 0 && rotationDeg < 90
                    || rotationDeg > 90 && rotationDeg < 270)
                {
                    position.Z = beam2Top;
                }
            }
            //test bottom side of beam2
            if (position.Z < beam2Bottom && position.Z >= beam2Bottom2 &&
                position.X > beam2Left && position.X < beam2Right)
            {
                if (rotationDeg > 90 && rotationDeg < 270 || rotationDeg > 270 && rotationDeg < 360 ||
                    rotationDeg >= 0 && rotationDeg < 90)
                {
                    position.Z = beam2Bottom;
                }
            }
            //tests to see if character is going inside beam3
            //test left side of beam3
            if (position.X > beam3Left && position.X < beam3Left2 &&
                position.Z > beam3Top && position.Z < beam3Bottom)
            {
                if (rotationDeg > 0 && rotationDeg < 180 || rotationDeg < 360 && rotationDeg > 180)
                {
                    position.X = beam3Left;
                }
            }
            //test right side of beam3
            if (position.X < beam3Right && position.X >= beam3Right2 &&
             position.Z > beam3Top && position.Z < beam3Bottom)
            {
                if (rotationDeg < 360 && rotationDeg > 180 || rotationDeg == 0 ||
                    rotationDeg < 180 && rotationDeg > 0)
                {
                    position.X = beam3Right;
                }
            }
            //test top side of beam3
            if (position.Z > beam3Top && position.Z < beam3Top2 &&
                position.X > beam3Left && position.X < beam3Right)
            {
                if (rotationDeg > 270 && rotationDeg < 360 || rotationDeg >= 0 && rotationDeg < 90
                    || rotationDeg > 90 && rotationDeg < 270)
                {
                    position.Z = beam3Top;
                }
            }
            //test bottom side of beam3
            if (position.Z < beam3Bottom && position.Z >= beam3Bottom2 &&
                position.X > beam3Left && position.X < beam3Right)
            {
                if (rotationDeg > 90 && rotationDeg < 270 || rotationDeg > 270 && rotationDeg < 360 ||
                    rotationDeg >= 0 && rotationDeg < 90)
                {
                    position.Z = beam3Bottom;
                }
            }
            //tests to see if character is going inside beam4
            //test left side of beam4
            if (position.X > beam4Left && position.X < beam4Left2 &&
                position.Z > beam4Top && position.Z < beam4Bottom)
            {
                if (rotationDeg > 0 && rotationDeg < 180 || rotationDeg < 360 && rotationDeg > 180)
                {
                    position.X = beam4Left;
                }
            }
            //test right side of beam4
            if (position.X < beam4Right && position.X >= beam4Right2 &&
             position.Z > beam4Top && position.Z < beam4Bottom)
            {
                if (rotationDeg < 360 && rotationDeg > 180 || rotationDeg == 0 ||
                    rotationDeg < 180 && rotationDeg > 0)
                {
                    position.X = beam4Right;
                }
            }
            //test top side of beam4
            if (position.Z > beam4Top && position.Z < beam4Top2 &&
                position.X > beam4Left && position.X < beam4Right)
            {
                if (rotationDeg > 270 && rotationDeg < 360 || rotationDeg >= 0 && rotationDeg < 90
                    || rotationDeg > 90 && rotationDeg < 270)
                {
                    position.Z = beam4Top;
                }
            }
            //test bottom side of beam4
            if (position.Z < beam4Bottom && position.Z >= beam4Bottom2 &&
                position.X > beam4Left && position.X < beam4Right)
            {
                if (rotationDeg > 90 && rotationDeg < 270 || rotationDeg > 270 && rotationDeg < 360 ||
                    rotationDeg >= 0 && rotationDeg < 90)
                {
                    position.Z = beam4Bottom;
                }
            }
        }
       
    }
}
