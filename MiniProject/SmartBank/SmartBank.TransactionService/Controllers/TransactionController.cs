using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Services;

namespace SmartBank.TransactionService.Controllers
{
    /// <summary>
    /// BEGINNER NOTE: This controller handles all transaction-related API requests
    /// It receives deposit/withdrawal requests from the AccountService and saves them to the database
    /// </summary>
    [ApiController]
    [Route("api/transaction")] // FIXED: Changed from [controller] to "transaction" - this creates the route "api/transaction"
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        // BEGINNER NOTE: Constructor receives the service through Dependency Injection (configured in Program.cs)
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// BEGINNER NOTE: This endpoint is called when someone deposits or withdraws money
        /// POST request to: https://localhost:7185/api/transaction
        /// It saves the transaction details to the SQL Server database
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(TransactionDto dto)
        {
            try
            {
                // BEGINNER NOTE: This calls the service which saves the transaction to the database
                await _service.CreateTransactionAsync(dto);
                return Ok(new { message = "Transaction saved successfully", success = true });
            }
            catch (Exception ex)
            {
                // BEGINNER NOTE: If something goes wrong, return an error message
                return BadRequest(new { message = $"Transaction failed: {ex.Message}", success = false });
            }
        }
    }
}
