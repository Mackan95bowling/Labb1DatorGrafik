using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //-------------CAMERA------------------
        Camera camera;

        //-------------TERRAIN-----------------
        Terrain landscape;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // initialize camera start position
            camera = new Camera(new Vector3(-100, 0, 0), Vector3.Zero, new Vector3(2, 2, 2), new Vector3(0, -100, 256));

            // initialize terrain
            landscape = new Terrain(GraphicsDevice);

            base.Initialize();
        }


        protected override void LoadContent()
        {

            //load heightMap and heightMapTexture to create landscape
            landscape.SetHeightMapData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("sand"));

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // move camera position with keyboard
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.A))
            {
                camera.Update(1);
            }
            if (key.IsKeyDown(Keys.D))
            {
                camera.Update(2);
            }
            if (key.IsKeyDown(Keys.W))
            {
                camera.Update(3);
            }
            if (key.IsKeyDown(Keys.S))
            {
                camera.Update(4);
            }
            if (key.IsKeyDown(Keys.F))
            {
                camera.Update(5);
            }
            if (key.IsKeyDown(Keys.R))
            {
                camera.Update(6);
            }
            if (key.IsKeyDown(Keys.Q))
            {
                camera.Update(7);
            }
            if (key.IsKeyDown(Keys.E))
            {
                camera.Update(8);
            }
            if (key.IsKeyDown(Keys.G))
            {
                camera.Update(9);
            }
            if (key.IsKeyDown(Keys.T))
            {
                camera.Update(10);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // to get landscape viewable
            camera.Draw(landscape);

            base.Draw(gameTime);
        }
    }
}
