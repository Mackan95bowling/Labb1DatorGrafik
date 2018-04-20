using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModelDemo2;

namespace Labb2DatorGrafik.Models
{
   public class Characters : GameObject
    {
        private Body body;
        private BasicEffect effect;
        public Vector3 Position { get; set; }

        public Characters(GraphicsDevice graphics, Vector3 position)
        {
            body = new Body(graphics, position);
            Position = position;
            WorldMatrix = Matrix.CreateTranslation(position);
            effect = new BasicEffect(graphics);

            effect.VertexColorEnabled = true;

            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 16 / 9f, 0.01f, 1000f);
            effect.View = Matrix.CreateLookAt(new Vector3(10f, 10f, 10f), new Vector3(0, 0, 0), Vector3.Up);

        }

        public override void Draw(Matrix view, Matrix projection)
        {
            body.Draw(effect, WorldMatrix);
        }

        public override void Update(GameTime gameTime)
        {
            body.Update(gameTime);
        }
    }
}
