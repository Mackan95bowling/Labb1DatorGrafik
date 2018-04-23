using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModelDemo2;
using Labb1DatorGrafik.Component;
using Labb1DatorGrafik.Manager;

namespace Labb2DatorGrafik.Models
{
   public class Robot : GameObject
    {
        public Body body;
        private BasicEffect effect;
        private Vector3 position;
        public Robot(GraphicsDevice graphics, Vector3 position, HeightmapSystem heightMap)
        {
            this.position = position;
            body = new Body(graphics, position, heightMap);
            WorldMatrix = Matrix.CreateTranslation(position);

            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;

            effect = new BasicEffect(graphics);
            effect.VertexColorEnabled = true;

        }
        public override void Draw(Matrix view, Matrix projection)
        {
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;

            effect.Projection = cameraComp.projection;
            effect.View = cameraComp.view;
            effect.World = WorldMatrix;
            body.Draw(effect, WorldMatrix);
        }

        public override void Update(GameTime gameTime)
        {
            body.Update(gameTime);
        }

    }
}
