using Labb1DatorGrafik.System;
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
    public class Body : CuboidMesh
    {
        private List<IGameObject> _children = new List<IGameObject>();

        private Vector3 _rotation = new Vector3(0,0,0);
        public Vector3 _position = Vector3.Zero;
        public HeightmapSystem heightmap;
        float[,] heightMapData;
        public Body(GraphicsDevice graphics, Vector3 pos, HeightmapSystem heightmapSystem)
            : base(graphics, .3f,.3f, .3f)
        {
            heightmap = heightmapSystem;
            heightMapData = heightmap.GetHeightMapData();
            _position = pos;
            // Head
            _children.Add(new Head(graphics, new Vector3(0,_sizeY-0.1f,0)));
            // Legs
            _children.Add(new Leg(graphics, new Vector3(0, -(_sizeY/2), _sizeZ/2), new Vector3(0, 3.15f, 0), true));
            _children.Add(new Leg(graphics, new Vector3(0, -(_sizeY/2), -(_sizeZ/2)), new Vector3(0, 3.15f, 0), false));
            //// Arms
            _children.Add(new Arm(graphics, new Vector3(0, _sizeY/2, _sizeZ/2f), new Vector3(0, 2, 0)));
            _children.Add(new Arm(graphics, new Vector3(0, _sizeY / 2, -(_sizeZ / 2f)), new Vector3(0, -2, 0)));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _rotation = new Vector3(_rotation.X + 0.01f, _rotation.Y, _rotation.Z);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                _position = new Vector3(_position.X + .01f, _position.Y, _position.Z);
            }

            Console.WriteLine();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _rotation = new Vector3(_rotation.X - 0.01f, _rotation.Y, _rotation.Z);

            //TEST POSITION
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _position = new Vector3(_position.X - 0.05f, _position.Y, _position.Z);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                _position = new Vector3(_position.X + 0.05f, _position.Y, _position.Z);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _position = new Vector3(_position.X , _position.Y - 0.05f, _position.Z);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _position = new Vector3(_position.X, _position.Y + 0.05f, _position.Z);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _position = new Vector3(_position.X, _position.Y, _position.Z - 0.05f);
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                _position = new Vector3(_position.X, _position.Y , _position.Z + 0.05f);
            // _________________ //

            BoundPlayerToGround();

            World = Matrix.Identity *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z)) *
                Matrix.CreateTranslation(_position);
            Console.WriteLine(_position.ToString());

            foreach (IGameObject go in _children)
                go.Update(gameTime);
        }
        private void BoundPlayerToGround() {
            if (_position.X < 0) _position.X = 0;
            if (_position.X > 1080) _position.X = 1080;
            if (_position.Z < 0) _position.Z = 0;
            if (_position.Z > 1080) _position.Z = 1080;

            _position.Y = heightMapData[Convert.ToInt32(_position.X), Convert.ToInt32(_position.Z)];
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
