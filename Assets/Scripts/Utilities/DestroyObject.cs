using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    GameObject destroyMe;

    public void DestroyThis()
    {
        destroyMe = this.gameObject;
        Destroy(destroyMe.gameObject);
    }
}
