using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModificarUsuario : MonoBehaviour
{
    int puesto;
    public TextMeshProUGUI usuario;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI password;
    public TMP_InputField id_user;
    public void ChangePuesto(int i)
    {
        puesto = i;
        Debug.Log("Puesto Modificado");
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


        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);

        }

    }

}
