using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Labb2DatorGrafik.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik.Factory
{
    class EntityFactory
    {
        private Labb2 game;
        public EntityFactory(Labb2 labb)
        {
            this.game = labb;
        }

        public void CreateCameraEntity()
        {
            var cameraID = ComponentManager.Get().NewEntity();
            ComponentManager.Get().AddComponentToEntity(
                new CameraComponent()
                {
                    fieldOfView = MathHelper.ToRadians(45f),
                    aspectRatio = game.GraphicsDevice.Viewport.AspectRatio,
                    cameraPosition = new Vector3(0, -10, 50),
                    cameraTarget = new Vector3(0, -10, 0),
                    FollowPlayer = true,
                    World = Matrix.Identity,
                    projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45f),
                        game.GraphicsDevice.Viewport.AspectRatio, 
                        1f, 
                        1000f)

                }, cameraID);
        }

        public List<GameObject> CreateHouseStaticObject(int amount, Model houses, Texture2D texture)
        {
            List<GameObject> house = new List<GameObject>();
            var heightData = game.heightmapSystem.GetHeightMapData();
            List<Vector3> modelPositions = new List<Vector3>();

            modelPositions = game.GetHeightMapPositionPositions(heightData, amount);
            for (int i = 0; i < amount; i++)
            {
                house.Add(new House(game.GraphicsDevice, houses, texture, modelPositions[i]));

            }
            return house;
        }
        public List<GameObject> CreateOtherStaticObject(int amount, Model houses, Texture2D texture) //FIX
        {
            List<GameObject> Other = new List<GameObject>();
            var heightData = game.heightmapSystem.GetHeightMapData();
            //add position to HouseConstructor!
            //GetHeightMapPositionPosition();
            for (int i = 0; i < amount; i++)
            {

                Other.Add(new House(game.GraphicsDevice, houses, texture, new Vector3(10, 0, 100)));

            }
            return Other;
        }

        public Robot CreateRobot(Vector3 pos, BasicEffect effect)
        {
            Robot robot = new Robot(
                game.GraphicsDevice,
                new Vector3(0, 0, 0), game.heightmapSystem,
                new BasicEffect(game.GraphicsDevice)
                {
                    VertexColorEnabled = true,
            });
            return robot;
        }
    }
}
