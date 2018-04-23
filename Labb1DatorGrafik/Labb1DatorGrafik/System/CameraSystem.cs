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
                camera.World = Matrix.Identity;
                camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
                camera.projection = Matrix.CreatePerspectiveFieldOfView(camera.fieldOfView, camera.aspectRatio, 1f, 1000f);
                camera.ClippingProjection = Matrix.CreatePerspectiveFieldOfView(1.1f * camera.fieldOfView, camera.aspectRatio,
                0.5f * 1f, 1.3f * 1000f);
                camera.BoundingFrustum = new BoundingFrustum(camera.view * camera.ClippingProjection);
            }

        }

        public void Update(GameTime gameTime) {

        }
    }
}
