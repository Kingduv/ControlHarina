using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Security.Policy;
using Michsky.UI.ModernUIPack;

public class InsertUser : MonoBehaviour
{
    int puesto;
    public GameObject dady;
    public GameObject success;
    public TextMeshProUGUI usuario;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI password;
    public TMP_InputField id_user;

    
    public void ChangePuesto(int i)
    {
        puesto = i;        
    }

    public void CorrutinaInsertarUsuario()
    {
        StartCoroutine(InsertarUsuario());
    }
    IEnumerator InsertarUsuario()
    {
        int id;
        int.TryParse(id_user.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único
        form.AddField("id_puesto", 1); //Número de 1 a 3
        form.AddField("nombre", nombre.text); //Viene del text input
        form.AddField("usuario", usuario.text);
        form.AddField("password", password.text); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertarusuario.php", form);

        yield return www;

        ShowUsers s = this.gameObject.GetComponent<ShowUsers>();
        s.Empleados();
        if (www.text[0] == '0')
        {
            Debug.Log("User Created susccesfully.");
            InstantiateSuccess("Operación exitosa", "Usuario creado exitosamente");
            
        }
        else
        {
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
