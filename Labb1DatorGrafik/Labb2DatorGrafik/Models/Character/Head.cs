using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModelDemo
{
    class Head : CuboidMesh
    {
        private List<IGameObject> _children = new List<IGameObject>();

        private Vector3 _rotation = Vector3.Zero;
        private Vector3 _position = Vector3.Zero;

        public Head(GraphicsDevice graphics, Vector3 pos)
            : base(graphics, 0.15f, .15f, .15f)
        {
            _position = pos;
        }

        public override void Update(GameTime gameTime)
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //    _rotation = new Vector3(_rotation.X + 0.01f, _rotation.Y, _rotation.Z);

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //    _rotation = new Vector3(_rotation.X - 0.01f, _rotation.Y, _rotation.Z);

            World = Matrix.Identity *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z)) *
                Matrix.CreateTranslation(_position);

            foreach (IGameObject go in _children)
                go.Update(gameTime);
        }

        public override void Draw(BasicEffect effect, Matrix world)
        {
            effect.World = World * world;
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.SetVertexBuffer(VertexBuffer);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 36);

            foreach (IGameObject go in _children)
                go.Draw(effect, World * world);
        }
    }
}
