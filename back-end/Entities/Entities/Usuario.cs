using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Bio { get; set; }
        public string? Localizacao { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Links { get; set; }
    }
}
