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
        private CameraSystem cameraSystem;
        LightSystem lightSystem;
        ShadowSystem shadowSystem;
        Model dude;
        Model Chopper;
        Model House;
        Model ground;
        Texture2D houseTexture;
        Texture2D groundTexture;
        Texture2D dudeTexture;
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
            Chopper = Content.Load<Model>("Chopper");
            House = Content.Load<Model>("farmhouse_obj");
            houseTexture = Content.Load<Texture2D>("Farmhouse_Texture");
            ground = Content.Load<Model>("Models/grid");
            groundTexture = Content.Load<Texture2D>("Grid");
            dude = Content.Load<Model>("Models/dude");
            dudeTexture = Content.Load<Texture2D>("Grid");
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

           //CreateChopper();
           //CreateHouse();
            CreateDude();
            CreateGround();
            CreateShadowRender();
            CreateFog();
            cameraSystem.SetCameraView();
            var ambientID = ComponentManager.Get().NewEntity();
            var ambientComponent = new AmbientComponent() { AmbientColor = Color.White.ToVector4(), Intensity = 0.2f };
            ComponentManager.Get().AddComponentToEntity(ambientComponent, ambientID);
            var lightID = ComponentManager.Get().NewEntity();
            var lightComponent = new LightComponent() { LightDir = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f), DiffLightColor = Color.White.ToVector4(), DiffIntensity = 0.5f };
            lightComponent.DiffLightDir = lightComponent.LightDir;
            ComponentManager.Get().AddComponentToEntity(lightComponent,lightID);
        }

        private void CreateDude()
        {
            var dudeID = ComponentManager.Get().NewEntity();
            var modelComponent = new ModelComponent(dudeTexture, dude, new Vector3(0, 1, 0));
            modelComponent.ModelEffect = Content.Load<Effect>("ShadowMapEffect");
            modelComponent.ShadowMapRender = true;
            modelComponent.ObjectWorld = GameService.Instance().WorldMatrix;
            ComponentManager.Get().AddComponentToEntity(modelComponent, dudeID);
            var shadowEffectDude = new ShadowMapEffect();
            shadowEffectDude.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectDude, dudeID);
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
            modelComponentGround.ModelEffect = Content.Load<Effect>("ShadowMapEffect");
            modelComponentGround.ShadowMapRender = true;
            modelComponentGround.ObjectWorld = GameService.Instance().WorldMatrix;
            ComponentManager.Get().AddComponentToEntity(modelComponentGround, groundId);
            var shadowEffectGround = new ShadowMapEffect();
            shadowEffectGround.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectGround,groundId);

        }

        public void  CreateHouse() {
            var HouseID = ComponentManager.Get().NewEntity();
            var modelComponentHouse = new ModelComponent(houseTexture, House, (new Vector3(10, 10, 0)* Matrix.Identity.Translation));
            modelComponentHouse.ModelEffect = Content.Load<Effect>("ShadowMapEffect");
            modelComponentHouse.ShadowMapRender = true;
            modelComponentHouse.ObjectWorld = GameService.Instance().WorldMatrix;
            ComponentManager.Get().AddComponentToEntity(modelComponentHouse, HouseID);
            var shadowEffectHouse = new ShadowMapEffect();
            shadowEffectHouse.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectHouse, HouseID);
        }
        public void CreateChopper() {

            var chopperID = ComponentManager.Get().NewEntity();
            var modelComponentChopper = new ModelComponent(groundTexture, Chopper ,new Vector3(-10, 10, 0 ));
            modelComponentChopper.ModelEffect = Content.Load<Effect>("ShadowMapEffect");
            modelComponentChopper.ShadowMapRender = true;
            modelComponentChopper.ObjectWorld = GameService.Instance().WorldMatrix;
            ComponentManager.Get().AddComponentToEntity(modelComponentChopper, chopperID);
            var shadowEffectChopper = new ShadowMapEffect();
            shadowEffectChopper.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectChopper, chopperID);

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

            shadowSystem.Draw(gameTime);

            lightSystem.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
