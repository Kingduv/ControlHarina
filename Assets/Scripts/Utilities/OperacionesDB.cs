using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperacionesDB : MonoBehaviour
{
    public static GameObject Exito;
    public static GameObject Error;
    
    
    public static void InstantiateSuccess(string title, string message)
    {
        GameObject dady= GameObject.FindGameObjectWithTag("dady");
        GameObject exito = Instantiate(GameObject.FindGameObjectWithTag("success"));
        exito.transform.SetParent(GameObject.Find("Canvas").transform, false);
        exito.transform.SetParent(dady.transform);
        Debug.Log(exito.transform.GetChild(1).GetChild(2).name);
        Debug.Log(exito.transform.GetChild(1).GetChild(3).name);
        exito.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = title;
        exito.transform.GetChild(1).GetChild(3). GetComponent<TextMeshProUGUI>().text = message;
    }
}
