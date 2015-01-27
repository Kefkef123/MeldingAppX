using System.Data.Entity;
using MeldingAppX.Api.Models;

namespace MeldingAppX.Api
{
    public class MeldingAppContext : DbContext
    {
        public DbSet<Notice> Notices { get; set; }
    }
}