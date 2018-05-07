﻿using Labb3DatorGrafik.Component;
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

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameService.Instance().WorldMatrix = Matrix.Identity;
            heightmapSystem = new HeightmapSystem();
            cameraSystem = new CameraSystem();


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
                .SetHeightMapTextureData(Content.Load<Texture2D>("Untitled"), Content.Load<Texture2D>("sand"))
                .SetHeights()
                .SetVertices()
                .SetIndices()
                //.InitNormal()
                .SetEffects(graphics.GraphicsDevice)
                .Build();

            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(new CameraComponent() { fieldOfView = MathHelper.ToRadians(45f), aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio, cameraPosition = new Vector3(0, 0, -100), cameraTarget = Vector3.Zero }, cameraID);

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

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            heightmapSystem.Draw(graphics.GraphicsDevice);

            base.Draw(gameTime);
        }

        public static Game GetGame()
        {
            return _thisGame;
        }
    }
}
