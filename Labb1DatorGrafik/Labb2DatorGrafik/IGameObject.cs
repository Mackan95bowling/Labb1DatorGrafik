using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik
{
    public interface IGameObject
    {
        Vector3 BoundingSphere { get; set; }
        Matrix WorldMatrix { get; set; }

        void Draw(Matrix view, Matrix projection);
        void Update(GameTime gameTime);
    }
}
