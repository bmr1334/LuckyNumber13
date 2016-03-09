using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheImpossiblerGame
{
    class Box : MapEditor
    {

        //public override void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        //{

        //    //sets the initial values of width and height counters
        //    widthCounter = 0;
        //    heightCounter = 0;

        //    for (int i = 0; i < DataPoints.Count; i++) //loop to go through each tile on the screen
        //    {
        //        if (i != 0)
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
        //        if (DataPoints[i] == "1") //if a 1 is found in the textfile/list of strings then load a square
        //        {
        //            if (i == 0) //used to draw the top left block
        //            {
        //                spriteBatch.Draw(Boxtexture, Platforms = new Rectangle(0, 0, TileWidth, TileHeight), Color.White);
        //                allPlatforms.Add(Platforms);
        //                Squares.Add(Platforms);
        //            }
        //            else if (i < 20) //used to draw the rest of the blocks at the top
        //            {
        //                spriteBatch.Draw(Boxtexture, Platforms = new Rectangle(widthCounter * TileWidth, 0, TileWidth, TileHeight), Color.White);
        //                allPlatforms.Add(Platforms);
        //                Squares.Add(Platforms);
        //            }
        //            else if (i > DataPoints.Count - 21) //used to draw the blocks at the very bottom of the screen
        //            {

        //                spriteBatch.Draw(Boxtexture, Platforms = new Rectangle(widthCounter * TileWidth, 560, TileWidth, TileHeight), Color.White);
        //                allPlatforms.Add(Platforms);
        //                Squares.Add(Platforms);
        //            }
        //            else //used to draw the blocks throughout the map
        //            {
        //                spriteBatch.Draw(Boxtexture, Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
        //                allPlatforms.Add(Platforms);
        //                Squares.Add(Platforms);
        //            }
        //        }
        //    }

        //}




    }
}

