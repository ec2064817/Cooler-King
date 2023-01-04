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

            //sets the old position as the current position and stores it in the 'posi' var
            oldPos = pos = posi;

            //sets motion as a vector2 with (1, -1)
            motion = new Vector2(1, -1);

            //sets speed with a vector2 with (3, 3)
            Speed = new Vector2(3, 3);

            //sets Rect as a new rectangle with the 'posi' var as its position and the textures height and width divided by the size var as it's own
            Rect = new Rectangle((int) posi.X, (int) pos.Y, tex.Width / size, tex.Height / size);
        }

        public void MoveBack()
        {
            //moves the back the ball to the last frame
            pos = oldPos;
            Rect.Location = pos.ToPoint();
        }

        public void BounceX()
        {
            //flips the X velocity
            Speed.X = -Speed.X;
        }

        public void BounceY()
        {
            //flips the Y velocity
            Speed.Y = -Speed.Y;
        }

        //Update
        public void UpdateMe(int screenHeight,int screenWidth)
        {
            //sets the 'oldPos' as the 'pos' var
            oldPos = pos;

            //if the ball hits the left or right of the screen
            if ((pos.X < 0) || (pos.X > screenWidth - Rect.Width))
            {
                // Flip the X (left/right) direction of travel
                BounceX();
            }

            //if the ball hits the top or bottom of the screen
            if ((pos.Y < 0) || (pos.Y > screenHeight - Rect.Height))
            {
                // Flip the Y (up/down) direction of travel
                BounceY();
            }

            
            pos += Speed;

            Rect.Location = pos.ToPoint();


            //Makes it so the ball can not leave the screen
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
            //draws the ball at the predefined location with the texture
            sb.Draw(tex, Rect, Color.White);
        }
    }
}
