using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//Brandon Rodriguez - worked with Parker to load in textures, create a finite state machine and add in a score counter
//Parker Wilson - drew up concept art, loaded in textures with Brandon, started to work out game logic, created finite state machine
//Nicholas Cato - made the MapEditor class and its methods, made the Box class and Triangle class, Coded Scrolling, Made the external tool: Map Maker
//Brandon Guglielmo - made the player class, fall method, gravity flipping method, and worked with collision

namespace TheImpossiblerGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //variables for textures and creating a MapEditor object
        Texture2D player, box, triangle, flip;
        MapEditor mapEditor;
        Player p1;
        Box Box;
        Triangle Triangle;

        //variable for gravity
        int g;

        //variable for speed of movement/scrolling
        int speed = 8;

        //score counter variables - Brandon Rodriguez, Parker Wilson
        SpriteFont font;
        double score = 0;

        //texture variables - Brandon Rodriguez, Parker Wilson
        Texture2D logo, spaceBar, options, play, resume, exit, pause, //menus
            spikeDark, spikeLight, sqCity1, sqCity2, sqLab1, sqLab2, //objects
            sqSub1, sqSub2, sqSwitchOff, sqSwitchOn, sqWarn, triCity1, //objects
            triCity1Flip, triCity2, triCity2Flip, triLab1, triLab1Flip, //objects
            triLab2, triLab2Flip, triSub1, triSub1Flip, triSub2, triSub2Flip, //objects
            triSwitch, triSwitchFlip, //objects
            labBg1, labBg2, labBg3, labBg4,  //backgrounds
            subBg1, subBg2, subBg3, subBg4, subCol, //backgrounds
            cityBgBack1, cityBgBack2, cityBgFront, //backgrounds
            menuText, //menu texture for return to main menu
            menuTextScreen, //menu texture that is under text so it can be read easier
            menuScreenshot, titleBackground, gameOverLogo, newGame;

        List<Texture2D> backgrounds;
        List<Texture2D> parallax;

        //rectangle variables
        Rectangle playRect, resumeRect, exitRect, menuRect, title, newGameRect;

        //enum to switch game states  - Brandon Rodriguez, Parker Wilson
        enum GameState { titleMenu, mainMenu, pauseMenu, game, gameOver, credits, garageDoor };
        GameState gamestate;
        KeyboardState kstate, prevKstate; //used in finite state machine
        MouseState mstate, prevMstate; //used in finite state machine

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Creat objects for all the classes and instantiate gravity, also changes the size of the window
            Box = new Box();
            Triangle = new Triangle();
            g = 1;
            mapEditor = new MapEditor(); //creates a map editor object
            ChangeWindowsSize(); //changes the window size when game initially runs
            mapEditor.SetDimensions();
            p1 = new Player(200, 935, mapEditor.tileWidth, mapEditor.tileHeight);
            backgrounds = new List<Texture2D>();
            parallax = new List<Texture2D>();
            title = new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //loads in content
            box = this.Content.Load<Texture2D>("SquareLab1");
            triangle = this.Content.Load<Texture2D>("TriangleLab1");
            flip = this.Content.Load<Texture2D>("TriangleLab1Flip");
            player = this.Content.Load<Texture2D>("Player");

            //general menu loads - Brandon Rodriguez, Parker Wilson
            menuText = Content.Load<Texture2D>("Menus\\MenuText");
            logo = Content.Load<Texture2D>("Menus\\Logo");
            exit = Content.Load<Texture2D>("Menus\\ExitText");
            options = Content.Load<Texture2D>("Menus\\OptionsText");
            pause = Content.Load<Texture2D>("Menus\\Pause");
            play = Content.Load<Texture2D>("Menus\\PlayText");
            resume = Content.Load<Texture2D>("Menus\\ResumeText");
            spaceBar = Content.Load<Texture2D>("Menus\\SpacebarText");
            font = Content.Load<SpriteFont>("Menus\\font");
            menuTextScreen = Content.Load<Texture2D>("Menus\\menuTextScreen");
            menuScreenshot = Content.Load<Texture2D>("Menus\\MenuScreenshot");
            titleBackground = Content.Load<Texture2D>("Menus\\TitleBackground");
            gameOverLogo = Content.Load<Texture2D>("Menus\\GameOver");
            newGame = Content.Load<Texture2D>("Menus\\NewGameText");

            //general game object loads - Brandon Rodriguez, Parker Wilson
            spikeDark = Content.Load<Texture2D>("Game Textures\\SpikeDark");
            spikeLight = Content.Load<Texture2D>("Game Textures\\SpikeLight");
            sqCity1 = Content.Load<Texture2D>("Game Textures\\SquareCity1");
            sqCity2 = Content.Load<Texture2D>("Game Textures\\SquareCity2");
            sqLab1 = Content.Load<Texture2D>("Game Textures\\SquareLab1");
            sqLab2 = Content.Load<Texture2D>("Game Textures\\SquareLab2");
            sqSub1 = Content.Load<Texture2D>("Game Textures\\SquareSubway1");
            sqSub2 = Content.Load<Texture2D>("Game Textures\\SquareSubway2");
            sqSwitchOff = Content.Load<Texture2D>("Game Textures\\SquareSwitchOff");
            sqSwitchOn = Content.Load<Texture2D>("Game Textures\\SquareSwitchOn");
            sqWarn = Content.Load<Texture2D>("Game Textures\\SquareWarning");
            triCity1 = Content.Load<Texture2D>("Game Textures\\TriangleCity1");
            triCity1Flip = Content.Load<Texture2D>("Game Textures\\TriangleCity1Flip");
            triCity2 = Content.Load<Texture2D>("Game Textures\\TriangleCity2");
            triCity2Flip = Content.Load<Texture2D>("Game Textures\\TriangleCity2Flip");
            triLab1 = Content.Load<Texture2D>("Game Textures\\TriangleLab1");
            triLab1Flip = Content.Load<Texture2D>("Game Textures\\TriangleLab1Flip");
            triLab2 = Content.Load<Texture2D>("Game Textures\\TriangleLab2");
            triLab2Flip = Content.Load<Texture2D>("Game Textures\\TriangleLab2Flip");
            triSub1 = Content.Load<Texture2D>("Game Textures\\TriangleSubway1");
            triSub1Flip = Content.Load<Texture2D>("Game Textures\\TriangleSubway1Flip");
            triSub2 = Content.Load<Texture2D>("Game Textures\\TriangleSubway2");
            triSub2Flip = Content.Load<Texture2D>("Game Textures\\TriangleSubway2Flip");
            triSwitch = Content.Load<Texture2D>("Game Textures\\TriangleSwitch");
            triSwitchFlip = Content.Load<Texture2D>("Game Textures\\TriangleSwitchFlip");

            //Background loads
            labBg1 = Content.Load<Texture2D>("Game Textures\\labBg1");
            labBg2 = Content.Load<Texture2D>("Game Textures\\labBg2");
            labBg3 = Content.Load<Texture2D>("Game Textures\\labBg3");
            labBg4 = Content.Load<Texture2D>("Game Textures\\labBg4");
            subBg1 = Content.Load<Texture2D>("Game Textures\\subBg1");
            subBg2 = Content.Load<Texture2D>("Game Textures\\subBg2");
            subBg3 = Content.Load<Texture2D>("Game Textures\\subBg3");
            subBg4 = Content.Load<Texture2D>("Game Textures\\subBg4");
            subCol = Content.Load<Texture2D>("Game Textures\\subCol");
            cityBgBack1 = Content.Load<Texture2D>("Game Textures\\cityBgBack1");
            cityBgBack2 = Content.Load<Texture2D>("Game Textures\\cityBgBack2");
            cityBgFront = Content.Load<Texture2D>("Game Textures\\cityBgFront");


            //sets the textures to their respective values
            mapEditor.player = player;
            mapEditor.BoxTexture = box;
            mapEditor.flip = flip;
            mapEditor.TriangleTexture = triangle;
            mapEditor.SpikeTexture = spikeLight;
            mapEditor.WarningBlockTexture = sqWarn;
            mapEditor.SwitchBlockTexture = sqSwitchOn;
            mapEditor.SwitchTriangleTexture = triSwitch;
            mapEditor.NextSwitchTriangleTexture = triSwitch;
            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
            mapEditor.BackgroundTexture = labBg1;
            mapEditor.NextBackgroundTexture = labBg2;
            mapEditor.ParallaxTexture = cityBgBack1;
            mapEditor.NextParallaxTexture = cityBgBack2;

            //add backgrounds to list for switching between them
            backgrounds.Add(labBg1);
            backgrounds.Add(labBg2);
            backgrounds.Add(labBg3);
            backgrounds.Add(labBg4);
            backgrounds.Add(labBg2);
            backgrounds.Add(labBg1);
            backgrounds.Add(labBg4);
            backgrounds.Add(labBg3);
            backgrounds.Add(subBg1);
            backgrounds.Add(subBg2);
            backgrounds.Add(subBg3);
            backgrounds.Add(subBg4);
            backgrounds.Add(subBg2);
            backgrounds.Add(subBg1);
            backgrounds.Add(subBg4);
            backgrounds.Add(subBg3);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);
            backgrounds.Add(cityBgFront);

            //add parallax backgrounds to list for switching between them
            //parallax.Add(subCol);
            parallax.Add(cityBgBack1);
            parallax.Add(cityBgBack2);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.None))
                Exit();
            prevMstate = mstate;
            mstate = Mouse.GetState();

            //switches between states - Brandon Rodriguez, Parker Wilson, Nicholas Cato
            switch (gamestate)
            {
                case GameState.titleMenu:
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Space) && prevKstate.IsKeyUp(Keys.Space)){
                        gamestate = GameState.garageDoor;
                    }

                    break;

                case GameState.garageDoor:
                    if (title.Bottom > 0)
                    {
                        title.Y -= (GraphicsDevice.DisplayMode.Height / 216) * 3;
                    }
                    else
                    {
                        gamestate = GameState.mainMenu;
                    }

                    break;

                case GameState.mainMenu:

                    //if play game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > playRect.X && mstate.X < playRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > playRect.Y && mstate.Y < playRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        score = 0;
                        gamestate = GameState.game;
                        
                    }

                    //if exit game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > exitRect.X && mstate.X < exitRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > exitRect.Y && mstate.Y < exitRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        Exit();
                    }
                    break;
                case GameState.credits: //functional but draws nothing as of yet
                    kstate = Keyboard.GetState();
                    break;
                case GameState.game:
                    mapEditor.ResetFiles();
                    if (speed != mapEditor.ScrollingCounter)
                    {
                        speed = mapEditor.ScrollingCounter;
                    }
                    if (mapEditor.CanLoadInitialParallax == true)
                    {
                        mapEditor.GenerateParallaxOnScreen();
                    }
                    if (mapEditor.CanLoadNextParallax == true)
                    {
                        ChangeParallaxTexture();
                        mapEditor.GenerateParallaxOffScreen();
                    }
                    if (mapEditor.CanLoadInitialBackground == true)
                    {
                        mapEditor.GenerateBackgroundsOnScreen();
                    }
                    if (mapEditor.CanLoadNextBackground == true)
                    {
                         ChangeBackgroundTexture();
                        mapEditor.GenerateBackgroundsOffScreen();
                    }
                    if (mapEditor.CanLoadInitial == true) //used to load the initial platforms when the game is started
                    {
                        mapEditor.LoadTextFile(); //calls method to find the file
                        mapEditor.ReadTextFile(); //calls method to read the file
                        mapEditor.GeneratePlatformsOnScreen();
                    }
                    if (mapEditor.CanLoadNext == true) //used to load all other platforms off screen to have scrolling
                    {
                        mapEditor.LoadTextFile(); //calls method to find the file
                        mapEditor.ReadTextFile(); //calls method to read the file
                        mapEditor.GeneratePlatformsOffScreen();
                    }
                    if (mapEditor.CanSwitch == true)
                    {
                        if (mapEditor.TextureCounter == 0)
                        {
                            //mapEditor.BoxTexture = sqLab1;
                            mapEditor.NextBoxTexture = sqLab1;
                            //mapEditor.flip = triLab1Flip;
                            mapEditor.Nextflip = triLab1Flip;
                            //mapEditor.TriangleTexture = triLab1;
                            mapEditor.NextTriangleTexture = triLab1;
                            //mapEditor.SpikeTexture = spikeLight;
                            mapEditor.NextSpikeTexture = spikeLight;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                        if (mapEditor.TextureCounter == 1)
                        {
                            // mapEditor.BoxTexture = sqLab2;
                            mapEditor.NextBoxTexture = sqLab2;
                            // mapEditor.flip = triLab2Flip;
                            mapEditor.Nextflip = triLab2Flip;
                            // mapEditor.TriangleTexture = triLab2;
                            mapEditor.NextTriangleTexture = triLab2;
                            // mapEditor.SpikeTexture = spikeLight;
                            mapEditor.NextSpikeTexture = spikeLight;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                        if (mapEditor.TextureCounter == 2)
                        {
                            //mapEditor.BoxTexture = sqSub1;
                            mapEditor.NextBoxTexture = sqSub1;
                            //mapEditor.flip = triSub1Flip;
                            mapEditor.Nextflip = triSub1Flip;
                            //mapEditor.TriangleTexture = triSub1;
                            mapEditor.NextTriangleTexture = triSub1;
                            //mapEditor.SpikeTexture = spikeDark;
                            mapEditor.NextSpikeTexture = spikeDark;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                        if (mapEditor.TextureCounter == 3)
                        {
                            //mapEditor.BoxTexture = sqSub2;
                            mapEditor.NextBoxTexture = sqSub2;
                            //mapEditor.flip = triSub2Flip;
                            mapEditor.Nextflip = triSub2Flip;
                            //mapEditor.TriangleTexture = triSub2;
                            mapEditor.NextTriangleTexture = triSub2;
                            //mapEditor.SpikeTexture = spikeDark;
                            mapEditor.NextSpikeTexture = spikeDark;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                        if (mapEditor.TextureCounter == 4)
                        {
                            //mapEditor.BoxTexture = sqCity1;
                            mapEditor.NextBoxTexture = sqCity1;
                            //mapEditor.flip = triCity1Flip;
                            mapEditor.Nextflip = triCity1Flip;
                            //mapEditor.TriangleTexture = triCity1;
                            mapEditor.NextTriangleTexture = triCity1;
                            //mapEditor.SpikeTexture = spikeLight;
                            mapEditor.NextSpikeTexture = spikeLight;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                        if (mapEditor.TextureCounter == 5)
                        {
                            //mapEditor.BoxTexture = sqCity2;
                            mapEditor.NextBoxTexture = sqCity2;
                            // mapEditor.flip = triCity2Flip;
                            mapEditor.Nextflip = triCity2Flip;
                            //mapEditor.TriangleTexture = triCity2;
                            mapEditor.NextTriangleTexture = triCity2;
                            //mapEditor.SpikeTexture = spikeLight;
                            mapEditor.NextSpikeTexture = spikeLight;
                            mapEditor.WarningBlockTexture = sqWarn;
                            mapEditor.SwitchBlockTexture = sqSwitchOn;
                            mapEditor.SwitchTriangleTexture = triSwitch;
                            mapEditor.NextSwitchTriangleTexture = triSwitch;
                            mapEditor.SwitchTriangleAltTexture = triSwitchFlip;
                            mapEditor.NextSwitchTriangleAltTexture = triSwitchFlip;
                        }
                    }
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Space) && !prevKstate.IsKeyDown(Keys.Space)) flipGrav();
                    Fall(g);
                    Scrolling(); //calls scrolling method to have infinite scrolling

                    if (p1.y < 0 || p1.y > mapEditor.ScreenHeight)
                    {
                        gamestate = GameState.gameOver;
                    }

                    //score increases here
                    //score += 1;
                    score += gameTime.ElapsedGameTime.TotalSeconds * 2;

                    //pause if escape is pressed
                    if (kstate.IsKeyDown(Keys.Escape) && prevKstate.IsKeyUp(Keys.Escape)) gamestate = GameState.pauseMenu;

                    break;
                case GameState.gameOver:
                    kstate = Keyboard.GetState();

                    mapEditor.Score = (int)score;
                    mapEditor.SaveScore();
                    mapEditor.ReadScore();
                    mapEditor.SaveHighScore();
                    mapEditor.ReadHighScore();

                    //if new game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > newGameRect.X && mstate.X < newGameRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > newGameRect.Y && mstate.Y < newGameRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        mapEditor.ResetGame();
                        g = 1;
                        p1.SetX(2 * (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135));
                        p1.SetY((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 9 + (GraphicsDevice.DisplayMode.Height / 216) * 7);
                        ResetTileTexture();
                        ResetParallaxTexture();
                        ResetBackgroundTexture();
                        score = 0;
                        gamestate = GameState.game;
                    }

                    //if return to menu is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > menuRect.X && mstate.X < menuRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > menuRect.Y && mstate.Y < menuRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        mapEditor.ResetGame();
                        g = 1;
                        p1.SetX(2 * (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135));
                        p1.SetY((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 9 + (GraphicsDevice.DisplayMode.Height / 216) * 7);
                        ResetTileTexture();
                        ResetParallaxTexture();
                        ResetBackgroundTexture();
                        gamestate = GameState.mainMenu;
                    }

                    //if exit game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > exitRect.X && mstate.X < exitRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > exitRect.Y && mstate.Y < exitRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        Exit();
                    }

                    break;
                case GameState.pauseMenu:
                    kstate = Keyboard.GetState();

                    //unpause if escape is pressed
                    if (kstate.IsKeyDown(Keys.Escape) && prevKstate.IsKeyUp(Keys.Escape)) gamestate = GameState.game;

                    //if resume game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > resumeRect.X && mstate.X < resumeRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > resumeRect.Y && mstate.Y < resumeRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        gamestate = GameState.game;
                    }

                    //if return to menu is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > menuRect.X && mstate.X < menuRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > menuRect.Y && mstate.Y < menuRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        mapEditor.ResetGame();
                        g = 1;
                        p1.SetX(2 * (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135));
                        p1.SetY((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 9 + (GraphicsDevice.DisplayMode.Height / 216) * 7);
                        ResetTileTexture();
                        ResetParallaxTexture();
                        ResetBackgroundTexture();
                        gamestate = GameState.mainMenu;
                    }

                    //if exit game is clicked
                    if (mstate.LeftButton == ButtonState.Released && mstate.X > exitRect.X && mstate.X < exitRect.X + GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96 && //width
                        mstate.Y > exitRect.Y && mstate.Y < exitRect.Y + GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135 && prevMstate.LeftButton == ButtonState.Pressed) //height
                    {
                        Exit();
                    }
                    break;
            }

            base.Update(gameTime);

            //gets previous keyboard state, MUST be after base.Update! - Brandon Rodriguez, Parker Wilson
            prevKstate = kstate;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied); //need overrides for transparency to work properly

            //game state switches - Brandon Rodriguez, Parker Wilson, Nicholas Cato
            switch (gamestate)
            {
                case GameState.titleMenu:

                    //draws splash screen
                    spriteBatch.Draw(titleBackground, title, Color.White);

                    //draws main logo
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, GraphicsDevice.DisplayMode.Width - (GraphicsDevice.DisplayMode.Width / 3) - (GraphicsDevice.DisplayMode.Width / 24),
                        GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96), Color.White);

                    //draws text screen overlay
                    spriteBatch.Draw(menuTextScreen, new Rectangle((GraphicsDevice.DisplayMode.Width - (GraphicsDevice.DisplayMode.Width / 3) - (GraphicsDevice.DisplayMode.Width / 24)) / 2,
                        GraphicsDevice.DisplayMode.Height - GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 9 + GraphicsDevice.DisplayMode.Height / 216,
                        GraphicsDevice.DisplayMode.Height - GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 24,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) + (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) / 2), Color.White);

                    //draws "Press spacebar" text
                    spriteBatch.Draw(spaceBar, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    break;

                case GameState.garageDoor:

                    //draws fake background
                    spriteBatch.Draw(menuScreenshot, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);

                    //draws text screen overlay
                    spriteBatch.Draw(menuTextScreen, new Rectangle((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6 + (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) / 2,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 24,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 3), Color.White);

                    //draws "Play game" text
                    spriteBatch.Draw(play, playRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 4, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws splash screen
                    spriteBatch.Draw(titleBackground, title, Color.White);

                    //draws main logo
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, GraphicsDevice.DisplayMode.Width - (GraphicsDevice.DisplayMode.Width / 3) - (GraphicsDevice.DisplayMode.Width / 24),
                        GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96), Color.White);

                    break;

                case GameState.mainMenu:

                    //draws fake background
                    spriteBatch.Draw(menuScreenshot, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);

                    //draws main logo
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, GraphicsDevice.DisplayMode.Width - (GraphicsDevice.DisplayMode.Width / 3) - (GraphicsDevice.DisplayMode.Width / 24),
                        GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96), Color.White);

                    //draws text screen overlay
                    spriteBatch.Draw(menuTextScreen, new Rectangle((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6 + (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) / 2,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 24,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 3), Color.White);

                    //draws "Play game" text
                    spriteBatch.Draw(play, playRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 4, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    break;
                case GameState.credits:

                    break;
                case GameState.game:

                    //draws the objects in the text file
                    mapEditor.Draw(spriteBatch);

                    if(g == 1)
                    {
                        p1.Draw(spriteBatch, player, SpriteEffects.None);
                    }
                    else if(g == -1)
                    {
                        p1.Draw(spriteBatch, player, SpriteEffects.FlipVertically);
                    }

                    //draws score
                    //spriteBatch.DrawString(font, "Speed: " + mapEditor.Number, new Vector2(5, -10), Color.Black);
                    spriteBatch.DrawString(font, string.Format("Score--- {0:0}", score), new Vector2(GraphicsDevice.DisplayMode.Height / 216,
                        (GraphicsDevice.DisplayMode.Height / 216) * -2), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

                    break;
                case GameState.gameOver:

                    //draws the objects in the text file
                    mapEditor.Draw(spriteBatch);

                    //draws text screen overlay
                    spriteBatch.Draw(menuTextScreen, new Rectangle((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6 + (GraphicsDevice.DisplayMode.Height / 10 -
                        GraphicsDevice.DisplayMode.Height / 135) / 2, GraphicsDevice.DisplayMode.Height / 2 - GraphicsDevice.DisplayMode.Height / 6,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 24, (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 4 +
                        GraphicsDevice.DisplayMode.Height / 8), Color.White);

                    //draws score
                    spriteBatch.DrawString(font, string.Format("S   c   o   r   e      :      " + mapEditor.DisplayScore +
                        "                         H   i   g   h     S   c   o   r   e      :      " +
                        mapEditor.DisplayHighScore), new Vector2(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                        GraphicsDevice.DisplayMode.Height / 3 + GraphicsDevice.DisplayMode.Height / 25), Color.White);

                    //draws "New Game" text
                    spriteBatch.Draw(newGame, newGameRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 5, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Return to menu" text
                    spriteBatch.Draw(menuText, menuRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 10, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Game Over" text
                    spriteBatch.Draw(gameOverLogo, new Rectangle(GraphicsDevice.DisplayMode.Width / 4 - (GraphicsDevice.DisplayMode.Height / 216) * 3,
                        GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135, (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 10,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 2 + (GraphicsDevice.DisplayMode.Height / 216) * 15), Color.White);

                    break;
                case GameState.pauseMenu:

                    //draws the objects in the text file
                    mapEditor.Draw(spriteBatch);

                    //draws pause logo
                    spriteBatch.Draw(pause, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 7,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 7,
                        (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 3), Color.White);

                    //draws text screen overlay
                    spriteBatch.Draw(menuTextScreen, new Rectangle((GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 6 + (GraphicsDevice.DisplayMode.Height / 10 -
                        GraphicsDevice.DisplayMode.Height / 135) / 2, GraphicsDevice.DisplayMode.Height / 2 - GraphicsDevice.DisplayMode.Height / 24 - GraphicsDevice.DisplayMode.Height / 216,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 24, (GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135) * 4 +
                        (GraphicsDevice.DisplayMode.Height / 216) * 2), Color.White);

                    //draws "Resume game" text
                    spriteBatch.Draw(resume, resumeRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 5, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    //draws "Return to menu" text
                    spriteBatch.Draw(menuText, menuRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 9,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 10, GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 96,
                       GraphicsDevice.DisplayMode.Height / 10 - GraphicsDevice.DisplayMode.Height / 135), Color.White);

                    if (g == 1)
                    {
                        p1.Draw(spriteBatch, player, SpriteEffects.None);
                    }
                    else if (g == -1)
                    {
                        p1.Draw(spriteBatch, player, SpriteEffects.FlipVertically);
                    }

                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ChangeWindowsSize() //method to change the window size - Brandon Rodriguez, Parker Wilson
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            mapEditor.ScreenWidth = graphics.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            mapEditor.ScreenHeight = graphics.PreferredBackBufferHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        public void ResetTileTexture()
        {
            mapEditor.BoxTexture = box;
            mapEditor.flip = flip;
            mapEditor.TriangleTexture = triangle;
            mapEditor.SpikeTexture = spikeLight;
        }

        public void ResetParallaxTexture()
        {
            mapEditor.ParallaxTexture = cityBgBack1;
            mapEditor.NextParallaxTexture = cityBgBack2;
        }

        public void ResetBackgroundTexture()
        {
            mapEditor.BackgroundTexture = labBg1;
            mapEditor.NextBackgroundTexture = labBg2;
        }

        public void ChangeParallaxTexture()
        {
            for (int i = 0; i < parallax.Count; i++)
            {
                if (i == mapEditor.ParallaxCounter)
                {
                    mapEditor.NextParallaxTexture = parallax[i];
                }
            }
        }

        public void ChangeBackgroundTexture()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                //if (mapEditor.Number == 0)
                //{
                //    mapEditor.BackgroundCounter = 0;
                //}
                if (mapEditor.Number == 4)
                {

                    mapEditor.BackgroundCounter = 8;
                }
                if (mapEditor.Number == 8)
                {
                    mapEditor.BackgroundCounter = 16;
                }
                if (i == mapEditor.BackgroundCounter)
                {

                    mapEditor.NextBackgroundTexture = backgrounds[i];
                }
            }
        }

        public void Fall(int grav) // method to iniatiate the player falling based on the gravity
        {
            if (grav == 1) //if gravity is pushing down
            {
                p1.SetY(p1.y + speed); //makes player always fall down

                //for loops calculate collision for the platforms on and off screen
                if (mapEditor.SWITCH == false)
                {
                    for (int i = 0; i < mapEditor.SwitchCollisionRectanglealt.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.SwitchCollisionRectanglealt[i]) == true)
                        {
                            p1.SetY(mapEditor.SwitchCollisionRectanglealt[i].Y - mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                    for (int i = 0; i < mapEditor.NextSwitchCollisionRectanglealt.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextSwitchCollisionRectanglealt[i]) == true)
                        {
                            p1.SetY(mapEditor.NextSwitchCollisionRectanglealt[i].Y - mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                }
                if (mapEditor.SWITCH == true)
                {
                    for (int i = 0; i < mapEditor.SwitchCollisionrectangle.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.SwitchCollisionrectangle[i]) == true)
                        {
                            p1.SetY(mapEditor.SwitchCollisionrectangle[i].Y - mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                    for (int i = 0; i < mapEditor.NextSwitchCollisionrectangle.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextSwitchCollisionrectangle[i]) == true)
                        {
                            p1.SetY(mapEditor.NextSwitchCollisionrectangle[i].Y - mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                }

                for (int i = 0; i < mapEditor.Collisionrectangle.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.Collisionrectangle[i]) == true)
                    {
                        p1.SetY(mapEditor.Collisionrectangle[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextCollisionrectangle.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextCollisionrectangle[i]) == true)
                    {
                        p1.SetY(mapEditor.NextCollisionrectangle[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.spikes.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.spikes[i]) == true)
                    {
                        p1.SetY(mapEditor.spikes[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Nextspikes.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.Nextspikes[i]) == true)
                    {
                        p1.SetY(mapEditor.Nextspikes[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.squares.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.squares[i]) == true)
                    {
                        p1.SetY(mapEditor.squares[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Nextsquares.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.Nextsquares[i]) == true)
                    {
                        p1.SetY(mapEditor.Nextsquares[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Sidewayscollision.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y, p1.w, p1.h), mapEditor.Sidewayscollision[i]) == true)
                    {
                        p1.SetY(mapEditor.Sidewayscollision[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSidewayscollision.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y, p1.w, p1.h), mapEditor.NextSidewayscollision[i]) == true)
                    {
                        p1.SetY(mapEditor.NextSidewayscollision[i].Y - mapEditor.tileHeight - 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Switchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.Switchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.Switchblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextSwitchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.NextSwitchblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.SwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.SwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.SwitchBlockalt[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextSwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.NextSwitchBlockalt[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Warningblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.Warningblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.Warningblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextWarningblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y + speed, p1.w, p1.h), mapEditor.NextWarningblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.NextWarningblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
            }

            if (grav == -1) //if gravity is reversed
            {
                p1.SetY(p1.y - speed); //makes player fall up

                //for loops calculate collision for the platforms on and off screen
                if (mapEditor.SWITCH == false)
                {
                    for (int i = 0; i < mapEditor.SwitchCollisionRectanglealt.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.SwitchCollisionRectanglealt[i]) == true)
                        {
                            p1.SetY(mapEditor.SwitchCollisionRectanglealt[i].Y + mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                    for (int i = 0; i < mapEditor.NextSwitchCollisionRectanglealt.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchCollisionRectanglealt[i]) == true)
                        {
                            p1.SetY(mapEditor.NextSwitchCollisionRectanglealt[i].Y + mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                }
                if (mapEditor.SWITCH == true)
                {
                    for (int i = 0; i < mapEditor.SwitchCollisionrectangle.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.SwitchCollisionrectangle[i]) == true)
                        {
                            p1.SetY(mapEditor.SwitchCollisionrectangle[i].Y + mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                    for (int i = 0; i < mapEditor.NextSwitchCollisionrectangle.Count; i++)
                    {
                        if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchCollisionrectangle[i]) == true)
                        {
                            p1.SetY(mapEditor.NextSwitchCollisionrectangle[i].Y + mapEditor.tileHeight + 5000);
                            break;
                        }
                    }
                }
                for (int i = 0; i < mapEditor.Collisionrectangle.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Collisionrectangle[i]) == true)
                    {
                        p1.SetY(mapEditor.Collisionrectangle[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextCollisionrectangle.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextCollisionrectangle[i]) == true)
                    {
                        p1.SetY(mapEditor.NextCollisionrectangle[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.spikes.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.spikes[i]) == true)
                    {
                        p1.SetY(mapEditor.spikes[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Nextspikes.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Nextspikes[i]) == true)
                    {
                        p1.SetY(mapEditor.Nextspikes[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.squares.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.squares[i]) == true)
                    {
                        p1.SetY(mapEditor.squares[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Nextsquares.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Nextsquares[i]) == true)
                    {
                        p1.SetY(mapEditor.Nextsquares[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Sidewayscollision.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y, p1.w, p1.h), mapEditor.Sidewayscollision[i]) == true)
                    {
                        p1.SetY(mapEditor.Sidewayscollision[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSidewayscollision.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y, p1.w, p1.h), mapEditor.NextSidewayscollision[i]) == true)
                    {
                        p1.SetY(mapEditor.NextSidewayscollision[i].Y + mapEditor.tileHeight + 5000);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Switchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Switchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.Switchblock[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.NextSwitchblock[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.SwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.SwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.SwitchBlockalt[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.NextSwitchBlockalt[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Warningblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Warningblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.Warningblock[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextWarningblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextWarningblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.NextWarningblock[i].Y + mapEditor.tileHeight);
                        break;
                    }
                }
            }
        }



        public void flipGrav() // Changes the gravity, 1 being normal, -1 being reversed.
        {
            if (g == 1)
            {
                g = -1;
            }
            else if (g == -1)
            {
                g = 1;
            }
        }

        public void Scrolling()
        {
            if (mapEditor.CanLoadNext == true) //this resets the canloadnext variable to prevent infinite loading of a single file
            {
                mapEditor.CanLoadNext = false;
            }
            if (mapEditor.CanSwitch == true)
            {
                mapEditor.CanSwitch = false;
            }
            if (mapEditor.CanLoadNextParallax == true)
            {
                mapEditor.CanLoadNextParallax = false;
            }
            if (mapEditor.CanLoadNextBackground == true)
            {
                mapEditor.CanLoadNextBackground = false;
            }
            if (mapEditor.ScrollingParallaxX <= -mapEditor.ScreenWidth + speed - 3)
            {
                if (mapEditor.ParallaxTexture != mapEditor.NextParallaxTexture)
                {
                    mapEditor.ParallaxTexture = mapEditor.NextParallaxTexture;
                }
                mapEditor.ScrollingParallaxX = speed / 4;
                mapEditor.CanLoadNextParallax = true;
                mapEditor.ParallaxList.Clear();
                for (int i = 0; i < mapEditor.NextParallaxlist.Count; i++)
                {
                    mapEditor.Parallaxlist.Add(mapEditor.NextParallaxlist[i]);
                }
                mapEditor.NextParallaxlist.Clear();
            }
            if (mapEditor.ScrollingBackgroundX <= -mapEditor.ScreenWidth + speed)
            {
                if (mapEditor.BackgroundTexture != mapEditor.NextBackgroundTexture)
                {
                    mapEditor.BackgroundTexture = mapEditor.NextBackgroundTexture;
                }
                mapEditor.ScrollingBackgroundX = speed / 3;
                mapEditor.CanLoadNextBackground = true;
                mapEditor.Backgroundlist.Clear();
                for (int i = 0; i < mapEditor.NextBackgroundlist.Count; i++)
                {
                    mapEditor.Backgroundlist.Add(mapEditor.NextBackgroundlist[i]);
                }
                mapEditor.NextBackgroundlist.Clear();
            }
            if (mapEditor.ScrollingBlockX <= -mapEditor.ScreenWidth + speed - 3) //(+ 8)if our scrolling indicator has reached a screen's width then erase the platforms that are off screen to the left
            {
                if (mapEditor.BoxTexture != mapEditor.NextBoxTexture && mapEditor.TriangleTexture != mapEditor.NextTriangleTexture && mapEditor.flip != mapEditor.Nextflip)
                {
                    mapEditor.BoxTexture = mapEditor.NextBoxTexture;
                    mapEditor.TriangleTexture = mapEditor.NextTriangleTexture;
                    mapEditor.flip = mapEditor.Nextflip;
                }
                if (mapEditor.SpikeTexture != mapEditor.NextSpikeTexture)
                {
                    mapEditor.SpikeTexture = mapEditor.NextSpikeTexture;
                }

                mapEditor.ScrollingBlockX = 0; //resets value
                mapEditor.CanLoadNext = true; //prepare to load in the next file
                mapEditor.squares.Clear(); //clear the list of platforms that are off screen to the left
                mapEditor.triangles.Clear();
                mapEditor.UpsideDowntriangles.Clear();
                mapEditor.spikes.Clear();
                mapEditor.Switchblock.Clear();
                mapEditor.SwitchBlockalt.Clear();
                mapEditor.Switchtriangle.Clear();
                mapEditor.SwitchTrianglealt.Clear();
                mapEditor.Warningblock.Clear();
                mapEditor.Collisionrectangle.Clear();
                mapEditor.SwitchCollisionrectangle.Clear();
                mapEditor.SwitchCollisionRectanglealt.Clear();
                mapEditor.Sidewayscollision.Clear();
                for (int i = 0; i < mapEditor.Nextsquares.Count; i++)
                {
                    mapEditor.squares.Add(mapEditor.Nextsquares[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.Nexttriangles.Count; i++)
                {
                    mapEditor.triangles.Add(mapEditor.Nexttriangles[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextUpsideDowntriangles.Count; i++)
                {
                    mapEditor.UpsideDowntriangles.Add(mapEditor.NextUpsideDowntriangles[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.Nextspikes.Count; i++)
                {
                    mapEditor.spikes.Add(mapEditor.Nextspikes[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    mapEditor.Switchblock.Add(mapEditor.NextSwitchblock[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    mapEditor.SwitchBlockalt.Add(mapEditor.NextSwitchBlockalt[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchtriangle.Count; i++)
                {
                    mapEditor.Switchtriangle.Add(mapEditor.NextSwitchtriangle[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchTrianglealt.Count; i++)
                {
                    mapEditor.SwitchTrianglealt.Add(mapEditor.NextSwitchTrianglealt[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextWarningblock.Count; i++)
                {
                    mapEditor.Warningblock.Add(mapEditor.NextWarningblock[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextCollisionrectangle.Count; i++)
                {
                    mapEditor.Collisionrectangle.Add(mapEditor.NextCollisionrectangle[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchCollisionrectangle.Count; i++)
                {
                    mapEditor.SwitchCollisionrectangle.Add(mapEditor.NextSwitchCollisionrectangle[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSwitchCollisionRectanglealt.Count; i++)
                {
                    mapEditor.SwitchCollisionRectanglealt.Add(mapEditor.NextSwitchCollisionRectanglealt[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                for (int i = 0; i < mapEditor.NextSidewayscollision.Count; i++)
                {
                    mapEditor.Sidewayscollision.Add(mapEditor.NextSidewayscollision[i]); //adds the platforms that are currently visible to the cleared list(moves one list to another list)
                }
                mapEditor.Nextsquares.Clear(); //clears the list that the platforms were moved from to create space for the next text file to add to this list
                mapEditor.Nexttriangles.Clear();
                mapEditor.NextUpsideDowntriangles.Clear();
                mapEditor.Nextspikes.Clear();
                mapEditor.NextSwitchblock.Clear();
                mapEditor.NextSwitchtriangle.Clear();
                mapEditor.NextSwitchTrianglealt.Clear();
                mapEditor.NextWarningblock.Clear();
                mapEditor.NextSwitchBlockalt.Clear();
                mapEditor.NextCollisionrectangle.Clear();
                mapEditor.NextSwitchCollisionrectangle.Clear();
                mapEditor.NextSwitchCollisionRectanglealt.Clear();
                mapEditor.NextSidewayscollision.Clear();

            }
            else //CODE BELOW ACTUALLY SCROLLS THE PLATFORMS
            {
                mapEditor.ScrollingParallaxX -= speed - 4;
                mapEditor.ScrollingBlockX -= speed; //scrolls by a factor of the speed
                mapEditor.ScrollingBackgroundX -= speed - 2;

                //IMPORTANT: Code below creates a new rectangle that is the same value
                // as the rectangle in the list so that we can alter the x or y values.
                // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                // You have to make a new rectangle that has the same values, change the 
                // value of the rectangle made, and put that rectangle in the list at the correspoing spot.
                // Code below handles scrolling for all platforms on and off screen.
                for (int i = 0; i < mapEditor.Sidewayscollision.Count; i++)
                {
                    Rectangle same = mapEditor.Sidewayscollision[i];
                    same.X -= speed;
                    mapEditor.Sidewayscollision[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSidewayscollision.Count; i++)
                {
                    Rectangle same = mapEditor.NextSidewayscollision[i];
                    same.X -= speed;
                    mapEditor.NextSidewayscollision[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchCollisionRectanglealt.Count; i++)
                {
                    Rectangle same = mapEditor.SwitchCollisionRectanglealt[i];
                    same.X -= speed;
                    mapEditor.SwitchCollisionRectanglealt[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchCollisionRectanglealt.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchCollisionRectanglealt[i];
                    same.X -= speed;
                    mapEditor.NextSwitchCollisionRectanglealt[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchCollisionrectangle.Count; i++)
                {
                    Rectangle same = mapEditor.SwitchCollisionrectangle[i];
                    same.X -= speed;
                    mapEditor.SwitchCollisionrectangle[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchCollisionrectangle.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchCollisionrectangle[i];
                    same.X -= speed;
                    mapEditor.NextSwitchCollisionrectangle[i] = same;
                }
                for (int i = 0; i < mapEditor.Collisionrectangle.Count; i++)
                {
                    Rectangle same = mapEditor.Collisionrectangle[i];
                    same.X -= speed;
                    mapEditor.Collisionrectangle[i] = same;
                }
                for (int i = 0; i < mapEditor.NextCollisionrectangle.Count; i++)
                {
                    Rectangle same = mapEditor.NextCollisionrectangle[i];
                    same.X -= speed;
                    mapEditor.NextCollisionrectangle[i] = same;
                }
                for (int i = 0; i < mapEditor.Parallaxlist.Count; i++)
                {
                    Rectangle same = mapEditor.Parallaxlist[i];
                    same.X -= speed - 4;
                    mapEditor.Parallaxlist[i] = same;
                }
                for (int i = 0; i < mapEditor.NextParallaxlist.Count; i++)
                {
                    Rectangle same = mapEditor.NextParallaxlist[i];
                    same.X -= speed - 4;
                    mapEditor.NextParallaxlist[i] = same;
                }
                for (int i = 0; i < mapEditor.Backgroundlist.Count; i++)
                {
                    Rectangle same = mapEditor.Backgroundlist[i];
                    same.X -= speed - 2;
                    mapEditor.Backgroundlist[i] = same;
                }
                for (int i = 0; i < mapEditor.NextBackgroundlist.Count; i++)
                {
                    Rectangle same = mapEditor.NextBackgroundlist[i];
                    same.X -= speed - 2;
                    mapEditor.NextBackgroundlist[i] = same;
                }
                for (int i = 0; i < mapEditor.squares.Count; i++)
                {
                    Rectangle same = mapEditor.squares[i];
                    same.X -= speed;
                    mapEditor.squares[i] = same;
                }
                for (int i = 0; i < mapEditor.Nextsquares.Count; i++)
                {
                    Rectangle same = mapEditor.Nextsquares[i];
                    same.X -= speed;
                    mapEditor.Nextsquares[i] = same;
                }
                for (int i = 0; i < mapEditor.triangles.Count; i++)
                {
                    Rectangle same = mapEditor.triangles[i];
                    same.X -= speed;
                    mapEditor.triangles[i] = same;
                }
                for (int i = 0; i < mapEditor.Nexttriangles.Count; i++)
                {
                    Rectangle same = mapEditor.Nexttriangles[i];
                    same.X -= speed;
                    mapEditor.Nexttriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.spikes.Count; i++)
                {
                    Rectangle same = mapEditor.spikes[i];
                    same.X -= speed;
                    mapEditor.spikes[i] = same;
                }
                for (int i = 0; i < mapEditor.Nextspikes.Count; i++)
                {
                    Rectangle same = mapEditor.Nextspikes[i];
                    same.X -= speed;
                    mapEditor.Nextspikes[i] = same;
                }
                for (int i = 0; i < mapEditor.UpsideDowntriangles.Count; i++)
                {
                    Rectangle same = mapEditor.UpsideDowntriangles[i];
                    same.X -= speed;
                    mapEditor.UpsideDowntriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.NextUpsideDowntriangles.Count; i++)
                {
                    Rectangle same = mapEditor.NextUpsideDowntriangles[i];
                    same.X -= speed;
                    mapEditor.NextUpsideDowntriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.Warningblock.Count; i++)
                {
                    Rectangle same = mapEditor.Warningblock[i];
                    same.X -= speed;
                    mapEditor.Warningblock[i] = same;
                }
                for (int i = 0; i < mapEditor.NextWarningblock.Count; i++)
                {
                    Rectangle same = mapEditor.NextWarningblock[i];
                    same.X -= speed;
                    mapEditor.NextWarningblock[i] = same;
                }
                for (int i = 0; i < mapEditor.Switchblock.Count; i++)
                {
                    Rectangle same = mapEditor.Switchblock[i];
                    same.X -= speed;
                    mapEditor.Switchblock[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchblock[i];
                    same.X -= speed;
                    mapEditor.NextSwitchblock[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchBlockalt.Count; i++)
                {
                    Rectangle same = mapEditor.SwitchBlockalt[i];
                    same.X -= speed;
                    mapEditor.SwitchBlockalt[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchBlockalt[i];
                    same.X -= speed;
                    mapEditor.NextSwitchBlockalt[i] = same;
                }
                for (int i = 0; i < mapEditor.Switchtriangle.Count; i++)
                {
                    Rectangle same = mapEditor.Switchtriangle[i];
                    same.X -= speed;
                    mapEditor.Switchtriangle[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchtriangle.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchtriangle[i];
                    same.X -= speed;
                    mapEditor.NextSwitchtriangle[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchTrianglealt.Count; i++)
                {
                    Rectangle same = mapEditor.SwitchTrianglealt[i];
                    same.X -= speed;
                    mapEditor.SwitchTrianglealt[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchTrianglealt.Count; i++)
                {
                    Rectangle same = mapEditor.NextSwitchTrianglealt[i];
                    same.X -= speed;
                    mapEditor.NextSwitchTrianglealt[i] = same;
                }
            }
        }
    }
}