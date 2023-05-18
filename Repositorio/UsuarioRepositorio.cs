using Microsoft.EntityFrameworkCore;
using CRUD.Data;
using CRUD.Models;
using CRUD.Repositorio.Interfaces;

namespace CRUD.Repositorio
{

    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ILogger _logger;
        private readonly SistemaDeTarefasDBContext _dbContext;

        public UsuarioRepositorio(SistemaDeTarefasDBContext sistemaDeTarefasDBContext, ILogger<UsuarioRepositorio> logger)
        {
            _dbContext = sistemaDeTarefasDBContext;
            _logger = logger;
        }



        #region APIS De Busca

        #region Buscar Por ID
        public async Task<UsuarioModel> BuscarPorId(int id)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        #endregion

        #region Buscar Todos os Usuarios
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            _logger.LogInformation("#########################################################");
            _logger.LogInformation("Fazendo a Query no banco de todos os Usuarios");
            try
            {

                return await _dbContext.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro - Buscar todos os Usuarios -----{ex} ");
                _logger.LogWarning($"Retornando Lista Vazia");
                
                return new List<UsuarioModel>();
               
            }
        }

        #endregion

        #endregion

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Id: {id} não foi encontrado.");
            }
            else
            {
                usuarioPorId.Nome = usuario.Nome;
                usuarioPorId.Email = usuario.Email;

                _dbContext.Usuarios.Update(usuarioPorId);
                _dbContext.SaveChanges();
                return usuarioPorId;

            }
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);
            if (usuarioPorId == null)
            {
                throw new Exception($"Usuario para o Id: {id} não foi encontrado.");
            }
            else
            {
                _dbContext.Usuarios.Remove(usuarioPorId);
                _dbContext.SaveChanges();
                return true;
            }


        }
    }
}
