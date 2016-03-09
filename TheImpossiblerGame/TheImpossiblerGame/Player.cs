using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace TheImpossiblerGame
{
    class Player
    {
        Rectangle r;
        Texture2D img;
        public Player(Rectangle rect)
        {
            r = rect;
        }

        public void Draw(SpriteBatch sb, Texture2D tex)
        {
            sb.Draw(tex, r, Color.White);
        }

        public Rectangle getRectangle()
        {
            return r;
        }

        public void Physics()
        {

        }
    }
}
