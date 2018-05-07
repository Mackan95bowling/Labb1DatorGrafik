using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.EngineHelpers;
using Labb1DatorGrafik.Manager;
using Labb1DatorGrafik.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labb3DatorGrafik
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Labb3 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Effect myEffect;

        Texture2D texture, textureImage;
        CameraSystem cameraSystem;
        TransformSystem transformSystem;
        ModelSystem modelSystem;
        HeightmapSystem heightmapSystem;
        public Labb3()
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
            cameraSystem = new CameraSystem();
            transformSystem = new TransformSystem();
            modelSystem = new ModelSystem();
            heightmapSystem = new HeightmapSystem();
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
            //this is how we load our EFFECT
            myEffect = Content.Load<Effect>("test1");
            var test = myEffect;


            texture = Content.Load<Texture2D>("US_Canyon");
            textureImage = Content.Load<Texture2D>("sand");
            CreateEntities();
            HeightMapBuilder heightMap = new HeightMapBuilder()
                .SetHeightMapTextureData(Content.Load<Texture2D>("US_Canyon"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                .InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .SetWorldMatrix(Matrix.CreateTranslation(new Vector3(0, 0, 1080)))
                .Build();
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
            cameraSystem.Update(gameTime);
            transformSystem.Update(gameTime);
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

            modelSystem.Draw(gameTime);
            heightmapSystem.Draw(graphics.GraphicsDevice);

            base.Draw(gameTime);
        }
        private void CreateEntities()
        {
            var chopperID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new TransformComponent() { }, chopperID);
            //ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = chopper }, chopperID);



            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(15, 10, 20), cameraTarget = Vector3.Zero }, cameraID);
        }
    }
}
