using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Michsky.UI.ModernUIPack;
using System;

public class CRUDEquipos : MonoBehaviour
{
    public GameObject equipoGO;
    public GameObject success;
    public GameObject dady;
    public GameObject equipodady;
    public int tipo=1;
    public TMP_InputField id_delete;
    public TMP_InputField i;
    public TMP_InputField m;
    public TMP_InputField mo;
    public TMP_InputField s;
    public TMP_InputField p;
    public TMP_InputField date;
    public TMP_InputField ga;
    public TMP_InputField dl;
    public TMP_InputField dc;
    public TMP_InputField ub;
    public TMP_InputField ma;
    public TMP_InputField res;

    //**********LISTO*********
    // INSERTAR / VER / MODIFICAR / ELIMINAR
    //**********FALTA*********
    //  



    public void CambiarTipo()
    {
        tipo = tipo == 1 ? 2 : 1;
    }
    public void MostrarEquipo()
    {
        StartCoroutine(MostrarEquipos());
    }
    public void InsertarEquipos()
    {
        StartCoroutine(InsertarEquipo());
    }
    public void ModificarEquipos()
    {
        StartCoroutine(UpdateEquipo());

    }
    public void EliminarEquipo()
    {
        StartCoroutine(Eliminar());

    }
    IEnumerator Eliminar()
    {
        int id;
        int.TryParse(id_delete.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_equipo", id); //No repetir porque debe ser único

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/deleteequipo.php", form);

        yield return www;
        Debug.Log(www.text);
        
        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Equipo eliminado exitosamente");

        }
        else
        {
            InstantiateSuccess("Operación fallida", "Error al eliminar equipo");
        }

    }
    IEnumerator UpdateEquipo()
    {
        int id;
        int.TryParse(i.text.ToString(), out id);
        
        WWWForm form = new WWWForm();
        form.AddField("id_equipo", id); //No repetir porque debe ser único
        form.AddField("marca", m.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("modelo", mo.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("serie", s.text.Replace("\u200b", ""));
        form.AddField("proveedor_equipo", p.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("fecha_adquisicion", date.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("garantia", ga.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_larga", dl.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_corta", dc.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("ubicacion", ub.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("mantenimiento", ma.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("id_tipo",tipo ); //Viene del text input
        form.AddField("responsable", res.text.Replace("\u200b", "")); //Viene del text inputform.AddField("id_equipo", id); //No repetir porque debe ser único
        //form.AddField("id_equipo", 1552); //No repetir porque debe ser único
        //form.AddField("marca", "AAAA"); //Número de 1 a 3
        //form.AddField("modelo","UUUUU"); //Viene del text input
        //form.AddField("serie", "UUUUU");
        //form.AddField("proveedor_equipo", "UUUUU"); //Viene del text input
        //form.AddField("fecha_adquisicion", "10/10/1998"); //Viene del text input
        //form.AddField("garantia", "UUUUU"); //Viene del text input
        //form.AddField("descripcion_larga", "UUUUU"); //Viene del text input
        //form.AddField("descripcion_corta", "UUUUU"); //Viene del text input
        //form.AddField("ubicacion", "UUUUU"); //Viene del text input
        //form.AddField("mantenimiento", "UUUUU"); //Viene del text input
        //form.AddField("id_tipo", 2); //Viene del text input
        //form.AddField("responsable", 1); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00298634/Desarollo/modificaequipo.php", form);

        yield return www;

        Debug.Log(www.text);
        if (www.text[0] == '0')
        {
            Debug.Log("Equipo Modificado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Equipo modificado exitosamente");
            MostrarEquipo();
        }
        else
        {
            InstantiateSuccess("Error"+www.text, "No se modificó el equipo");
            Debug.Log(" Error #" + www.text);

        }

    }
    IEnumerator MostrarEquipos()
    {
        //ELIMAR EQUIPOS
        GameObject[] eList = GameObject.FindGameObjectsWithTag("equipo");
        foreach (GameObject g in eList)
        {
            Destroy(g.gameObject);
        }
        yield return new WaitForSeconds(.2f);

        //PHP EQUIPOS
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultaequipo.php"); //GET data is sent via the URL

        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);

            List<Equipo> equipo = JsonConvert.DeserializeObject<List<Equipo>>(www.text);

            foreach (Equipo e in equipo)
            {
                GameObject ego = Instantiate(equipoGO, equipodady.transform);
               //Debug.Log(e.id_tipo);
                string tipo = e.id_tipo == "1" ? "Farinografo" : "Alveografo";
                //id , marca, serie, proveedor, fecha adquicisión garantía, descripción larga, descripción corta, ubicación, mantenimiento, tipoo de equipo, responsable 
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = e.id_equipo.ToString();
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = e.marca;
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = e.modelo;
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = e.serie;
                ego.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = e.proveedor_equipo;
                ego.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = e.fecha_adquisicion;
                ego.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = e.garantia;
                ego.transform.GetChild(7).GetComponentInChildren<TextMeshProUGUI>().text = e.descripcion_larga;
                ego.transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().text = e.descripcion_corta;
                ego.transform.GetChild(9).GetComponentInChildren<TextMeshProUGUI>().text = e.ubicacion;
                ego.transform.GetChild(10).GetComponentInChildren<TextMeshProUGUI>().text = e.mantenimiento;
                ego.transform.GetChild(11).GetComponentInChildren<TextMeshProUGUI>().text = tipo;
                ego.transform.GetChild(12).GetComponentInChildren<TextMeshProUGUI>().text = e.responsable;

            }
        }
        else Debug.LogWarning(www.error);
        yield return new WaitForSeconds(.3f);
    }
    IEnumerator InsertarEquipo()
    {
        int id;
        int.TryParse(i.text.ToString(), out id);
        int idRes;
        int.TryParse(res.text.ToString(), out idRes);
        

//        Debug.Log(idRes + "," + id + "," + idTip + ",");
        WWWForm form = new WWWForm();
        Debug.Log(DateTime.Today.ToString());
        form.AddField("id_equipo", id); //No repetir porque debe ser único
        form.AddField("marca", m.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("modelo", mo.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("serie", s.text.Replace("\u200b", ""));
        form.AddField("proveedor_equipo", p.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("fecha_adquisicion", date.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("garantia", ga.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_larga", dl.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_corta", dc.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("ubicacion", ub.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("mantenimiento", ma.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("id_tipo", tipo); //Viene del text input
        form.AddField("responsable", idRes); //Viene del text input

        //form.AddField("id_equipo", 152); //No repetir porque debe ser único
        //form.AddField("marca", "ASF"); //Número de 1 a 3
        //form.AddField("modelo", "1234"); //Viene del text input
        //form.AddField("serie", "1234");
        //form.AddField("proveedor_equipo", "1234"); //Viene del text input
        //form.AddField("fecha_adquisicion", "10/10/1998"); //Viene del text input
        //form.AddField("garantia", "1234"); //Viene del text input
        //form.AddField("descripcion_larga", "1234"); //Viene del text input
        //form.AddField("descripcion_corta", "1234"); //Viene del text input
        //form.AddField("ubicacion", "1234"); //Viene del text input
        //form.AddField("mantenimiento", "1234"); //Viene del text input
        //form.AddField("id_tipo", 2); //Viene del text input
        //form.AddField("responsable", 1); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertarequipo.php", form);

        yield return www;


        if (www.text[0] == '0')
        {
            Debug.Log("Equipo Creado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Equipo creado exitosamente");
            MostrarEquipo();
        }
        else if (www.text[0] == '1')
        {
            InstantiateSuccess("Error: " + www.text, "No se creo el equipo, el id del responsable no existe");
            Debug.Log("No se creo equipo. Error #" + www.text);

        }
        else if(www.text[0] == '2')
        {
            InstantiateSuccess("Error: " + www.text, "No se creo el equipo, el id del equipo ya existe");
            Debug.Log("No se creo equipo. Error #" + www.text);

        }

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
