using System.Collections;
using System.Collections.Generic;
using TMPro;
using Newtonsoft.Json;
using UnityEngine;
using Michsky.UI.ModernUIPack;


public class AnalisisCRUD : MonoBehaviour
{
    public GameObject analisisGO;
    public GameObject success;
    public GameObject dady;
    public GameObject analisisdady;

    public TMP_InputField buscarAnalisis;
    public TMP_InputField numeroAnalisis;
    //public TMP_InputField id_analisis;
    public TMP_InputField lote;
    public TMP_InputField absorcion_agua;
    public TMP_InputField desarrollo_masa;
    public TMP_InputField estabilidad;
    public TMP_InputField grado_reblandecimineto;
    public TMP_InputField fqn;
    public TMP_InputField tenacidad;
    public TMP_InputField extensibilidad;
    public TMP_InputField fuerza_harina;
    public TMP_InputField configuracion;
    public TMP_InputField indice_elasticidad;

    public void Insertar()
    {
        StartCoroutine(InsertarAnalisis());
    }
    public void Modificar()
    {
        StartCoroutine(ModificarAnalisis());
    }
    public void BuscarAnalisis()
    {
        StartCoroutine(Buscar());
    }
    IEnumerator Buscar()
    {
        int id;
        int.TryParse(numeroAnalisis.text.ToString(), out id);
        
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("numero_analisis", id); //No repetir porque debe ser único
        form.AddField("lote", lote.text.Replace("\u200b", "")); //No repetir porque debe ser único

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultaanalisisc.php", form);

        yield return www;
        Debug.Log(www.text);

        string json = "";
        if (www.text[0] == '1')
        {
            json = www.text.Substring(1, www.text.Length - 1);
        }

        List<Analisis> e = JsonConvert.DeserializeObject<List<Analisis>>(www.text);


        tenacidad.text = e[0].tenacidad.ToString();
        extensibilidad.text = e[0].extensibilidad.ToString();
        absorcion_agua.text = e[0].absorcion_agua.ToString();
        fqn.text = e[0].fqn.ToString();
        grado_reblandecimineto.text = e[0].grado_reblandecimineto.ToString();
        fuerza_harina.text = e[0].fuerza_harina.ToString();
        configuracion.text = e[0].configuracion.ToString();
        indice_elasticidad.text = e[0].indice_elasticidad.ToString();
        desarrollo_masa.text = e[0].desarrollo_masa.ToString();
        estabilidad.text = e[0].estabilidad.ToString();
        /*
        form.AddField("absorcion_agua", datos[0]);
        form.AddField("desarrollo_masa", datos[1]);
        form.AddField("estabilidad", datos[2]);
        form.AddField("grado_reblandecimineto", datos[3]);
        form.AddField("fqn", datos[4]);
        form.AddField("tenacidad", datos[5]);
        form.AddField("extensibilidad", datos[6]);
        form.AddField("fuerza_harina", datos[7]);
        form.AddField("configuracion", datos[8]);
        form.AddField("indice_elasticidad", datos[9]);

         **/

    }
    IEnumerator ModificarAnalisis() //Que datos recibe?
    {

        int[] datos = new int[10];
        int.TryParse(absorcion_agua.text.ToString(), out datos[0]);
        int.TryParse(desarrollo_masa.text.ToString(), out datos[1]);
        int.TryParse(estabilidad.text.ToString(), out datos[2]);
        int.TryParse(grado_reblandecimineto.text.ToString(), out datos[3]);
        int.TryParse(fqn.text.ToString(), out datos[4]);
        int.TryParse(tenacidad.text.ToString(), out datos[5]);
        int.TryParse(extensibilidad.text.ToString(), out datos[6]);
        int.TryParse(fuerza_harina.text.ToString(), out datos[7]);
        int.TryParse(configuracion.text.ToString(), out datos[8]);
        int.TryParse(indice_elasticidad.text.ToString(), out datos[9]);
        int id, numeroA;
        //int.TryParse(id_analisis.text.ToString(), out id);
        int.TryParse(numeroAnalisis.text.ToString(), out numeroA);

        WWWForm form = new WWWForm();

       // form.AddField("id_analisis", id);
        form.AddField("lote", lote.text.Replace("\u200b", ""));
        form.AddField("numero_analisis", numeroA);
        form.AddField("absorcion_agua", datos[0]);
        form.AddField("desarrollo_masa", datos[1]);
        form.AddField("estabilidad", datos[2]);
        form.AddField("grado_reblandecimineto", datos[3]);
        form.AddField("fqn", datos[4]);
        form.AddField("tenacidad", datos[5]);
        form.AddField("extensibilidad", datos[6]);
        form.AddField("fuerza_harina", datos[7]);
        form.AddField("configuracion", datos[8]);
        form.AddField("indice_elasticidad", datos[9]);

        //form.AddField("id_analisis", 27);
        //form.AddField("lote", "nm");
        //form.AddField("numero_analisis", 1);
        //form.AddField("absorcion_agua", 5);
        //form.AddField("desarrollo_masa", 5);
        //form.AddField("estabilidad", 5);
        //form.AddField("grado_reblandecimineto", 5);
        //form.AddField("fqn", 5);
        //form.AddField("tenacidad", 5);
        //form.AddField("extensibilidad", 5);
        //form.AddField("fuerza_harina", 5);
        //form.AddField("configuracion", 5);
        //form.AddField("indice_elasticidad", 5);

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/modificaranalisis.php", form);

        yield return www;
        Debug.Log(www.text);
        if (www.text[0] == '0')
        {
            Debug.Log("Análisis Creado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Análisis modificado exitosamente");

        }
        else
        {
            InstantiateSuccess("Error: #" + www.text, "No se modificó el análsis");

        }
    }
    IEnumerator InsertarAnalisis()
    {
        int[] datos= new int[10];
        int.TryParse(absorcion_agua.text.ToString(), out datos[0]);
        int.TryParse(desarrollo_masa.text.ToString(), out datos[1]);
        int.TryParse(estabilidad.text.ToString(), out datos[2]);
        int.TryParse(grado_reblandecimineto.text.ToString(), out datos[3]);
        int.TryParse(fqn.text.ToString(), out datos[4]);
        int.TryParse(tenacidad.text.ToString(), out datos[5]);
        int.TryParse(extensibilidad.text.ToString(), out datos[6]);
        int.TryParse(fuerza_harina.text.ToString(), out datos[7]);
        int.TryParse(configuracion.text.ToString(), out datos[8]);
        int.TryParse(indice_elasticidad.text.ToString(), out datos[9]);
        //int i = 0;
        //foreach(int d in datos)
        //{
        //    datos[i] = i;
        //    i++;
        //}

        WWWForm form = new WWWForm();

        form.AddField("lote", lote.text.Replace("\u200b", ""));
        form.AddField("absorcion_agua",datos[0]);
        form.AddField("desarrollo_masa", datos[1]);
        form.AddField("estabilidad", datos[2]);
        form.AddField("grado_reblandecimineto", datos[3]);
        form.AddField("fqn", datos[4]);
        form.AddField("tenacidad", datos[5]);
        form.AddField("extensibilidad", datos[6]);
        form.AddField("fuerza_harina", datos[7]);
        form.AddField("configuracion", datos[8]);
        form.AddField("indice_elasticidad", datos[9]);

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertaranalisis.php", form);

        yield return www;
        Debug.Log(www.text);

        if (www.text[0] == '0')
        {
            Debug.Log("Análisis Creado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Análisis creado exitosamente");
            
        }
        else 
        {
            InstantiateSuccess("Error: " + www.text, "no se insertó el análsis");

        }
    }
    public void Consultar()
    {
        StartCoroutine(ConsultarAnalisis());
    }
    IEnumerator ConsultarAnalisis()
    {

        GameObject[] eList = GameObject.FindGameObjectsWithTag("analisis");
        foreach (GameObject g in eList)
        {
            Destroy(g.gameObject);
        }
        
        WWWForm form = new WWWForm();

        form.AddField("lote", buscarAnalisis.text.Replace("\u200b", ""));
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultaanalisis.php", form);

        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }
        Debug.Log(www.text);

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);

            List<Analisis> analisis = JsonConvert.DeserializeObject<List<Analisis>>(www.text);

           
            foreach (Analisis c in analisis)
            {
                int i = 0;
                GameObject ego = Instantiate(analisisGO, analisisdady.transform);
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.id_analisis.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.lote; i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.numero_analisis.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.absorcion_agua.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.desarrollo_masa.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.estabilidad.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.grado_reblandecimineto.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.fqn.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.tenacidad.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.extensibilidad.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.fuerza_harina.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.configuracion.ToString(); i++;
                ego.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = c.indice_elasticidad.ToString();
                /*{"id_analisis":"1","lote":"$lote",
                 * "numero_analisis":"1","absorcion_agua":"1",
                 * "desarrollo_masa":"1","estabilidad":"0.4","grado_reblandecimineto":"4",
                 * "fqn":"1.1","tenacidad":"1","extensibilidad":"1","fuerza_harina":"1",
                 * "configuracion":"1","indice_elasticidad":"1"*/
            }
        }
        Debug.Log(www.text);
        yield return www;
    }
    public void InstantiateSuccess(string title, string message)
    {

        GameObject exito = Instantiate(success.gameObject);
        exito.GetComponent<ModalWindowManager>().titleText = title;
        exito.GetComponent<ModalWindowManager>().descriptionText = message;
        exito.transform.SetParent(GameObject.Find("Canvas").transform, false);
        exito.transform.SetParent(dady.transform);
        exito.transform.localScale = new Vector3(0.5f, 0.5f, 1);

    }

}
