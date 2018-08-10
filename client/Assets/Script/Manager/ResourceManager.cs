using System;
using UnityEngine;
public class ResourceManager : ManagerBase<ResourceManager>
{
    public void LoadResource(string path, Action<GameObject> onLoaded, Action<string> onLoadFailed = null)
    {
        var go = Resources.Load<GameObject>(path);

        if (go == null)
        {
            Debug.Log(path + "のロード失敗した");

            if (onLoadFailed != null)
            {
                onLoadFailed(path);
            }
        }
        else if (onLoaded != null)
        {
            onLoaded(go);
        }
    }
}
