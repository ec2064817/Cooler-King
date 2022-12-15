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
                rect.X = location.X;
                rect.Y = location.Y;
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

            rect = new Rectangle(0, 0, tex.Width, tex.Height);
            //Rect = new Rectangle((int)posi.X, (int)posi.Y, tex.Width, tex.Height);

        }


        

        public void CheckCollision(Ball ball)
        {


        }

        //Update
        public void UpdateMe()
        {
            
           
        }

        public void DrawMe(SpriteBatch sb)
        {
            if (alive)
            sb.Draw(tex, location, tint);
        }
    }
}
