using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cooler_King
{
    internal class Paddle
    {
        Vector2 pos, oldPos;
        public Rectangle Rect;
        

        KeyboardState kb;

        Texture2D tex;
        

        //Constructer
        public Paddle(Texture2D tex, Vector2 posi)
        {
            this.tex = tex;

            oldPos = pos = posi;

            Rect = new Rectangle((int)posi.X, (int)posi.Y, tex.Width, tex.Height);
        }

        //public void MoveBack()
        //{
        //    pos = oldPos;
        //    Rect.Location = pos.ToPoint();
        //}

        //Update
        public void UpdateMe(int screenWidth)
        {
            //oldPos = pos;

            

            //Setting kb as the keyboard get state function
            kb = Keyboard.GetState();

            //if the left arrow is pressed the paddle goes left
            if (kb.IsKeyDown(Keys.Left))
                Rect.X -= 10;

            //if the right arrow is pressed the paddle goes right
            if (kb.IsKeyDown(Keys.Right))
            {
                Rect.X += 10;
            }
                

            //Makes it so the paddle can not leave the screen
            if (Rect.X > screenWidth - tex.Width)
            {
                Rect.X = screenWidth - tex.Width;   
            }

            if (Rect.X < 0)
            {
                Rect.X = 0;
            }

            //Rect.Location = pos.ToPoint();
        }

        public void DrawMe(SpriteBatch sb)
        {
            //draws the paddle at the predefined location with the texture
            sb.Draw(tex, Rect, Color.White);
        }

        
        
    }
}
