using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Resource;

namespace Game.Resource
{
    public enum WindowIndex
    {
        TestWindow,
    }
}
public class WindowManager : SingleTonBase<WindowManager>
{
    Dictionary<WindowIndex, string> windowPathDict = new Dictionary<WindowIndex, string>
    {
        {WindowIndex.TestWindow, "Assets/Prefab/UI/Windows/TestWindow/TestWindow.prefab"},
    };

    List<WindowBase> _windowList;
    List<WindowBase> windowList
    {
        get
        {
            if (_windowList == null)
            {
                var windowCount = Enum.GetNames(typeof(WindowIndex)).Length;
                Debug.Log("WindowIndex の 初期からを行うと　結果" + windowCount + "個のWindowが設定されています");

                for (int i = 0; i < windowCount; i++)
                {
                    _windowList.Add(null);
                }
            }

            return _windowList;
        }
    }
    public void CreateAndOpen(WindowIndex index, bool openAsTop = true, Action<WindowBase> onLoaded = null)
    {
        var targetWindow = windowList[(int)index];

        if (targetWindow == null)
        {
            targetWindow = CreateWindow(index);
        }
        else
        {
            if (openAsTop)
            {
                windowList[(int)index].OpenAsTop();
            }
            else
            {
                windowList[(int)index].Open();
            }
        }

        if (IsWindowOpened(index) && onLoaded != null)
        {
            onLoaded(targetWindow);
        }
    }
    public bool IsWindowOpened(WindowIndex index)
    {
        var targetWindow = windowList[(int)index];

        if (targetWindow == null)
        {
            return false;
        }
        else
        {
            return targetWindow.gameObject.activeSelf;
        }
    }
    WindowBase CreateWindow(WindowIndex index)
    {
        ResourceManager.Get().LoadResource(windowPathDict[index], (o) =>
        {
            var wind = o.GetComponent<WindowBase>();

            if (wind == null)
            {
                Debug.Log("ロードしたオブジェクトにwindowが無かった");
            }
        }, (path) =>
        {

        });
    }
}
