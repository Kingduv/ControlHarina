using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Michsky.UI.ModernUIPack;

public class CRUDEquipos : MonoBehaviour
{
    public GameObject equipoGO;
    public GameObject success;
    public GameObject dady;

    public TMP_InputField i;
    public TMP_InputField m;
    public TMP_InputField mo;
    public TMP_InputField s;
    public TMP_InputField p;
    public TMP_InputField fa;
    public TMP_InputField ga;
    public TMP_InputField dl;
    public TMP_InputField dc;
    public TMP_InputField ub;
    public TMP_InputField ma;
    public TMP_InputField t;
    public TMP_InputField res;

    //**********LISTO*********
    // INSERTAR / VER / MODIFICAR / ELIMINAR
    //**********FALTA*********
    //  




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
        int.TryParse(i.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/deleteusuario.php", form);

        yield return www;

        ShowUsers s = this.gameObject.GetComponent<ShowUsers>();
        s.Empleados();
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
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_equipo", id); //No repetir porque debe ser único
        form.AddField("marca", m.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("modelo", mo.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("serie", s.text.Replace("\u200b", ""));
        form.AddField("proveedor_equipo", p.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("fechas_adquisicion", fa.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("garantia", ga.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_larga", dl.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_corta", dc.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("ubicacion", ub.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("mantenimiento", ma.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("id_tipo", t.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("responsable", res.text.Replace("\u200b", "")); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertarusuario.php", form);

        yield return www;


        if (www.text[0] == '0')
        {
            Debug.Log("Equipo Modificado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Equipo modificado exitosamente");
            MostrarEquipo();
        }
        else
        {
            InstantiateSuccess("Error", "No se modificó el equipo");
            Debug.Log("User login failed. Error #" + www.text);

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
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultausuario.php"); //GET data is sent via the URL

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
                GameObject ego = Instantiate(equipoGO, dady.transform);

                string tipo = e.id_tipo == "1" ? "Farinografo" : "Alveografo";
                //id , marca, serie, proveedor, fecha adquicisión garantía, descripción larga, descripción corta, ubicación, mantenimiento, tipoo de equipo, responsable 
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = e.id.ToString();
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = e.marca;
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = e.serie;
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = e.proveedor_equipo;
                ego.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = e.fechas_adquisicion;
                ego.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = e.garantia;
                ego.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = e.descripcion_larga;
                ego.transform.GetChild(7).GetComponentInChildren<TextMeshProUGUI>().text = e.descripcion_corta;
                ego.transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().text = e.ubicacion;
                ego.transform.GetChild(9).GetComponentInChildren<TextMeshProUGUI>().text = e.mantenimiento;
                ego.transform.GetChild(10).GetComponentInChildren<TextMeshProUGUI>().text = tipo;
                ego.transform.GetChild(11).GetComponentInChildren<TextMeshProUGUI>().text = e.responsable;


            }



        }
        else Debug.LogWarning(www.error);
        yield return new WaitForSeconds(.3f);
    }
    IEnumerator InsertarEquipo()
    {
        int id;
        int.TryParse(i.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_equipo", id); //No repetir porque debe ser único
        form.AddField("marca", m.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("modelo", mo.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("serie", s.text.Replace("\u200b", ""));
        form.AddField("proveedor_equipo", p.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("fechas_adquisicion", fa.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("garantia", ga.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_larga", dl.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("descripcion_corta", dc.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("ubicacion", ub.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("mantenimiento", ma.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("id_tipo", t.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("responsable", res.text.Replace("\u200b", "")); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertarusuario.php", form);

        yield return www;


        if (www.text[0] == '0')
        {
            Debug.Log("Equipo Creado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Equipo creado exitosamente");
            MostrarEquipo();
        }
        else
        {
            InstantiateSuccess("Error", "No se creo el equipo");
            Debug.Log("User login failed. Error #" + www.text);

        }

    }
    public void InstantiateSuccess(string title, string message)
    {

        GameObject exito = Instantiate(success.gameObject);
        exito.GetComponent<ModalWindowManager>().titleText = title;
        exito.GetComponent<ModalWindowManager>().descriptionText = message;
        exito.transform.SetParent(GameObject.Find("Canvas").transform, false);
        exito.transform.SetParent(dady.transform);

    }

}
