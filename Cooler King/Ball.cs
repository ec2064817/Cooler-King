using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Cooler_King
{
    internal class Ball
    {
        public Vector2 pos,oldPos;
        public Rectangle Rect;
        public Vector2 Speed;

        Vector2 motion;
       

        Texture2D tex;

        int size = 10;


        //Constructer
        public Ball(Texture2D tex, Vector2 posi)
        {
            this.tex = tex;

            oldPos = pos = posi;

            motion = new Vector2(1, -1);
            Speed = new Vector2(3, 3);

            Rect = new Rectangle((int) posi.X, (int) pos.Y, tex.Width / size, tex.Height / size);
        }

        public void MoveBack()
        {
            pos = oldPos;
            Rect.Location = pos.ToPoint();
        }

        public void BounceX()
        {
            Speed.X = -Speed.X;
        }

        public void BounceY()
        {
            Speed.Y = -Speed.Y;
        }

        //Update
        public void UpdateMe(int screenHeight,int screenWidth)
        {
            oldPos = pos;

            if ((pos.X < 0) || (pos.X > screenWidth - Rect.Width))
            {
                // Flip the X (left/right) direction of travel
                BounceX();
            }

            if ((pos.Y < 0) || (pos.Y > screenHeight - Rect.Height))
            {
                // Flip the Y (up/down) direction of travel
                BounceY();
            }

            pos += Speed;

            Rect.Location = pos.ToPoint();


            //Makes it so the paddle can not leave the screen
            if (Rect.X > screenWidth)
            {
                Rect.X = screenWidth;
            }

            if (Rect.X < 0)
            {
                Rect.X = 0;
            }
        }

        public void DrawMe(SpriteBatch sb)
        {
            //draws the paddle at the predefined location with the texture
            sb.Draw(tex, Rect, Color.White);
        }
    }
}
