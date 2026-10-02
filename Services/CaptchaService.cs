using RCMS.IServices;
using RCMS.ModelHelpers;
using System;

namespace RCMS.Services
{
    public class CaptchaService : ICaptchaService
    {
        private static readonly Random random = new Random();

        private string GenerateRandomText(int length = 6)
        {
            try
            {
                //Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

      

    }
}
