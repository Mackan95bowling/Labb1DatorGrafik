using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.EngineHelpers;
using Labb3DatorGrafik.Manager;
using Labb3DatorGrafik.Service;
using Labb3DatorGrafik.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labb3DatorGrafik
{

    public class Game1 : Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Effect myEffect;

        // Systems
        private HeightmapSystem heightmapSystem;
        private CameraSystem cameraSystem;

        public ModelSystem modelSystem;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameService.Instance().WorldMatrix = Matrix.Identity;
            GameService.Instance().graphics = graphics.GraphicsDevice;
            heightmapSystem = new HeightmapSystem();
            cameraSystem = new CameraSystem();
            modelSystem = new ModelSystem();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //this is how we load our EFFECT
            myEffect = Content.Load<Effect>("test1");
            var test = myEffect;


            HeightMapBuilder heightMap = new HeightMapBuilder()
                .SetHeightMapTextureData(Content.Load<Texture2D>("US_CANYON"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                .InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .Build();

            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(10, 15, 70), cameraTarget = Vector3.Zero}, cameraID);

            var chopperID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = Content.Load<Model>("Chopper"), modelPosition = new Vector3(50, 10, 80)}, chopperID);
            var testID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = Content.Load<Model>("farmhouse_obj"), modelPosition = new Vector3(110, 10, 110) }, testID);
            cameraSystem.SetCameraView();
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            cameraSystem.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            heightmapSystem.Draw(graphics.GraphicsDevice);
            modelSystem.Draw(gameTime);
            base.Draw(gameTime);
        }

        //public static Game GetGame()
        //{
        //    return _thisGame;
        //}
    }
}
