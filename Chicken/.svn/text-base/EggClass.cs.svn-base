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
    class EggClass
    {
        //basic Egg stats
        public float scale = 1.5f;
        public Model myEgg;
        float aspectRatio; 
        
        //movement variables
        public Vector3 position;
        public float gravity = -0.01f;
        public Vector3 velocity = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero; 
               
                
        public EggClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            myEgg = content.Load<Model>("Models\\whiteEggModel");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            position = new Vector3(4000, 400, 4000);
        }

        public void initializeEgg()
        {
        }

        public void update(GameTime gameTime)
        {
            if(position.Y >= 140)
            {
                //implements gravity on egg until it reaches the ground level
                velocity.Y += gravity;
                position.Y += velocity.Y;
            }
        }

        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            Matrix[] transforms2 = new Matrix[myEgg.Bones.Count];
            myEgg.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myEgg.Meshes)
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