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
        public Vector3 position;
        public RobotCameraSystem tst;
        public Robot(GraphicsDevice graphics, Vector3 position, HeightmapSystem heightMap)
        {
            body = new Body(graphics, position, heightMap);
            this.position = position;
            WorldMatrix = Matrix.CreateTranslation(position);
            effect = new BasicEffect(graphics);

            effect.VertexColorEnabled = true;
            var cameraComps = ComponentManager.Get().GetComponents<CameraComponent>();
            var cameraComp = cameraComps.FirstOrDefault().Value as CameraComponent;
            effect.Projection = cameraComp.projection;
            effect.View = cameraComp.view;

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
