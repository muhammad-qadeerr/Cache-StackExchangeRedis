namespace OtpVerification
{
    public class OtpOptions
    {
        public int CodeLength { get; set; } = 6;          // Default length of OTP code
        public bool IsNumeric { get; set; } = true;       // Generate numeric or alphanumeric codes
        public TimeSpan ExpirationTime { get; set; } = TimeSpan.FromMinutes(5);
    }
}
