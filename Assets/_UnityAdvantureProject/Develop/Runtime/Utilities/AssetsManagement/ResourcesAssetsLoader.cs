using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Utilities.AssetsManagement
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
            => Resources.Load<T>(resourcePath);
    }
}