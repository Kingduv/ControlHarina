using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class DeleteUser : MonoBehaviour
{
    public TMP_InputField id_user;
    public GameObject success;
    public GameObject dady;
    public void EliminarUsuario()
    {
        StartCoroutine(Eliminar());
    }
    IEnumerator Eliminar()
    {
        int id;
        int.TryParse(id_user.text.ToString(), out id);
        Debug.Log(id);
        WWWForm form = new WWWForm();
        form.AddField("id_empleado", id); //No repetir porque debe ser único
       
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/deleteusuario.php", form);

        yield return www;

        ShowUsers s = this.gameObject.GetComponent<ShowUsers>();
        s.Empleados();
        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Usuario eliminado exitosamente");

        }
        else
        {
            InstantiateSuccess("Operación fallida", "Error al eliminar usuario");
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
