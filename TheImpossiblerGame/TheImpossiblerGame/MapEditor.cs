using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace TheImpossiblerGame
{
    class MapEditor
    {
        //textures for the different objects
        protected Texture2D Boxtexture;
        protected Texture2D Triangletexture;
        protected Texture2D Flip;
        protected Texture2D Player;

        //Rectangles for collision
        protected Rectangle Platforms;
        protected List<Rectangle> allPlatforms;
        public List<Rectangle> Squares;
        public List<Rectangle> Triangles;

        StreamReader reader; //used to open the file for reading
        protected List<string> DataPoints; //used to store the text in the textfile into a list 

        //used to get the filename and store the text in a string to add it to the list
        string ReadFile;
        string TextFile;
        string TextPath;

        //used to keep track of the height and width of the screen objects are being mapped to
        protected int heightCounter;
        protected int widthCounter;

        //sets the screen into specific tiles
        protected int TileWidth;
        protected int TileHeight;

        //keeps track of the screen dimensions
        const int SCREEN_HEIGHT = 600;
        const int SCREEN_WIDTH = 900;

        public MapEditor()
        {
            //makes the screen 20 tiles wide by 15 tiles high
            TileHeight = SCREEN_HEIGHT / 15; //equals 40
            TileWidth = SCREEN_WIDTH / 20; //equals 45
            
        }

        public Texture2D player //property for player texture
        {
            get { return Player; }
            set
            {
                Player = value;
            }
        }

        public Texture2D BoxTexture //property for box texture
        {
            get { return Boxtexture; }
            set
            {
                Boxtexture = value;
            }
        }

        public Texture2D TriangleTexture //property for triangle texture
        {
            get { return Triangletexture; }
            set
            {
                Triangletexture = value;
            }
        }

        public Texture2D flip //property for flipped triangles (upside down)
        {
            get { return Flip; }
            set
            {
                Flip = value;
            }
        }

        public void LoadTextFile() //method to find the text file
        {
            string[] files = Directory.GetFiles(".");
            foreach (string s in files) //foreach loop to search for the specified file
            {
                if (s.Contains("Testing.txt"))
                {
                    TextPath = s; //set the textpath to the file found
                }
            }
            TextFile = Path.GetFileName(TextPath); //format the textpath to only include the filename
        }

        public void ReadTextFile() //method to read the text file
        {
            DataPoints = new List<string>();
            ReadFile = "";
            reader = new StreamReader(TextFile);
            while ((ReadFile = reader.ReadLine()) != null) //loop to read every line
            {
                foreach (char c in ReadFile) //foreach character in the line add that to a list
                {
                    DataPoints.Add(c.ToString());
                }
            }
        }

        //public void GeneratePlatforms()
        // {
        //      boxes = new List<Rectangle>();
        //    for (int i = 0; i < DataPoints.Count; i++)
        //    {
        //       if (DataPoints[i] == "1")
        //      {
        //         platform = new Rectangle(i * TileWidth, TileHeight, TileWidth, TileHeight);
        //        boxes.Add(platform);
        //    }
        // }
        // }

        public virtual void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        {
            //sets the initial values of width and height counters
            widthCounter = 0;
            heightCounter = 0;

            for (int i = 0; i < DataPoints.Count; i++) //loop to go through each tile on the screen
            {
                if (i != 0) 
                {
                    if (widthCounter > 18) //resets the width when you reached the end of the screen and increases the height 
                    {
                        widthCounter = 0;
                        heightCounter++;
                    }
                    else //else increase the width to get to the end of the screen
                    {
                        widthCounter++;
                    }
                }
                //if (DataPoints[i] == "0") //not needed but included anyway
                //{

                //}
                //else if (DataPoints[i] == "1") //if a 1 is found in the textfile/list of strings then load a square
                //{
                    //if (i == 0) //used to draw the top left block
                    //{
                        //spriteBatch.Draw(Boxtexture, new Rectangle(0, 0, TileWidth, TileHeight), Color.White);
                    //}
                    //else if (i < 20) //used to draw the rest of the blocks at the top
                    //{
                        //spriteBatch.Draw(Boxtexture, new Rectangle(widthCounter * TileWidth, 0, TileWidth, TileHeight), Color.White);
                    //}
                    //else if (i > DataPoints.Count - 21) //used to draw the blocks at the very bottom of the screen
                    //{

                        //spriteBatch.Draw(Boxtexture, new Rectangle(widthCounter * TileWidth, 560, TileWidth, TileHeight), Color.White);

                    //}
                    //else //used to draw the blocks throughout the map
                    //{
                        //spriteBatch.Draw(Boxtexture, new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
                    //}
                //}
                //else if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                //{
                //    spriteBatch.Draw(Triangletexture, new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
                //}
                //else if (DataPoints[i] == "3") //if a 3 is found then load a upside down triangle
                //{
                //    spriteBatch.Draw(Flip, new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
                //}
                //else if (DataPoints[i] == "4") //if a 3 is found then load a upside down triangle
               // {
               //     spriteBatch.Draw(Player, new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight), Color.White);
                //}
            }
        }

        public bool Collision(Player p, Rectangle r)
        {
                if (r.Intersects(p.getRectangle()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        }
    }
