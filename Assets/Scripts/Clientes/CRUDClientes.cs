using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Michsky.UI.ModernUIPack;
using System;
using System.Globalization;

public class CRUDClientes : MonoBehaviour
{
    public GameObject clienteGO;
    public GameObject success;
    public GameObject dady;
    public GameObject equipodady;

    public TMP_InputField id_delete;

    public TMP_InputField id_cliente;
    public TMP_InputField nombre;
    public TMP_InputField rfc;
    public TMP_InputField domicilio;
    public TMP_InputField contacto;
    public static int habilitado=1;
    public static int personalizado=1;
    public TMP_InputField ab_lim_sup;
    public TMP_InputField ab_lim_inf;
    public TMP_InputField dm_lim_sup;
    public TMP_InputField dm_lim_inf;
    public TMP_InputField e_lim_sup;
    public TMP_InputField e_lim_inf;
    public TMP_InputField gr_lim_sup;
    public TMP_InputField gr_lim_inf;
    public TMP_InputField fqn_lim_sup;
    public TMP_InputField fqn_lim_inf;
    public TMP_InputField t_lim_sup;
    public TMP_InputField t_lim_inf;
    public TMP_InputField ex_lim_sup;
    public TMP_InputField ex_lim_inf;
    public TMP_InputField fh_lim_sup;
    public TMP_InputField fh_lim_inf;
    public TMP_InputField cc_lim_sup;
    public TMP_InputField cc_lim_inf;
    public TMP_InputField ie_lim_sup;
    public TMP_InputField ie_lim_inf;

    //**********LISTO*********
    // INSERTAR / VER / MODIFICAR / ELIMINAR
    //**********FALTA*********
    //  


    public void Habilitado()
    {
        habilitado = habilitado == 1 ? 0 : 1;
        Debug.Log(habilitado);
    }
    public void Personalizado()
    {
        personalizado = personalizado == 1 ? 0 : 1;
        Debug.Log("Cliente personalizado " + personalizado);
    }
    public void IniciarMostrarClientes()
    {
        StartCoroutine(MostrarClientes());
    }
    public void InsertarClientes()
    {
        StartCoroutine(InsertarCliente());
    }
    public void ModificarClientes()
    {
        StartCoroutine(UpdateCliente());

    }
    public void DeshabilitarCliente()
    {
        StartCoroutine(Deshabilitar());

    }
    IEnumerator Deshabilitar()
    {
        int id;
        Debug.Log(id_delete.text);
        int.TryParse(id_delete.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_equipo", id); //No repetir porque debe ser único

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/baja_cliente.php", form);

        yield return www;


        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Cliente deshabilitado");

        }
        else
        {
            InstantiateSuccess("Operación fallida", "Error al deshabilitar cliente, "+www.text);
        }

    }
    
