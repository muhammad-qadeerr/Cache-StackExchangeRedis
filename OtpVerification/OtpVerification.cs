using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace OtpVerification
{
    public class OtpVerification : IOtpVerification
    {
        private readonly IDistributedCache _cache;
        private readonly OtpOptions _options;

        public OtpVerification(IDistributedCache cache, IOptions<OtpOptions> options)
        {
            _cache = cache;
            _options = options.Value;
        }

        public async Task<string> GenerateOtpAsync(string userId)
        {
            // Generate a random OTP code
            var otpCode = GenerateRandomCode(_options.CodeLength);

            // Store OTP in Redis with expiration
            var cacheKey = $"Otp_{userId}";
            var expirationTime = _options.ExpirationTime;

            await _cache.SetStringAsync(cacheKey, otpCode, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime
            });

            return otpCode;
        }

        public async Task<bool> ValidateOtpAsync(string userId, string otp)
        {
            var cacheKey = $"Otp_{userId}";
            var cachedOtp = await _cache.GetStringAsync(cacheKey);

            if (cachedOtp == null || cachedOtp != otp)
            {
                return false;
            }

            // OTP is valid, so remove it from cache
            await _cache.RemoveAsync(cacheKey);
            return true;
        }

        private string GenerateRandomCode(int length)
        {
            var random = new Random();
            var otpBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                if (_options.IsNumeric)
                {
                    otpBuilder.Append(random.Next(0, 10)); 
                }
                else
                {
                    otpBuilder.Append((char)random.Next('A', 'Z' + 1)); 
                }
            }

            return otpBuilder.ToString();
        }

    }
}
