using Michsky.UI.ModernUIPack;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CRUDPedido : MonoBehaviour
{
    [Header("Mostrar pedido")]
    public GameObject pedidoGO;
    public GameObject success;
    public GameObject dady;
    public GameObject pedidodady;

    [Header("InsertarModificar")]
    public TMP_InputField id_pedido;
    public TMP_InputField id_cliente;
    public TMP_InputField lote;
    public TMP_InputField cant_solictada;
    public TMP_InputField cant_tot_entrega;
    public TMP_InputField fecha_envio;
    public TMP_InputField fecha_caducidad;
    public TMP_InputField num_factura;
    public TMP_InputField num_compra;
    public TMP_InputField fecha_produccion;




    public void Pedidos()
    {
        StartCoroutine(MostrarPedidos());
    }

    public void InsertarPedido()
    {
        StartCoroutine(Insertar());
    }
    IEnumerator Insertar()
    {
        int[] datos = new int[6];
        int.TryParse(id_pedido.text.ToString(), out datos[0]);
        int.TryParse(id_cliente.text.ToString(), out datos[1]);
        int.TryParse(cant_solictada.text.ToString(), out datos[2]);
        int.TryParse(cant_tot_entrega.text.ToString(), out datos[3]);
        int.TryParse(num_factura.text.ToString(), out datos[4]);
        int.TryParse(num_compra.text.ToString(), out datos[5]);

        WWWForm form = new WWWForm();

        form.AddField("id_pedido", datos[0]);
        form.AddField("id_cliente", datos[1]);
        form.AddField("lote", lote.text.ToString());
        form.AddField("cant_solicitada", datos[2]);
        form.AddField("cant_tot_entrega", datos[3]);
        form.AddField("fecha_envio", fecha_envio.text.ToString());
        form.AddField("fecha_caducidad", fecha_caducidad.text.ToString());
        form.AddField("fecha_produccion", fecha_produccion.text.ToString());
        form.AddField("num_factura", datos[4]);
        form.AddField("num_compra", datos[5]);
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/insertar_pedido.php", form);
        yield return www;
        Debug.Log(www.text);

        if (www.text[0] == '0')
        {
            InstantiateSuccess("Operación exitosa", "Pedido creado exitosamente");

        }
        else
        {
            InstantiateSuccess("Error: " + www.text, "No se creó el pedido.");

        }
    }
    IEnumerator MostrarPedidos()
    {
        //ELIMAR EQUIPOS
        GameObject[] eList = GameObject.FindGameObjectsWithTag("pedido");
        foreach (GameObject g in eList)
        {
            Destroy(g.gameObject);
        }
        yield return new WaitForSeconds(.2f);

        //PHP EQUIPOS
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultapedido.php"); //GET data is sent via the URL
        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            yield return null;
        }
        Debug.Log(www.text);

        if (string.IsNullOrEmpty(www.error))
        {

            List<Pedido> pedido = JsonConvert.DeserializeObject<List<Pedido>>(www.text);

            foreach (Pedido p in pedido)
            {
                GameObject ego = Instantiate(pedidoGO, pedidodady.transform);
                ////id , marca, serie, proveedor, fecha adquicisión garantía, descripción larga, descripción corta, ubicación, mantenimiento, tipoo de equipo, responsable 
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = p.id_pedido.ToString();
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = p.id_cliente.ToString();
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = p.lote;
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = p.cant_solicitada.ToString();
                ego.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = p.cant_tot_entrega.ToString();
                ego.transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = p.fecha_envio;
                ego.transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = p.fecha_caducidad;
                ego.transform.GetChild(7).GetComponentInChildren<TextMeshProUGUI>().text = p.num_factura.ToString();
                ego.transform.GetChild(8).GetComponentInChildren<TextMeshProUGUI>().text = p.num_compra.ToString();
                ego.transform.GetChild(9).GetComponentInChildren<TextMeshProUGUI>().text = p.fecha_produccion;


            }

            /*
                 lote,
                 numero de analis
                 pedido
                 cliente
            */


        }
        else Debug.LogWarning(www.error);
        yield return new WaitForSeconds(.3f);
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
