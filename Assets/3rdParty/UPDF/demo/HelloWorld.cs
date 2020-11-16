using System.Collections;
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

public class HelloWorld : MonoBehaviour {
    
	void Start () {
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
        gfx.DrawString("Hello, World!", font, XBrushes.Black,
        new XRect(0, 0, page.Width, page.Height),  
        XStringFormat.Center);
        string filename = "HelloWorld.pdf";
        document.Save(filename);
        Process.Start(filename);
    }
	

}
