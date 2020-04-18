using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxDestroy : MonoBehaviour
{
    public float second = 1f;
    void Awake()
    {
        Invoke("Destroy", second);
    }
    private void Destroy()
    {
        ObjectPool.GetInstance().RecycleObj(gameObject);
    }
}
