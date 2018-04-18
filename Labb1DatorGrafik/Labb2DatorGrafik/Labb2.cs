using Labb2DatorGrafik.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Labb1DatorGrafik.Manager;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.System;
using System.Collections.Generic;

namespace Labb2DatorGrafik
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Labb2 : Game
    {
        GraphicsDeviceManager graphics;
        WorldTerrain worldTerrain;
        Texture2D texture, textureImage;
        public House woodhouse,brickHouse;
        BasicEffect basicEffect;

        List<IGameObject> gameObjects = new List<IGameObject>(100);
        /*private CameraSystem cameraSystem;
        private TransformSystem transformSystem;
        private ModelSystem modelSystem;
        private HeightmapSystem heightmapSystem;*/

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
            
            //cameraSystem = new CameraSystem();
            //transformSystem = new TransformSystem();
            //modelSystem = new ModelSystem();
            //heightmapSystem = new HeightmapSystem();
           // woodhouse.SetPosition(new Vector3(worldTerrain.Width/2,100,0));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            basicEffect = new BasicEffect(this.GraphicsDevice);
            // Create a new SpriteBatch, which can be used to draw textures.
            Texture2D houseTexture1 = Content.Load<Texture2D>("Farmhouse Texture");
            Texture2D houseTexture2 = Content.Load<Texture2D>("woodHouse");
            Model houses = Content.Load<Model>("farmhouse_obj");
           
            texture = Content.Load<Texture2D>("US_Canyon");
            textureImage = Content.Load<Texture2D>("sand");
            brickHouse = new House(this.GraphicsDevice, houses, houseTexture1);
            woodhouse = new House(this.GraphicsDevice,houses,houseTexture1);
            worldTerrain = new WorldTerrain(this.GraphicsDevice, texture, new Texture2D[4] {textureImage, textureImage, textureImage, textureImage });

            gameObjects.Add(brickHouse);
            gameObjects.Add(woodhouse);
            //gameObjects.Add(brickHouse);
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

            // looping through all the game objects
            // gameObjects.ForEach(o => o.Update(gameTime));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //graphics.GraphicsDevice.Clear(ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);
            // terrain.DrawGround();

           worldTerrain.BasicEffect.View = Matrix.CreateLookAt(new Vector3(0, 0, -20), new Vector3(0, 0, 0), Vector3.Up);

           worldTerrain.BasicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
          
          worldTerrain.Draw(worldTerrain.BasicEffect);
          brickHouse.Draw(worldTerrain.BasicEffect);
         //  woodhouse.Draw();

           // drawing all game objects
           // gameObjects.ForEach(o => o.Draw());
           base.Draw(gameTime);
        }

        //test
       /* public void CreateEntity() {
            var HouseId = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new ModelComponent() { model = woodhouse.model}, HouseId);


            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(15, 10, 20), cameraTarget = Vector3.Zero }, cameraID);

        }*/
    }
}
