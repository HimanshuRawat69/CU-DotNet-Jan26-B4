using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    [HttpGet("gps")]
    [Authorize(Roles = "Manager")]
    public IActionResult GetGps()
    {
        var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        return Ok(new
        {
            success = true,
            message = "GPS data fetched successfully",
            user = new
            {
                id = userId,
                email = userEmail,
                role = role
            },
            data = new
            {
                Height=34.556,
                longitude = "79.500",
                vehicle = "Truck-11"
            }
        });
    }
}