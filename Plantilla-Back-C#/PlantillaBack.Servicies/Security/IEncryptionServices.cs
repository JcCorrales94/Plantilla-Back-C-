using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Services.Security
{
    public interface IEncryptionServices
    {
        string EncryptString(string text);
        string DecryptString(string cipherText);
    }
}
