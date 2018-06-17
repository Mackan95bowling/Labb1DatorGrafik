using Microsoft.Xna.Framework;

namespace Labb3DatorGrafik.Component
{
    public class LightComponent : IComponent
    {
        public Vector3 LightDir { get; set; }
        public Matrix LightProjection { get; set; }

        public Vector3 DiffLightDir { get; set; }
        public Vector4 DiffLightColor { get; set; }
        public float DiffIntensity { get; set; }

    }
}