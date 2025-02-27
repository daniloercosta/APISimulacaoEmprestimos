using System.ComponentModel.DataAnnotations;

namespace APISimulacaoEmprestimos.Models
{
    public class LoanRequestModel
    {
        public decimal LoanAmount { get; set; }
        public decimal AnnualInterestRate { get; set; }
        public int NumberOfMonths { get; set; }
    }
}
