using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace CManager.Application.Helpers
{
    public static class IdGenerator
    {
        // En enkel metod som bara gör en sak: Skapar ID
    public static Guid CreateId()
        {
            return Guid.NewGuid();
        }
    }
}
