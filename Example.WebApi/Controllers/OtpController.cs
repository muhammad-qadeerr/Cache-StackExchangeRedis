using Microsoft.AspNetCore.Mvc;
using OtpVerification;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/otp")]
    public class OtpController : ControllerBase
    {
        private readonly IOtpVerification _otpService;

        public OtpController(IOtpVerification otpService)
        {
            _otpService = otpService;
        }

        [HttpGet("generate")]
        public IActionResult GenerateOtp([FromQuery] string key)
        {
            var otp = _otpService.GenerateOtpAsync(key);
            return Ok(new { otp });
        }

        [HttpPost("validate")]
        public IActionResult ValidateOtp([FromQuery] string key, [FromQuery] string otp)
        {
            var isValid = _otpService.ValidateOtpAsync(key, otp);
            return Ok(new { isValid });
        }
    }
}
