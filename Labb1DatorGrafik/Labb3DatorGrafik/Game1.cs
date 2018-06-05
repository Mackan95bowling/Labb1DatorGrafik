using System;
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
        ShadowSystem shadowSystem;
        Model House;
        Model ground;
        Texture2D houseTexture;
        Texture2D groundTexture;

        public ModelSystem modelSystem;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this)

            {

                PreferredBackBufferWidth = 1280,

                PreferredBackBufferHeight = 720,

                GraphicsProfile = GraphicsProfile.HiDef

            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameService.Instance().WorldMatrix = Matrix.Identity;
            GameService.Instance().graphics = graphics.GraphicsDevice;
            //heightmapSystem = new HeightmapSystem();
            cameraSystem = new CameraSystem();
            modelSystem = new ModelSystem();
            lightSystem = new LightSystem();
            shadowSystem = new ShadowSystem();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            House = Content.Load<Model>("farmhouse_obj");
            houseTexture = Content.Load<Texture2D>("Farmhouse_Texture");
            ground = Content.Load<Model>("Models/grid");
            groundTexture = Content.Load<Texture2D>("Grid");
            //HeightMapBuilder heightMap = new HeightMapBuilder()
            //    .SetHeightMapTextureData(Content.Load<Texture2D>("US_CANYON"), Content.Load<Texture2D>("sand"))
            //    .SetHeights()
            //    .SetVertices()
            //    .SetIndices()
            //    .InitNormal()
            //    .SetEffects(graphics.GraphicsDevice)
            //    .Build();

            var cameraID = ComponentManager.Get().NewEntity();
            var cameraComponent = new CameraComponent() {fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(0, 10,20), cameraTarget = (new Vector3(10,10,0)* GameService.Instance().WorldMatrix.Translation) };
            cameraComponent.BoundingFrustum = new BoundingFrustum(Matrix.CreatePerspectiveFieldOfView(1.1f * MathHelper.PiOver2, graphics.GraphicsDevice.Viewport.AspectRatio,
                        0.5f * 0.1f, 1.3f * 1000f) * cameraComponent.view);
            ComponentManager.Get().AddComponentToEntity(cameraComponent, cameraID);

            //var chopperID = ComponentManager.Get().NewEntity();
            //ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = Content.Load<Model>("Chopper"), modelPosition = new Vector3(15, 10, 0), ModelEffect = Content.Load<Effect>("ShadowEffect") }, chopperID);
            CreateHouse();
            CreateGround();
            CreateShadowRender();
            CreateFog();
            cameraSystem.SetCameraView();
            var ambientID = ComponentManager.Get().NewEntity();
            var ambientComponent = new AmbientComponent() { AmbientColor = Color.White.ToVector4(), Intensity = 0.2f };
            ComponentManager.Get().AddComponentToEntity(ambientComponent, ambientID);
            var lightID = ComponentManager.Get().NewEntity();
            var lightComponent = new LightComponent() { LightDir = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f), DiffLightColor = Color.White.ToVector4(), DiffIntensity = 1.0f };
            lightComponent.DiffLightDir = lightComponent.LightDir;
            ComponentManager.Get().AddComponentToEntity(lightComponent,lightID);
        }

        private void CreateShadowRender()
        {
            var shadowRenderCompID = ComponentManager.Get().NewEntity();
            var shadowRenderComp = new ShadowRenderTargetComponent();
            shadowRenderComp.shadowRenderTarget = new RenderTarget2D(GameService.Instance().graphics,2048,2048,false,SurfaceFormat.Single,DepthFormat.Depth24);
            ComponentManager.Get().AddComponentToEntity(shadowRenderComp, shadowRenderCompID);


        }
        private void CreateFog() {
            var fogCompID = ComponentManager.Get().NewEntity();
            var fogComp = new FogComponent();
            fogComp.Color = Color.CornflowerBlue.ToVector4();
            fogComp.Enabled = true;
            fogComp.FogStart = 200f;
            fogComp.FogEnd = 300f;
            ComponentManager.Get().AddComponentToEntity(fogComp, fogCompID);

        }

        private void CreateGround()
        {
            var groundId = ComponentManager.Get().NewEntity();
            var modelComponentGround = new ModelComponent(groundTexture, ground, new Vector3(0,0,0));
            modelComponentGround.ModelEffect = Content.Load<Effect>("ShadowEffect");
            modelComponentGround.ShadowMapRender = true;
            ComponentManager.Get().AddComponentToEntity(modelComponentGround, groundId);
            var shadowEffectGround = ComponentManager.Get().EntityComponent<ShadowMapEffect>(groundId);
            shadowEffectGround.effect = Content.Load<Effect>("ShadowEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectGround,groundId);

        }

        public void  CreateHouse() {
            var testID = ComponentManager.Get().NewEntity();
            var modelComponentHouse = new ModelComponent(houseTexture, House, (new Vector3(10, 10, 0)* Matrix.Identity.Translation));
            modelComponentHouse.ModelEffect = Content.Load<Effect>("ShadowEffect");
            modelComponentHouse.ShadowMapRender = true;
            ComponentManager.Get().AddComponentToEntity(modelComponentHouse, testID);
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
            lightSystem.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           // heightmapSystem.Draw(graphics.GraphicsDevice);
            //modelSystem.Draw(gameTime);
            lightSystem.Draw(gameTime);
            shadowSystem.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
