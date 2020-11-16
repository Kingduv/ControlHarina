using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using UnityEngine;

public class Imprimir : MonoBehaviour
{

    public void PrintPDF(CertificadoCompleto c)
    {
        string nombre;
        int mX = 50; // Margen x
        int mY = 70; // Margen y
        int yR = 100;// donde esta
        int br = 25;
        string s = "       ";
        string s2 = "    ";

        nombre = c.cl.nombre;
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

        gfx.DrawString("Harina Real S.A. de C.V.", font, XBrushes.Black,
        new XRect(0, 25, page.Width, page.Height),
        XStringFormat.TopCenter);

        gfx.DrawString("Certificado emitido para: ", font, XBrushes.Black,
       new XRect(0, 45, page.Width, page.Height),
       XStringFormat.TopCenter);
       
        gfx.DrawString(nombre, font, XBrushes.Coral,
       new XRect(0, 75, page.Width, page.Height),
       XStringFormat.TopCenter);

        yR += 50;

        //***************CLIENTE***********
        XFont cliente = new XFont("Calibri", 16, XFontStyle.Bold);
        gfx.DrawString("Datos Cliente:", cliente, XBrushes.Blue,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 30;
        //**************DATOS*****************
        XFont datosC = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Id Cliente:" +c.cl.id_cliente+ "       Nombre: " + c.cl.nombre + "       RFC: " + c.cl.rfc + "       Contacto: " + c.cl.contacto, datosC, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        XFont datosC2 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Domicilio: " + c.cl.domicilio, datosC, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += br;


        //***************PEDIDO***********

        XFont pedido = new XFont("Calibri", 16, XFontStyle.Bold);
        gfx.DrawString("Datos Pedido: ", pedido, XBrushes.Blue,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 30;
        XFont datosP1 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Id Pedido: " + c.p.id_pedido + "       Lote: " + c.p.lote + "       Id Cliente: " + c.p.id_cliente , datosP1, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        XFont datosP2 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Fecha de Caducidad: " + c.p.fecha_caducidad + "       Fecha de Envio: " + c.p.fecha_envio   , datosP2, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        XFont datosP3 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Número de Compra: " + c.p.num_compra+ "       Número de Factura: " + c.p.num_factura, datosP3, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        XFont datosP4 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Cantidad Solicitada: " + c.p.cant_solicitada + "       Cantidad Total de Entrega: " + c.p.cant_tot_entrega, datosP4, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += br;


        //***************ANALISIS***********
        XFont analisis = new XFont("Calibri", 16, XFontStyle.Bold);
        gfx.DrawString("Datos Análisis:", cliente, XBrushes.Blue,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 25;
        //**************DATOS*****************
        XFont datosA = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Id Análisis: "+c.a.id_analisis+s+"Lote: "+c.a.lote+s+"Número de Análisis: "+c.a.numero_analisis, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        List<String> pC = new List<string>();
        if (c.cl.personalizado==1)
        {
            pC.Add("Límite Inferior:" + c.cl.ab_lim_inf + s2 + "Límite Superior:" + c.cl.ab_lim_sup + s2 + "Comentario:" + c.com_absorcion_agua);
            pC.Add("Límite Inferior:" + c.cl.dm_lim_inf + s2 + "Límite Superior:" + c.cl.dm_lim_sup + s2 + "Comentario:" + c.com_desarrollo_masa);
            pC.Add("Límite Inferior:" + c.cl.e_lim_inf + s2 + "Límite Superior:" + c.cl.e_lim_sup + s2 + "Comentario:" + c.com_estabilidad);
            pC.Add("Límite Inferior:" + c.cl.gr_lim_inf + s2 + "Límite Superior:" + c.cl.gr_lim_sup + s2 + "Comentario:" + c.com_grado_reblandecimineto);
            pC.Add("Límite Inferior:" + c.cl.fqn_lim_inf + s2 + "Límite Superior:" + c.cl.fqn_lim_sup + s2 + "Comentario:" + c.com_fqn);
            pC.Add("Límite Inferior:" + c.cl.t_lim_inf + s2 + "Límite Superior:" + c.cl.t_lim_sup + s2 + "Comentario:" + c.com_tenacidad);
            pC.Add("Límite Inferior:" + c.cl.ex_lim_inf + s2 + "Límite Superior:" + c.cl.ex_lim_sup + s2 + "Comentario:" + c.com_extensibilidad);
            pC.Add("Límite Inferior:" + c.cl.fh_lim_inf + s2 + "Límite Superior:" + c.cl.fh_lim_sup + s2 + "Comentario:" + c.com_fuerza_harina);
            pC.Add("Límite Inferior:" + c.cl.cc_lim_inf + s2 + "Límite Superior:" + c.cl.cc_lim_sup + s2 + "Comentario:" + c.com_configuracion);
            pC.Add("Límite Inferior:" + c.cl.ie_lim_inf + s2 + "Límite Superior:" + c.cl.ie_lim_sup + s2 + "Comentario:" + c.com_indice_elasticidad);
        }
        string d = c.cl.personalizado == 1 ?pC[0]:"";
        XFont datosA1 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Absorción agua: "+c.a.absorcion_agua+s+d, datosA1, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        d = c.cl.personalizado == 1 ? pC[1] : "";
        XFont datosA2 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Desarrollo Masa:"+c.a.desarrollo_masa + s + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        d = c.cl.personalizado == 1 ? pC[2] : "";
        XFont datosA3 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Estabilidad:" + c.a.estabilidad + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        
        d = c.cl.personalizado == 1 ? pC[2] : "";
        XFont datosA4 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Grado de Reblandecimiento:" + c.a.grado_reblandecimineto + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        d = c.cl.personalizado == 1 ? pC[3] : "";
        XFont datosA5 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("FQN:" + c.a.fqn + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        d = c.cl.personalizado == 1 ? pC[4] : "";
        XFont datosA6 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Tenacidad:" + c.a.tenacidad + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;
        d = c.cl.personalizado == 1 ? pC[5] : "";
        XFont datosA7 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Extensabilidad:" + c.a.extensibilidad+ s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;

        d = c.cl.personalizado == 1 ? pC[6] : "";
        XFont datosA8 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Fuerza Harina:" + c.a.fuerza_harina + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;

        d = c.cl.personalizado == 1 ? pC[7] : "";
        XFont datosA9 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Configuración de Curva:" + c.a.configuracion + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;

        d = c.cl.personalizado == 1 ? pC[1] : "";
        XFont datosA10 = new XFont("Verdana", 10, XFontStyle.Regular);
        gfx.DrawString("Indice de Elasticidad:" + c.a.indice_elasticidad + s2 + d, datosA, XBrushes.Black,
        new XRect(50, yR, page.Width, page.Height),
        XStringFormat.TopLeft);
        yR += 20;

        string filename = "Certificado.pdf";
        document.Save(filename);
        Process.Start(filename);
    }
}
