using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModelDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik.Models
{
    public class RobotCameraSystem 
    {
        private Robot _robot;
        private GraphicsDevice _graphics;
        public RobotCameraSystem(GraphicsDevice graphics,Robot robot) {
            _robot = robot;
            _graphics = graphics;
        }
        public void Update(GameTime gameTime)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComp in cameraComps)
            {
                var camera = cameraComp.Value as CameraComponent;

                if (camera.FollowPlayer)
                {
                    camera.cameraTarget = _robot.RobotBody.WorldMatrix.Translation;
                    camera.cameraPosition = (_robot.RobotBody.WorldMatrix.Translation + new Vector3(0,0,5));
                    camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
                    camera.BoundingFrustum = CreateBoundingFrustum(camera);
                }
            }

        }
        public BoundingFrustum CreateBoundingFrustum(CameraComponent camera)
        {
            BoundingFrustum boundingFrustum = new BoundingFrustum(Matrix.CreatePerspectiveFieldOfView(1.1f * MathHelper.PiOver2, _graphics.Viewport.AspectRatio,
                        0.5f * 0.1f, 1.3f * 1000f) * camera.view);
            return boundingFrustum;
        }
    }
}
