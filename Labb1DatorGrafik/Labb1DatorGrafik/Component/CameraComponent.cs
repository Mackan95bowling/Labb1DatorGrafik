﻿using Microsoft.Xna.Framework;
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
		

    }
}
