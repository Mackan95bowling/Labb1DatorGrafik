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
        public Vector3 position;
        public Vector3 scaling;
        public Vector3 rotation;
        Matrix rotationMatrix { get; set; }
        public TransformComponent() {
            position = new Vector3(0, 0, 0);
            scaling = new Vector3(0, 0, 0);
            rotation = new Vector3(0, 0, 0);
            rotationMatrix = Matrix.Identity;

        }
    }
}
