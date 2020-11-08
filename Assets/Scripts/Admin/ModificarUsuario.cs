using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ModificarUsuario : MonoBehaviour
{
    public GameObject success;
    public GameObject dady;
    int puesto;
    
    public TextMeshProUGUI usuario;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI password;
    public TMP_InputField id_user;
    public void ChangePuesto(int i)
    {
        puesto = i;
    }

    public void ModificarEmpleado()
    {
        StartCoroutine(Modificar());
    }
    IEnumerator Modificar()
    {
        int id;
        int.TryParse(id_user.text.ToString(), out id);
        //Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único
        form.AddField("id_puesto", puesto); //Número de 1 a 3
        form.AddField("nombre", nombre.text); //Viene del text input
        form.AddField("usuario", usuario.text);
        form.AddField("password", password.text); //Viene del text input
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
