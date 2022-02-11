using System.Threading.Tasks;

namespace FelipeEventos.Persistence.Interfaces
{
    public interface IGeral
    {
        // Geral
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void DeleteRange<T>(T[] entity) where T: class;
         Task<bool> SaveChangesAsync();
    }
}