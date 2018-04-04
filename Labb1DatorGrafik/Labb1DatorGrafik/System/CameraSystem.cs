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
        
        //ComponentManager.Get().GetSpecificComponents(typeof(CameraComponent), out cameraComponents);

        public void Update(GameTime gameTime)
        {
            foreach (var cameraComponent in cameraComponents)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    cameraPosition.X -= 1f;
                    cameraTarget.X -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    camPosition.X += 1f;
                    camTarget.X += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    camPosition.Y -= 1f;
                    camTarget.Y -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    camPosition.Y += 1f;
                    camTarget.Y += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                {
                    camPosition.Z += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                {
                    camPosition.X -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    orbit = !orbit;
                }

            }
        }
    }
}
