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
    class RoosterClass
    {
        //rooster class variables
        public float scale = 1.5f;
        Random rand = new Random();
        public Model myRooster;
        float aspectRatio;

        public int state = 1;
        // 1 = wander
        // 2 = chase
        // 3 = attack

        //node traversal system
        List<Vector3> roosterPathValues = new List<Vector3>();
        int[,] roosterPathA;
        public int roosterInitNode = 0;
        public int roosterPreviousNode = 0;
        public int roosterCurrentNode = 0;
        public int roosterNextNode = 0;

        //movement variables
        public Vector3 position;
        public float rotation = 0;
        float speed = 3.0f;
        
        public Vector3 riseRun = new Vector3(0, -250, 0);
        public Vector3 riseRun2 = Vector3.Zero;

        float risex = 0.0f;
        float risez = 0.0f;

        float posx = 0.0f;
        float posz = 0.0f;

        //chase variables
        float speedChase = 4.0f;
        int chaseLen = 500;
        
        //attack variables
        int attackCounter = 0; //tracks seconds between
        int attackLength = 20;

        //bounding area's and index
        BoundBox barn;
        BoundBox fence;
        public bool start = true;



        public RoosterClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            //load my rooster
            myRooster = content.Load<Model>("Models\\rooster[1]Basic");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            position = new Vector3(0, 0, 0);
        }

        public void InitializeRooster(int init)
        {

            //Populate List to hold vector3 node positions for rooster

            roosterPathValues.Add(new Vector3(1300, 270, 1000));  //0
            roosterPathValues.Add(new Vector3(1300, 270, 3500));  //1
            roosterPathValues.Add(new Vector3(1300, 270, 5200));  //2
            roosterPathValues.Add(new Vector3(2200, 270, 4000));  //3
            roosterPathValues.Add(new Vector3(3500, 270, 1000));  //4
            roosterPathValues.Add(new Vector3(3000, 270, 3500));  //5
            roosterPathValues.Add(new Vector3(3500, 270, 3500));  //6
            roosterPathValues.Add(new Vector3(3500, 270, 5200));  //7
            roosterPathValues.Add(new Vector3(3700, 270, 2200));  //8
            roosterPathValues.Add(new Vector3(3700, 270, 4300));  //9
            roosterPathValues.Add(new Vector3(4800, 270, 1000));  //10
            roosterPathValues.Add(new Vector3(4800, 270, 3500));  //11
            roosterPathValues.Add(new Vector3(4800, 270, 5200));  //12
            roosterPathValues.Add(new Vector3(2150, 270, 2200));  //13
            roosterPathValues.Add(new Vector3(2200, 270, 3500));  //14



            //populate array with path connections for rooster
            roosterPathA = new int[,] {
                {0,1,0,0,1,0,0,0,0,0,0,0,0,0,0}, //0
                {1,0,1,1,0,0,0,0,0,0,0,0,0,0,1}, //1
                {0,1,0,1,0,0,0,1,0,0,0,0,0,0,0}, //2
                {0,1,1,0,0,1,0,1,0,0,0,0,0,0,1}, //3
                {1,0,0,0,0,1,1,0,1,0,1,0,0,0,0}, //4
                {0,0,0,1,1,0,1,1,0,0,0,0,0,0,1}, //5
                {0,0,0,0,1,1,0,1,1,1,0,1,0,0,0}, //6
                {0,0,1,1,0,1,1,0,0,1,0,0,1,0,0}, //7
                {0,0,0,0,1,0,1,0,0,0,1,1,0,0,0}, //8
                {0,0,0,0,0,0,1,1,0,0,0,1,1,0,0}, //9
                {0,0,0,0,1,0,0,0,1,0,0,1,0,0,0}, //10
                {0,0,0,0,0,0,1,0,1,1,1,0,1,0,0}, //11
                {0,0,0,0,0,0,0,1,0,1,0,1,0,0,0}, //12
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1}, //13
                {0,1,0,1,0,0,1,0,0,0,0,0,0,0,0}  //14
            };

            //bounding box setting
            barn = new BoundBox(2000, 3000, 1600, 2750);
            fence = new BoundBox(620, 5405, 370, 6005);

            //get rooster initial position
            roosterInitNode = init;  
            

            //set rooster current position equal to the initial position
            roosterCurrentNode = roosterInitNode;
            position = (roosterPathValues[roosterCurrentNode]);
            RoosterNextNode();
            rotation = 0;
            //position = roosterPathValues[14];


        }
        public void RoosterNextNode()
        {
            if (roosterCurrentNode != 13)
            {
                start = false;
            }
            //create a list to hold all the possible directions from node
            List<int> temp = new List<int>();

            for (int i = 0; i < roosterPathValues.Count; i++)
            {
                int temp2 = roosterPathA[roosterCurrentNode, i];
                if (temp2 == 1)
                {
                    temp.Add(i);
                }
            }

            int temp3 = rand.Next(temp.Count);

            //test to see if rooster just came from that position
            while (temp[temp3] == roosterPreviousNode)
            {
                temp3 = rand.Next(temp.Count);
            }

            roosterNextNode = temp[temp3];

            int temp4 = rand.Next(temp.Count);
            
            //update rooster movement

            //find distance between current position and next node position
            risex = roosterPathValues[roosterNextNode].X;
            risez = roosterPathValues[roosterNextNode].Z;

            posx = position.X;
            posz = position.Z;

            riseRun.X = risex - posx;
            riseRun.Z = risez - posz;

            riseRun.X = risex - position.X;
            riseRun.Z = risez - position.Z;
            riseRun.Normalize();

            //move rooster at constant speed
            riseRun.X = riseRun.X * speed;
            riseRun.Z = riseRun.Z * speed;

            //find angle of rotation
            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(roosterPathValues[roosterNextNode].X, roosterPathValues[roosterNextNode].Z);

            rotation = (float)getRotation(position1, position2);

        }
        public void update(GameTime gameTime, Vector3 chasePosition, ref int hitPoints)
        {
            if (barn.inside(position) && start == false)
            {
                //if rooster go inside barn push out
                position = barn.pushOut(position, speed);
                
            }
            else if(!fence.inside(position))
            {
                //if rooster goes outside fence find a node

                //if rooster out of chase range go back to wander
                state = 1;
                float disttemp = Vector3.Distance(position, roosterPathValues[0]);
                roosterNextNode = 0;
                //find closest node after chase
                for (int i = 1; i < roosterPathValues.Count; i++)
                {
                    if (disttemp > Vector3.Distance(position, roosterPathValues[i]))
                    {
                        disttemp = Vector3.Distance(position, roosterPathValues[i]);
                        roosterNextNode = i;
                    }
                }
                //movement to chase object
                riseRun.X = roosterPathValues[roosterNextNode].X - position.X;
                riseRun.Z = roosterPathValues[roosterNextNode].Z - position.Z;
                riseRun.Normalize();
                riseRun.X = riseRun.X * speed;
                riseRun.Z = riseRun.Z * speed;

                //find angle of rotation
                Vector2 position1 = new Vector2(position.X, position.Z);
                Vector2 position2 = new Vector2(roosterPathValues[roosterNextNode].X, roosterPathValues[roosterNextNode].Z);

                rotation = (float)getRotation(position1, position2);

            }
            else
            {

                //Rooster State Machine
                switch (state)
                {
                    case 1:
                        //default state
                        wander();
                        if (Math.Abs(position.X - roosterPathValues[roosterNextNode].X) < speed/*Math.Abs(riseRun.X)*/ + 1 &&
                            Math.Abs(position.Z - roosterPathValues[roosterNextNode].Z) < speed /*Math.Abs(riseRun.Z)*/ + 1)
                        {
                            position = roosterPathValues[roosterNextNode];
                            roosterPreviousNode = roosterCurrentNode;
                            roosterCurrentNode = roosterNextNode;
                            RoosterNextNode();
                        }

                        //start chasing
                        if (Math.Abs(Vector3.Distance(position, chasePosition)) - 100 < chaseLen)
                        {
                            state = 2;
                        }
                        break;
                    case 2:
                        //chase object
                        start = false;
                        chase(chasePosition);

                        if (Math.Abs(Vector3.Distance(position, chasePosition)) - 100 > chaseLen)
                        {
                            //if rooster out of chase range go back to wander
                            state = 1;
                            float disttemp = Vector3.Distance(position, roosterPathValues[0]);
                            roosterNextNode = 0;
                            //find closest node after chase
                            for (int i = 1; i < roosterPathValues.Count; i++)
                            {
                                if (disttemp > Vector3.Distance(position, roosterPathValues[i]))
                                {
                                    disttemp = Vector3.Distance(position, roosterPathValues[i]);
                                    roosterNextNode = i;
                                }
                            }
                            //movement to chase object
                            riseRun.X = roosterPathValues[roosterNextNode].X - position.X;
                            riseRun.Z = roosterPathValues[roosterNextNode].Z - position.Z;
                            riseRun.Normalize();
                            riseRun.X = riseRun.X * speed;
                            riseRun.Z = riseRun.Z * speed;

                            //find angle of rotation
                            Vector2 position1 = new Vector2(position.X, position.Z);
                            Vector2 position2 = new Vector2(roosterPathValues[roosterNextNode].X,
                                roosterPathValues[roosterNextNode].Z);

                            rotation = (float)getRotation(position1, position2);
                        }
                        break;
                    case 3:
                        if (attackCounter == 0)
                        {
                            attack(ref hitPoints);
                        }
                        attackCounter++;
                        if (attackCounter == 200)
                        {
                            state = 2;
                            attackCounter = 0;
                        }


                        break;

                    default:
                        break;
                }
            }


        }
        public void wander()
        {

            if (Math.Abs(position.X - roosterPathValues[roosterNextNode].X) < speed + 1)
            {
            }
            else if (riseRun.X > 0)
            {
                position.X = riseRun.X + position.X;
            }
            else
            {
                position.X += riseRun.X;
                //position.X = (((riseRun.X - position.X) / time) - position.X);
            }


            if (Math.Abs(position.Z - roosterPathValues[roosterNextNode].Z) < speed + 1)//Math.Abs(riseRun.Z) + 1)
            {
            }
            else if (riseRun.Z > 0)
            {
                position.Z += riseRun.Z;
            }
            else
            {
                position.Z += riseRun.Z;
            }

        }
        public void chase(Vector3 chase)
        {
            
            riseRun2.X = chase.X - position.X;
            riseRun2.Z = chase.Z - position.Z;
            riseRun2.Normalize();
            riseRun2.X = riseRun2.X * speedChase;
            riseRun2.Z = riseRun2.Z * speedChase;

            //find angle of rotation
            //find angle of rotation
            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(chase.X, chase.Z);

            rotation = (float)getRotation(position1, position2);
            
            
            //attack
            if (Math.Abs(position.X - chase.X) < attackLength && Math.Abs(position.Z - chase.Z) < attackLength)
            {
                state = 3;
            }

            //chase toward character
            if (Math.Abs(position.X - chase.X) < Math.Abs(riseRun2.X) + 1)
            {
            }
            else if (riseRun2.X > 0)
            {
                position.X = riseRun2.X + position.X;
            }
            else
            {
                position.X += riseRun2.X;
            }

            if (Math.Abs(position.Z - chase.Z) < Math.Abs(riseRun2.Z) + 1)
            {
            }
            else if (riseRun2.Z > 0)
            {
                position.Z += riseRun2.Z;
            }
            else
            {
                position.Z += riseRun2.Z;
            }

        }
        //attack funtion reduce's hit points
        public void attack(ref int hitPoints)
        {
            if (Chicken.AudioManager.instance.sFXOn == true)
            {
                Chicken.AudioManager.instance.roosterAttackSound.Play();
            }
            hitPoints -= 20;
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
        
        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            Matrix[] transforms2 = new Matrix[myRooster.Bones.Count];
            myRooster.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myRooster.Meshes)
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
