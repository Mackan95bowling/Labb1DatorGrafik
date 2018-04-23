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
        public RobotCameraSystem(Robot robot) {
            _robot = robot;
        }
        public void Update(GameTime gameTime)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComp in cameraComps)
            {
                var camera = cameraComp.Value as CameraComponent;

                if (camera.FollowPlayer)
                {
                    camera.cameraTarget = _robot.RobotBody._position;
                    camera.cameraPosition = _robot.RobotBody._position - new Vector3(0, 0, 5f);
                    camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);

                    Console.WriteLine("CameraTargetRobotCamera" + camera.cameraTarget);
                    Console.WriteLine("CameraPositionRobotCamera" + camera.cameraPosition);
                }
            }

        }
    }
}
