using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModelDemo;

namespace ModelDemo2
{
    class Arm : CuboidMesh
    {
        private List<IGameObject> _children = new List<IGameObject>();

        private Vector3 _rotation = Vector3.Zero;
        private Vector3 _position = new Vector3(0, 1.5f, 0);
        private Vector3 _jointPos = new Vector3(0, 1.0f, 0);

        public Arm(GraphicsDevice graphics, Vector3 jointPos, Vector3 rotation)
            : base(graphics, 1f, 3f, 1f)
        {
            _jointPos = jointPos;
            _rotation = rotation;
            _children.Add(new OuterLimb(graphics));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _rotation = new Vector3(_rotation.X, _rotation.Y + 0.01f, _rotation.Z);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _rotation = new Vector3(_rotation.X, _rotation.Y - 0.01f, _rotation.Z);

            World = Matrix.Identity *
                Matrix.CreateTranslation(_position) *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z)) *
                Matrix.CreateTranslation(_jointPos);

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
