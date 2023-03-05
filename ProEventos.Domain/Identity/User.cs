using Microsoft.AspNetCore.Identity;
using ProEventos.Domain.Enum;

namespace ProEventos.Domain.Identity
{
    public class User: IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Titulo Titulo { get; set; }
        public string? Description { get; set; }
        public Funcao Funcao { get; set; }
        public string? ImagemUrl { get; set; }
        public IEnumerable<UserRole>? UserRoles { get; set; }
    }
}
