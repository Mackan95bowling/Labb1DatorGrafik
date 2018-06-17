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

                PreferredBackBufferWidth = 800,

                PreferredBackBufferHeight = 480,


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

            var cameraID = ComponentManager.Get().NewEntity();
            var cameraComponent = new CameraComponent() {fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(0, 150,50), cameraTarget = new Vector3(0, -0.4472136f, -0.8944272f) };
            cameraComponent.BoundingFrustum = new BoundingFrustum(Matrix.Identity);
            ComponentManager.Get().AddComponentToEntity(cameraComponent, cameraID);

            CreateChopper();
            CreateHouse();
            CreateDude();
            CreateGround();
            CreateShadowRender();
            CreateFog();
            cameraSystem.SetCameraView();
            CreateLight();
      
        }
        public void CreateLight() {

            var lightID = ComponentManager.Get().NewEntity();
            var lightComponent = new LightComponent();
            lightComponent.LightDir = new Vector3(-0.3333333f, 0.6666667f, 0.6666667f);
            lightComponent.DiffLightColor = Color.White.ToVector4(); 
            lightComponent.DiffIntensity = 0.5f;
            lightComponent.DiffLightDir = lightComponent.LightDir;
   
           
            ComponentManager.Get().AddComponentToEntity(lightComponent, lightID);
            var ambientID = ComponentManager.Get().NewEntity();
            var ambientComponent = new AmbientComponent() { AmbientColor = Color.White.ToVector4(), Intensity = 0.2f };
            ComponentManager.Get().AddComponentToEntity(ambientComponent, ambientID);

        }

        private void CreateDude()
        {
            var dudeID = ComponentManager.Get().NewEntity();
            var modelComponent = new ModelComponent(dudeTexture, dude, new Vector3(0, 0, 0));
            modelComponent.ShadowMapRender = true;
            modelComponent.ObjectWorld = GameService.Instance().WorldMatrix * Matrix.CreateTranslation(modelComponent.modelPosition); 
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
            var modelComponentGround = new ModelComponent(groundTexture, ground, new Vector3(0,1,0));
            modelComponentGround.ShadowMapRender = true;
            modelComponentGround.ObjectWorld = GameService.Instance().WorldMatrix;
            ComponentManager.Get().AddComponentToEntity(modelComponentGround, groundId);
            var shadowEffectGround = new ShadowMapEffect();
            shadowEffectGround.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectGround,groundId);

        }

        public void  CreateHouse() {
            var HouseID = ComponentManager.Get().NewEntity();
            var modelComponentHouse = new ModelComponent(houseTexture, House,new Vector3(-10, 1, 0));
            modelComponentHouse.ShadowMapRender = true;
            modelComponentHouse.ObjectWorld = GameService.Instance().WorldMatrix * Matrix.CreateTranslation(modelComponentHouse.modelPosition);
            ComponentManager.Get().AddComponentToEntity(modelComponentHouse, HouseID);
            var shadowEffectHouse = new ShadowMapEffect();
            shadowEffectHouse.effect = Content.Load<Effect>("ShadowMapEffect");
            ComponentManager.Get().AddComponentToEntity(shadowEffectHouse, HouseID);
        }
        public void CreateChopper() {

            var chopperID = ComponentManager.Get().NewEntity();
            var modelComponentChopper = new ModelComponent(groundTexture, Chopper ,new Vector3(10, 1, 0 ));
            modelComponentChopper.ShadowMapRender = true;
            modelComponentChopper.ObjectWorld = GameService.Instance().WorldMatrix * Matrix.CreateTranslation(modelComponentChopper.modelPosition);
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
