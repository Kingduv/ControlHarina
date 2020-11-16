using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGO : MonoBehaviour
{
    public void ActiveObject(GameObject go)
    {
        if (!go.activeSelf)
            go.SetActive(true);
        else
            go.SetActive(false);
    }
    
}
