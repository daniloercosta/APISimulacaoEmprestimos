using APISimulacaoEmprestimos.Models;
using Microsoft.EntityFrameworkCore;

namespace APISimulacaoEmprestimos.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<PaymentFlowSummary> PaymentFlowSummaries { get; set; }
    }
}
