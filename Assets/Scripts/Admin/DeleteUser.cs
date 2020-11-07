using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeleteUser : MonoBehaviour
{
    public TMP_InputField id_user;
   
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
            Debug.Log("User Created susccesfully.");

        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }
}
