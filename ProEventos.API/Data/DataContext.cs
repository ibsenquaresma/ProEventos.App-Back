using Microsoft.EntityFrameworkCore;

namespace ProEventos.API.Data
{
    // Criação do DATA CONTEXT - *Curso
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
    //
}
