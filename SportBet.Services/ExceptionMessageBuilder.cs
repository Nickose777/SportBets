using System;
using System.Text;

namespace SportBet.Services
{
    static class ExceptionMessageBuilder
    {
        public static string BuildMessage(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            do
            {
                builder.AppendLine(ex.Message);
                ex = ex.InnerException;
            } while (ex != null);

            return "Internal server errors: " + builder.ToString();
        }
    }
}
