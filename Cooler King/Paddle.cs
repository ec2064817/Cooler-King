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

            //sets the old position as the current position and stores it in the 'posi' var
            oldPos = pos = posi;

            //sets Rect as a new rectangle with the 'posi' var as its position and the textures height and width as it's own
            Rect = new Rectangle((int)posi.X, (int)posi.Y, tex.Width, tex.Height);
        }

        //Update
        public void UpdateMe(int screenWidth)
        {
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
                //pushes the paddle so it does not leave the right of the screen
                Rect.X = screenWidth - tex.Width;   
            }

            if (Rect.X < 0)
            {
                //pushes the paddle so it does not leave the left of the screen
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
