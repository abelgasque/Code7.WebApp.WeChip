using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Models;

namespace WeChip.Data
{
    public class Repository : IRepository
    {
        private readonly AppDataContext _context;

        public Repository(AppDataContext context)
        {
            _context = context;
        }

        public Repository()
        {
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Task<Cliente[]> GetAllClientesAsysnc()
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(c => c.Status.FinalizaCliente == false);
            return query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteAsysncById(long id)
        {
            IQueryable<Cliente> query = _context.Clientes;
            query = query.AsNoTracking()
                        .Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Status> GetStatusAsysncById(long id)
        {
            IQueryable<Status> query = _context.Status;
            query = query.AsNoTracking()
                        .Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Status[]> GetStatusPorClienteIdAsysnc(long id)
        {
            IQueryable<Status> query = _context.Status;
            query = query.AsNoTracking()
                        .Where(c => c.IdCliente == id);
            return await query.ToArrayAsync();
        }

        public async Task<Endereco> GetEnderecoAsysncById(long id)
        {
            IQueryable<Endereco> query = _context.Enderecos;
            query = query.AsNoTracking()
                        .Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Oferta[]> GetOfertasPorClienteIdAsysnc(long id)
        {
            IQueryable<Oferta> query = _context.Ofertas;
            query = query.AsNoTracking()
                        .Where(c => c.IdCliente == id);
            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetProdutoAsysncById(long id)
        {
            IQueryable<Produto> query = _context.Produtos;
            query = query.AsNoTracking()
                        .Where(c => c.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Produto[]> GetAllProdutosAsysnc()
        {
            IQueryable<Produto> query = _context.Produtos;
            query = query.AsNoTracking()
                        .OrderBy(c => c.Id);
            return await query.ToArrayAsync();
        }
    }
}
