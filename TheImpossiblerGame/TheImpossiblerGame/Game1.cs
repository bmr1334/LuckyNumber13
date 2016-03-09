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
        Texture2D player;
        Texture2D box;
        Texture2D triangle;
        Player p1;
        Texture2D flip;
        MapEditor mapEditor;
        Box Box;
        Triangle Triangle;
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
            mapEditor = new MapEditor(); //creates a map editor object
            Box = new Box();
            Triangle = new Triangle();
            p1 = new Player(new Rectangle(0, 0, 30, 45), mapEditor.player);
            base.Initialize();
            ChangeWindowsSize(); //changes the window size when game initially runs
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
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
            spriteBatch.End();


            base.Draw(gameTime);
        }

        public void ChangeWindowsSize() //method to change the window size
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            mapEditor.ScreenWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            mapEditor.ScreenHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }



    }
}
