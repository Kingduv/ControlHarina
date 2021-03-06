﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;

public class ModificarUsuario : MonoBehaviour
{
    public GameObject success;
    public GameObject dady;
    int puesto;
    public CustomDropdown drop;
    
    public TMP_InputField usuario;
    public TMP_InputField nombre;
    public TMP_InputField password;
    public TMP_InputField id_user;
    public void ChangePuesto(int i)
    {
        puesto = i;
    }

    public void BuscarUsuario()
    {
        StartCoroutine(Buscar());
    }
    public void ModificarEmpleado()
    {
        StartCoroutine(Modificar());
    }
    IEnumerator Buscar()
    {
        int id;
        int.TryParse(id_user.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único

        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultausuarioc.php", form);

        yield return www;
        Debug.Log(www.text);

        string json = "";
        if (www.text[0] == '1')
        {
            json = www.text.Substring(1, www.text.Length - 1);
        }

        List<Empleado> e = JsonConvert.DeserializeObject<List<Empleado>>(json);
        Debug.Log(e[0].nombre);

        id_user.text = e[0].id_empleado.ToString();
        usuario.text=e[0].usuario.ToString();
        password.text = e[0].password.ToString();
        nombre.text = e[0].nombre.ToString();
        
        int p;
        int.TryParse(e[0].id_puesto, out p);

        ChangePuesto(p);
        drop.selectedItemIndex = p;
        drop.SetupDropdown();



    }
    IEnumerator Modificar()
    {
        int id;
        int.TryParse(id_user.text.ToString(), out id);
        //Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único
        form.AddField("id_puesto", puesto); //Número de 1 a 3
        form.AddField("nombre", nombre.text.Replace("\u200b", "")); //Viene del text input
        form.AddField("usuario", usuario.text.Replace("\u200b", ""));
        form.AddField("password", password.text.Replace("\u200b", "")); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/modificarusuario.php", form);

        yield return www;
        
        if (www.text[0] == '0')
        {
            ShowUsers s = this.gameObject.GetComponent<ShowUsers>();
            s.Empleados();
            InstantiateSuccess("Operación exitosa", "Usuario modificado exitosamente");

        }
        else
        {
            InstantiateSuccess("Operación fallida", "Hubo un error");

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
