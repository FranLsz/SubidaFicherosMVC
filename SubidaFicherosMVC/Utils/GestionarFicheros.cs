using System;
using System.IO;
using System.Web;

namespace SubidaFicherosMVC.Utils
{
    public class GestionarFicheros
    {
        public static String GuardarFicheroDisco(HttpPostedFileBase fichero, HttpServerUtilityBase server)
        {
            var id = Guid.NewGuid().ToString();
            String nombre = null;
            if (fichero != null && fichero.ContentLength > 0)
            {
                var ext = fichero.FileName.Substring(fichero.FileName.LastIndexOf(".") + 1);
                nombre = $"{id}.{ext}";

                fichero.SaveAs(server.MapPath("/ficheros") + "/" + nombre);
            }
            return nombre;
        }

        public static byte[] ToBinario(HttpPostedFileBase fichero)
        {
            byte[] data = null;
            if (fichero != null && fichero.ContentLength > 0)
            {
                using (var stream = fichero.InputStream)
                {
                    var ms = stream as MemoryStream;
                    if (ms == null)
                    {
                        ms =new MemoryStream();
                        stream.CopyTo(ms);
                    }
                    data = ms.ToArray();
                }
            }
            return data;
        }
    }
}