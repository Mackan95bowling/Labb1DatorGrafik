using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.Component
{
    public class TransformComponent : IComponent
    {
        //// Position
        //public float positionX { get; set; }
        //public float positionY { get; set; }
        //public float positionZ { get; set; }
        ////Scaling
        //public float scalingX { get; set; }
        //public float scalingY { get; set; }
        //public float scalingZ { get; set; }
        ////Rotation 
        //public float rotationX { get; set; }
        //public float rotationY { get; set; }
        //public float rotationZ { get; set; }

        public Vector3 position { get; set; }
        public Vector3 scaling { get; set; }
        public Vector3 rotation { get; set; }
    }
}
