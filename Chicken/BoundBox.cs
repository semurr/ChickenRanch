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
    class BoundBox
    {
        //four corners of box
        float Left;
        float Right;
        float Top;
        float Bottom;

        //midpoint
        Vector2 midPoint;

        public BoundBox(float tLeft, float tRight, float tTop, float tBottom)
        {
            Left = tLeft;
            Right = tRight;
            Top = tTop;
            Bottom = tBottom;

            midPoint = new Vector2(Math.Abs(Left - Right), Math.Abs(Top - Bottom)); 
        }
        //check if you are inside the box
        public bool inside(Vector3 position)
        {
            //check if x value is inside of the top values
            if (position.X > Left && position.X < Right && position.Z > Top && position.Z < Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

         //check if your outside the box
        public bool outside(Vector3 position)
        {
            if (position.X > Left && position.X < Right && position.Z > Top && position.Z < Bottom)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //push outside        
        public Vector3 pushOut(Vector3 position, float speed)
        {
            int side = 0;
            float shortDis = Math.Abs(position.X - Left);

            //check right
            if (Math.Abs(position.X - Right) < shortDis)
            {
                side = 1;
                shortDis = Math.Abs(position.X - Right);
            }

            //check top
            if (Math.Abs(position.Z - Top) < shortDis)
            {
                side = 2;
                shortDis = Math.Abs(position.Z - Top);
            }

            //check Bottom
            if (Math.Abs(position.Z - Bottom) < shortDis)
            {
                side = 3;
                shortDis = Math.Abs(position.Z - Bottom);
            }

            switch (side)
            {
                case 0:     //left

                    position.Z += speed;
                    return position;
                case 1:     //right
                    position.Z += speed;
                    return position;
                case 2:     //top
                    position.X += speed;
                    return position;
                case 3:     //bottom
                    position.X += speed;
                    return position;
                default:
                    return position;
            }

       

        }

        //push outside  2      
        public Vector3 pushOut(Vector3 position,float nextX, float nextZ, float speed)
        {
            int side = 0;
            float shortDis = Math.Abs(position.X - Left);

            //check right
            if (Math.Abs(position.X - Right) < shortDis)
            {
                side = 1;
                shortDis = Math.Abs(position.X - Right);
            }

            //check top
            if (Math.Abs(position.Z - Top) < shortDis)
            {
                side = 2;
                shortDis = Math.Abs(position.Z - Top);
            }

            //check Bottom
            if (Math.Abs(position.Z - Bottom) < shortDis)
            {
                side = 3;
                shortDis = Math.Abs(position.Z - Bottom);
            }

            switch (side)
            {
                case 0:     //left
                    if (position.Z < nextZ)
                    {
                        position.Z += speed;
                    }
                    else
                    {
                        position.Z -= speed;
                    }
                    return position;
                case 1:     //right
                    if (position.Z < nextZ)
                    {
                        position.Z += speed;
                    }
                    else
                    {
                        position.Z -= speed;
                    }
                    return position;
                case 2:     //top
                    if (position.X < nextX)
                    {
                        position.X += speed;
                    }
                    else
                    {
                        position.X -= speed;
                    }
                    return position;
                case 3:     //bottom
                    if (position.X < nextX)
                    {
                        position.X += speed;
                    }
                    else
                    {
                        position.X -= speed;
                    }
                    return position;
                default:
                    return position;
            }

        }
    }
}
