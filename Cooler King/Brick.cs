using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cooler_King
{
    internal class Brick
    {
        Rectangle location;
        Color tint;
        public bool alive;
        Texture2D tex;
        public Rectangle rect;

        public Rectangle Rect
        {
            get
            {
                //gets location and sets it as rect
                rect.X = location.X;
                rect.Y = location.Y;

                //returns it into rect var
                return rect;
            }
            
        }

        //Constructer
        public Brick(Texture2D tex, Rectangle location, Color tint)
        {
            this.tex = tex;
            this.location = location;
            this.tint = tint;
            this.alive = true;

            //sets rect position and texture
            rect = new Rectangle(0, 0, tex.Width, tex.Height);
            

        }

        public void DrawMe(SpriteBatch sb)
        {
            //if the brick has not been hit by the ball it draws the brick
            if (alive)
            sb.Draw(tex, location, tint);
        }
    }
}
