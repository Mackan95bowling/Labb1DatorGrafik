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
        Matrix view, projection; // camera eller transform
        Matrix[] boneTransformations;
        Model chopper;
        Vector3 rotation;
        private CameraSystem cameraSystem;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cameraSystem = new CameraSystem();
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
            view = Matrix.CreateLookAt(new Vector3(10,10,10), Vector3.Zero,Vector3.Up); //Camera eller Transform?
            rotation = Vector3.Zero; //Transform
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, .1f, 1000f); //Camera? eller Transform
            CreateEntities();
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
            rotation.Y += 1f;
            rotation.X += 1f;
            chopper.Bones["Main_Rotor"].Transform = Matrix.CreateRotationY(rotation.Y) * Matrix.CreateTranslation(chopper.Bones["Main_Rotor"].Transform.Translation);
            chopper.Bones["Back_Rotor"].Transform = Matrix.CreateRotationZ((float) MathHelper.Pi/2 ) * Matrix.CreateRotationX(rotation.X)* Matrix.CreateTranslation(chopper.Bones["Back_Rotor"].Transform.Translation);
   
            cameraSystem.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            boneTransformations = new Matrix[chopper.Bones.Count];
            chopper.CopyAbsoluteBoneTransformsTo(boneTransformations);
            foreach (ModelMesh mesh in chopper.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.World = boneTransformations[mesh.ParentBone.Index] * Matrix.CreateScale(1f) * Matrix.CreateRotationX(0) * Matrix.CreateRotationY(0) * Matrix.CreateRotationZ(0);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();

                }
                mesh.Draw();
            }

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
        private void CreateEntities()
        {
            var chopperID = ComponentManager.Get().NewEntity();



            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { cameraPosition = new Vector3(10, 10, 10),cameraTarget = Vector3.Zero }, chopperID);
            //ComponentManager.Get().AddComponentToEntity(new TransformComponent() { }, chopperID);
            ComponentManager.Get().AddComponentToEntity(new ModelComponent() {model = Content.Load<Model>("Chopper")},chopperID);

        }
    }
}
