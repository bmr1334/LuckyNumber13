using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheImpossiblerGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //variables for textures and creating a mapeditor object
        Texture2D player, box, triangle, flip;
        MapEditor mapEditor;
        Player p1;
        Box Box;
        Triangle Triangle;

        //texture variables
        Texture2D logo, spaceBar, options, play, resume, exit, pause, //menus
            spikeDark, spikeLight, sqCity1, sqCity2, sqLab1, sqLab2, //objects
            sqSub1, sqSub2, sqSwitchOff, sqSwitchOn, sqWarn, triCity1,
            triCity2, triLab1, triLab2, triSub1, triSub2, triSwitch;

        //enum to switch game states
        enum GameState { titleMenu, mainMenu, pauseMenu, game, gameOver, credits };
        GameState gamestate;
        KeyboardState kstate, prevKstate;
        MouseState mstate, prevMstate;
        
        //List<Rectangle> boxes;
        //Rectangle platform;

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
            Box = new Box();
            Triangle = new Triangle();
            mapEditor = new MapEditor(); //creates a map editor objec
            ChangeWindowsSize(); //changes the window size when game initially runs
            mapEditor.SetDimensions();
            p1 = new Player(new Rectangle(0, 930, mapEditor.tileWidth, mapEditor.tileHeight));
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

            //general menu loads
            logo = Content.Load<Texture2D>("Menus\\Logo");
            exit = Content.Load<Texture2D>("Menus\\ExitText");
            options = Content.Load<Texture2D>("Menus\\OptionsText");
            pause = Content.Load<Texture2D>("Menus\\Pause");
            play = Content.Load<Texture2D>("Menus\\PlayText");
            resume = Content.Load<Texture2D>("Menus\\ResumeText");
            spaceBar = Content.Load<Texture2D>("Menus\\SpacebarText");

            //general game object loads
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

            // TODO: Add your update logic here

            //sets the textures in the mapeditor class to the ones loaded in this class
            mapEditor.player = player;
            mapEditor.BoxTexture = box;
            mapEditor.flip = flip;
            mapEditor.TriangleTexture = triangle;
            mapEditor.LoadTextFile(); //calls method to find the file
            mapEditor.ReadTextFile(); //calls method to read the file
            mapEditor.GeneratePlatforms();

            //switches between states
            switch (gamestate)
            {
                case GameState.titleMenu:
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Space) && prevKstate.IsKeyUp(Keys.Space)) gamestate = GameState.mainMenu;
                    break;
                case GameState.mainMenu:
                    //kstate = Keyboard.GetState();
                    //if (kstate.IsKeyDown(Keys.Space) && prevKstate.IsKeyUp(Keys.Space)) gamestate = GameState.mainMenu;
                    mstate = Mouse.GetState();
                    if (mstate.LeftButton == ButtonState.Pressed)
                    {
                        gamestate = GameState.game;
                    }
                    break;
                case GameState.credits:
                    kstate = Keyboard.GetState();
                    break;
                case GameState.game:
                    kstate = Keyboard.GetState();
                    if (kstate.IsKeyDown(Keys.Escape) && prevKstate.IsKeyUp(Keys.Escape)) gamestate = GameState.pauseMenu;
                    break;
                case GameState.gameOver:
                    kstate = Keyboard.GetState();
                    break;
                case GameState.pauseMenu:
                    kstate = Keyboard.GetState();
                    break;
            }

            foreach (Rectangle r in mapEditor.Squares)
            {
                if (mapEditor.Collision(p1, r) == true)
                {

                }
            }

            foreach (Rectangle r in mapEditor.Triangles)
            {
                if (mapEditor.Collision(p1, r) == true)
                {

                }
            }

            base.Update(gameTime);

            //gets previos keyboard state
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
            spriteBatch.Begin();
            mapEditor.Draw(spriteBatch); //draws the objects in the text file
            // Box.Draw(spriteBatch);
            //Triangle.Draw(spriteBatch);

            //game state switches
            switch (gamestate)
            {
                case GameState.titleMenu:
                    //Color trans = new Color(255, 255, 255, 0);
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 1200, 500), Color.White);
                    spriteBatch.Draw(spaceBar, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, 500, 100), Color.White);
                    break;
                case GameState.mainMenu:
                    spriteBatch.Draw(logo, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 25,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 1200, 500), Color.White);
                    spriteBatch.Draw(play, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 7, 500, 100), Color.White);
                    spriteBatch.Draw(exit, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                        GraphicsDevice.DisplayMode.Height / 2 + GraphicsDevice.DisplayMode.Height / 4, 500, 100), Color.White);

                    break;
                case GameState.credits:

                    break;
                case GameState.game:
                    mapEditor.Draw(spriteBatch); //draws the objects in the text file
                    p1.Draw(spriteBatch, player);

                    break;
                case GameState.gameOver:

                    break;
                case GameState.pauseMenu:
                    spriteBatch.Draw(pause, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 3 + GraphicsDevice.DisplayMode.Width / 7,
                        GraphicsDevice.DisplayMode.Height / 4 - GraphicsDevice.DisplayMode.Height / 6, 700, 300), Color.White);
                    spriteBatch.Draw(resume, new Rectangle(GraphicsDevice.DisplayMode.Width / 2 - GraphicsDevice.DisplayMode.Width / 4 + GraphicsDevice.DisplayMode.Width / 12,
                       GraphicsDevice.DisplayMode.Height / 2, 500, 100), Color.White);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ChangeWindowsSize() //method to change the window size
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            mapEditor.ScreenWidth = graphics.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            mapEditor.ScreenHeight = graphics.PreferredBackBufferHeight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
    }
}
