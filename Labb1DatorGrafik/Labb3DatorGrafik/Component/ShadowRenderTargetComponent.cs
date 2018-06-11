using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3DatorGrafik.Component
{
    public class ShadowRenderTargetComponent : IComponent
    {
      public RenderTarget2D shadowRenderTarget { get; set; }
    }
}
