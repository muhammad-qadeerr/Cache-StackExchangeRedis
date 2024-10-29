using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtpVerification
{
    public interface IOtpVerification
    {
        Task<string> GenerateOtpAsync(string userId);
        Task<bool> ValidateOtpAsync(string userId, string otp);
    }
}
