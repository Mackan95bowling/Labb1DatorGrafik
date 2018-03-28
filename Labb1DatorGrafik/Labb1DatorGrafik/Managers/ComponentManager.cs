using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.Component;

namespace Labb1DatorGrafik.Entity
{
    class ComponentManager
    {
        private Dictionary<Type, Dictionary<uint, IComponent>> _components = new Dictionary<Type, Dictionary<uint, IComponent>>();
    }
}
