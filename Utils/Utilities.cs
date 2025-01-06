using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Youth.Utils
{
    internal class Utilities
    {
        public static JsonSerializer serializer
        {
            get
            {
                var serializer = new JsonSerializer();
                serializer.Error += (sender, args) =>
                {
                    if (args.ErrorContext.Error is JsonException)
                    {
                        args.ErrorContext.Handled = true;
                    }
                };
                return serializer;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                Console.WriteLine(e);
                return false;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Define a regular expression pattern for phone numbers.
            // This example assumes a basic 10-digit phone number with optional country code and dashes.
            // Feel free to modify the pattern based on the phone number format you want to validate.
            string pattern = @"^(\+\d{1,2}\s?)?(\(\d{3}\)|\d{3})[-.\s]?\d{3}[-.\s]?\d{4}$";

            // Use Regex.IsMatch to check if the phone number matches the pattern.
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
