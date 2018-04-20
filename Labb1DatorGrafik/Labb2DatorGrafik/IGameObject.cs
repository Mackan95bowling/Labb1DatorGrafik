using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik
{
    public abstract class GameObject
    {
        public BoundingBox BoundingBox { get; set; }
        public Matrix WorldMatrix { get; set; }

        public abstract void Draw(Matrix view, Matrix projection);
        public abstract void Update(GameTime gameTime);
    }
}
