using System.Security.Cryptography;
using System.Text;

namespace centroDeportivo.Aplicacion.Seguridad;

public class ServicioHash
{
    public string CalcularHash(string texto)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convertir la cadena de entrada en un array de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            // Convertir el array de bytes en una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}