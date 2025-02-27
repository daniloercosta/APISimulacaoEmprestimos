namespace APISimulacaoEmprestimos.Models
{
    public class LoanSimulationResultModel
    {
        public decimal MonthlyPayment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalPayment { get; set; }
        public List<PaymentScheduleModel> PaymentSchedule { get; set; }
    }
}
