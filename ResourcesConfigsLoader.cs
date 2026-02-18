using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using UnityEngine;

public class ResourcesConfigsLoader : IConfigsLoader
{
    private readonly ResourcesAssetsLoader _resources;

    private readonly Dictionary<Type, string> _configsResorcesPaths = new()
    {
        { typeof(TestConfig), "TestConfig" }
    };

    public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
    {
        _resources = resources;
    }

    public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
    {
        Dictionary<Type, object> loadedConfigs = new();

        foreach (KeyValuePair<Type, string> configsResorcesPath in _configsResorcesPaths)
        {
            ScriptableObject config = _resources.Load<ScriptableObject>(configsResorcesPath.Value);
            loadedConfigs.Add(configsResorcesPath.Key, config);
            yield return null;
        }

        onConfigsLoaded?.Invoke(loadedConfigs);
    }
}
