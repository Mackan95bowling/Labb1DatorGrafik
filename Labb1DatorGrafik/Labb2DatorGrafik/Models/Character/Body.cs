using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModelDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDemo2
{
    class Body : CuboidMesh
    {
        private List<IGameObject> _children = new List<IGameObject>();

        //private Vector3 _rotation = Vector3.Zero;
        private Vector3 _rotation = new Vector3(0,0,0);
        private Vector3 _position = Vector3.Zero;

        public Body(GraphicsDevice graphics, Vector3 pos)
            : base(graphics, 3, 3, 5)
        {
            _position = pos;
            // Head
            _children.Add(new Head(graphics, new Vector3(0, 2.5f, 0)));
            // Legs
            _children.Add(new Leg(graphics, new Vector3(0, -1f, 1f), new Vector3(0, 3.15f, 0)));
            _children.Add(new Leg(graphics, new Vector3(0, -1f, -1f), new Vector3(0, 3.15f, 0)));
            // Arms
            _children.Add(new Arm(graphics, new Vector3(0, 0.5f, 2f), new Vector3(0, 2, 0)));
            _children.Add(new Arm(graphics, new Vector3(0, 0.5f, -2f), new Vector3(0, -2, 0)));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _rotation = new Vector3(_rotation.X + 0.01f, _rotation.Y, _rotation.Z);

            Console.WriteLine();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _rotation = new Vector3(_rotation.X - 0.01f, _rotation.Y, _rotation.Z);

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
