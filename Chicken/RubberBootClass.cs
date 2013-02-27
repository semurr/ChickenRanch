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
    class RubberBootClass
    {
        //basic boot stats
        public float scale = 1.5f;
        public Model myBoots;
        float aspectRatio;
        Random rand = new Random();

        //spawning variables
        public Vector3 position;
        public Vector3 rotation = Vector3.Zero;
        List<Vector3> bootsSpawnPos = new List<Vector3>();
        public int initBootPos = 0;

        public RubberBootClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            myBoots = content.Load<Model>("Models\\rubberBootSet");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            position = new Vector3(3700, 190, 3700);
        }

        public void initializeBoots()
        {
            //possible spawning positions
            bootsSpawnPos.Add(new Vector3(800, 200, 600));  //0
            bootsSpawnPos.Add(new Vector3(800, 200, 3500));  //1
            bootsSpawnPos.Add(new Vector3(800, 200, 5700));  //2
            bootsSpawnPos.Add(new Vector3(1950, 200, 1500));  //3
            bootsSpawnPos.Add(new Vector3(1950, 200, 2700));  //4
            bootsSpawnPos.Add(new Vector3(2800, 200, 4200));  //5
            bootsSpawnPos.Add(new Vector3(3000, 200, 1500));  //6
            bootsSpawnPos.Add(new Vector3(3000, 200, 2700));  //7
            bootsSpawnPos.Add(new Vector3(3400, 200, 600));  //8
            bootsSpawnPos.Add(new Vector3(3300, 200, 5700));  //9
            bootsSpawnPos.Add(new Vector3(4000, 200, 2600));  //10
            bootsSpawnPos.Add(new Vector3(4000, 200, 3700));  //11
            bootsSpawnPos.Add(new Vector3(5200, 200, 600));  //12
            bootsSpawnPos.Add(new Vector3(5200, 200, 3500));  //13
            bootsSpawnPos.Add(new Vector3(5200, 200, 5700));  //14

            initBootPos = rand.Next() % 14;
            position = bootsSpawnPos[initBootPos];
        }

        public void update(GameTime gameTime)
        {

        }

        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            Matrix[] transforms2 = new Matrix[myBoots.Bones.Count];
            myBoots.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myBoots.Meshes)
            {
                foreach (BasicEffect effect in mesh2.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = transforms2[mesh2.ParentBone.Index] *
                        Matrix.CreateScale(scale) *
                        //Matrix.CreateRotationY(modelRotation) *
                    Matrix.CreateTranslation(position);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh2.Draw();
            }
        }

    }
}