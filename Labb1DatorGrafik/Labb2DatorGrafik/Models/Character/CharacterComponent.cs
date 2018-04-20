using Labb1DatorGrafik.Component;
using ModelDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2DatorGrafik.Models.Character
{
    public class CharacterComponent : IComponent
    {
        IGameObject Character { get; set; } 
    }
}
