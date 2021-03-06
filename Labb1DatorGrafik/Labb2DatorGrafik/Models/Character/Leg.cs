﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ModelDemo
{
    class Leg : CuboidMesh
    {
        private List<IGameObject> _children = new List<IGameObject>();

        private Vector3 _rotation = Vector3.Zero;
        private Vector3 _position = new Vector3(0, 0, 0);
        private Vector3 _jointPos = new Vector3(0, 0, 0);
        private float maxRotation = 0.8f;
        private bool rotatePositive = true;

        public Leg(GraphicsDevice graphics, Vector3 jointPos, Vector3 rotation,Texture2D texture, bool rotationSide)
            : base(graphics, .1f, .1f, .1f)
        {
            _jointPos = jointPos;
            _rotation = rotation;
            _children.Add(new OuterLimb(graphics,new Vector3(0,_sizeY/2,_sizeZ/2),texture));
            rotatePositive = rotationSide;
            Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {

            if (_rotation.Z >= maxRotation) rotatePositive = false;
            if (_rotation.Z <= -maxRotation) rotatePositive = true;


            if (Keyboard.GetState().IsKeyDown(Keys.Up) && rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z + 0.01f);
            if(Keyboard.GetState().IsKeyDown(Keys.Up) && !rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z - 0.01f);


            if (Keyboard.GetState().IsKeyDown(Keys.Down) && rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z + 0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z - 0.01f);

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z + 0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z - 0.01f);

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z + 0.01f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !rotatePositive)
                _rotation = new Vector3(_rotation.X, _rotation.Y, _rotation.Z - 0.01f);


            // ------------------- >>
            WorldMatrix = Matrix.Identity *
                Matrix.CreateTranslation(_position) *
                Matrix.CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z)) *
                Matrix.CreateTranslation(_jointPos);

            foreach (IGameObject go in _children)
                go.Update(gameTime);
        }

        public override void Draw(BasicEffect effect, Matrix world)
        {
            effect.World = WorldMatrix * world;
            effect.CurrentTechnique.Passes[0].Apply();
            effect.Texture = Texture;
            GraphicsDevice.SetVertexBuffer(VertexBuffer);
            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, VertexBuffer.VertexCount);

            foreach (IGameObject go in _children)
                go.Draw(effect, WorldMatrix * world);
        }
    }
}
