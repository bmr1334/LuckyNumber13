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
        protected Texture2D Parallaxtexture;
        protected Texture2D NextParallaxtexture;
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
        public List<Rectangle> ParallaxList;
        public List<Rectangle> NextParallaxList;
        protected Rectangle Parallax;
        public Rectangle ScrollingParallax;
        public List<Rectangle> BackgroundList;
        public List<Rectangle> NextBackgroundList;
        protected Rectangle Background;
        public Rectangle ScrollingBackground;

        //Rectangles for collision
        protected Rectangle Platforms;
        protected Rectangle Collision;
        public Rectangle ScrollingBlock;
        protected List<Rectangle> allPlatforms;
        public List<Rectangle> SidewaysCollision;
        public List<Rectangle> NextSidewaysCollision;
        public List<Rectangle> SwitchCollisionRectangleAlt;
        public List<Rectangle> NextSwitchCollisionRectangleAlt;
        public List<Rectangle> SwitchCollisionRectangle;
        public List<Rectangle> NextSwitchCollisionRectangle;
        public List<Rectangle> CollisionRectangle;
        public List<Rectangle> NextCollisionRectangle;
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

        //ints to save the score
        int highscore;
        int displayHighscore;
        int score;
        int displayScore;

        int number = 0; //used to load another text file

        //counters use to switch textures and increase scrolling speed
        int scrollingCounter;
        int textureSwitch = 0;
        int textureCounter = 0;
        int parallaxCounter = 0;
        int backgroundCounter = 0;

        //bool used for the switch blocks
        bool Switch = false;

        //bool to handle loading files so they won't load forever
        bool canLoadinitial;
        bool canLoadnext;

        //bool to handle switching textures
        bool canSwitch;

        //bool to handle reading and saving the scores
        bool canSave;
        bool canRead;
        bool canReadhighScore;

        //bool to handle loading parallax backgrounds
        bool canLoadinitialParallax;
        bool canLoadnextParallax;

        //bool to handle loading backgrounds
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


        public MapEditor()
        {
            //creates the rectangle lists to populate and established initial values
            ParallaxList = new List<Rectangle>();
            NextParallaxList = new List<Rectangle>();
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
            CollisionRectangle = new List<Rectangle>();
            NextCollisionRectangle = new List<Rectangle>();
            SwitchCollisionRectangle = new List<Rectangle>();
            NextSwitchCollisionRectangle = new List<Rectangle>();
            SwitchCollisionRectangleAlt = new List<Rectangle>();
            NextSwitchCollisionRectangleAlt = new List<Rectangle>();
            SidewaysCollision = new List<Rectangle>();
            NextSidewaysCollision = new List<Rectangle>();
            Collision = new Rectangle();
            canLoadinitial = true;
            canLoadnext = true;
            canSwitch = true;
            canLoadinitialParallax = true;
            canLoadnextParallax = true;
            canLoadinitialBackground = true;
            canLoadnextBackground = true;
            canSave = true;
            canRead = true;
            canReadhighScore = true;
            ScrollingBlock = new Rectangle(0, 0, 45, 40);
            ScrollingParallax = new Rectangle(0, 0, 45, 40);
            //ScrollingBackground = new Rectangle(8 / 3, 0, 45, 40); //(-10) moved to the screen dimension method to maintain consistency across screens
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

        public Texture2D SpikeTexture //property for spike texture
        {
            get { return Spiketexture; }
            set
            {
                Spiketexture = value;
            }
        }

        public Texture2D NextSpikeTexture //property for spike texture
        {
            get { return NextSpiketexture; }
            set
            {
                NextSpiketexture = value;
            }
        }

        public Texture2D WarningBlockTexture //property for warning block texture
        {
            get { return WarningBlocktexture; }
            set
            {
                WarningBlocktexture = value;
            }
        }

        public Texture2D SwitchBlockTexture //property for switch block texture
        {
            get { return SwitchBlocktexture; }
            set
            {
                SwitchBlocktexture = value;
            }
        }

        public Texture2D SwitchTriangleTexture //property for switch triangle texture
        {
            get { return SwitchTriangletexture; }
            set
            {
                SwitchTriangletexture = value;
            }
        }

        public Texture2D NextSwitchTriangleTexture //property for switch triangle texture
        {
            get { return NextSwitchTriangletexture; }
            set
            {
                NextSwitchTriangletexture = value;
            }
        }

        public Texture2D SwitchTriangleAltTexture //property for switch triangle texture
        {
            get { return SwitchTriangleAlttexture; }
            set
            {
                SwitchTriangleAlttexture = value;
            }
        }

        public Texture2D NextSwitchTriangleAltTexture //property for switch triangle texture
        {
            get { return NextSwitchTriangleAlttexture; }
            set
            {
                NextSwitchTriangleAlttexture = value;
            }
        }

        public Texture2D BackgroundTexture //property for background texture
        {
            get { return Backgroundtexture; }
            set
            {
                Backgroundtexture = value;
            }
        }

        public Texture2D NextBackgroundTexture //property for background texture
        {
            get { return NextBackgroundtexture; }
            set
            {
                NextBackgroundtexture = value;
            }
        }

        public Texture2D ParallaxTexture //property for parallax texture
        {
            get { return Parallaxtexture; }
            set
            {
                Parallaxtexture = value;
            }
        }

        public Texture2D NextParallaxTexture //property for parallax texture
        {
            get { return NextParallaxtexture; }
            set
            {
                NextParallaxtexture = value;
            }
        }

        public List<Rectangle> Parallaxlist //property for parallax list
        {
            get { return ParallaxList; }
            set
            {
                ParallaxList = value;
            }
        }

        public List<Rectangle> NextParallaxlist //property for parallax list
        {
            get { return NextParallaxList; }
            set
            {
                NextParallaxList = value;
            }
        }

        public List<Rectangle> Backgroundlist //property for list of backgrounds
        {
            get { return BackgroundList; }
            set
            {
                BackgroundList = value;
            }
        }

        public List<Rectangle> NextBackgroundlist //property for list of backgrounds
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

        public List<Rectangle> triangles //property for obstacles
        {
            get { return Triangles; }
            set
            {
                Triangles = value;
            }
        }

        public List<Rectangle> Nexttriangles //property for obstacles
        {
            get { return NextTriangles; }
            set
            {
                NextTriangles = value;
            }
        }

        public List<Rectangle> spikes //property for spikes
        {
            get { return Spikes; }
            set
            {
                Spikes = value;
            }
        }

        public List<Rectangle> Nextspikes //property for spikes
        {
            get { return NextSpikes; }
            set
            {
                NextSpikes = value;
            }
        }

        public List<Rectangle> UpsideDowntriangles //property for obstacles
        {
            get { return UpsideDownTriangles; }
            set
            {
                UpsideDownTriangles = value;
            }
        }

        public List<Rectangle> NextUpsideDowntriangles //property for obstacles
        {
            get { return NextUpsideDownTriangles; }
            set
            {
                NextUpsideDownTriangles = value;
            }
        }

        public List<Rectangle> Warningblock //property for warning blocks
        {
            get { return WarningBlock; }
            set
            {
                WarningBlock = value;
            }
        }

        public List<Rectangle> NextWarningblock //property for warning blocks
        {
            get { return NextWarningBlock; }
            set
            {
                NextWarningBlock = value;
            }
        }

        public List<Rectangle> Switchblock //property for switch blocks
        {
            get { return SwitchBlock; }
            set
            {
                SwitchBlock = value;
            }
        }

        public List<Rectangle> NextSwitchblock //property for switch blocks
        {
            get { return NextSwitchBlock; }
            set
            {
                NextSwitchBlock = value;
            }
        }

        public List<Rectangle> SwitchBlockalt //property for switch blocks
        {
            get { return SwitchBlockAlt; }
            set
            {
                SwitchBlockAlt = value;
            }
        }

        public List<Rectangle> NextSwitchBlockalt //property for switch blocks
        {
            get { return NextSwitchBlockAlt; }
            set
            {
                NextSwitchBlockAlt = value;
            }
        }

        public List<Rectangle> Switchtriangle //property for switch triangles
        {
            get { return SwitchTriangle; }
            set
            {
                SwitchTriangle = value;
            }
        }

        public List<Rectangle> NextSwitchtriangle //property for switch triangles
        {
            get { return NextSwitchTriangle; }
            set
            {
                NextSwitchTriangle = value;
            }
        }

        public List<Rectangle> SwitchTrianglealt //property for switch triangles
        {
            get { return SwitchTriangleAlt; }
            set
            {
                SwitchTriangleAlt = value;
            }
        }

        public List<Rectangle> NextSwitchTrianglealt //property for switch triangles
        {
            get { return NextSwitchTriangleAlt; }
            set
            {
                NextSwitchTriangleAlt = value;
            }
        }

        public List<Rectangle> Collisionrectangle //property for triangle collision
        {
            get { return CollisionRectangle; }
            set
            {
                CollisionRectangle = value;
            }
        }

        public List<Rectangle> NextCollisionrectangle //property for triangle collision
        {
            get { return NextCollisionRectangle; }
            set
            {
                NextCollisionRectangle = value;
            }
        }

        public List<Rectangle> SwitchCollisionrectangle //property for triangle collision
        {
            get { return SwitchCollisionRectangle; }
            set
            {
                SwitchCollisionRectangle = value;
            }
        }

        public List<Rectangle> NextSwitchCollisionrectangle //property for triangle collision
        {
            get { return NextSwitchCollisionRectangle; }
            set
            {
                NextSwitchCollisionRectangle = value;
            }
        }

        public List<Rectangle> SwitchCollisionRectanglealt //property for triangle collision
        {
            get { return SwitchCollisionRectangleAlt; }
            set
            {
                SwitchCollisionRectangleAlt = value;
            }
        }

        public List<Rectangle> NextSwitchCollisionRectanglealt //property for triangle collision
        {
            get { return NextSwitchCollisionRectangleAlt; }
            set
            {
                NextSwitchCollisionRectangleAlt = value;
            }
        }

        public List<Rectangle> Sidewayscollision //property for sideways collision
        {
            get { return SidewaysCollision; }
            set
            {
                SidewaysCollision = value;
            }
        }

        public List<Rectangle> NextSidewayscollision //property for sideways collision
        {
            get { return NextSidewaysCollision; }
            set
            {
                NextSidewaysCollision = value;
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

        public int ScrollingParallaxX //property for scrolling parallax
        {
            get { return ScrollingParallax.X; }
            set
            {
                ScrollingParallax.X = value;
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

        public bool SWITCH //property for switch boolean for switch blocks
        {
            get { return Switch; }
            set
            {
                Switch = value;
            }
        }

        public int Score //property for saving the score
        {
            get { return score; }
            set
            {
                score = value;
            }
        }

        public int DisplayScore //property for displaying the score
        {
            get { return displayScore; }
            set
            {
                displayScore = value;
            }
        }

        public int HighScore //property for saving high score
        {
            get { return highscore; }
            set
            {
                highscore = value;
            }
        }

        public int DisplayHighScore //property for displaying high score
        {
            get { return displayHighscore; }
            set
            {
                displayHighscore = value;
            }
        }

        public int ScrollingCounter //property to increase scrolling speed
        {
            get { return scrollingCounter; }
        }

        public int TextureCounter //property for switching block textures
        {
            get { return textureCounter; }
        }

        public int ParallaxCounter //property for switching parallax textures
        {
            get { return parallaxCounter; }
        }

        public int BackgroundCounter //property for switching background textures
        {
            get { return backgroundCounter; }
            set
            {
                backgroundCounter = value;
            }
        }


        public void SetDimensions() //method to set the tiles according to the screen dimensions
        {
            TileHeight = screenHeight / 15;
            TileWidth = screenWidth / 20;
            scrollingCounter = screenWidth / 240; // = 8
            ScrollingBackground = new Rectangle(screenWidth / 960, 0, 45, 40); //x = 2
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

        public bool CanSwitch //property to switch textures
        {
            get { return canSwitch; }
            set
            {
                canSwitch = value;
            }
        }

        public bool CanLoadInitialParallax //property to load parallax
        {
            get { return canLoadinitialParallax; }
            set
            {
                canLoadinitialParallax = value;
            }
        }

        public bool CanLoadNextParallax //property to load parallax off screen
        {
            get { return canLoadnextParallax; }
            set
            {
                canLoadnextParallax = value;
            }
        }

        public bool CanLoadInitialBackground //property to load background
        {
            get { return canLoadinitialBackground; }
            set
            {
                canLoadinitialBackground = value;
            }
        }

        public bool CanLoadNextBackground //property to load background off screen
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

        public void SaveScore() //method to save the score
        {
            if (canSave == true) //if statement to prevent constant saving in the update method
            {
                //open a binary writer and write down the score
                Stream write = File.OpenWrite("score.dat");
                BinaryWriter writer = new BinaryWriter(write);
                writer.Write(score);
                writer.Close();
            }
            canSave = false;
        }

        public void ReadScore() //method to read in the score
        {
            if (canRead == true) //if statement to prevent constant reading in the update method
            {
                //open a binary reader and read in the score
                Stream read = File.OpenRead("score.dat");
                BinaryReader reader = new BinaryReader(read);
                displayScore = reader.ReadInt32();
                reader.Close();
            }
            canRead = false;
        }

        public void SaveHighScore() //method to save the high score
        {
            GetHighScore(); //gets the highscore to save the highscore even when exiting the game
            if (highscore < score) //changes highscore to new highscore
            {
                highscore = score;
                canSave = true; //sets can save to true to allow the new highscore to write to the binary file
            }
            if (canSave == true)
            {
                //write the new highscore to the binary file
                Stream write = File.OpenWrite("highscore.dat");
                BinaryWriter writer = new BinaryWriter(write);
                writer.Write(highscore);
                writer.Close();
            }
            canSave = false;
        }

        public void ReadHighScore() //method to read in the highscore
        {
            if (canReadhighScore == true) //if statement to prevent constant reading in the update method
            {
                //use binary reader to read in the highscore
                Stream read = File.OpenRead("highscore.dat");
                BinaryReader reader = new BinaryReader(read);
                displayHighscore = reader.ReadInt32();
                reader.Close();
            }
            canReadhighScore = false;
        }

        public void GetHighScore() //method to find the highscore
        {
            //searches through the files to get what the high score is before checking to see if the new score is higher than the current highscore
            string[] files = Directory.GetFiles(".");
            foreach (string s in files)
            {
                if (s.Contains("highscore.dat"))
                {
                    Stream read = File.OpenRead("highscore.dat");
                    BinaryReader reader = new BinaryReader(read);
                    highscore = reader.ReadInt32();
                    reader.Close();
                }
            }
        }

        public void ResetHighScore() //method to reset the highscore
        {
            //forcibly make the highscore 0
            Stream write = File.OpenWrite("highscore.dat");
            BinaryWriter writer = new BinaryWriter(write);
            writer.Write(0);
            writer.Close();
        }

        public void ResetGame() //method to reset the game when the player has lost, by resetting all the values back to their defaults
        {
            //reset all textures
            Parallaxtexture = null;
            NextParallaxtexture = null;
            Backgroundtexture = null;
            NextBackgroundtexture = null;
            Boxtexture = null;
            SwitchTriangletexture = null;
            SwitchTriangleAlttexture = null;
            SwitchBlocktexture = null;
            WarningBlocktexture = null;
            Triangletexture = null;
            Spiketexture = null;
            Flip = null;
            NextBoxtexture = null;
            NextSwitchTriangletexture = null;
            NextSwitchTriangleAlttexture = null;
            NextSwitchBlocktexture = null;
            NextTriangletexture = null;
            NextSpiketexture = null;
            NextFlip = null;
            Player = null;

            //reset all backgrounds
            ParallaxList.Clear();
            NextParallaxList.Clear();
            ScrollingParallax.X = 0;
            BackgroundList.Clear();
            NextBackgroundList.Clear();
            ScrollingBackground.X = screenWidth / 960;

            //reset all rectangle lists for collision
            ScrollingBlock.X = 0;
            allPlatforms.Clear();
            SidewaysCollision.Clear();
            NextSidewaysCollision.Clear();
            SwitchCollisionRectangleAlt.Clear();
            NextSwitchCollisionRectangleAlt.Clear();
            SwitchCollisionRectangle.Clear();
            NextSwitchCollisionRectangle.Clear();
            CollisionRectangle.Clear();
            NextCollisionRectangle.Clear();
            Squares.Clear();
            NextSquares.Clear();
            Triangles.Clear();
            NextTriangles.Clear();
            UpsideDownTriangles.Clear();
            NextUpsideDownTriangles.Clear();
            Spikes.Clear();
            NextSpikes.Clear();
            WarningBlock.Clear();
            NextWarningBlock.Clear();
            SwitchBlock.Clear();
            NextSwitchBlock.Clear();
            SwitchBlockAlt.Clear();
            NextSwitchBlockAlt.Clear();
            SwitchTriangle.Clear();
            NextSwitchTriangle.Clear();
            SwitchTriangleAlt.Clear();
            NextSwitchTriangleAlt.Clear();

            //reset all counters
            reader = null;
            //DataPoints.Clear(); 
            number = 0;
            scrollingCounter = screenWidth / 240;
            textureSwitch = 0;
            textureCounter = 0;
            parallaxCounter = 0;
            backgroundCounter = 0;
            Switch = false;

            //reset all booleans
            canLoadinitial = true;
            canLoadnext = true;

            canSwitch = true;

            canSave = true;
            canRead = true;
            canReadhighScore = true;

            canLoadinitialParallax = true;
            canLoadnextParallax = true;

            canLoadinitialBackground = true;
            canLoadnextBackground = true;

            //reset all file names
            ReadFile = null;
            TextFile = null;
            TextPath = null;

            //reset counters when loading in the file
            heightCounter = 0;
            widthCounter = 0;
        }

        public int ResetFiles() //method to get the scrolling to repeat
        {
            if (number == 3 || number == 5 || number == 7 || number == 9 || number == 11 || number == 13) //if a certain level is loaded
            {
                if (textureSwitch == 0) //then change its texture
                {
                    canSwitch = true;
                    textureSwitch++;
                    textureCounter++;
                }
            }
            else
            {
                textureSwitch = 0; //reset texture switch to 0 to allow for the texture to be changed on other levels
            }
            if (parallaxCounter > 1) //has to be one less than the number of parallax elements(resets parallax elements)
            {
                parallaxCounter = 0;
            }
            if (backgroundCounter > 23) //has to be one less than the number of backgrounds(resets backgrounds)
            {
                backgroundCounter = 0;
            }
            if (textureCounter > 5) //resets texture being displayed for the blocks
            {
                textureCounter = 0;
            }
            if (number > 11) //resets the background when the levels are finished loading
            {
                backgroundCounter = 0;
            }
            if (number > 10) //has to be one less than the number of levels(resets the levels)
            {
                //reset the texture values upon going over the next iteration of the game
                canSwitch = true;
                textureCounter = 0;
                textureSwitch = 0;
                //backgroundCounter = 0;
                Switch = false;
                number = 0;
                if (scrollingCounter < ((screenWidth/ 384) * 3) - 1) //increases the scrolling speed
                {
                    scrollingCounter += screenWidth / 960;
                }
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
                        //allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                    else if (i < 20) //used to draw the rest of the blocks at the top
                    {
                        Platforms = new Rectangle(widthCounter * TileWidth, 0, TileWidth, TileHeight);
                        //allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                    else //used to draw the blocks throughout the map
                    {
                        Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                        //allPlatforms.Add(Platforms);
                        Squares.Add(Platforms);
                    }
                }
                else if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, (Platforms.Y + TileHeight) - TileHeight / 3, TileWidth - TileWidth / 4, TileHeight / 3);
                    CollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    CollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, Platforms.Y + TileHeight / 11, ((TileWidth / 8)), TileHeight / 4);
                    CollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    Triangles.Add(Platforms);
                }
                else if (DataPoints[i] == "3") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, Platforms.Y, TileWidth - TileWidth / 4, TileHeight / 3);
                    CollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    CollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, (Platforms.Y + TileHeight) - TileHeight / 3, ((TileWidth / 8)), TileHeight / 4);
                    CollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    UpsideDowntriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "4") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    //allPlatforms.Add(Platforms);
                    Spikes.Add(Platforms);
                }
                else if (DataPoints[i] == "5") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    //allPlatforms.Add(Platforms);
                    WarningBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "6") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    //allPlatforms.Add(Platforms);
                    SwitchBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "7") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    //allPlatforms.Add(Platforms);
                    SwitchBlockAlt.Add(Platforms);
                }
                else if (DataPoints[i] == "8") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, (Platforms.Y + TileHeight) - TileHeight / 3, TileWidth - TileWidth / 4, TileHeight / 3);
                    SwitchCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    SwitchCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, Platforms.Y + TileHeight / 11, ((TileWidth / 8)), TileHeight / 4);
                    SwitchCollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    SwitchTriangle.Add(Platforms);
                }
                else if (DataPoints[i] == "9") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle(widthCounter * TileWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, Platforms.Y, TileWidth - TileWidth / 4, TileHeight / 3);
                    SwitchCollisionRectangleAlt.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    SwitchCollisionRectangleAlt.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, (Platforms.Y + TileHeight) - TileHeight / 3, ((TileWidth / 8)), TileHeight / 4);
                    SwitchCollisionRectangleAlt.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    SwitchTriangleAlt.Add(Platforms);
                }
            }
            DataPoints.Clear(); //clears the information read in the file to open space to write new information for another file
            canLoadnext = true; //set this value to true so we can load 2 files at once at the beginning
            //textureSwitch++;
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
                        //allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                    else if (i < 20) //used to draw the rest of the blocks at the top
                    {
                        Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, 0, TileWidth, TileHeight);
                        //allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                    else //used to draw the blocks throughout the map
                    {
                        Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                        Collision = new Rectangle((widthCounter * TileWidth) + screenWidth - 1, heightCounter * TileHeight + tileHeight / 4, 1, TileHeight - TileHeight / 2);
                        NextSidewaysCollision.Add(Collision);

                        //allPlatforms.Add(Platforms);
                        NextSquares.Add(Platforms);
                    }
                }
                else if (DataPoints[i] == "2") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, (Platforms.Y + TileHeight) - TileHeight / 3, TileWidth - TileWidth / 4, TileHeight / 3);
                    NextCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    NextCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, Platforms.Y + TileHeight / 11, ((TileWidth / 8)), TileHeight / 4);
                    NextCollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextTriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "3") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, Platforms.Y, TileWidth - TileWidth / 4, TileHeight / 3);
                    NextCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    NextCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, (Platforms.Y + TileHeight) - TileHeight / 3, ((TileWidth / 8)), TileHeight / 4);
                    NextCollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextUpsideDownTriangles.Add(Platforms);
                }
                else if (DataPoints[i] == "4") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);
                    //allPlatforms.Add(Platforms);
                    NextSpikes.Add(Platforms);
                }
                else if (DataPoints[i] == "5") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle((widthCounter * TileWidth) + screenWidth - 1, heightCounter * TileHeight + tileHeight / 4, 1, TileHeight - TileHeight / 2);
                    NextSidewaysCollision.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextWarningBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "6") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle((widthCounter * TileWidth) + screenWidth - 1, heightCounter * TileHeight + tileHeight / 4, 1, TileHeight - TileHeight / 2);
                    NextSidewaysCollision.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextSwitchBlock.Add(Platforms);
                }
                else if (DataPoints[i] == "7") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle((widthCounter * TileWidth) + screenWidth - 1, heightCounter * TileHeight + tileHeight / 4, 1, TileHeight - TileHeight / 2);
                    NextSidewaysCollision.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextSwitchBlockAlt.Add(Platforms);
                }
                else if (DataPoints[i] == "8") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, (Platforms.Y + TileHeight) - TileHeight / 3, TileWidth - TileWidth / 4, TileHeight / 3);
                    NextSwitchCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    NextSwitchCollisionRectangle.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, Platforms.Y + TileHeight / 11, ((TileWidth / 8)), TileHeight / 4);
                    NextSwitchCollisionRectangle.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextSwitchTriangle.Add(Platforms);
                }
                else if (DataPoints[i] == "9") //if a 2 is found then load a triangle
                {
                    Platforms = new Rectangle((widthCounter * TileWidth) + screenWidth, heightCounter * TileHeight, TileWidth, TileHeight);

                    Collision = new Rectangle(Platforms.X + TileWidth / 8, Platforms.Y, TileWidth - TileWidth / 4, TileHeight / 3);
                    NextSwitchCollisionRectangleAlt.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 6, (Platforms.Y + TileHeight) - ((TileHeight / 3) * 2), ((TileWidth / 2) - TileWidth / 6), TileHeight / 3);
                    NextSwitchCollisionRectangleAlt.Add(Collision);
                    Collision = new Rectangle(Platforms.Center.X - TileWidth / 15, (Platforms.Y + TileHeight) - TileHeight / 3, ((TileWidth / 8)), TileHeight / 4);
                    NextSwitchCollisionRectangleAlt.Add(Collision);

                    //allPlatforms.Add(Platforms);
                    NextSwitchTriangleAlt.Add(Platforms);
                }
            }
            DataPoints.Clear(); //clear the data stored from the file after the platforms are generated to open space for a new file to be read
            canLoadnext = true; //set this value to true so we can load 2 files at once at the beginning
            //textureSwitch++;
        }

        public void GenerateParallaxOnScreen() //method to load parallax elements on screen
        {
            //create a parallax element the size of the screen and add it to the list to allow it to scroll
            parallaxCounter++;
            Parallax = new Rectangle(0, 0, screenWidth, screenHeight);
            ParallaxList.Add(Parallax);
            canLoadinitialParallax = false; //disbaled to not load anything on screen again
            canLoadnextParallax = true; //enabled to allow the next parallax elements to load offscreen
        }

        public void GenerateParallaxOffScreen() //method to load parallax elements off screen
        {
            //create a parallax element the size of the screen and move it a screen's width over to the right and add it to the list to scroll
            parallaxCounter++;
            Parallax = new Rectangle(screenWidth, 0, screenWidth, screenHeight);
            NextParallaxList.Add(Parallax);
            canLoadnextParallax = true;
        }

        public void GenerateBackgroundsOnScreen() //method to generat backgrounds on screen
        {
            //create a background the size of the screen and add it to the list to scroll
            //textureSwitch++;
            backgroundCounter++;
            Background = new Rectangle(0, 0, screenWidth, screenHeight);
            BackgroundList.Add(Background);
            canLoadinitialBackground = false; //disabled to not load another background directly on screen afain
            canLoadnextBackground = true; //enabled to allow the next background to load off screen
        }

        public void GenerateBackgroundsOffScreen() //method to generate backgrounds off screen
        {
            //create a background the size of the screen and move it a screen's width to the right and add it to the list to scroll
            //textureSwitch++;
            backgroundCounter++;
            Background = new Rectangle(screenWidth, 0, screenWidth, screenHeight);
            NextBackgroundList.Add(Background);
            canLoadnextBackground = true;
        }




        public virtual void Draw(SpriteBatch spriteBatch) //method to draw platforms/objects
        {
            //for (int i = 0; i < CollisionRectangle.Count; i++)
            //{
            //    spriteBatch.Draw(SwitchBlockTexture, CollisionRectangle[i], Color.White);
            //}
            //for (int i = 0; i < NextCollisionRectangle.Count; i++)
            //{
            //    spriteBatch.Draw(SwitchBlockTexture, NextCollisionRectangle[i], Color.White);
            //}
            for (int i = 0; i < ParallaxList.Count; i++)
            {
                spriteBatch.Draw(Parallaxtexture, ParallaxList[i], Color.White);
            }
            for (int i = 0; i < NextParallaxList.Count; i++)
            {
                spriteBatch.Draw(NextParallaxtexture, NextParallaxList[i], Color.White);
            }
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
            //for (int i = 0; i < SidewaysCollision.Count; i++)
            //{
            //    spriteBatch.Draw(Parallaxtexture, SidewaysCollision[i], Color.White);
            //}
            //for (int i = 0; i < NextSidewaysCollision.Count; i++)
            //{
            //    spriteBatch.Draw(NextParallaxtexture, NextSidewaysCollision[i], Color.White);
            //}
        }
    }
}

