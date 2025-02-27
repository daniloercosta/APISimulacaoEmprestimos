namespace APISimulacaoEmprestimos.Models
{
    public class Proposta
    {
        public int Id { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int NumberOfMonths { get; set; }
    }
}
