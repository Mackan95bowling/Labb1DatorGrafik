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
        public Effect ModelEffect { get; set; }
        public Model model { get; set; }
        public Texture2D texture { get; set; }
        public float rotate { get; set; }
        public bool ShadowMapRender { get; set; }
        public ModelComponent(Texture2D texturemodel, Model model, Vector3 position) {
            this.texture = texturemodel;
            this.model = model;
            this.modelPosition = position;

        }
    }
}

