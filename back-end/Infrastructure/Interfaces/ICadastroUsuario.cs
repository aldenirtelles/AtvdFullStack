using Datas.Contracts.Requests;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICadastroUsuario
    {
        Task<bool> Adicionar(Usuario usuario);
        Task<bool> AtualizarDadosDaConta(PerfilResponse perfil, int usuarioId);
        Task<List<Usuario>> GetAll();
        Task<Usuario> GetById(int usuarioId);
        Task<bool> Delete(int usuarioId);
        Task<Usuario> VerificarLogin(LoginResponse login);
    }
}
