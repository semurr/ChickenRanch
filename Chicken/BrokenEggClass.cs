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
    class BrokenEggClass
    {
        public Vector3 position;
        public Vector3 rotation = Vector3.Zero;
        public float scale = 1.0f;
        public Model myBrokenEgg;
        float aspectRatio;
        
        
        
        public BrokenEggClass(ContentManager content, GraphicsDeviceManager graphics)
        {
            myBrokenEgg = content.Load<Model>("Models\\brokenEggT");
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            position = new Vector3(4000, 400, 4000);
        }

        public void initializeBrokenEgg()
        {
        }

        public void update(GameTime gameTime)
        {
        }

        public void draw(GameTime gameTime, Matrix viewMatrix, Matrix projectionMatrix)
        {
            Matrix[] transforms2 = new Matrix[myBrokenEgg.Bones.Count];
            myBrokenEgg.CopyAbsoluteBoneTransformsTo(transforms2);
            foreach (ModelMesh mesh2 in myBrokenEgg.Meshes)
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
