using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;


public class VerLotes : MonoBehaviour
{
    public GameObject lotedady;
    public GameObject loteGO;
    public void MostrarLotes()
    {
        StartCoroutine(Mostrar());
    }
    IEnumerator Mostrar()
    {
        GameObject[] eList = GameObject.FindGameObjectsWithTag("lote");
        foreach (GameObject g in eList)
        {
            Destroy(g.gameObject);
        }

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultalote.php");

        yield return www;
       while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);

            List<Lote> lote = JsonConvert.DeserializeObject<List<Lote>>(www.text);
            Debug.Log(lote.Count);
            foreach (Lote c in lote)
            {
                int i = 0;
                GameObject l = Instantiate(loteGO, lotedady.transform);
                l.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = c.lote; i++;
                l.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = c.count.ToString(); i++;

            }
        }
    }
}
