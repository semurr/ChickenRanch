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
    class FoxClass
    {
        //fox basic variables
        public Model myFox;
        float aspectRatio;
        public float scale = 30.0f;
        Random rand = new Random();

        public bool wandering = false; //default state is attacking

        //node traversal system
        List<Vector3> foxPathValues = new List<Vector3>();
        int[,] foxPathA;
        public int foxInitNode = 0;
        public int foxPreviousNode = 0;
        public int foxCurrentNode = 0;
        public int foxNextNode = 0;

        //movement variables
        Vector3[,] gridPosition = new Vector3[10, 10];
        public Vector3 position;
        public Vector3 positionOld;
        public float rotation = 0;
        public float speed = 4.0f;
        public Vector3 riseRun = new Vector3(0, 130, 0);
        public Vector3 riseRun2 = Vector3.Zero;

        float posx = 0.0f;
        float posz = 0.0f;

        float risex = 0.0f;
        float risez = 0.0f;
        
        //attack variables
        int attackLength = 20;
        public int chasing = 0;   //0 for not chasing anything 1 for chicken 2 for rooster
       
        //avoid variables
        float avoidD = 300;
        public bool avoiding = false;

        //wait variables
        bool wait = false;
        public float waitTime = 10.0f; //when timer equals waitTime, fox moves again
        public float timer = 0.0f; //tracks time since waiting began
        public float chaseTime = 0.0f;  //checks the time to see which object is closer
        float waitTime2 = 2.5f; //when timer equals waitTime, fox moves again
        public float timer2 = 0.0f; //tracks time since waiting began

        //chicken variables
        ChickenClass[] chickenList;
        int chickenClose = 0;
        int chickenDistance = 0;

        //rooster variables
        RoosterClass[] roosterList;
        int roosterClose = 0;
        int roosterDistance = 0;

        //bounding box for fox
        BoundBox barn;
        BoundBox fence;
        public bool home = true;
        public bool start = true;
        public bool temp1 = false;
        


        public FoxClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            myFox = content.Load<Model>("Models\\newFoxModel[final]");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            
        }
        public void initializeFox(Vector3 newPosition, int numChic)
        {
            // set the poistion to the spot it starts off at
            position = newPosition;
            positionOld = newPosition;

            //set the amount of max chickens we have
            chickenList = new ChickenClass[numChic];

            //path values
            foxPathValues.Add(new Vector3( 800, 180,  600));  //0
            foxPathValues.Add(new Vector3( 800, 180, 3500));  //1
            foxPathValues.Add(new Vector3( 800, 180, 5700));  //2
            foxPathValues.Add(new Vector3(1950, 180, 1500));  //3
            foxPathValues.Add(new Vector3(1950, 180, 2700));  //4
            foxPathValues.Add(new Vector3(2800, 180, 4200));  //5
            foxPathValues.Add(new Vector3(3000, 180, 1500));  //6
            foxPathValues.Add(new Vector3(3000, 180, 2700));  //7
            foxPathValues.Add(new Vector3(3400, 180,  600));  //8
            foxPathValues.Add(new Vector3(3300, 180, 5700));  //9
            foxPathValues.Add(new Vector3(4000, 180, 2600));  //10
            foxPathValues.Add(new Vector3(4000, 180, 3700));  //11
            foxPathValues.Add(new Vector3(5200, 180,  600));  //12
            foxPathValues.Add(new Vector3(5200, 180, 3500));  //13
            foxPathValues.Add(new Vector3(5200, 180, 5700));  //14
            foxPathValues.Add(new Vector3(1500, 180, 5700));  //15  
            foxPathValues.Add(new Vector3(1500, 180, 6500));  //16  start and run to position

            //populate array with path connections for rooster
            foxPathA = new int[,] {
                {0,1,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0},  //0
                {1,0,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0},  //1
                {0,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},  //2
                {1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0},  //3
                {0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0},  //4
                {0,1,1,0,1,0,0,1,0,1,0,1,0,0,0,0,0},  //5
                {0,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0},  //6
                {0,0,0,0,1,1,1,0,1,1,1,1,0,0,0,0,0},  //7
                {1,0,0,0,0,0,1,1,0,0,1,0,1,0,0,0,0},  //8
                {0,0,1,0,0,1,0,1,0,0,0,1,0,0,1,0,0},  //9
                {0,0,0,0,0,0,0,1,1,0,0,1,1,1,0,0,0},  //10
                {0,0,0,0,0,1,0,1,0,1,1,0,0,1,1,0,0},  //11
                {0,0,0,0,0,0,0,0,1,0,1,0,0,1,0,0,0},  //12
                {0,0,0,0,0,0,0,0,0,0,1,1,1,0,1,0,0},  //13
                {0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0},  //14
                {0,0,1,0,0,1,0,0,0,1,0,0,0,0,0,0,0},  //15
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1}   //16
            };

            //get rooster initial position
            foxInitNode = 16;

            //set rooster current position equal to the initial position
            foxCurrentNode = foxInitNode;
            position = (foxPathValues[foxCurrentNode]);
            FoxNextNode();
            position = (foxPathValues[16]);
            rotation = MathHelper.Pi;

            //bounding box for fox
            barn = new BoundBox(2000, 3000, 1600, 2650);
            fence = new BoundBox(620, 5405, 370, 6005);
            

        }
        //fox resest
        public void FoxReset()
        {
            FoxNextNode();
            position = (foxPathValues[16]);
            rotation = MathHelper.Pi;
            home = true;
            start = true;
            avoiding = false;
            wait = false;
            chickenClose = 0;
            chickenDistance = 0;
            roosterClose = 0;
            roosterDistance = 0;
            timer = 0.0f;
            chaseTime = 0.0f;
            //turn off the warning automatically
            GameUI.gameInterface.instance.foxWarning.visible = false;
            //reset the warning bool
            GameUI.gameInterface.instance.wasWarned = false;

        }
        public void FoxNextNode()
        {
            if (start != true)
            {
                home = false;
            }
            //create a list to hold all the possible directions from node
            List<int> temp = new List<int>();

            for (int i = 0; i < foxPathValues.Count; i++)
            {
                int temp2 = foxPathA[foxCurrentNode, i];
                if (temp2 == 1)
                {
                    temp.Add(i);
                }
            }

            int temp3 = rand.Next(temp.Count);

                                                                                         //
            //test to see if fox just came from that position
            while (temp[temp3] == foxPreviousNode)
            {
                temp3 = rand.Next(temp.Count);
            }

            foxNextNode = temp[temp3];
              
                                                       
                                                     

            int temp4 = rand.Next(temp.Count);
            
            //update fox movement

            //find distance between current position and next node position
            risex = foxPathValues[foxNextNode].X;
            risez = foxPathValues[foxNextNode].Z;

            posx = position.X;
            posz = position.Z;

            riseRun.X = risex - posx;
            riseRun.Z = risez - posz;

            riseRun.X = risex - position.X;
            riseRun.Z = risez - position.Z;
            riseRun.Normalize();

            //move fox at constant speed
            riseRun.X = riseRun.X * speed;
            riseRun.Z = riseRun.Z * speed;

            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(foxPathValues[foxNextNode].X, foxPathValues[foxNextNode].Z);

            rotation = (float)getRotation(position1, position2);

        }

        public void wander()
        {
            //turn alarm on if alarm has not already been 
            if (GameUI.gameInterface.instance.wasWarned != true)
            {
                GameUI.gameInterface.instance.foxWarning.visible = true;
            }
            //turn alarm off after 10 seconds
            if (GameUI.gameInterface.instance.foxWarningCounter == 20)
            {
                GameUI.gameInterface.instance.foxWarning.visible = false;
                GameUI.gameInterface.instance.foxWarningCounter = 0;
                GameUI.gameInterface.instance.wasWarned = true;
            }
            if (Math.Abs(position.X - foxPathValues[foxNextNode].X) < speed + 1)
            {
            }
            else if (riseRun.X > 0)
            {
                positionOld.X = position.X;
                position.X = riseRun.X + position.X;
            }
            else
            {
                positionOld.X = position.X;
                position.X += riseRun.X;
                //position.X = (((riseRun.X - position.X) / time) - position.X);
            }


            if (Math.Abs(position.Z - foxPathValues[foxNextNode].Z) < speed + 1)//Math.Abs(riseRun.Z) + 1)
            {
            }
            else if (riseRun.Z > 0)
            {
                positionOld.Z = position.Z;
                position.Z += riseRun.Z;
            }
            else
            {
                positionOld.Z = position.Z;
                position.Z += riseRun.Z;
            }

            if( (Math.Abs(position.X - foxPathValues[foxNextNode].X) < (speed + 1)) &&
                (position.Z - foxPathValues[foxNextNode].Z) < ((speed + 1)))
            {
                foxPreviousNode = foxCurrentNode;
                foxCurrentNode = foxNextNode;
                FoxNextNode();
            }

        }

        //get rotation value point towards object
        public double getRotation(Vector2 pos1, Vector2 pos2)
        {
            double feta = Math.Atan((pos2.X - pos1.X) / (pos2.Y - pos1.Y));
            if (pos2.Y < pos1.Y)
            {
                feta += MathHelper.ToRadians(180);
            }
            return feta;
        }

        public void update(GameTime gameTime, ref ChickenClass[] chickenlst, ref RoosterClass[] roosterlst,
            ref int numRooster, ref int numChic,
            Vector3 avoid, int rosQueue, int chicqueue, EconomicsClass player)//, ChickenClass cl, RoosterClass rl)
        {
            //first time 
            if (start == true)
            {
                if (position.Z < 6000)
                {
                    home = false;
                    start = false;
                }
            }

            //check bounding area's 
            if(barn.inside(position))
            {
                //if fox go inside barn push out
                position = barn.pushOut(position,riseRun.X, riseRun.Z, speed);


            }
            else if (!fence.inside(position) && home == false)
            {
                float disttemp = Vector3.Distance(position, foxPathValues[0]);
                foxNextNode = 0;
                //find closest node after chase
                for (int i = 1; i < foxPathValues.Count; i++)
                {
                    if (disttemp > Vector3.Distance(position, foxPathValues[i]))
                    {
                        disttemp = Vector3.Distance(position, foxPathValues[i]);
                        foxNextNode = i;
                    }
                }
                riseRun.X = foxPathValues[foxNextNode].X - position.X;
                riseRun.Z = foxPathValues[foxNextNode].Z - position.Z;
                riseRun.Normalize();
                riseRun.X = riseRun.X * speed;
                riseRun.Z = riseRun.Z * speed;
                wandering = true;

                Vector2 position1 = new Vector2(position.X, position.Z);
                Vector2 position2 = new Vector2(foxPathValues[foxNextNode].X, foxPathValues[foxNextNode].Z);

                rotation = (float)getRotation(position1, position2);

            }
            else
            {
                //checks if fox is close to character, if so run away from character
                if( Vector3.Distance(avoid, position) < avoidD || avoiding == true)
                {
                    home = true;
                    start = true;

                    if (temp1 == true)
                    {
                        avoidChar(foxPathValues[16]);
                    }
                    else
                    {
                        avoidChar(foxPathValues[15]);
                    }
                }
                else if (wait == true)  //wait certain amount of time then start chasing again
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer > waitTime)
                    {
                        GameUI.gameInterface.instance.foxWarning.visible = true;
                        if (GameUI.gameInterface.instance.foxWarningCounter == 5)
                        {
                            GameUI.gameInterface.instance.foxWarning.visible = false;
                            GameUI.gameInterface.instance.foxWarningCounter = 0;
                        }
                        timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        position.Z -= speed;
                        rotation = MathHelper.Pi;

                        if (timer2 > waitTime2)
                        {
                            wait = false;
                            timer = 0;
                            timer2 = 0;
                            FoxNextNode();
                        }
                    }

                }
                else
                {
                    //find the closest object and chase

                    chickenList = chickenlst;
                    roosterList = roosterlst;
                    chaseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    
                    if (chaseTime > 3)
                    {
                        chaseTime = 0;
                    }

                    if (chaseTime == 0)
                    {
                        chaseTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        

                        if (chickenDistance >= 0)
                        {
                            chickenDistance = 0;
                            chickenClose = -1;
                        }

                        if (roosterDistance >= 0)
                        {
                            roosterDistance = 0;
                            roosterClose = -1;
                        }

                        //find closest chicken
                        for (int i = 0; i < numChic - chicqueue; i++)
                        {
                            //check if they are on ground
                            if (chickenList[i].ground == 3)
                            {
                                if (chickenDistance > Vector3.Distance(chickenList[i].position, position) || chickenClose == -1)
                                {
                                    chickenDistance = (int)Vector3.Distance(chickenList[i].position, position);
                                    chickenClose = i;
                                }
                            }
                        }

                        //find the closet rooster
                        for (int i = 0; i < numRooster; i++)
                        {
                            //check if they are on ground
                            if (roosterList[i] != null)
                            {
                                if (roosterDistance > Vector3.Distance(roosterList[i].position, position) || roosterClose == -1)
                                {
                                    roosterDistance = (int)Vector3.Distance(roosterList[i].position, position);
                                    roosterClose = i;
                                }
                            }
                        }
                    }

                    //if nothing to chase find next node and wander
                    if(chickenDistance == 0 && roosterDistance == 0)
                    {
                        chasing = 0;
                        if (wandering == true)
                        {
                            wander();
                        }
                        else
                        {
                            float disttemp = Vector3.Distance(position, foxPathValues[0]);
                            foxNextNode = 0;
                            //find closest node after chase
                            for (int i = 1; i < foxPathValues.Count; i++)
                            {
                                if (disttemp > Vector3.Distance(position, foxPathValues[i]))
                                {
                                    disttemp = Vector3.Distance(position, foxPathValues[i]);
                                    foxNextNode = i;
                                }
                            }
                            riseRun.X = foxPathValues[foxNextNode].X - position.X;
                            riseRun.Z = foxPathValues[foxNextNode].Z - position.Z;
                            riseRun.Normalize();
                            riseRun.X = riseRun.X * speed;
                            riseRun.Z = riseRun.Z * speed;
                            wandering = true;

                            Vector2 position1 = new Vector2(position.X, position.Z);
                            Vector2 position2 = new Vector2(foxPathValues[foxNextNode].X, foxPathValues[foxNextNode].Z);

                            rotation = (float)getRotation(position1, position2);
                        }
                    }

                    //if no rooster and chicken on the ground chase chicken
                    if (roosterDistance == 0)
                    {
                        if(chickenDistance != 0)
                        {
                            //chase chicken
                            chasing = 1;
                            chase(chickenList[chickenClose].position, ref numRooster, ref roosterlst, ref numChic,
                                ref chickenList, player);
                            wandering = false;
                        }
                    }

                    //if no chicken and a rooster on the ground
                    if (chickenDistance == 0)
                    {
                        if (roosterDistance != 0)
                        {
                            //chase rooster
                            chasing = 2;
                            chase(roosterList[roosterClose].position, ref numRooster, ref roosterlst, ref numChic,
                                ref chickenList, player);
                            wandering = false;
                        }
                    }

                    //if rooster and chicken on ground chase closest one
                    if (chickenDistance != 0 && roosterDistance != 0)
                    {
                        if (roosterDistance > chickenDistance)
                        {
                            chasing = 1;
                            wandering = false;
                            chase(chickenList[chickenClose].position, ref numRooster, ref roosterlst, ref numChic,
                                ref chickenList, player);
                        }
                        else
                        {
                            chasing = 2;
                            wandering = false;
                            chase(roosterList[roosterClose].position, ref numRooster, ref roosterlst, ref numChic,
                                ref chickenList, player);
                        }
                    }
                }
            }
        }

        //chase rooster or chicken
        public void chase(Vector3 chase, ref int nRooster, ref RoosterClass[] roosterList, ref int nChicken,
            ref ChickenClass[] chickenList, EconomicsClass player)
        {
            //turn alarm on if alarm has not already been 
            if (GameUI.gameInterface.instance.wasWarned != true)
            {
                GameUI.gameInterface.instance.foxWarning.visible = true;
            }
            //turn alarm off after 10 seconds
            if (GameUI.gameInterface.instance.foxWarningCounter == 20)
            { 
                GameUI.gameInterface.instance.foxWarning.visible = false;
                GameUI.gameInterface.instance.foxWarningCounter = 0;
                GameUI.gameInterface.instance.wasWarned = true;
            }

            //find the vector object needs to moe to collide
            riseRun2.X = chase.X - position.X;
            riseRun2.Z = chase.Z - position.Z;
            riseRun2.Normalize();
            riseRun2.X = riseRun2.X * speed;
            riseRun2.Z = riseRun2.Z * speed;

            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(chase.X, chase.Z);

            rotation = (float)getRotation(position1, position2);

            //attack if object gets to close
            if (Math.Abs(position.X - chase.X) < attackLength && Math.Abs(position.Z - chase.Z) < attackLength)
            {
                if (chasing == 1)
                {
                    //attack chicken
                    if (Chicken.AudioManager.instance.sFXOn == true && GameUI.gameWorld.instance.isPaused != true)
                    {
                        Chicken.AudioManager.instance.foxAttackSound.Play();
                    }
                    chickenList[chickenClose] = null;
                    decrementChicken(ref chickenList, chickenClose, nChicken);
                    chickenDistance = 0;
                    nChicken--;
                    player.chickenEaten++;
                    

                }
                else
                {
                    //attack rooster
                    if (Chicken.AudioManager.instance.sFXOn == true && GameUI.gameWorld.instance.isPaused != true)
                    {
                        Chicken.AudioManager.instance.foxAttackSound.Play();
                    }
                    roosterList[roosterClose] = null;
                    decrementRooster(ref roosterList, roosterClose, nRooster);
                    roosterDistance = 0;
                    nRooster--;
                    player.roosterEaten++;
                }
            }

            //chase toward entity position updates
            if (Math.Abs(position.X - chase.X) < Math.Abs(riseRun2.X) + 1)
            {
            }
            else if (riseRun2.X > 0)
            {
                positionOld.X = position.X;
                position.X = riseRun2.X + position.X;
            }
            else
            {
                positionOld.X = position.X;
                position.X += riseRun2.X;
            }

            if (Math.Abs(position.Z - chase.Z) < Math.Abs(riseRun2.Z) + 1)
            {
            }
            else if (riseRun2.Z > 0)
            {
                positionOld.Z = position.Z;
                position.Z += riseRun2.Z;
            }
            else
            {
                positionOld.Z = position.Z;
                position.Z += riseRun2.Z;
            }
        }

        //function to avoid the farmer
        public void avoidChar(Vector3 avoid)
        {
            GameUI.gameInterface.instance.foxWarning.visible = false;
            //find vector to chase towards
            riseRun2.X = avoid.X - position.X;
            riseRun2.Z = avoid.Z - position.Z;
            riseRun2.Normalize();
            riseRun2.X = riseRun2.X * speed;
            riseRun2.Z = riseRun2.Z * speed;

            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(avoid.X, avoid.Z);

            rotation = (float)getRotation(position1, position2);

            //chase toward entity
            if (Math.Abs(position.X - avoid.X) < Math.Abs(riseRun2.X) + 1)
            {
            }
            else if (riseRun2.X > 0)
            {
                positionOld.X = position.X;
                position.X = riseRun2.X + position.X;
            }
            else
            {
                positionOld.X = position.X;
                position.X += riseRun2.X;
            }

            if (Math.Abs(position.Z - avoid.Z) < Math.Abs(riseRun2.Z) + 1)
            {
            }
            else if (riseRun2.Z > 0)
            {
                positionOld.Z = position.Z;
                position.Z += riseRun2.Z;
            }
            else
            {
                positionOld.Z = position.Z;
                position.Z += riseRun2.Z;
            }

            //set flags
            avoiding = true;
            wandering = false;

            //check for distance from reset point
            float tmp1 = (float)Vector3.Distance(position, avoid);
            float tmp2 = (float)Vector3.Distance(position, avoid);
            if (Vector3.Distance(position, avoid) < Math.Abs(riseRun2.X) + 5 && Vector3.Distance(position, avoid) <
                Math.Abs(riseRun2.Z) + 5)
            {
                //if within distance go into wait mode

                if (avoid == foxPathValues[16])
                {

                    wait = true;
                    avoiding = false;
                    temp1 = false;
                }
                else
                {
                    temp1 = true;
                }
                

                
            }
            
        }

        //function to shift rooster list over
        public void decrementRooster(ref RoosterClass[] list, int count, int maxCount)
        {
            for(int i = count; i < maxCount - 1; i++)
            {
                list[i] = list[i + 1];

                if (i == maxCount - 1)
                {
                    list[i + 1] = null;
                }
            }
        }

        //function to shift chicken list over
        public void decrementChicken(ref ChickenClass[] list, int count, int maxCount)
        {
            for (int i = count; i < maxCount - 1; i++)
            {
                list[i] = list[i + 1];

                if (i == maxCount - 1)
                {
                    list[i + 1] = null;
                }
            }
        }

        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            Matrix[] transforms2 = new Matrix[myFox.Bones.Count];
            myFox.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myFox.Meshes)
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
        }
    }
}