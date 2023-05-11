using Datas.Contracts.Requests;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioUsuario : ICadastroUsuario
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioUsuario()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<bool> Adicionar(Usuario usuario)
        {
            try
            {
                using (var data = new ContextBase(_OptionsBuilder))
                {
                    var result = await data.Set<Usuario>().AddAsync(usuario);
                    await data.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AtualizarDadosDaConta(PerfilResponse perfil, int usuarioId)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                var usuario = await data.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId);

                if (usuario != null)
                {
                    usuario.Nome = perfil.Nome;
                    usuario.Bio = perfil.Bio;
                    usuario.Localizacao = perfil.Localizacao;
                    usuario.Links = perfil.Links;
                    usuario.DataNascimento = perfil.DataNascimento;

                    await data.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }


        public async Task<bool> Delete(int usuarioId)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                var usuario = await data.Set<Usuario>().FindAsync(usuarioId);
                data.Set<Usuario>().Remove(usuario);
                await data.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Usuario>> GetAll()
        {
            using(var data = new ContextBase(_OptionsBuilder))
            {
                var usuarios = await data.Usuario.ToListAsync();
                return usuarios.ToList();
            }
        }

        public async Task<Usuario> GetById(int usuarioId)
        {
            using(var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<Usuario>().FirstOrDefaultAsync(u => u.Id == usuarioId);
            }
        }

        public async Task<Usuario> VerificarLogin(LoginResponse login)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                var usuario = await data.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);

                return usuario;
            }
        }
    }
}
