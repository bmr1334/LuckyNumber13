using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace TheImpossiblerGame
{
    class Player
    {
        Rectangle r;
        public int x, y, w, h;
        public Player(int px, int py, int pw, int ph)
        {
            x = px;
            y = py;
            w = pw;
            h = ph;
            r = new Rectangle(px, py, pw, ph);
        }

        public void Draw(SpriteBatch sb, Texture2D tex)
        {
            sb.Draw(tex, r, Color.White);
        }

        public Rectangle getRectangle()
        {
            return r;
        }

        public bool Collision(Rectangle prect, Rectangle rect)
        {
            if (prect.Intersects(rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update()
        {

        }

        public void SetX(int px)
        {
            x = px;
            r = new Rectangle(x, y, w, h);
        }

        public void SetY(int py)
        {
            y = py;
            r = new Rectangle(x, y, w, h);
        }

        public void Move(KeyboardState ks, MapEditor mapEditor)
        {
            if (ks.IsKeyDown(Keys.Left) == true || (ks.IsKeyDown(Keys.A) == true))
            {
                SetX(x - 3);
            }


            if (ks.IsKeyDown(Keys.Right) == true || (ks.IsKeyDown(Keys.D) == true))
            {
                SetX(x + 3);
            }
        }
    }
}
