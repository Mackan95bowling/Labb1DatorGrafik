using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;

using Microsoft.Xna.Framework.Input;

namespace Labb1DatorGrafik.System
{
    public class CameraSystem
    {
        Dictionary<uint, IComponent> cameraComponents;
        public void SetCameraView(){
            cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComponent in cameraComponents) {
                var camera = cameraComponent.Value as CameraComponent;
                camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
                camera.projection = Matrix.CreatePerspectiveFieldOfView(camera.fieldOfView, camera.aspectRatio, 1f, 1000f);

            }

        }
        private void MoveCamera()
        {
            cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComponent in cameraComponents)
            {
                var camera = cameraComponent.Value as CameraComponent;
                var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
                var transformComponents = ComponentManager.Get().GetComponents<TransformComponent>();
                foreach (var transformComponent in transformComponents) {
                    var transform = transformComponent.Value as TransformComponent;
                    camera.cameraTarget = transform.position;
                    camera.cameraPosition = new Vector3(camera.cameraTarget.X,(camera.cameraTarget.Y + 10),(camera.cameraTarget.Z +20));
                    camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);

                }
                //foreach (var modelComponent in modelComponents)
                //{
                //    transformComponent
                //    var model = modelComponent.Value as ModelComponent;
                //    camera.cameraTarget = model.modelPosition; //camera fck the Z doesnt work as i want it tooo
                //    camera.cameraPosition = new Vector3(camera.cameraTarget.X, (camera.cameraTarget.Y + 10), (camera.cameraTarget.Z + 20));
                //    camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
                //}
                //Console.WriteLine(camera.cameraPosition.ToString());
            }
        }
        public void Update(GameTime gameTime) {

          MoveCamera();
        }
    }
}
