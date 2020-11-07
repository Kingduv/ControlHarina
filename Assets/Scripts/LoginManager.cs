using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Libreria para usar elementos de la UI con texto de calidad


public class LoginManager : MonoBehaviour
{
    public TextMeshProUGUI usuario;
    public TextMeshProUGUI password;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }





IEnumerator Register() {
        Debug.LogError("hola");
 WWWForm form = new WWWForm();

        form.AddField("nombre", usuario.text);

        form.AddField("edad", password.text);
        



        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertar.php", form);

        Debug.LogError("hola2   ");

        yield return www;


    }



    public void Update() //sIRVE PARA LLAMAR A ESTA CLASE CADA MILISEGUNDO
    {
        //Debug.Log("hOLA");
    }

    

}
