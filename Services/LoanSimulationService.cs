using APISimulacaoEmprestimos.Models;

namespace APISimulacaoEmprestimos.Services
{
    public class LoanSimulationService
    {
        public LoanSimulationResultModel SimulateLoan(LoanRequestModel request)
        {
            decimal monthlyInterestRate = request.AnnualInterestRate / 12;
            decimal monthlyPayment = CalculateMonthlyPayment(request.LoanAmount, monthlyInterestRate, request.NumberOfMonths);

            var paymentSchedule = new List<PaymentScheduleModel>();
            decimal balance = request.LoanAmount;
            decimal totalInterest = 0;

            for (int month = 1; month <= request.NumberOfMonths; month++)
            {
                decimal interest = balance * monthlyInterestRate;
                decimal principal = monthlyPayment - interest;
                balance -= principal;

                totalInterest += interest;

                paymentSchedule.Add(new PaymentScheduleModel
                {
                    Month = month,
                    Principal = principal,
                    Interest = interest,
                    Balance = balance
                });
            }

            decimal totalPayment = request.LoanAmount + totalInterest;

            return new LoanSimulationResultModel
            {
                MonthlyPayment = monthlyPayment,
                TotalInterest = totalInterest,
                TotalPayment = totalPayment,
                PaymentSchedule = paymentSchedule
            };
        }

        private decimal CalculateMonthlyPayment(decimal loanAmount, decimal monthlyInterestRate, int numberOfMonths)
        {
            return loanAmount * (monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), numberOfMonths)) /
                   (decimal)(Math.Pow((double)(1 + monthlyInterestRate), numberOfMonths) - 1);
        }
    }

}
