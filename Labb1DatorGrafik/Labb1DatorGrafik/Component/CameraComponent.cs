using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.Component
{
    public class CameraComponent : IComponent
    {
        public float fieldOfView;
        //ska de va float?
        public float aspectRatio;
        public Vector3 cameraPosition;
        public Vector3 cameraTarget;
        public Matrix World { get; set; }
        public Matrix view { get; set; }
        public Matrix projection { get; set; }
        public Matrix ClippingProjection { get; set; }
        public bool FollowPlayer { get; set; }

        public BoundingFrustum BoundingFrustum { get; set; }
    }
}
