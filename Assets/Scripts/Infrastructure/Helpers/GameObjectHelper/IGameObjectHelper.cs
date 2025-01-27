using UnityEngine;
namespace Infrastructure.Helpers.GameObjectHelper
{
    public interface IGameObjectHelper
    {
        GameObject InstantiatePrefab(UnityEngine.Object prefab);
        
        T InstantiatePrefabInScene<T>(Object prefab, Transform parent, bool withInjection, bool inWorldSpace = false) 
            where T : Object;
        
        TComponentOnPrefab InstantiateObjectWithComponentInScene<TComponentOnPrefab>(Object prefab, Transform parent = null, bool withInjection = true, bool inWorldSpace = false) 
            where TComponentOnPrefab : Component;
    }
}