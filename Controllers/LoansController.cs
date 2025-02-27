using APISimulacaoEmprestimos.Context;
using APISimulacaoEmprestimos.Models;
using APISimulacaoEmprestimos.Services;
using Microsoft.AspNetCore.Mvc;

namespace APISimulacaoEmprestimos.Controllers
{
    /// <summary>
    /// Controlador responsável pelas simulações de empréstimos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoanSimulationService _loanSimulationService;
        private readonly ApplicationDbContext _context;

        public LoansController(LoanSimulationService loanSimulationService, ApplicationDbContext context)
        {
            _loanSimulationService = loanSimulationService;
            _context = context;
        }

        /// <summary>
        /// Realiza uma simulação de empréstimo com base nos dados fornecidos.
        /// </summary>
        /// <param name="request">Objeto com os dados do empréstimo a ser simulado.</param>
        /// <returns>Resultado da simulação, incluindo o valor das parcelas e o fluxo de pagamentos.</returns>
        /// <response code="200">Simulação realizada com sucesso.</response>
        /// <response code="400">Dados fornecidos são inválidos.</response>
        [HttpPost("simulate")]
        public ActionResult<LoanSimulationResultModel> SimulateLoan([FromBody] LoanRequestModel request)
        {

            var proposal = new Proposta
            {
                LoanAmount = request.LoanAmount,
                AnnualInterestRate = request.AnnualInterestRate,
                NumberOfMonths = request.NumberOfMonths
            };

            _context.Propostas.Add(proposal);
            _context.SaveChanges();

            var simulationResult = _loanSimulationService.SimulateLoan(request);

            foreach (var payment in simulationResult.PaymentSchedule)
            {
                var paymentSummary = new PaymentFlowSummary
                {
                    Month = payment.Month,
                    Principal = payment.Principal,
                    Interest = payment.Interest,
                    Balance = payment.Balance,
                    ProposalId = proposal.Id
                };
                _context.PaymentFlowSummaries.Add(paymentSummary);
            }
            _context.SaveChanges();

            return Ok(simulationResult);
        }
    }
}