    IEnumerator UpdateCliente()
    {
        
        int id; int.TryParse(id_cliente.text.ToString(), out id);

        int abs, abi, dms, dmi, es, ei, grs, gri,
            fqns, fqni, ts, ti, exs, exi, fhs, fhi, ccs, cci, ies, iei;

        abs = abi = dms = dmi = es = ei = grs = gri =
        fqns = fqni = ts = ti = exs = exi = fhs = fhi = ccs = cci = ies = iei = 0;
        
        if (personalizado == 1)
        {
            int.TryParse(ab_lim_sup.text.ToString(), out abs);
            int.TryParse(ab_lim_sup.text.ToString(), out abi);
            int.TryParse(dm_lim_sup.text.ToString(), out dms);
            int.TryParse(dm_lim_sup.text.ToString(), out dmi);
            int.TryParse(e_lim_sup.text.ToString(), out es);
            int.TryParse(e_lim_sup.text.ToString(), out ei);
            int.TryParse(gr_lim_sup.text.ToString(), out grs);
            int.TryParse(gr_lim_sup.text.ToString(), out gri);
            int.TryParse(fqn_lim_sup.text.ToString(), out fqns);
            int.TryParse(fqn_lim_sup.text.ToString(), out fqni);
            int.TryParse(t_lim_sup.text.ToString(), out ts);
            int.TryParse(t_lim_sup.text.ToString(), out ti);
            int.TryParse(ex_lim_sup.text.ToString(), out exs);
            int.TryParse(ex_lim_sup.text.ToString(), out exi);
            int.TryParse(fh_lim_sup.text.ToString(), out fhs);
            int.TryParse(fh_lim_sup.text.ToString(), out fhi);
            int.TryParse(cc_lim_sup.text.ToString(), out ccs);
            int.TryParse(cc_lim_sup.text.ToString(), out cci);

        }
        WWWForm form = new WWWForm();
        //form.AddField("id_equipo", id); //No repetir porque debe ser único
        //form.AddField("nombre", nombre.text));
        //form.AddField("domicilio", domicilio.text));
        //form.AddField("rfc", rfc.text)); 
        //form.AddField("contacto", contacto.text)); 
        //form.AddField("habilitado", habilitado); // crear Toogle de cliente habilitado
        //form.AddField("personalizado",  personalizado); //toogle personalizado

        form.AddField("id_cliente", id); //No repetir porque debe ser único
        form.AddField("nombre", nombre.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("rfc", rfc.text.Replace("\u200b", ""));
        form.AddField("domicilio", domicilio.text.Replace("\u200b", ""));
        form.AddField("contacto", contacto.text.Replace("\u200b", ""));
        form.AddField("habilitado", habilitado); // crear Toogle de cliente habilitado
        form.AddField("personalizado", personalizado);

        //toogle personalizado 
        form.AddField("ab_lim_sup", abs);
        form.AddField("ab_lim_inf", abi);
        form.AddField("dm_lim_sup", dms);
        form.AddField("dm_lim_inf", dmi);
        form.AddField("e_lim_sup", es);
        form.AddField("e_lim_inf", ei);
        form.AddField("gr_lim_sup", grs);
        form.AddField("gr_lim_inf", gri);
        form.AddField("fqn_lim_sup", fqns);
        form.AddField("fqn_lim_inf", fqni);
        form.AddField("t_lim_sup", ts);
        form.AddField("t_lim_inf", ti);
        form.AddField("ex_lim_sup", exs);
        form.AddField("ex_lim_inf", exi);
        form.AddField("fh_lim_sup", fhs);
        form.AddField("fh_lim_inf", fhi);
        form.AddField("cc_lim_sup", ccs);
        form.AddField("cc_lim_inf", cci);
        form.AddField("ie_lim_sup", ies);
        form.AddField("ie_lim_inf", iei);


        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/modificar_cliente.php", form);

        yield return www;


        if (www.text=="12")
        {
            Debug.Log("Cliente Modificado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Cliente modificado exitosamente");
            IniciarMostrarClientes();
        }
        else if (www.text[0] == '1' && www.text[0] == '1')
        {
            InstantiateSuccess("Operación fallida ERROR: "+ www.text, "El cliente no existe o no esta habilitado");
            Debug.Log(" Error #" + www.text);

        }
        if (www.text[0] == '1' && www.text[0] == '3')
        {
            Debug.Log("Cliente.");
            InstantiateSuccess("Operación exitosa", "Cliente dado de baja exitosamente");
            IniciarMostrarClientes();
        }

    }
    IEnumerator MostrarClientes()
    {
        //ELIMAR EQUIPOS
        GameObject[] eList = GameObject.FindGameObjectsWithTag("cliente");
        foreach (GameObject g in eList)
        {
            Destroy(g.gameObject);
        }
        yield return new WaitForSeconds(.2f);

        //PHP EQUIPOS
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/mostrar_cliente.php"); //GET data is sent via the URL
        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }
        Debug.Log(www.text);

        if (string.IsNullOrEmpty(www.error))
        {

            List<Cliente> cliente = JsonConvert.DeserializeObject<List<Cliente>>(www.text);

            foreach (Cliente c in cliente)
            {
                GameObject ego = Instantiate(clienteGO, equipodady.transform);
                Debug.Log(c.habilitado);
                ////id , marca, serie, proveedor, fecha adquicisión garantía, descripción larga, descripción corta, ubicación, mantenimiento, tipoo de equipo, responsable 
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = c.id_cliente.ToString();
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = c.nombre;
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = c.rfc;
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = c.domicilio;
                ego.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = c.contacto.ToString();
                ego.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = c.habilitado.ToString();
                ego.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = c.personalizado.ToString();
                ego.transform.GetChild(7).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.ab_lim_inf + " L.S.:" + c.ab_lim_sup;
                ego.transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.dm_lim_inf + " L.S.:" + c.dm_lim_sup;
                ego.transform.GetChild(9).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.e_lim_inf + " L.S.:" + c.e_lim_sup;
                ego.transform.GetChild(10).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.gr_lim_inf + " L.S.:" + c.gr_lim_sup;
                ego.transform.GetChild(11).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.fqn_lim_inf + " L.S.:" + c.fqn_lim_sup;
                ego.transform.GetChild(12).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.t_lim_inf + " L.S.:" + c.t_lim_sup;
                ego.transform.GetChild(13).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.ex_lim_inf + " L.S.:" + c.ex_lim_sup;
                ego.transform.GetChild(14).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.fh_lim_inf + " L.S.:" + c.fh_lim_sup;
                ego.transform.GetChild(15).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.cc_lim_inf + " L.S.:" + c.cc_lim_sup;
                ego.transform.GetChild(16).GetComponentInChildren<TextMeshProUGUI>().text = "L.I.: " + c.ie_lim_inf + " L.S.:" + c.ie_lim_sup;


            }



        }
        else Debug.LogWarning(www.error);
        yield return new WaitForSeconds(.3f);
    }
    IEnumerator InsertarCliente()
    {
        int id; int.TryParse(id_cliente.text.ToString(), out id);

        int abs, abi, dms, dmi, es, ei, grs, gri, 
            fqns, fqni, ts, ti, exs, exi, fhs, fhi, ccs, cci, ies, iei;
       
            abs = abi = dms = dmi = es = ei = grs = gri =
            fqns = fqni = ts = ti = exs = exi = fhs = fhi = ccs = cci = ies = iei = 0;
        Debug.Log(abs);
        if (personalizado == 1)
        {
            int.TryParse(ab_lim_sup.text.ToString(), out abs);
            int.TryParse(ab_lim_sup.text.ToString(), out abi);
            int.TryParse(dm_lim_sup.text.ToString(), out dms);
            int.TryParse(dm_lim_sup.text.ToString(), out dmi);
            int.TryParse(e_lim_sup.text.ToString(), out es);
            int.TryParse(e_lim_sup.text.ToString(), out ei);
            int.TryParse(gr_lim_sup.text.ToString(), out grs);
            int.TryParse(gr_lim_sup.text.ToString(), out gri);
            int.TryParse(fqn_lim_sup.text.ToString(), out fqns);
            int.TryParse(fqn_lim_sup.text.ToString(), out fqni);
            int.TryParse(t_lim_sup.text.ToString(), out ts);
            int.TryParse(t_lim_sup.text.ToString(), out ti);
            int.TryParse(ex_lim_sup.text.ToString(), out exs);
            int.TryParse(ex_lim_sup.text.ToString(), out exi);
            int.TryParse(fh_lim_sup.text.ToString(), out fhs);
            int.TryParse(fh_lim_sup.text.ToString(), out fhi);
            int.TryParse(cc_lim_sup.text.ToString(), out ccs);
            int.TryParse(cc_lim_sup.text.ToString(), out cci);

        }


        WWWForm form = new WWWForm();

       
        form.AddField("id_cliente", id); //No repetir porque debe ser único
        form.AddField("nombre", nombre.text.Replace("\u200b", "")); //Número de 1 a 3
        form.AddField("rfc", rfc.text.Replace("\u200b", ""));
        form.AddField("domicilio", domicilio.text.Replace("\u200b", ""));        
        form.AddField("contacto", contacto.text.Replace("\u200b", ""));
        form.AddField("habilitado", habilitado); // crear Toogle de cliente habilitado
        form.AddField("personalizado", personalizado);

        //toogle personalizado 
        form.AddField("ab_lim_sup", abs);
        form.AddField("ab_lim_inf", abi);
        form.AddField("dm_lim_sup", dms);
        form.AddField("dm_lim_inf", dmi);
        form.AddField("e_lim_sup", es);
        form.AddField("e_lim_inf", ei);
        form.AddField("gr_lim_sup", grs);
        form.AddField("gr_lim_inf", gri);
        form.AddField("fqn_lim_sup", fqns);
        form.AddField("fqn_lim_inf", fqni);
        form.AddField("t_lim_sup", ts);
        form.AddField("t_lim_inf", ti);
        form.AddField("ex_lim_sup", exs);
        form.AddField("ex_lim_inf", exi);
        form.AddField("fh_lim_sup", fhs);
        form.AddField("fh_lim_inf", fhi);
        form.AddField("cc_lim_sup", ccs);
        form.AddField("cc_lim_inf", cci);
        form.AddField("ie_lim_sup", ies);
        form.AddField("ie_lim_inf", iei);

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/insertar_cliente.php", form);

        yield return www;


        if (www.text[0] == '0')
        {
            Debug.Log("Cliente Creado Exitosamente.");
            InstantiateSuccess("Operación exitosa", "Cliente creado exitosamente");
            MostrarClientes();
        }
        else if(www.text[0]=='1' && www.text[0] == '0')
        {
            InstantiateSuccess("Error: " + www.text, "No se creo el cliente, el id ya existe.");
            Debug.Log("No se creo cliente. Error #" + www.text);

        }
        else
        {
            InstantiateSuccess("Error: " + www.text, "No se creo el cliente");
            Debug.Log("No se creo cliente. Error #" + www.text);

        }


    }
    public void InstantiateSuccess(string title, string message)
    {

        GameObject exito = Instantiate(success.gameObject);
        exito.GetComponent<ModalWindowManager>().titleText = title;
        exito.GetComponent<ModalWindowManager>().descriptionText = message;
        exito.transform.SetParent(GameObject.Find("Canvas").transform, false);
        exito.transform.SetParent(dady.transform);
        exito.transform.localScale=new Vector3(0.5f, 0.5f, 1);

    }

}
