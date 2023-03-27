using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Services.Email
{
    public interface IEmailService
    {
        Task SendMail(string from, string to, string subject, string text);
    }
}
