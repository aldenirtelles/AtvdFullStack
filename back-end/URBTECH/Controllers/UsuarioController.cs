using AutoMapper;
using Data.Contracts;
using Datas.Contracts.Requests;
using Datas.Contracts.Responses;
using Entities.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;

namespace URBTECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly ICadastroUsuario _ICadastroUsuario;

        public UsuarioController(IMapper iMapper, ICadastroUsuario iCadastroUsuario)
        {
            _IMapper = iMapper;
            _ICadastroUsuario = iCadastroUsuario;
        }

        [Produces("application/json")]
        [HttpPost("urbtech/cadastro")]
        public async Task<IActionResult> Add (UsuarioDto usuario)
        {
            var usuarioMap = _IMapper.Map<Usuario>(usuario);
            var result = await _ICadastroUsuario.Adicionar(usuarioMap);
            if(result)
            {
                return CreatedAtAction(nameof(GetById), new { usuarioId = usuarioMap.Id }, usuarioMap);
            }

            return BadRequest("Não foi possível adicionar o usuário.");
        }

        [Produces("application/json")]
        [HttpPut("urbtech/cadastro/{usuarioId}")]
        public async Task<IActionResult> Perfil (PerfilResponse perfil, int usuarioId)
        { 
            var result = await _ICadastroUsuario.AtualizarDadosDaConta(perfil, usuarioId);

            if (result)
            {
                return Ok();
            }
            return NotFound();
            /*var result = await _ICadastroUsuario.Atualizar(perfil, usuarioId);

            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();*/
        }

        [Produces("application/json")]
        [HttpGet("urbtech/usuarios/{usuarioId}")]
        public async Task<IActionResult> GetById (int usuarioId)
        {
            var usuario = await _ICadastroUsuario.GetById(usuarioId);

            if(usuario == null)
            {
                return NotFound();
            }

            var usuarioMap = _IMapper.Map<PerfilResponse>(usuario);
            return Ok(usuarioMap);
        }


        [Produces("application/json")]
        [HttpGet("urbtech/usuarios")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _ICadastroUsuario.GetAll();
            List<UsuarioResponse> usuarioResponses = new List<UsuarioResponse>();

            foreach(var usuario in usuarios)
            {
                var usuarioMap = _IMapper.Map<UsuarioResponse>(usuario);
                usuarioResponses.Add(usuarioMap);
            }

            return Ok(usuarioResponses);
        }

        [Produces("application/json")]
        [HttpPost("urbtech/login")]
        public async Task<IActionResult> Login(LoginResponse login)
        {
            var usuario = await _ICadastroUsuario.VerificarLogin(login);

            if(usuario != null)
            {
                return Ok(usuario);
            }

            return NotFound();
        }

        [Produces("application/json")]
        [HttpDelete("urbtech/delete")]
        public async Task<IActionResult> Delete(int usuarioId)
        {
            _ICadastroUsuario.Delete(usuarioId);
            return Ok();
        }


    }
}
