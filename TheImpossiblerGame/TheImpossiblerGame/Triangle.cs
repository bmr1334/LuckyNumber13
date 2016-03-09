using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheImpossiblerGame
{
    class Triangle : MapEditor
    {
        //public override void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        //{

        //    //sets the initial values of width and height counters
        //    widthCounter = 0;
        //    heightCounter = 0;

        //    for (int i = 0; i < DataPoints.Count; i++) //loop to go through each tile on the screen
        //    {
        //        if(i != 0)
        //        {
        //            if (widthCounter > 18) //resets the width when you reached the end of the screen and increases the height 
        //            {
        //                widthCounter = 0;
        //                heightCounter++;
        //            }
        //            else //else increase the width to get to the end of the screen
        //            {
        //                widthCounter++;
        //            }
        //        }
        //        if (DataPoints[i] == "2") //if a 2 is found then load a triangle
        //        {
        //            spriteBatch.Draw(Triangletexture, Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
        //            allPlatforms.Add(Platforms);
        //            Triangles.Add(Platforms);
        //        }
        //    }
        //}
    }
}
