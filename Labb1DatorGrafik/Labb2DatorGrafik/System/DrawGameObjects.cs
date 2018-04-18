using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Labb2DatorGrafik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik.System
{
    public class DrawGameObjects
    {
       public List<IGameObject> gameObjects;
        public DrawGameObjects() {
            gameObjects = new List<IGameObject>();

        }

        public void Draw()
        {
            var cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var camera in cameraComponents) {
                var cam = camera.Value as CameraComponent;
                foreach (var gameobject in gameObjects) {
                    gameobject.Draw(cam.view, cam.projection);

                }

            }
        }
           

    }
}
