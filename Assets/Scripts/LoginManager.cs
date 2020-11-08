using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Libreria para usar elementos de la UI con texto de calidad
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usuario;
    public TMP_InputField password;

    public GameObject success;
    public GameObject dady;

    public void CallRegister()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        
        WWWForm form = new WWWForm();        
        form.AddField("usuario", usuario.text.Trim());
        form.AddField("password", password.text.Trim()); //Viene del text input
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/login.php", form);

        yield return www;

        Debug.Log(www.text);
        if (www.text[0] == '0')
        {
            Debug.Log("User Created susccesfully.");
            InstantiateSuccess("Bienvenido", "Usuario creado exitosamente");
            
        }
        else
        {
            InstantiateSuccess("Error", "No se creo el usuario");
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