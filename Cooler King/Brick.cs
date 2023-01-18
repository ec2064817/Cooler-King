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
        public int hitpoints;
        Texture2D tex;
        public Rectangle rect;
        public bool isGlass;
        public bool isBomb;

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
        public Brick(Texture2D tex, Rectangle location, Color tint, int hp)
        {
            this.tex = tex;
            this.location = location;
            this.tint = tint;
            this.alive = true;

            //sets rect position and texture
            rect = new Rectangle(0, 0, tex.Width, tex.Height);
            //sets hitpoints as hp
            hitpoints = hp;
        }

        public void DrawMe(SpriteBatch sb)
        {
            //if the brick has not been killed
            if (alive)
                //draw the brick
                sb.Draw(tex, location, tint);
        }
    }
}
