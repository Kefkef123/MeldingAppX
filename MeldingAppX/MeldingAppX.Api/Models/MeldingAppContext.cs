using System.Data.Entity;
using MeldingAppX.Models;

namespace MeldingAppX.Api
{
    public class MeldingAppContext : DbContext
    {
        public DbSet<Notice> Notices { get; set; }
    }
}