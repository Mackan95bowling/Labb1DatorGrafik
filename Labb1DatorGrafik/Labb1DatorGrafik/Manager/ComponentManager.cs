using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb1DatorGrafik.Component;

namespace Labb1DatorGrafik.Manager
{
    public class ComponentManager
    {

        private static ComponentManager cm;
        private Dictionary<Type, Dictionary<uint, IComponent>> Components = new Dictionary<Type, Dictionary<uint, IComponent>>();
        private uint nextEntityId = 1;

        private ComponentManager()
        {

        }

        public static ComponentManager Get()
        {
            if (cm == null)
            {
                cm = new ComponentManager();
                return cm;
            }
            else
                return cm;
        }


        public void GetSpecificComponents(Type componentType, out Dictionary<uint, IComponent> specificComponents)
        {
            Components.TryGetValue(componentType, out specificComponents);
        }

        public T EntityComponent<T>(uint entityKey)
        {
            AddComponentKeyIfNotPresent(typeof(T));
            var specificComponents = Components[typeof(T)];
            if (specificComponents.ContainsKey(entityKey))
            {
                var entityComponent = specificComponents[entityKey];
                return (T)entityComponent;
            }
            return default(T);
        }

        public void TryAddEntityComponent(uint entityKey, IComponent component)
        {
            AddComponentKeyIfNotPresent(component.GetType());
            var specificComponents = Components[component.GetType()];
            specificComponents.Add(entityKey, component);
        }

        private void AddComponentKeyIfNotPresent(Type component)
        {
            if (!Components.ContainsKey(component))
            {
                Components[component] = new Dictionary<uint, IComponent>(40);
            }
        }

        public bool EntityHasComponent<T>(uint entityKey)
        {
            AddComponentKeyIfNotPresent(typeof(T));
            var specificComponents = Components[typeof(T)];
            if (specificComponents.ContainsKey(entityKey))
            {
                return true;
            }
            return false;
        }

        public uint GetNextId()
        {
            return nextEntityId++;
        }


    }
}
