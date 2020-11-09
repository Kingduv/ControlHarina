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

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();        
        form.AddField("usuario",@usuario.text.ToString() );
        form.AddField("password", @password.text); //Viene del text input
        
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/login.php", form);

        Debug.Log(@usuario.text.ToString());
        yield return www;

        Debug.Log(www.text);
        if (www.text[0] == '1')
        {

            SceneManager.LoadScene(2);            
            
        }
        if (www.text[0] == '2')
        {

            InstantiateSuccess("Bienvenido", "Es un gusto tenerte de vuelta");

        }
        if (www.text[0] == '3')
        {

            InstantiateSuccess("Bienvenido", "Es un gusto tenerte de vuelta");

        }
        else if(www.text[0] == '1' && www.text[1]=='1')
        {
            InstantiateSuccess("Error Code: 11", "Usuario o Contraseña incorrectos");
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