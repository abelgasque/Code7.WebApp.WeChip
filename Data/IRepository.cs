using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Models;

namespace WeChip.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Cliente[]> GetAllClientesAsysnc();
        
        Task<Cliente> GetClienteAsysncById(long id);

        Task<Status> GetStatusAsysncById(long id);

        Task<Status[]> GetStatusPorClienteIdAsysnc(long id);

        Task<Endereco> GetEnderecoAsysncById(long id);

        Task<Produto> GetProdutoAsysncById(long id);

        Task<Oferta[]> GetOfertasPorClienteIdAsysnc(long id);

        Task<Produto[]> GetAllProdutosAsysnc();
    }
}
