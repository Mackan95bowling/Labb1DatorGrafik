using Labb1DatorGrafik.System;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chopper
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Model chopper;
        private CameraSystem cameraSystem;
        private TransformSystem transformSystem;
        private ModelSystem modelSystem;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cameraSystem = new CameraSystem();
            transformSystem = new TransformSystem();
            modelSystem = new ModelSystem();
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            chopper = Content.Load<Model>("Chopper");
            CreateEntities();
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

            modelSystem.Draw(gameTime);
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
        private void CreateEntities()
        {
            var chopperID = ComponentManager.Get().NewEntity();

            ComponentManager.Get().AddComponentToEntity(new CameraComponent() {view = Matrix.CreateLookAt(new Vector3(15, 10, 45), Vector3.Zero, Vector3.Up),projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, .1f, 1000f)}, chopperID);
            ComponentManager.Get().AddComponentToEntity(new TransformComponent() { }, chopperID);
            ComponentManager.Get().AddComponentToEntity(new ModelComponent() {model = chopper},chopperID);

        }
    }
}
