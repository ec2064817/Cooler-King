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
        Vector2 pos;
        public Rectangle Rect;

        Vector2 motion;
        float ballSpeed = 4;

        Texture2D tex;

        int size = 10;


        //Constructer
        public Ball(Texture2D tex, int xPos, int yPos)
        {
            this.tex = tex;

            motion = new Vector2(1, -1);

            Rect = new Rectangle(xPos, yPos, tex.Width / size, tex.Height / size);
        }

        //Update
        public void UpdateMe(int screenWidth)
        {

            pos += motion * ballSpeed;

            //Makes it so the paddle can not leave the screen
            if (Rect.X > screenWidth - tex.Width)
            {
                Rect.X = screenWidth - tex.Width;
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
