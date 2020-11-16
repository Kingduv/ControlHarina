using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Linq;


public class CRUDCertificado : MonoBehaviour
{
    [Header("ExitoFracaso")]
    public GameObject success;
    public GameObject dady;
    public GameObject certificadoGO;
    public GameObject dadyCertificado;

    [Header("Inputfields")]
    public TMP_InputField lote;
    public TMP_InputField id_pedido;
    public TMP_InputField id_cliente;
    public TMP_InputField num_analisis;

    CertificadoCompleto certCompleto = new CertificadoCompleto();



    public void InsertarCertificados()
    {
        StartCoroutine(Insertar());
    }
    public void MostrarCertificados()
    {
        StartCoroutine(Mostrar());
    }
    public void ConsultarCertificado()
    {
        StartCoroutine(Consultar());
    }
    IEnumerator Mostrar()
    {
        int id_p, id_c, n_a;
        int.TryParse(id_pedido.text.ToString(), out id_p);
        int.TryParse(id_cliente.text.ToString(), out id_c);
        int.TryParse(num_analisis.text.ToString(), out n_a);

        WWWForm form = new WWWForm();
        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/cnpcertificado.php", form);
        yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                List<Certificado> certificados = JsonConvert.DeserializeObject<List<Certificado>>(www.text);
                foreach(Certificado c in certificados)
                {
                GameObject ego = Instantiate(certificadoGO, dadyCertificado.transform);
           
                ego.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = c.lote;
                ego.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = c.numero_analisis.ToString();
                ego.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = c.id_certificado.ToString();
                ego.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = c.id_pedido.ToString();
                ego.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = c.id_cliente.ToString();

            }
        }
        }
    IEnumerator Consultar()
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
        //form.AddField("id_pedido", 10);
        //form.AddField("id_cliente", 101);
        //form.AddField("numero_analisis", 1);


        WWW www = new WWW("https://lab.anahuac.mx/~a00289882/DSF/cnpcertificado.php", form);
        yield return www;
        Debug.Log(www.text);
        string lt = lote.text.ToString();
        //id_p = 10;
        //id_c = 101;
        //n_a = 1;
        if (string.IsNullOrEmpty(www.error))
        {
            List<Certificado> certificados = JsonConvert.DeserializeObject<List<Certificado>>(www.text);
            

            WWWForm form2 = new WWWForm();
            certCompleto.c = certificados.Where(
                c => c.id_cliente==id_c && c.id_pedido==id_p && c.lote.Equals(lt)
                ).FirstOrDefault();

            //Debug.Log(certCompleto.c.id_certificado+" lote: "+certCompleto.c.lote);

            form.AddField("lote", certCompleto.c.lote);
            WWW www2 = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultaanalisis.php", form);
            yield return www2;

            if (string.IsNullOrEmpty(www2.error)) //analisis
            {
                List<Analisis> analisis = JsonConvert.DeserializeObject<List<Analisis>>(www2.text);
                certCompleto.a = analisis.
                                    Where(a => a.numero_analisis == n_a).FirstOrDefault();
                Debug.Log(certCompleto.a.id_analisis);

            }
           
            
            WWW www3 = new WWW("https://lab.anahuac.mx/~a00289882/DSF/mostrar_cliente.php"); //GET data is sent via the URL
            while (!www3.isDone && string.IsNullOrEmpty(www3.error))
            {
                yield return null;
            }

            if (string.IsNullOrEmpty(www3.error))//cliente
            {

                List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(www3.text);
                certCompleto.cl = clientes.
                                    Where(cl => cl.id_cliente==id_c).
                                    FirstOrDefault();
                Debug.Log(certCompleto.cl.id_cliente);


            }

            WWW www4 = new WWW("https://lab.anahuac.mx/~a00289882/DS/consultapedido.php"); //GET data is sent via the URL
            while (!www4.isDone && string.IsNullOrEmpty(www3.error))
            {
                yield return null;
            }

            if (string.IsNullOrEmpty(www3.error))//pedido
            {
                List<Pedido> pedidos = JsonConvert.DeserializeObject<List<Pedido>>(www.text);
                certCompleto.p = pedidos.Where(p => p.id_pedido == id_p).FirstOrDefault();
                //Debug.Log(certCompleto.p.id_pedido);
            }

            certCompleto.com_absorcion_agua = 
                IsInRange(certCompleto.cl.ab_lim_sup,certCompleto.cl.ab_lim_inf,certCompleto.a.absorcion_agua) == 0 ? "En rango" :
                IsInRange(certCompleto.cl.ab_lim_sup, certCompleto.cl.ab_lim_inf, certCompleto.a.absorcion_agua) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_desarrollo_masa =
               IsInRange(certCompleto.cl.dm_lim_sup, certCompleto.cl.dm_lim_inf, certCompleto.a.desarrollo_masa) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.dm_lim_sup, certCompleto.cl.dm_lim_inf, certCompleto.a.desarrollo_masa) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_estabilidad =
               IsInRange(certCompleto.cl.e_lim_sup, certCompleto.cl.e_lim_inf, certCompleto.a.estabilidad) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.e_lim_sup, certCompleto.cl.e_lim_inf, certCompleto.a.estabilidad) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_grado_reblandecimineto =
               IsInRange(certCompleto.cl.gr_lim_sup, certCompleto.cl.gr_lim_inf, certCompleto.a.grado_reblandecimineto) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.gr_lim_sup, certCompleto.cl.gr_lim_inf, certCompleto.a.grado_reblandecimineto) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_fqn =
               IsInRange(certCompleto.cl.fqn_lim_sup, certCompleto.cl.fqn_lim_inf, certCompleto.a.fqn) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.fqn_lim_sup, certCompleto.cl.fqn_lim_inf, certCompleto.a.fqn) == 1 ? "Arriba del rango" : "Debajo del rango";
            certCompleto.com_tenacidad =
               IsInRange(certCompleto.cl.t_lim_sup, certCompleto.cl.t_lim_inf, certCompleto.a.tenacidad) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.t_lim_sup, certCompleto.cl.t_lim_inf, certCompleto.a.tenacidad) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_extensibilidad =
               IsInRange(certCompleto.cl.ex_lim_sup, certCompleto.cl.ex_lim_inf, certCompleto.a.extensibilidad) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.ex_lim_sup, certCompleto.cl.ex_lim_inf, certCompleto.a.extensibilidad) == 1 ? "Arriba del rango" : "Debajo del rango";
            certCompleto.com_fuerza_harina =
               IsInRange(certCompleto.cl.fh_lim_sup, certCompleto.cl.fh_lim_inf, certCompleto.a.fuerza_harina) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.fh_lim_sup, certCompleto.cl.fh_lim_inf, certCompleto.a.fuerza_harina) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_configuracion =
               IsInRange(certCompleto.cl.cc_lim_sup, certCompleto.cl.cc_lim_inf, certCompleto.a.configuracion) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.cc_lim_sup, certCompleto.cl.cc_lim_inf, certCompleto.a.configuracion) == 1 ? "Arriba del rango" : "Debajo del rango";

            certCompleto.com_indice_elasticidad =
               IsInRange(certCompleto.cl.ie_lim_sup, certCompleto.cl.ie_lim_inf, certCompleto.a.indice_elasticidad) == 0 ? "En rango" :
               IsInRange(certCompleto.cl.ie_lim_sup, certCompleto.cl.ie_lim_inf, certCompleto.a.indice_elasticidad) == 1 ? "Arriba del rango" : "Debajo del rango";

            Debug.Log(certCompleto.com_absorcion_agua + " " + certCompleto.cl.ab_lim_sup +" "+certCompleto.cl.ab_lim_inf + " "+ certCompleto.a.absorcion_agua) ;

            Imprimir pdf = new Imprimir();
            pdf.PrintPDF(certCompleto);
        }

    }

    public int IsInRange(int max, int min, int n)
    {
        int itIs = max < n ? 1 : min > n ? 2: 0;
        return itIs;
    }
    IEnumerator Insertar()
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