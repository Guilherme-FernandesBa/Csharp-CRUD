using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;
using CRUD.Repositorio.Interfaces;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
        }
        #region APIS De Busca

        #region Buscar Todos Os Usuarios
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarTodosUsuarios()
        {
            _logger.LogInformation("#########################################################");
            _logger.LogInformation("Chamando a API - Buscar Todos os Usuarios");
            
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            if(usuarios.Count >=1)
            {
            return Ok(usuarios);

            }
            else
            {
                return BadRequest("Não foi possivel retornar sua Query");
            }
        }
        #endregion

        #region Buscar Por ID
        [HttpGet("buscarPorId/{id}")]
        public async Task<ActionResult<UsuarioModel>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);

        }
        #endregion


        #endregion

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuario)
        {
            UsuarioModel user =await _usuarioRepositorio.Adicionar(usuario);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);
            return Ok(usuario);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar( int id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(id);
            return Ok(apagado);

        }
    }
}
