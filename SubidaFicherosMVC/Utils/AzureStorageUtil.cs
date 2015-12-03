using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace SubidaFicherosMVC.Utils
{
    public class AzureStorageUtil
    {
        private CloudStorageAccount _cuenta;
        private string _contenedor;

        public AzureStorageUtil(string accountname, string password, string contenedor)
        {
            StorageCredentials cred = new StorageCredentials(accountname, password);
            _cuenta = new CloudStorageAccount(cred, true);
            _contenedor = contenedor;
        }

        private void ComprobarContenedor(string contenedor = null)
        {
            if (contenedor != null)
            {
                _contenedor = contenedor;
            }

            var cliente = _cuenta.CreateCloudBlobClient();
            var cont = cliente.GetContainerReference(_contenedor);
            cont.CreateIfNotExists();

        }

        public void SubirFichero(Stream fichero, string nombre, string contenedor = null)
        {
            ComprobarContenedor();
            var cliente = _cuenta.CreateCloudBlobClient();
            var cont = cliente.GetContainerReference(_contenedor);
            var blob = cont.GetBlockBlobReference(nombre);
            blob.UploadFromStream(fichero);
            fichero.Close();
        }

        public byte[] RecuperarFichero(string nombre, string contenedor)
        {
            ComprobarContenedor();
            var cliente = _cuenta.CreateCloudBlobClient();
            var cont = cliente.GetContainerReference(_contenedor);
            var blob = cont.GetBlockBlobReference(nombre);

            blob.FetchAttributes();
            var lon = blob.Properties.Length;
            var dest = new byte[lon];
            blob.DownloadToByteArray(dest, 0);
            return dest;

        }
    }
}