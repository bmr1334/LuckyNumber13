using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheImpossiblerGame
{
    class Triangle:MapEditor
    {
        public override void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        {

            //sets the initial values of width and height counters
            widthCounter = 0;
            heightCounter = 0;

            for (int i = 0; i < DataPoints.Count; i++) //loop to go through each tile on the screen
            {
                base.Draw(spriteBatch);
                if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                {
                    spriteBatch.Draw(Triangletexture, Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
                    allPlatforms.Add(Platforms);
                    Triangles.Add(Platforms);
                }
            }
        }
   }
}
