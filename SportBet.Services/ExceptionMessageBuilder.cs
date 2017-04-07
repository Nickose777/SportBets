using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
