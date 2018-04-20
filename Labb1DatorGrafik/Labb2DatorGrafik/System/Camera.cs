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

namespace Labb2DatorGrafik.System
{
    public class Camera : IGameObject
    {
        // matrix for camera view and projection
        Matrix viewMatrix;
        Matrix projectionMatrix;

        // world matrix for our landscape
        public Matrix terrainMatrix;

        // actual camera position, direction, movement, rotation
        Vector3 position;
        Vector3 direction;
        Vector3 movement;
        Vector3 rotation;

        public Camera(Vector3 position, Vector3 direction, Vector3 movement)
        {
            this.position = position;
            this.direction = direction;
            this.movement = movement;
            rotation = movement * 0.02f;
            //camera position, view of camera, see what is over camera
            viewMatrix = Matrix.CreateLookAt(position, direction, Vector3.Up);
            //width and height of camera near plane, range of camera far plane (1-1000)
            projectionMatrix = Matrix.CreatePerspective(1.2f, 0.9f, 1.0f, 1000.0f);
        }

        public void Update(GameTime gameTime)
        {
            Vector3 tempMovement = Vector3.Zero;
            Vector3 tempRotation = Vector3.Zero;
            //left
            tempMovement.X = movement.X;

            //move camera to new position
            viewMatrix = viewMatrix * Matrix.CreateRotationX(tempRotation.X) * Matrix.CreateRotationY(tempRotation.Y) * Matrix.CreateTranslation(tempMovement);
            //update position
            position += tempMovement;
            direction += tempRotation;
        }

        public void Draw(BasicEffect effect, Matrix world)
        {
            effect.View = viewMatrix;
            effect.World = world;

        }

        //public void SetCameraView()
        //{
        //    var cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
        //    foreach (var cameraComponent in cameraComponents)
        //    {
        //        var camera = cameraComponent.Value as CameraComponent;
        //        camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
        //        camera.projection = Matrix.CreatePerspectiveFieldOfView(camera.fieldOfView, camera.aspectRatio, 1f, 1000f);

        //    }

        //}
        //private void MoveCamera()
        //{
        //    var cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
        //    foreach (var cameraComponent in cameraComponents)
        //    {
        //        var camera = cameraComponent.Value as CameraComponent;
        //        var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();

        //        foreach (var modelComponent in modelComponents)
        //        {
        //            if (camera.FollowPlayer)
        //            {
        //                var model = modelComponent.Value as ModelComponent;
        //                camera.cameraTarget = model.modelPosition; //camera fck the Z doesnt work as i want it tooo
        //                camera.cameraPosition = new Vector3(camera.cameraTarget.X, (camera.cameraTarget.Y + 10), (camera.cameraTarget.Z + 20));
        //                camera.view = Matrix.CreateLookAt(camera.cameraPosition, camera.cameraTarget, Vector3.Up);
        //            }
        //        }
        //        Console.WriteLine(camera.cameraPosition.ToString());
        //    }
        //}
        //public void Update(GameTime gameTime)
        //{

        //   MoveCamera();
        //}

    }
}
