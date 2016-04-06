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
        protected Texture2D Backgroundtexture;
        protected Texture2D NextBackgroundtexture;
        protected Texture2D Boxtexture;
        protected Texture2D SwitchTriangletexture;
        protected Texture2D SwitchTriangleAlttexture;
        protected Texture2D SwitchBlocktexture;
        protected Texture2D WarningBlocktexture;
        protected Texture2D Triangletexture;
        protected Texture2D Spiketexture;
        protected Texture2D Flip;
        protected Texture2D NextBoxtexture;
        protected Texture2D NextSwitchTriangletexture;
        protected Texture2D NextSwitchTriangleAlttexture;
        protected Texture2D NextSwitchBlocktexture;
        protected Texture2D NextTriangletexture;
        protected Texture2D NextSpiketexture;
        protected Texture2D NextFlip;
        protected Texture2D Player;

        //Rectangles for backgrounds
        public List<Rectangle> BackgroundList;
        public List<Rectangle> NextBackgroundList;
        protected Rectangle Background;
        public Rectangle ScrollingBackground;

        //Rectangles for collision
        protected Rectangle Platforms;
        public Rectangle ScrollingBlock;
        protected List<Rectangle> allPlatforms;
        public List<Rectangle> Squares;
        public List<Rectangle> NextSquares;
        public List<Rectangle> Triangles;
        public List<Rectangle> NextTriangles;
        public List<Rectangle> UpsideDownTriangles;
        public List<Rectangle> NextUpsideDownTriangles;
        public List<Rectangle> Spikes;
        public List<Rectangle> NextSpikes;
        public List<Rectangle> WarningBlock;
        public List<Rectangle> NextWarningBlock;
        public List<Rectangle> SwitchBlock;
        public List<Rectangle> NextSwitchBlock;
        public List<Rectangle> SwitchBlockAlt;
        public List<Rectangle> NextSwitchBlockAlt;
        public List<Rectangle> SwitchTriangle;
        public List<Rectangle> NextSwitchTriangle;
        public List<Rectangle> SwitchTriangleAlt;
        public List<Rectangle> NextSwitchTriangleAlt;

        StreamReader reader; //used to open the file for reading
        protected List<string> DataPoints; //used to store the text in the textfile into a list 
        int number = 0; //used to load another text file
        int scrollingCounter = 0;
        int textureCounter = 0;
        int backgroundCounter = 0;
        bool Switch = false;

        //bool to handle loading files so they won't load forever
        bool canLoadinitial;
        bool canLoadnext;

        bool canLoadinitialBackground;
        bool canLoadnextBackground;

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
            BackgroundList = new List<Rectangle>();
            NextBackgroundList = new List<Rectangle>();
            allPlatforms = new List<Rectangle>();
            Squares = new List<Rectangle>();
            NextSquares = new List<Rectangle>();
            Triangles = new List<Rectangle>();
            NextTriangles = new List<Rectangle>();
            UpsideDownTriangles = new List<Rectangle>();
            NextUpsideDownTriangles = new List<Rectangle>();
            Spikes = new List<Rectangle>();
            NextSpikes = new List<Rectangle>();
            WarningBlock = new List<Rectangle>();
            NextWarningBlock = new List<Rectangle>();
            SwitchBlock = new List<Rectangle>();
            NextSwitchBlock = new List<Rectangle>();
            SwitchBlockAlt = new List<Rectangle>();
            NextSwitchBlockAlt = new List<Rectangle>();
            SwitchTriangle = new List<Rectangle>();
            NextSwitchTriangle = new List<Rectangle>();
            SwitchTriangleAlt = new List<Rectangle>();
            NextSwitchTriangleAlt = new List<Rectangle>();
            canLoadinitial = true;
            canLoadnext = true;
            canLoadinitialBackground = true;
            canLoadnextBackground = true;
            ScrollingBlock = new Rectangle(0, 0, 45, 40);
            ScrollingBackground = new Rectangle(0, 0, 45, 40);
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

        public Texture2D NextBoxTexture //property for box texture
        {
            get { return NextBoxtexture; }
            set
            {
                NextBoxtexture = value;
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

        public Texture2D NextTriangleTexture //property for triangle texture
        {
            get { return NextTriangletexture; }
            set
            {
                NextTriangletexture = value;
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

        public Texture2D Nextflip //property for flipped triangles (upside down)
        {
            get { return NextFlip; }
            set
            {
                NextFlip = value;
            }
        }

        public Texture2D SpikeTexture //property for player texture
        {
            get { return Spiketexture; }
            set
            {
                Spiketexture = value;
            }
        }

        public Texture2D NextSpikeTexture //property for player texture
        {
            get { return NextSpiketexture; }
            set
            {
                NextSpiketexture = value;
            }
        }

        public Texture2D WarningBlockTexture //property for player texture
        {
            get { return WarningBlocktexture; }
            set
            {
                WarningBlocktexture = value;
            }
        }

        public Texture2D SwitchBlockTexture //property for player texture
        {
            get { return SwitchBlocktexture; }
            set
            {
                SwitchBlocktexture = value;
            }
        }

        public Texture2D SwitchTriangleTexture //property for player texture
        {
            get { return SwitchTriangletexture; }
            set
            {
                SwitchTriangletexture = value;
            }
        }

        public Texture2D NextSwitchTriangleTexture //property for player texture
        {
            get { return NextSwitchTriangletexture; }
            set
            {
                NextSwitchTriangletexture = value;
            }
        }

        public Texture2D SwitchTriangleAltTexture //property for player texture
        {
            get { return SwitchTriangleAlttexture; }
            set
            {
                SwitchTriangleAlttexture = value;
            }
        }

        public Texture2D NextSwitchTriangleAltTexture //property for player texture
        {
            get { return NextSwitchTriangleAlttexture; }
            set
            {
                NextSwitchTriangleAlttexture = value;
            }
        }

        public Texture2D BackgroundTexture //property for player texture
        {
            get { return Backgroundtexture; }
            set
            {
                Backgroundtexture = value;
            }
        }

        public Texture2D NextBackgroundTexture //property for player texture
        {
            get { return NextBackgroundtexture; }
            set
            {
                NextBackgroundtexture = value;
            }
        }

        public List<Rectangle> Backgroundlist //property for list of platforms/squares
        {
            get { return BackgroundList; }
            set
            {
                BackgroundList = value;
            }
        }

        public List<Rectangle> NextBackgroundlist //property for list of platforms/squares
        {
            get { return NextBackgroundList; }
            set
            {
                NextBackgroundList = value;
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

        public List<Rectangle> triangles
        {
            get { return Triangles; }
            set
            {
                Triangles = value;
            }
        }

        public List<Rectangle> Nexttriangles
        {
            get { return NextTriangles; }
            set
            {
                NextTriangles = value;
            }
        }

        public List<Rectangle> spikes
        {
            get { return Spikes; }
            set
            {
                Spikes = value;
            }
        }

        public List<Rectangle> Nextspikes
        {
            get { return NextSpikes; }
            set
            {
                NextSpikes = value;
            }
        }

        public List<Rectangle> UpsideDowntriangles
        {
            get { return UpsideDownTriangles; }
            set
            {
                UpsideDownTriangles = value;
            }
        }

        public List<Rectangle> NextUpsideDowntriangles
        {
            get { return NextUpsideDownTriangles; }
            set
            {
                NextUpsideDownTriangles = value;
            }
        }

        public List<Rectangle> Warningblock
        {
            get { return WarningBlock; }
            set
            {
                WarningBlock = value;
            }
        }

        public List<Rectangle> NextWarningblock
        {
            get { return NextWarningBlock; }
            set
            {
                NextWarningBlock = value;
            }
        }

        public List<Rectangle> Switchblock
        {
            get { return SwitchBlock; }
            set
            {
                SwitchBlock = value;
            }
        }

        public List<Rectangle> NextSwitchblock
        {
            get { return NextSwitchBlock; }
            set
            {
                NextSwitchBlock = value;
            }
        }

        public List<Rectangle> SwitchBlockalt
        {
            get { return SwitchBlockAlt; }
            set
            {
                SwitchBlockAlt = value;
            }
        }

        public List<Rectangle> NextSwitchBlockalt
        {
            get { return NextSwitchBlockAlt; }
            set
            {
                NextSwitchBlockAlt = value;
            }
        }

        public List<Rectangle> Switchtriangle
        {
            get { return SwitchTriangle; }
            set
            {
                SwitchTriangle = value;
            }
        }

        public List<Rectangle> NextSwitchtriangle
        {
            get { return NextSwitchTriangle; }
            set
            {
                NextSwitchTriangle = value;
            }
        }

        public List<Rectangle> SwitchTrianglealt
        {
            get { return SwitchTriangleAlt; }
            set
            {
                SwitchTriangleAlt = value;
            }
        }

        public List<Rectangle> NextSwitchTrianglealt
        {
            get { return NextSwitchTriangleAlt; }
            set
            {
                NextSwitchTriangleAlt = value;
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

        public int ScrollingBackgroundX //property for scrolling indicator block
        {
            get { return ScrollingBackground.X; }
            set
            {
                ScrollingBackground.X = value;
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

        public bool SWITCH
        {
            get { return Switch; }
            set
            {
                Switch = value;
            }
        }

        public int ScrollingCounter
        {
            get { return scrollingCounter; }
        }

        public int TextureCounter
        {
            get { return textureCounter; }
        }

        public int BackgroundCounter
        {
            get { return backgroundCounter; }
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

        public bool CanLoadInitialBackground //property to change value of loading the initial file
        {
            get { return canLoadinitialBackground; }
            set
            {
                canLoadinitialBackground = value;
            }
        }

        public bool CanLoadNextBackground //property to change the value for loading the next file offscreen
        {
            get { return canLoadnextBackground; }
            set
            {
                canLoadnextBackground = value;
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
                if (s.Contains("Level" + number.ToString() + ".txt"))
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

        public int ResetFiles()
        {
            if (backgroundCounter > 3) //has to be one less than the number of backgrounds
            {
                backgroundCounter = 0;
            }
            if (textureCounter > 5)
            {
                textureCounter = 0;
            }
            if (scrollingCounter > 2)
            {
                scrollingCounter = 0;
            }
            if (number > 5) //has to be one less than the number of levels
            {
                number = 0;
                scrollingCounter++;
                textureCounter++;
            }
            return scrollingCounter;
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
                else if (DataPoints[i] == "3") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    UpsideDowntriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "4") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    Spikes.Add(Platforms);
                }
                else if (DataPoints[i] == "5") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    WarningBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "6") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    SwitchBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "7") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    SwitchBlockAlt.Add(Platforms);
                }
                else if (DataPoints[i] == "8") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    SwitchTriangle.Add(Platforms);
                }
                else if (DataPoints[i] == "9") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    SwitchTriangleAlt.Add(Platforms);
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
                    NextTriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "3") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextUpsideDownTriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "4") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextSpikes.Add(Platforms);
                }
                else if (DataPoints[i] == "5") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextWarningBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "6") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextSwitchBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "7") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextSwitchBlockAlt.Add(Platforms);
                }
                else if (DataPoints[i] == "8") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextSwitchTriangle.Add(Platforms);
                }
                else if (DataPoints[i] == "9") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    allPlatforms.Add(Platforms);
                    NextSwitchTriangleAlt.Add(Platforms);
                }
            }
            DataPoints.Clear(); //clear the data stored from the file after the platforms are generated to open space for a new file to be read
            canLoadnext = true; //set this value to true so we can load 2 files at once at the beginning
        }

        public void GenerateBackgroundsOnScreen()
        {
            Background = new Rectangle(0, 0, screenWidth, screenHeight);
            BackgroundList.Add(Background);
            canLoadinitialBackground = false;
            canLoadnextBackground = true;
        }

        public void GenerateBackgroundsOffScreen()
        {
            backgroundCounter++;
            Background = new Rectangle(screenWidth, 0, screenWidth, screenHeight);
            NextBackgroundList.Add(Background);
            canLoadnextBackground = true;
        }




        public virtual void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        {
            for (int i = 0; i < BackgroundList.Count; i++)
            {
                spriteBatch.Draw(Backgroundtexture, BackgroundList[i], Color.White);
            }
            for (int i = 0; i < NextBackgroundList.Count; i++)
            {
                spriteBatch.Draw(NextBackgroundtexture, NextBackgroundList[i], Color.White);
            }
            for (int i = 0; i < Squares.Count; i++) //draws platforms currently visible
            {
                spriteBatch.Draw(Boxtexture, Squares[i], Color.White);
            }
            for (int i = 0; i < NextSquares.Count; i++) //draws platforms off screen
            {
                spriteBatch.Draw(NextBoxtexture, NextSquares[i], Color.White);
            }
            for (int i = 0; i < Triangles.Count; i++) //draws triangles
            {
                spriteBatch.Draw(Triangletexture, Triangles[i], Color.White);
            }
            for (int i = 0; i < NextTriangles.Count; i++) //draws triangles off screen
            {
                spriteBatch.Draw(NextTriangletexture, NextTriangles[i], Color.White);
            }
            for (int i = 0; i < Spikes.Count; i++) //draws triangles
            {
                spriteBatch.Draw(Spiketexture, Spikes[i], Color.White);
            }
            for (int i = 0; i < Nextspikes.Count; i++) //draws triangles off screen
            {
                spriteBatch.Draw(NextSpiketexture, NextSpikes[i], Color.White);
            }
            for (int i = 0; i < UpsideDownTriangles.Count; i++) //draws triangles off screen
            {
                spriteBatch.Draw(Flip, UpsideDownTriangles[i], Color.White);
            }
            for (int i = 0; i < NextUpsideDownTriangles.Count; i++) //draws triangles off screen
            {
                spriteBatch.Draw(NextFlip, NextUpsideDownTriangles[i], Color.White);
            }
            if (textureCounter > 1 && textureCounter <= 3)
            {
                for (int i = 0; i < WarningBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(WarningBlocktexture, WarningBlock[i], Color.LightGray);
                }
                for (int i = 0; i < NextWarningBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(WarningBlocktexture, NextWarningBlock[i], Color.LightGray);
                }
                for (int i = 0; i < SwitchBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, SwitchBlock[i], Color.LightGray);
                }
                for (int i = 0; i < NextSwitchBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, NextSwitchBlock[i], Color.LightGray);
                }
                for (int i = 0; i < SwitchBlockAlt.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, SwitchBlockAlt[i], Color.LightGray);
                }
                for (int i = 0; i < NextSwitchBlockAlt.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, NextSwitchBlockAlt[i], Color.LightGray);
                }
            }
            else
            {
                for (int i = 0; i < WarningBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(WarningBlocktexture, WarningBlock[i], Color.White);
                }
                for (int i = 0; i < NextWarningBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(WarningBlocktexture, NextWarningBlock[i], Color.White);
                }
                for (int i = 0; i < SwitchBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, SwitchBlock[i], Color.White);
                }
                for (int i = 0; i < NextSwitchBlock.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, NextSwitchBlock[i], Color.White);
                }
                for (int i = 0; i < SwitchBlockAlt.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, SwitchBlockAlt[i], Color.White);
                }
                for (int i = 0; i < NextSwitchBlockAlt.Count; i++) //draws triangles off screen
                {
                    spriteBatch.Draw(SwitchBlocktexture, NextSwitchBlockAlt[i], Color.White);
                }
            }
            if (textureCounter <= 1)
            {
                if (Switch == true)
                {
                    for (int i = 0; i < SwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangletexture, SwitchTriangle[i], Color.White);
                    }
                    for (int i = 0; i < NextSwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangletexture, NextSwitchTriangle[i], Color.White);
                    }
                }
                else if (Switch == false)
                {
                    for (int i = 0; i < SwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangleAlttexture, SwitchTriangleAlt[i], Color.White);
                    }
                    for (int i = 0; i < NextSwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangleAlttexture, NextSwitchTriangleAlt[i], Color.White);
                    }
                }
            }
            else if (textureCounter <= 3)
            {
                if (Switch == true)
                {
                    for (int i = 0; i < SwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangletexture, SwitchTriangle[i], Color.DarkGray);
                    }
                    for (int i = 0; i < NextSwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangletexture, NextSwitchTriangle[i], Color.DarkGray);
                    }
                }
                else if (Switch == false)
                {
                    for (int i = 0; i < SwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangleAlttexture, SwitchTriangleAlt[i], Color.DarkGray);
                    }
                    for (int i = 0; i < NextSwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangleAlttexture, NextSwitchTriangleAlt[i], Color.DarkGray);
                    }
                }
            }
            else if (textureCounter <= 5)
            {
                if (Switch == true)
                {
                    for (int i = 0; i < SwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangletexture, SwitchTriangle[i], Color.White);
                    }
                    for (int i = 0; i < NextSwitchTriangle.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangletexture, NextSwitchTriangle[i], Color.White);
                    }
                }
                else if (Switch == false)
                {
                    for (int i = 0; i < SwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(SwitchTriangleAlttexture, SwitchTriangleAlt[i], Color.White);
                    }
                    for (int i = 0; i < NextSwitchTriangleAlt.Count; i++) //draws triangles off screen
                    {
                        spriteBatch.Draw(NextSwitchTriangleAlttexture, NextSwitchTriangleAlt[i], Color.White);
                    }
                }
            }
        }
    }
}

