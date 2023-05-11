using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas.Contracts.Requests
{
    public class PerfilResponse
    {
        public string Nome { get; set; }
        public string? Bio { get; set; }
        public string? Localizacao { get; set; }
        public string? Links { get; set; }
        public DateTime? DataNascimento { get; set; }
 
    }
}
