using Labb2DatorGrafik.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labb2DatorGrafik
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Labb2 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World terrain;
        WorldTerrain worldTerrain;
        Texture2D texture, textureImage;
        House woodhouse;
        public Labb2()
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
            Window.Title = "Laboration2";
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
            Texture2D houseTexture1 = Content.Load<Texture2D>("brickHouse");
            Texture2D houseTexture2 = Content.Load<Texture2D>("woodHouse");
            Model houses = Content.Load<Model>("hus");
            texture = Content.Load<Texture2D>("US_Canyon");
            textureImage = Content.Load<Texture2D>("sand");
            woodhouse = new House(this.GraphicsDevice,houses,houseTexture1);
            terrain = new World(this.GraphicsDevice, Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("sand"));
            worldTerrain = new WorldTerrain(this.GraphicsDevice, texture, new Texture2D[4] {textureImage, textureImage, textureImage, textureImage });
            // TODO: use this.Content to load your game content here
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //terrain.BasicEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), Vector3.Zero, Vector3.Up);
            //terrain.BasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            //terrain.BasicEffect.World = Matrix.CreateTranslation(new Vector3(0, -100, 256));
            //foreach (EffectPass pass in terrain.BasicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    terrain.Draw();

            //}
            //graphics.GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);
            // terrain.DrawGround();
            
            worldTerrain.BasicEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, 20), new Vector3(0, 0, 0), Vector3.Up);

           worldTerrain.BasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
            woodhouse.Draw(worldTerrain.BasicEffect);
           worldTerrain.Draw();
            base.Draw(gameTime);
        }
    }
}
