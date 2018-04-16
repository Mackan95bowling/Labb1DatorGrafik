using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb2DatorGrafik.Models
{
    public class House : IGameObject
    {
        Vector3 position;
        BoundingBox boundingBoxHouse;


        public House() {


        }
        public void Draw(BasicEffect effect, Matrix world)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
