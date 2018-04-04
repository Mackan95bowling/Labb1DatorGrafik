using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb1DatorGrafik.Component
{
    public class ModelComponent : IComponent
    {
        Vector3 modelPosition;
        Model model;
        ModelMesh[] Meshes;//ModelMesh Meshes or ModelMesh[] Meshes;
    }
}
