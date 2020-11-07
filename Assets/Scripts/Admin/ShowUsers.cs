using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TMPro;

public class ShowUsers : MonoBehaviour
{
    public GameObject dady;
    public GameObject empleadoGO;
    public List<GameObject> s;
    public  void Empleados()
    {
        StartCoroutine(MostrarEmpleados());
    }
    private void Awake()
    {
        StartCoroutine(MostrarEmpleados());
    }
    IEnumerator MostrarEmpleados()
    {
        foreach (GameObject g in s)
        {
           
            Destroy(g.gameObject);
        }
        s.Clear();
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultausuario.php"); //GET data is sent via the URL
        Debug.Log("Usuarios");

        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {            
            yield return null;
        }

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);           

            List<Empleado> empleados= JsonConvert.DeserializeObject<List<Empleado>>(www.text);
            
            foreach(Empleado e in empleados)
            {
                GameObject ego = Instantiate(empleadoGO, dady.transform);
                

                //id =0 , puesto = 1, nombre = 2, usuario =3
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text=e.id_empleado;
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = e.id_puesto;
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = e.nombre;
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = e.usuario;

                s.Add(ego);

            }



        }
        else Debug.LogWarning(www.error);
        yield return new WaitForSeconds(.3f);
    }
}
