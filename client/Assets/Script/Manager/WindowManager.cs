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
    public void CreateAndOpen(WindowIndex index, bool openAsTop = true, Action<WindowBase> onOpened = null)
    {
        var targetWindow = windowList[(int)index];

        if (targetWindow == null)
        {
            CreateWindow(index, (wnd) =>
            {
                OpenWindow(index, openAsTop, onOpened);
            });
        }
        else
        {
            OpenWindow(index, openAsTop, onOpened);
        }
    }
    void OpenWindow(WindowIndex index, bool openAsTop = true, Action<WindowBase> onOpened = null
    )
    {
        var wnd = windowList[(int)index];

        if (wnd == null)
        {
            Debug.LogError("Wnd is not exist index " + index.ToString());
            return;
        }

        wnd.Open(openAsTop);

        if (onOpened != null)
        {
            onOpened(wnd);
        }
    }
    void CreateWindow(WindowIndex index, Action<WindowBase> onCreated)
    {
        ResourceManager.Get().LoadResource(windowPathDict[index], (o) =>
        {
            var wind = o.GetComponent<WindowBase>();

            if (wind == null)
            {
                Debug.Log("ロードしたオブジェクトにwindowが無かった");
            }
            else
            {
                windowList[(int)index] = wind;
            }


        }, (path) =>
        {
            Debug.LogError(index.ToString() + "のロードが失敗した");
        });
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
    public void UnInitManager()
    {
        var length = windowList.Count;

        for (int i = 0; i < length; i++)
        {
            var wnd = windowList[i];

            if (wnd != null)
            {
                wnd.DeleteThisObject();
            }
        }

        windowList.Clear();
    }
}
