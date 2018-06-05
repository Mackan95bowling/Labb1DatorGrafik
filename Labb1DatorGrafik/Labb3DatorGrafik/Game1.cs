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

        // Systems
        private HeightmapSystem heightmapSystem;
        private CameraSystem cameraSystem;
        LightSystem lightSystem;
        Model House;
        Texture2D houseTexture;

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
            lightSystem = new LightSystem();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            House = Content.Load<Model>("farmhouse_obj");
            houseTexture = Content.Load<Texture2D>("Farmhouse_Texture");

            HeightMapBuilder heightMap = new HeightMapBuilder()
                .SetHeightMapTextureData(Content.Load<Texture2D>("US_CANYON"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                .InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .Build();

            var cameraID = ComponentManager.Get().NewEntity();
            var cameraComponent = new CameraComponent() {fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(0, 10, 200), cameraTarget = Vector3.Zero };
            cameraComponent.BoundingFrustum = new BoundingFrustum(Matrix.CreatePerspectiveFieldOfView(1.1f * MathHelper.PiOver2, graphics.GraphicsDevice.Viewport.AspectRatio,
                        0.5f * 0.1f, 1.3f * 1000f) * cameraComponent.view);
            ComponentManager.Get().AddComponentToEntity(cameraComponent, cameraID);

            //var chopperID = ComponentManager.Get().NewEntity();
            //ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = Content.Load<Model>("Chopper"), modelPosition = new Vector3(15, 10, 0), ModelEffect = Content.Load<Effect>("ShadowEffect") }, chopperID);
            var testID = ComponentManager.Get().NewEntity();
            var modelComponentHouse = new ModelComponent(houseTexture, House, new Vector3(0, 10, 0));
            modelComponentHouse.ModelEffect = Content.Load<Effect>("ShadowEffect");
            ComponentManager.Get().AddComponentToEntity(modelComponentHouse, testID);
            cameraSystem.SetCameraView();
            var lightID = ComponentManager.Get().NewEntity();
            var lightComponent = new LightComponent() { LightDir = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f), DiffLightColor = Color.White.ToVector4(), DiffIntensity = 1.0f };
            lightComponent.DiffLightDir = lightComponent.LightDir;
            ComponentManager.Get().AddComponentToEntity(lightComponent,lightID);
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
         //   lightSystem.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            heightmapSystem.Draw(graphics.GraphicsDevice);
            modelSystem.Draw(gameTime);
        //    lightSystem.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
