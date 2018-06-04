using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.Component
{
   public class AmbientComponent : IComponent
    {
        public Vector4 AmbientColor { get; set; }
        public float Intensity { get; set; }

    }
}
