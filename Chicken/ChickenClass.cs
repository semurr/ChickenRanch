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
    class ChickenClass
    {
        //Chicken class variables
        public float scale = 0.25f;
        public Model myChicken;
        float aspectRatio;
        public float time = 0; //tracks amount of time passed
        Random rand = new Random();

        public int state = 1;
        // 1 = wander
        // 2 = prepEgg
        // 3 = layEgg

        public int ground = 1;
        // 1 = on rafters
        // 2 = transitioning between ground and rafters
        // 3 = on ground

        //node traversal system
        public List<Vector3> ChickenPathValues = new List<Vector3>();
        int[,] ChickenPathA;
        public int ChickenInitNode = 0;
        public int ChickenPreviousNode = 0;
        public int ChickenCurrentNode = 0;
        public int chickenNextNode = 0;

        //movement variables
        public Vector3 position;
        public float rotation = 0.0f;
        float speed = 4.0f;
        public Vector3 riseRun = new Vector3(0, -250, 0);
        
        float risex = 0.0f;
        float risey = 0.0f;
        float risez = 0.0f;

        float posx = 0.0f;
        float posy = 0.0f;
        float posz = 0.0f;

        float runx = 0.0f;
        float runy = 0.0f;
        float runz = 0.0f;
 
        //egg laying variables
        public bool eggLaid = false;
        float eggTime = 20.0f; //amount of time between laying egg and preparing to lay another egg
        float waitTime = 10.0f; //amount of time for preparing to lay egg

        public ChickenClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            //load my Chicken
            int cmc = rand.Next() % 3;

            myChicken = content.Load<Model>("Models\\simpleBrownChicken");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            position = new Vector3(0, 0, 0);
        }

        public void InitializeChicken(int init)
        {

            //Populate List to hold vector3 node positions for Chicken

            ChickenPathValues.Add(new Vector3(2800, 1025, 2150));  //0
            ChickenPathValues.Add(new Vector3(3285, 1025, 2150));  //1
            ChickenPathValues.Add(new Vector3(4025, 1025, 2150));  //2
            ChickenPathValues.Add(new Vector3(4025, 1025, 3140)); //3
            ChickenPathValues.Add(new Vector3(3205, 200, 3140)); //4
            ChickenPathValues.Add(new Vector3(4035, 1060, 4200)); //5
            ChickenPathValues.Add(new Vector3(3285, 1040, 4200)); //6
            ChickenPathValues.Add(new Vector3(2275, 200, 4200)); //7
            ChickenPathValues.Add(new Vector3(2150, 200, 3140));  //8
            ChickenPathValues.Add(new Vector3(2500, 200, 3700));  //9

            //populate array with path connections for Chicken 15x15
            ChickenPathA = new int[,] {
                {0,1,0,0,0,0,0,0,0,0}, //0
                {0,0,1,0,0,0,1,0,0,0}, //1
                {0,1,0,1,0,0,0,0,0,0}, //2
                {0,0,1,0,1,1,0,0,0,0}, //3
                {0,0,0,1,0,0,0,0,1,1}, //4
                {0,0,0,1,0,0,1,0,0,0}, //5
                {0,1,0,0,0,1,0,1,0,0}, //6
                {0,0,0,0,0,0,1,0,1,1}, //7
                {0,0,0,0,1,0,0,1,0,1}, //8
                {0,0,0,0,1,0,0,1,1,0}  //9
            };
            

            //get Chicken initial position
            ChickenInitNode = init; 

            //set Chicken current position equal to the initial position
            ChickenCurrentNode = ChickenInitNode;
            position = (ChickenPathValues[ChickenCurrentNode]);
            
            //find chicken next node
            ChickenNextNode();
            //rotation = MathHelper.PiOver2;

            //ChickenPreviousNode = ChickenCurrentNode;
           
            
        }
        public void ChickenNextNode()
        {
            //create a list to hold all the possible directions from node
            List<int> temp = new List<int>();

            for (int i = 0; i < ChickenPathValues.Count; i++)
            {
                int temp2 = ChickenPathA[ChickenCurrentNode, i];
                if (temp2 == 1)
                {
                    temp.Add(i);
                }
            }

            int temp3 = rand.Next(temp.Count);

            //test to see if Chicken just came from that position
            while (temp[temp3] == ChickenPreviousNode)
            {
                temp3 = rand.Next(temp.Count);
            }

            chickenNextNode = temp[temp3];

            //update Chicken movement

            //find rise run for variables
            risex = ChickenPathValues[chickenNextNode].X;
            risey = ChickenPathValues[chickenNextNode].Y;
            risez = ChickenPathValues[chickenNextNode].Z;

                  
            posx = position.X;
            posy = position.Y;
            posz = position.Z;
           
            runx = risex - posx;
            runy = risey - posy;
            runz = risez - posz;

            riseRun.X = runx;
            riseRun.Y = runy * -1;
            riseRun.Z = runz;

            riseRun.X = risex - position.X;
            riseRun.Y = risey - position.Y;
            riseRun.Z = risez - position.Z;
            riseRun.Normalize();
            riseRun.X = riseRun.X * speed;
            riseRun.Y = riseRun.Y * speed;
            riseRun.Z = riseRun.Z * speed;

                        
            //rotation test
            Vector2 position1 = new Vector2(position.X, position.Z);
            Vector2 position2 = new Vector2(ChickenPathValues[chickenNextNode].X, ChickenPathValues[chickenNextNode].Z);

            rotation = (float)getRotation(position1, position2);
           

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

        public void update(GameTime gameTime, int numRooster, ref int numEgg)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //determine if chicken is on the ground
            switch (ground)
            {
                case 1: //chicken on rafters
                    if (position.Y <= 1025)
                    {
                        ground = 2;
                    }
                    break;
                case 2: //chicken going to or from rafters to ground
                    if (position.Y <= 200)
                    {
                        ground = 3;
                    }
                    else if (position.Y >= 1025)
                    {
                        ground = 1;
                    }
                    break;
                case 3: //chicken on ground
                    if (position.Y >= 200)
                    {
                        ground = 2;
                    }
                    break;
                default:
                    break;
            }

            switch (state)
            {                    
                case 1:
                    wander();
                    if (Math.Abs(position.X - ChickenPathValues[chickenNextNode].X) < Math.Abs(riseRun.X) + 1 &&
                        Math.Abs(position.Y - ChickenPathValues[chickenNextNode].Y) < Math.Abs(riseRun.Y) + 1 &&
                        Math.Abs(position.Z - ChickenPathValues[chickenNextNode].Z) < Math.Abs(riseRun.Z) + 1)
                    {
                        position = ChickenPathValues[chickenNextNode];
                        ChickenPreviousNode = ChickenCurrentNode;
                        ChickenCurrentNode = chickenNextNode;
                        ChickenNextNode();
                    }
                    eggLaid = false;
                    eggTime = 20.0f;

                    if (numRooster < 1) //if there are no roosters
                    {
                        //do nothing
                    }
                    else if (numRooster == 1) //if there is only one rooster
                    {
                        //set time to default
                        eggTime = 20.0f;
                    }
                    else if (numRooster > 1) //if there are more than one rooster
                    {
                        //divide default time by the number of roosters
                        eggTime /= numRooster;
                    }

                    if (time > eggTime && ground == 1)
                    {
                        if (numRooster >= 1)
                        {
                            state = 2;
                            time = 0;
                        }
                    }
                    break;
                case 2:
                    if (time > waitTime && ground == 1)
                    {
                        time = 0;
                        state = 3;
                    }

                    break;
                case 3:
                    layEgg(numEgg);
                    time = 0;
                    state = 1;

                    break;
                default:
                    break;
            }


        }
        public void wander()
        {
            // x
            if (Math.Abs(position.X - ChickenPathValues[chickenNextNode].X) < Math.Abs(riseRun.X) + 1)
            {
            }
            else if (riseRun.X > 0)
            {
                position.X = riseRun.X + position.X;
            }
            else
            {
                position.X += riseRun.X;
            }
            //y
            if (Math.Abs(position.Y - ChickenPathValues[chickenNextNode].Y) < Math.Abs(riseRun.Y) + 1)
            {
            }
            else if (riseRun.Y > 0)
            {
                position.Y = riseRun.Y + position.Y;
            }
            else
            {
                position.Y += riseRun.Y;
            }
            //z
            if (Math.Abs(position.Z - ChickenPathValues[chickenNextNode].Z) < Math.Abs(riseRun.Z) + 1)
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

        public void layEgg(int numEgg)
        {
            eggLaid = true;
            numEgg++;
        }

        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            
            Matrix[] transforms2 = new Matrix[myChicken.Bones.Count];
            myChicken.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myChicken.Meshes)
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
