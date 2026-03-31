using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.AccountService.DTOs;
using SmartBank.AccountService.Services;
using System.Security.Claims;

namespace SmartBank.AccountService.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateAccountDto dto)
        //{
        //    var account = await _service.CreateAccount(dto.UserId);
        //    return Ok(account);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var account = await _service.CreateAccount(userId);
            return Ok(account);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    if (userId == null)
        //        return Unauthorized();

        //    var account = await _service.CreateAccount(userId);

        //    return Ok(account);
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var account = await _service.GetAllAccounts();
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var account = await _service.GetAccount(id);
            
            if (account == null)
                return NotFound($"Account with ID {id} not found");
                
            return Ok(account);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        //    var account = await _service.GetAccount(id);

        //    if (account.UserId != userId)
        //        return Forbid();

        //    return Ok(account);
        //}

        //[HttpPost("deposit")]
        //public async Task<IActionResult> Deposit(TransactionDto dto)
        //{
        //    await _service.Deposit(dto.AccountId, dto.Amount);
        //    return Ok("Deposit successful");
        //}

        [HttpPost("deposit")]
        // [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Deposit(TransactionDto dto)
        {
            var token = Request.Headers["Authorization"]
                            .ToString()
                            .Replace("Bearer ", "");

            await _service.Deposit(dto.AccountId, dto.Amount, token);

            return Ok("Deposit successful");
        }

        //[HttpPost("withdraw")]
        //public async Task<IActionResult> Withdraw(TransactionDto dto)
        //{
        //    await _service.Withdraw(dto.AccountId, dto.Amount);
        //    return Ok("Withdraw successful");
        //}

        [HttpPost("withdraw")]
        // [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Withdraw(TransactionDto dto)
        {
            // Extract JWT token from request header
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader))
                return Unauthorized("Missing token");

            var token = authHeader.Replace("Bearer ", "");

            await _service.Withdraw(dto.AccountId, dto.Amount, token);

            return Ok(new { message = "Withdrawal successful" });

            //try
            //{
            //    await _service.Withdraw(dto.AccountId, dto.Amount, token);

            //    return Ok(new
            //    {
            //        message = "Withdrawal successful"
            //    });
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new
            //    {
            //        message = ex.Message
            //    });
            //}
        }
    }
}
