using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            triCity2, triLab1, triLab2, triSub1, triSub2, triSwitch, //objects
            menuText; //menu texture for return to main menu

        //rectangle variables
        Rectangle playRect, resumeRect, exitRect, menuRect;

        //enum to switch game states  - Brandon Rodriguez, Parker Wilson
        enum GameState { titleMenu, mainMenu, pauseMenu, game, gameOver, credits };
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
            p1 = new Player(0, 600, mapEditor.tileWidth, mapEditor.tileHeight);
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
            triCity2 = Content.Load<Texture2D>("Game Textures\\TriangleCity2");
            triLab1 = Content.Load<Texture2D>("Game Textures\\TriangleLab1");
            triLab2 = Content.Load<Texture2D>("Game Textures\\TriangleLab2");
            triSub1 = Content.Load<Texture2D>("Game Textures\\TriangleSubway1");
            triSub2 = Content.Load<Texture2D>("Game Textures\\TriangleSubway2");
            triSwitch = Content.Load<Texture2D>("Game Textures\\TriangleSwitch");

            //sets the textures to their respective values
            mapEditor.player = player;
            mapEditor.BoxTexture = box;
            mapEditor.flip = flip;
            mapEditor.TriangleTexture = triangle;
            mapEditor.SpikeTexture = spikeLight;
            mapEditor.WarningBlockTexture = sqWarn;
            mapEditor.SwitchBlockTexture = sqSwitchOn;
            mapEditor.SwitchTriangleTexture = triSwitch;
            mapEditor.SwitchTriangleAltTexture = triSwitch;
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

            //switches between states - Brandon Rodriguez, Parker Wilson, Nicholas Cato
            switch (gamestate)
            {
                case GameState.titleMenu:
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Space) && prevKstate.IsKeyUp(Keys.Space)) gamestate = GameState.mainMenu;
                    break;
                case GameState.mainMenu:

                    //if play game is clicked
                    mstate = Mouse.GetState();
                    if (mstate.LeftButton == ButtonState.Pressed && mstate.X > playRect.X && mstate.X < playRect.X + 500 && //width
                        mstate.Y > playRect.Y && mstate.Y < playRect.Y + 100) //height
                    {
                        gamestate = GameState.game;
                    }

                    //if exit game is clicked
                    if (mstate.LeftButton == ButtonState.Pressed && mstate.X > exitRect.X && mstate.X < exitRect.X + 500 && //width
                        mstate.Y > exitRect.Y && mstate.Y < exitRect.Y + 100) //height
                    {
                        Exit();
                    }
                    break;
                case GameState.credits: //functional but draws nothing as of yet
                    kstate = Keyboard.GetState();
                    break;
                case GameState.game:
                    mapEditor.ResetFiles();
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
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Space) && !prevKstate.IsKeyDown(Keys.Space)) flipGrav();
                    Fall(g);
                    p1.Move(kstate, mapEditor);
                    Scrolling(); //calls scrolling method to have infinite scrolling

                    //score increases here
                    score += gameTime.ElapsedGameTime.TotalSeconds * 2;

                    //pause if escape is pressed
                    if (kstate.IsKeyDown(Keys.Escape) && prevKstate.IsKeyUp(Keys.Escape)) gamestate = GameState.pauseMenu;
                    break;
                case GameState.gameOver:
                    kstate = Keyboard.GetState();
                    break;
                case GameState.pauseMenu:
                    kstate = Keyboard.GetState();

                    //unpause if escape is pressed
                    if (kstate.IsKeyDown(Keys.Escape) && prevKstate.IsKeyUp(Keys.Escape)) gamestate = GameState.game;

                    //if resume game is clicked
                    mstate = Mouse.GetState();
                    if (mstate.LeftButton == ButtonState.Pressed && mstate.X > resumeRect.X && mstate.X < resumeRect.X + 500 && //width
                        mstate.Y > resumeRect.Y && mstate.Y < resumeRect.Y + 100) //height
                    {
                        gamestate = GameState.game;
                    }

                    //if return to menu is clicked
                    mstate = Mouse.GetState();
                    if (mstate.LeftButton == ButtonState.Pressed && mstate.X > menuRect.X && mstate.X < menuRect.X + 500 && //width
                        mstate.Y > menuRect.Y && mstate.Y < menuRect.Y + 100) //height
                    {
                        gamestate = GameState.mainMenu;
                        System.Threading.Thread.Sleep(200);
                    }

                    //if exit game is clicked
                    if (mstate.LeftButton == ButtonState.Pressed && mstate.X > exitRect.X && mstate.X < exitRect.X + 500 && //width
                        mstate.Y > exitRect.Y && mstate.Y < exitRect.Y + 100) //height
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
            mapEditor.Draw(spriteBatch); //draws the objects in the text file
            // Box.Draw(spriteBatch);
            //Triangle.Draw(spriteBatch);

            //game state switches - Brandon Rodriguez, Parker Wilson, Nicholas Cato
            switch (gamestate)
            {
                case GameState.titleMenu:

                    //draws main logo
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 1200, 500), Color.White);

                    //draws "Press spacebar" text
                    spriteBatch.Draw(spaceBar, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, 500, 100), Color.White);

                    break;
                case GameState.mainMenu:
                    //draws main logo
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 1200, 500), Color.White);

                    //draws "Play game" text
                    spriteBatch.Draw(play, playRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, 500, 100), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 4, 500, 100), Color.White);

                    break;
                case GameState.credits:

                    break;
                case GameState.game:
                    mapEditor.Draw(spriteBatch); //draws the objects in the text file
                    p1.Draw(spriteBatch, player);

                    //draws score
                    spriteBatch.DrawString(font, string.Format("Score--- {0:0}", score), new Vector2(5, -10), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

                    break;
                case GameState.gameOver:

                    break;
                case GameState.pauseMenu:

                    //draws pause logo
                    spriteBatch.Draw(pause, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 7,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 700, 300), Color.White);

                    //draws "Resume game" text
                    spriteBatch.Draw(resume, resumeRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                       GraphicsDevice.DisplayMode.Height / 2, 500, 100), Color.White);

                    //draws "Exit game" text
                    spriteBatch.Draw(exit, exitRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 5, 500, 100), Color.White);

                    //draws "Return to menu" text
                    spriteBatch.Draw(menuText, menuRect = new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                       GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 10, 500, 100), Color.White);
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

        public void Fall(int grav) // method to iniatiate the player falling based on the gravity
        {
            if (grav == 1) //if gravity is pushing down
            {
                p1.SetY(p1.y + speed); //makes player always fall down

                //for loops calculate collision for the platforms on and off screen
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
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Nextsquares[i]) == true)
                    {
                        p1.SetY(mapEditor.Nextsquares[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.Switchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.Switchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.Switchblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchblock[i]) == true)
                    {
                        mapEditor.SWITCH = true;
                        p1.SetY(mapEditor.NextSwitchblock[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.SwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.SwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.SwitchBlockalt[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    if (p1.Collision(new Rectangle(p1.x, p1.y - speed, p1.w, p1.h), mapEditor.NextSwitchBlockalt[i]) == true)
                    {
                        mapEditor.SWITCH = false;
                        p1.SetY(mapEditor.NextSwitchBlockalt[i].Y - mapEditor.tileHeight);
                        break;
                    }
                }

                //p1.SetY(p1.y + 5);
            }

            if (grav == -1) //if gravity is reversed
            {
                p1.SetY(p1.y - speed); //makes player fall up

                //for loops calculate collision for the platforms on and off screen
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

                //p1.SetY(p1.y - 5);
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
            //if (mapEditor.ScrollingBlockX <= -mapEditor.ScreenWidth + 8 && counter == 1)
            //{
            //    mapEditor.Number = 0;
            //    mapEditor.CanLoadInitial = true;
            //    mapEditor.squares.Clear();
            //    mapEditor.Nextsquares.Clear();
            //    counter = 0;
            //}
            if (mapEditor.ScrollingBlockX <= -mapEditor.ScreenWidth + 8) //if our scrolling indicator has reached a screen's width then erase the platforms that are off screen to the left
            {
                //counter++;
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
                mapEditor.Nextsquares.Clear(); //clears the list that the platforms were moved from to create space for the next text file to add to this list
                mapEditor.Nexttriangles.Clear();
                mapEditor.NextUpsideDowntriangles.Clear();
                mapEditor.Nextspikes.Clear();
                mapEditor.NextSwitchblock.Clear();
                mapEditor.NextSwitchtriangle.Clear();
                mapEditor.NextSwitchTrianglealt.Clear();
                mapEditor.NextWarningblock.Clear();
                mapEditor.NextSwitchBlockalt.Clear();

            }
            else //CODE BELOW ACTUALLY SCROLLS THE PLATFORMS
            {
                mapEditor.ScrollingBlockX -= speed; //scrolls by a factor of the speed
                for (int i = 0; i < mapEditor.squares.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.squares[i];
                    same.X -= speed;
                    mapEditor.squares[i] = same;
                }
                for (int i = 0; i < mapEditor.Nextsquares.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.Nextsquares[i];
                    same.X -= speed;
                    mapEditor.Nextsquares[i] = same;
                }
                for (int i = 0; i < mapEditor.triangles.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.triangles[i];
                    same.X -= speed;
                    mapEditor.triangles[i] = same;
                }
                for (int i = 0; i < mapEditor.Nexttriangles.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.Nexttriangles[i];
                    same.X -= speed;
                    mapEditor.Nexttriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.spikes.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.spikes[i];
                    same.X -= speed;
                    mapEditor.spikes[i] = same;
                }
                for (int i = 0; i < mapEditor.Nextspikes.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.Nextspikes[i];
                    same.X -= speed;
                    mapEditor.Nextspikes[i] = same;
                }
                for (int i = 0; i < mapEditor.UpsideDowntriangles.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.UpsideDowntriangles[i];
                    same.X -= speed;
                    mapEditor.UpsideDowntriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.NextUpsideDowntriangles.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextUpsideDowntriangles[i];
                    same.X -= speed;
                    mapEditor.NextUpsideDowntriangles[i] = same;
                }
                for (int i = 0; i < mapEditor.Warningblock.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.Warningblock[i];
                    same.X -= speed;
                    mapEditor.Warningblock[i] = same;
                }
                for (int i = 0; i < mapEditor.NextWarningblock.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextWarningblock[i];
                    same.X -= speed;
                    mapEditor.NextWarningblock[i] = same;
                }
                for (int i = 0; i < mapEditor.Switchblock.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.Switchblock[i];
                    same.X -= speed;
                    mapEditor.Switchblock[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchblock.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextSwitchblock[i];
                    same.X -= speed;
                    mapEditor.NextSwitchblock[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchBlockalt.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.SwitchBlockalt[i];
                    same.X -= speed;
                    mapEditor.SwitchBlockalt[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchBlockalt.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextSwitchBlockalt[i];
                    same.X -= speed;
                    mapEditor.NextSwitchBlockalt[i] = same;
                }
                for (int i = 0; i < mapEditor.Switchtriangle.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.Switchtriangle[i];
                    same.X -= speed;
                    mapEditor.Switchtriangle[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchtriangle.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextSwitchtriangle[i];
                    same.X -= speed;
                    mapEditor.NextSwitchtriangle[i] = same;
                }
                for (int i = 0; i < mapEditor.SwitchTrianglealt.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot
                    Rectangle same = mapEditor.SwitchTrianglealt[i];
                    same.X -= speed;
                    mapEditor.SwitchTrianglealt[i] = same;
                }
                for (int i = 0; i < mapEditor.NextSwitchTrianglealt.Count; i++)
                {
                    //IMPORTANT: Code below creates a new rectangle that is the same value
                    // as the rectangle in the list so that we can alter the x or y values.
                    // You cannot say mapEditor.squares[i].X -= 5;, because this is invalid.
                    // You have to make a new rectangle that has the same values, change the 
                    // value of the rectangle made, and put that rectangle in the list at the correspoing spot

                    Rectangle same = mapEditor.NextSwitchTrianglealt[i];
                    same.X -= speed;
                    mapEditor.NextSwitchTrianglealt[i] = same;
                }
            }
        }
    }
}
