using System;
using UnityEngine;
public class WindowBase : AmulatorBehavior
{
    public void Open()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
    }
    public void OpenAsTop()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            this.gameObject.transform.SetAsLastSibling();
        }
    }
    public void MoveToTop()
    {
        if(!this.gameObject.activeSelf)
        {
            Debug.Log("Window が 非アクティブ担っている、上に持ってくる意味ないよ");
            return;
        }

        this.gameObject.transform.SetAsLastSibling();
    }
    public void Close()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}