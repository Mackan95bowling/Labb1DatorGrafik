﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik
{
    public interface IGameObject
    {
        void Draw(BasicEffect BasicEffect);
        void Update(GameTime gameTime);
    }
}
