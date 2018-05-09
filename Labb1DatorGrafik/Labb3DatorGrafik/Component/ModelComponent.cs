using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.Component
{
    public class ModelComponent : IComponent
    {
        public Vector3 modelPosition;
        public Matrix[] boneTransformations { get; set; }
        public Model model { get; set; }
    }
}
}
