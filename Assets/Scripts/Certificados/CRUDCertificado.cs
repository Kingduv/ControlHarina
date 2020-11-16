using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CRUDCertificado : MonoBehaviour
{
    [Header("ExitoFracaso")]
    public GameObject success;
    public GameObject dady;

    [Header("Inputfields")]
    public TMP_InputField lote;
    public TMP_InputField id_pedido;
    public TMP_InputField id_cliente;
    public TMP_InputField num_analisis;


    public void InsertarCertificados()
    {
        StartCoroutine(Insertar());
    }
    public void ConsultarCertificado()
    {
        StartCoroutine(Consultar());
    }
    IEnumerator Consultar()
    {
        int id_p, id_c, n_a;
        int.TryParse(id_pedido.text.ToString(), out id_p);
        int.TryParse(id_cliente.text.ToString(), out id_c);
        int.TryParse(num_analisis.text.ToString(), out n_a);

        WWWForm form = new WWWForm();

        //form.AddField("lote", lote.text.ToString());
        //form.AddField("id_pedido", id_p);
        //form.AddField("id_cliente", id_c);
        //form.AddField("numero_analisis", n_a);

        form.AddField("lote", "nm");
        form.AddField("id_pedido", 10);
        form.AddField("id_cliente", 101);
        form.AddField("numero_analisis", 1);


        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultacertficado.php", form);
        yield return www;
        Debug.Log(www.text);

        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Pedido creado exitosamente");

        }
        else
        {
            InstantiateSuccess("Error: " + www.text, "No se creó el certificado.");

        }
    }
    IEnumerator Insertar()
    {
        int id_p, id_c, n_a;
        int.TryParse(id_pedido.text.ToString(), out id_p);
        int.TryParse(id_cliente.text.ToString(), out id_c);
        int.TryParse(num_analisis.text.ToString(), out n_a);

        WWWForm form = new WWWForm();

        form.AddField("lote", lote.text.ToString());
        form.AddField("id_pedido", id_p);
        form.AddField("id_cliente", id_c);
        form.AddField("numero_analisis", n_a);

        //form.AddField("lote", "nm");
        //form.AddField("id_pedido",10);
        //form.AddField("id_cliente",101);
        //form.AddField("numero_analisis",1);


        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/insertarcertificado.php", form);
        yield return www;
        Debug.Log(www.text);

        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Pedido creado exitosamente");

        }
        else
        {
            InstantiateSuccess("Error: " + www.text, "No se creó el certificado.");

        }
    }
    public void InstantiateSuccess(string title, string message)
    {

        GameObject exito = Instantiate(success.gameObject);
        exito.GetComponent<ModalWindowManager>().titleText = title;
        exito.GetComponent<ModalWindowManager>().descriptionText = message;
        exito.transform.SetParent(GameObject.Find("Canvas").transform, false);
        exito.transform.SetParent(dady.transform);
        exito.transform.localScale = new Vector3(0.5f, 0.5f, 1);

    }
}