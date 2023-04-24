using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EVoucherMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        internal static readonly char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        internal static readonly char[] charsDigits = "1234567890".ToCharArray();
        internal static readonly char[] charsAlphabets =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        [HttpPost("GeneratePromoCode")]
        public async Task<ActionResult<string>> GeneratePromoCode(int EVoucherId)
        {
            var code = GetUniqueKey(11, chars);
            while (IsCodeAlreadyUsed(code))
            {
                code = GetUniqueKey(11, chars);
            }
            return Ok(code);
        }
        private static bool IsCodeAlreadyUsed(string code)
        {
            // Check if the code exists in a database or any other storage mechanism
            // Return true if it exists, false otherwise
            return false;
        }

        private string GetUniqueKey(int size, char[] chars)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            var digitCount = 0;
            var alphabetCount = 0;
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;
                
                result.Append(chars[idx]);
                if (charsDigits.Contains(chars[idx]))
                {
                    digitCount++;
                    if(digitCount == 6)
                    {
                        chars = RemoveStringFromCharsAlphabets(result.ToString(), charsAlphabets);
                    }
                }
                if (charsAlphabets.Contains(chars[idx]))
                {
                    alphabetCount++;
                    if (alphabetCount == 5)
                    {
                        chars = RemoveStringFromCharsAlphabets(result.ToString(), charsDigits);
                    }
                }
            }

            return result.ToString();
        }

        private char[] RemoveStringFromCharsAlphabets(string s, char[] chars)
        {
            string charString = new string(chars);
            charString = charString.Replace(s, "");
            chars = charString.ToCharArray();
            return chars;
        }

    }
}
