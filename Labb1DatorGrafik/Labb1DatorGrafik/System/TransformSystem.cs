using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;

namespace Labb1DatorGrafik.System
{
    public class TransformSystem
    {
       
        public void Update(GameTime gameTime) {
            var transformComponents = ComponentManager.Get().GetComponents<TransformComponent>();
            //likadant för model??? för att få in model för att kunna använda chopper!
            foreach (var transformComponent in transformComponents)
            {
                var transform = transformComponent.Value as TransformComponent;
                transform.rotation.Y += 1f;
                transform.rotation.X += 1f;
                chopper.Bones["Main_Rotor"].Transform = Matrix.CreateRotationY(transform.rotation.Y) * Matrix.CreateTranslation(chopper.Bones["Main_Rotor"].Transform.Translation);
                chopper.Bones["Back_Rotor"].Transform = Matrix.CreateRotationZ((float)MathHelper.Pi / 2) * Matrix.CreateRotationX(transform.rotation.X) * Matrix.CreateTranslation(chopper.Bones["Back_Rotor"].Transform.Translation);
            }
        }
    }
}
