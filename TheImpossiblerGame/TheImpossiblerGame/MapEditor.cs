﻿using System;
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
        public Rectangle ScrollingBlock;
        protected List<Rectangle> allPlatforms;
        public List<Rectangle> Squares;
        public List<Rectangle> NextSquares;
        public List<Rectangle> Triangles;

        StreamReader reader; //used to open the file for reading
        protected List<string> DataPoints; //used to store the text in the textfile into a list 
        int number = 0; //used to load another text fuke

        //bool to handle loading files so they won't load forever
        bool canLoadinitial;
        bool canLoadnext;

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
        int screenHeight;
        int screenWidth;

        //previous dimensions(height = 600, width = 900)


        public MapEditor()
        {
            //creates the rectangle lists to populate and established initial values
            allPlatforms = new List<Rectangle>();
            Squares = new List<Rectangle>();
            NextSquares = new List<Rectangle>();
            Triangles = new List<Rectangle>();
            canLoadinitial = true;
            canLoadnext = true;
            ScrollingBlock = new Rectangle(0, 0, 45, 40);
        }



        public int ScreenWidth //property for screen width
        {
            get { return screenWidth; }
            set
            {
                screenWidth = value;
            }
        }

        public int tileWidth //property for tile width
        {
            get { return TileWidth; }
        }

        public int ScreenHeight //property for screen height
        {
            get { return screenHeight; }
            set
            {
                screenHeight = value;
            }
        }

        public int tileHeight //property for tile height
        {
            get { return TileHeight; }
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

        public List<Rectangle> squares //property for list of platforms/squares
        {
            get { return Squares; }
            set
            {
                Squares = value;
            }
        }

        public List<Rectangle> Nextsquares //property for list of platforms/squares
        {
            get { return NextSquares; }
            set
            {
                NextSquares = value;
            }
        }

        public int ScrollingBlockX //property for scrolling indicator block
        {
            get { return ScrollingBlock.X; }
            set
            {
                ScrollingBlock.X = value;
            }
        }

        public int Number //property for textfile extension incrementer
        {
            get { return number; }
            set
            {
                number = value;
            }
        }



        public void SetDimensions() //method to set the tiles according to the screen dimensions
        {
            TileHeight = screenHeight / 15; //equals 40
            TileWidth = screenWidth / 20; //equals 45
        }

        public bool CanLoadInitial //property to change value of loading the initial file
        {
            get { return canLoadinitial; }
            set
            {
                canLoadinitial = value;
            }
        }

        public bool CanLoadNext //property to change the value for loading the next file offscreen
        {
            get { return canLoadnext; }
            set
            {
                canLoadnext = value;
            }
        }

        public void LoadTextFile() //method to find the text file
        {
            //TextPath = null; IMPORTANT: without this, the textpath will always contain the name of the last file read, which will infinitely loop the last file

            //sets both values to true if we are trying to load a file
            canLoadnext = true;
            canLoadinitial = true;
            number++; //increases number to load in next file
            string[] files = Directory.GetFiles(".");
            foreach (string s in files) //foreach loop to search for the specified file
            {
                if (s.Contains("Test" + number.ToString() + ".txt"))
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
            reader.Close();

            //sets both values to false so that after we are done reading the file we won't read it again
            canLoadinitial = false;
            canLoadnext = false;
        }

        public void GeneratePlatformsOnScreen() //handles platform generation for the first text file
        {
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

                if (DataPoints[i] == "0") //not needed but included anyway
                {

                }
                else if (DataPoints[i] == "1") //if a 1 is found in the textfile/list of strings then load a square
                {
                    if (i == 0) //used to draw the top left block
                    {
                        Platforms = new Rectangle(0, 0, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                    else if (i < 20) //used to draw the rest of the blocks at the top
                    {
                        Platforms = new Rectangle(widthCounter * TileWidth, 0, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                    else //used to draw the blocks throughout the map
                    {
                        Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                }
                else if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    Triangles.Add(Platforms);
                }
            }
            DataPoints.Clear(); //clears the information read in the file to open space to write new information for another file
            canLoadnext = true; //set this value to true so we can load 2 files at once at the beginning
        }

        public void GeneratePlatformsOffScreen() //handles all platform generation after the first text file is read
        {
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

                if (DataPoints[i] == "0") //not needed but included anyway
                {

                }
                else if (DataPoints[i] == "1") //if a 1 is found in the textfile/list of strings then load a square
                {
                    if (i == 0) //used to draw the top left block
                    {
                        Platforms = new Rectangle(0 + screenWidth, 0, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                    else if (i < 20) //used to draw the rest of the blocks at the top
                    {
                        Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, 0, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                    else //used to draw the blocks throughout the map
                    {
                        Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                        allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                }
                else if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    Triangles.Add(Platforms);
                }
            }
            DataPoints.Clear(); //clear the data stored from the file after the platforms are generated to open space for a new file to be read
        }




        public virtual void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        {
            for (int i = 0; i < Squares.Count; i++) //draws platforms currently visible
            {
                spriteBatch.Draw(Boxtexture, Squares[i], Color.White);
            }
            for (int i = 0; i < NextSquares.Count; i++) //draws platforms off screen
            {
                spriteBatch.Draw(Boxtexture, NextSquares[i], Color.White);
            }
            for (int i = 0; i < Triangles.Count; i++) //draws triangles
            {
                spriteBatch.Draw(Triangletexture, Triangles[i], Color.White);
            }
        }
    }
}
