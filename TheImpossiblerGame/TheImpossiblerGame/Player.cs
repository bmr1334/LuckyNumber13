using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

// Brandon Guglielmo - Did most of the work for this class, though a lot is based off homework 2, so any one of us could have made this class.

namespace TheImpossiblerGame
{
    class Player
    {
        Rectangle r;
        public int x, y, w, h;
        public Player(int px, int py, int pw, int ph) // Creates a rectangle that represents the player
        {
            x = px;
            y = py;
            w = pw;
            h = ph;
            r = new Rectangle(px, py, pw, ph);
        }

        public void Draw(SpriteBatch sb, Texture2D tex, SpriteEffects effect) // Draws the player
        {
            sb.Draw(tex, r, null, Color.White, 0, new Vector2(0.0f, 0.0f), effect, 0);
        }

        public Rectangle getRectangle() // Gives the players rectangle
        {
            return r;
        }

        public bool Collision(Rectangle prect, Rectangle rect) // Checks the collision of the player rectangle with another rectangle
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

        public void SetX(int px) // Sets the player's X
        {
            x = px;
            r = new Rectangle(x, y, w, h);
        }

        public void SetY(int py) // Sets the player's Y
        {
            y = py;
            r = new Rectangle(x, y, w, h);
        }
    }
}
