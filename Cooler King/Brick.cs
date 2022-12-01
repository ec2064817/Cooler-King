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
        bool alive;
        Texture2D tex;

        //Constructer
        public Brick(Texture2D tex, Rectangle location, Color tint)
        {
            this.tex = tex;
            this.location = location;
            this.tint = tint;

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
