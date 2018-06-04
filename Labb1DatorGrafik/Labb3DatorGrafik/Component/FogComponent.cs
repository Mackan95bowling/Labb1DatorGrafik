using Microsoft.Xna.Framework;

namespace Labb3DatorGrafik.Component
{
    public class FogComponent : IComponent
    {
        public float FogStart { get; set; }
        public float FogEnd { get; set; }
        public Vector4 Color { get; set; }
        public bool Enabled { get; set; }
    }
}