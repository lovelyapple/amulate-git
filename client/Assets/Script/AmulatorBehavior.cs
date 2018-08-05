using System;
using UnityEngine;
public class AmulatorBehavior : MonoBehaviour
{

    //コルチンを事前止めたい場合、これを使う　
    void DeleteThisObject()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
}
