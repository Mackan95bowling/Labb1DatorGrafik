using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3DatorGrafik.Component;
using Labb3DatorGrafik.Manager;

using Microsoft.Xna.Framework.Input;
using Labb3DatorGrafik.Service;

namespace Labb3DatorGrafik.System
{
    public class CameraSystem
    {
        Dictionary<uint, IComponent> cameraComponents;
        public void SetCameraView(){
            cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComponent in cameraComponents) {
                var camera = cameraComponent.Value as CameraComponent;
              //  camera.World = GameService.Instance().WorldMatrix;
                camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
                camera.projection = Matrix.CreatePerspectiveFieldOfView(camera.fieldOfView, camera.aspectRatio, 1f, 1000f);
               
            }

        }

        public void Update(GameTime gameTime) {
            var camera = ComponentManager.Get().GetComponents<CameraComponent>().Values.FirstOrDefault() as CameraComponent;

            Vector3 cameraLeftRight = Vector3.Cross(Vector3.Up, camera.cameraTarget);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {//move
                camera.cameraPosition.Y += 0.5f;// camera.cameraTarget * 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {//move
                camera.cameraPosition.Y -= 0.5f;//camera.cameraTarget * 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {//move
                camera.cameraPosition -=  camera.cameraTarget * 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move
                camera.cameraPosition += camera.cameraTarget * 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //rotate
                camera.cameraPosition += cameraLeftRight * 0.5f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                //rotate
                camera.cameraPosition -= cameraLeftRight * 0.5f;
            }
            //camera.cameraTarget.Normalize();
            camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
            camera.BoundingFrustum.Matrix = camera.view * camera.projection;
            Console.WriteLine(camera.cameraPosition);
        }
    }
}
