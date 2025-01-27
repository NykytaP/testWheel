using UnityEngine;
using Zenject;
namespace Infrastructure.Helpers.GameObjectHelper
{
    public class GameObjectHelper : IGameObjectHelper
    {
        private readonly DiContainer container;

        public GameObjectHelper(DiContainer container)
        {
            this.container = container;
        }

        public GameObject InstantiatePrefab(Object prefab)
        {
            return container.InstantiatePrefab(prefab);
        }

        public T InstantiatePrefabInScene<T>(Object prefab, Transform parent, bool withInjection, bool inWorldSpace) 
            where T : Object
        {
            if (withInjection)
            {
                return container.InstantiatePrefab(prefab, parent) as T;
            }
            
            return Object.Instantiate(prefab, parent, inWorldSpace) as T;
        }
        
        public TComponentOnPrefab InstantiateObjectWithComponentInScene<TComponentOnPrefab>(Object prefab, Transform parent = null, bool withInjection = false, bool inWorldSpace = false) 
            where TComponentOnPrefab : Component
        {
            var instantiatedGameObject = InstantiatePrefabInScene<GameObject>(prefab, parent, withInjection, inWorldSpace);
            
            return instantiatedGameObject.GetComponent<TComponentOnPrefab>();
        }
    }
}