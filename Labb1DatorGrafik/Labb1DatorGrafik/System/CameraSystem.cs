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

        public void Update(GameTime gameTime)
        {
            cameraComponents = ComponentManager.Get().GetComponents<CameraComponent>();
            foreach (var cameraComponent in cameraComponents)
            {
                var camera = cameraComponent.Value as CameraComponent;
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    camera.cameraPosition.X -= 1f;
                    camera.cameraTarget.X -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    camera.cameraPosition.X += 1f;
                    camera.cameraTarget.X += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    camera.cameraPosition.Y -= 1f;
                    camera.cameraTarget.Y -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    camera.cameraPosition.Y += 1f;
                    camera.cameraTarget.Y += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                {
                    camera.cameraPosition.Z += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                {
                    camera.cameraPosition.Z -= 1f;
                }

            }
        }
    }
}
