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

        public void Draw(SpriteBatch sb, Texture2D tex) // Draws the player
        {
            sb.Draw(tex, r, Color.White);
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

        /* Method isn't used yet, but may be needed in the future.
        public void Update()
        {

        }
        */

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

        public void Move(KeyboardState ks, MapEditor mapEditor) // Moves the player, will not be in the final game.
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
