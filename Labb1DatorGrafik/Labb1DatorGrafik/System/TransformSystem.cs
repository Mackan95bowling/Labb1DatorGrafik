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
    public class TransformSystem
    {
        public void TransformMove() {
            var transformComponents = ComponentManager.Get().GetComponents<TransformComponent>();
            var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
            foreach (var transformComponent in transformComponents) {
                var transform = transformComponent.Value as TransformComponent;
                foreach (var modelcomponent in modelComponents) {
                    var model = modelcomponent.Value as ModelComponent;
                    if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(0, -1f, 0) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(0, -1f, 0);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(0, 1f, 0) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(0, 1f, 0);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(1f, 0, 0) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(1f, 0, 0);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(-1f, 0, 0) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(-1f, 0, 0);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(0, 0, 1f) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(0, 0, 1f);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        model.model.Bones[0].Transform *= Matrix.CreateTranslation(0, 0, -1f) * Matrix.CreateRotationX(0) * Matrix.CreateTranslation(0, 0, -1f);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                        model.model.Bones[0].Transform *= Matrix.CreateRotationY(1f);
                    }
                }
            }
        }
        public void Update(GameTime gameTime) {
            RotationOnRotors();
            TransformMove();
        }
        public void RotationOnRotors() {
            var transformComponents = ComponentManager.Get().GetComponents<TransformComponent>();
            var modelComponents = ComponentManager.Get().GetComponents<ModelComponent>();
            foreach (var transformComponent in transformComponents)
            {
                var transform = transformComponent.Value as TransformComponent;
                transform.rotation.Y += .9f;
                transform.rotation.X += .9f;
                foreach (var modelComponent in modelComponents)
                {
                    var model = modelComponent.Value as ModelComponent;
                    model.model.Bones["Main_Rotor"].Transform = Matrix.CreateRotationY(transform.rotation.Y) * Matrix.CreateTranslation(model.model.Bones["Main_Rotor"].Transform.Translation);
                    model.model.Bones["Back_Rotor"].Transform = Matrix.CreateRotationZ((float)MathHelper.Pi / 2) * Matrix.CreateRotationX(transform.rotation.X) * Matrix.CreateTranslation(model.model.Bones["Back_Rotor"].Transform.Translation);
                }
            }
        }
    }
}
