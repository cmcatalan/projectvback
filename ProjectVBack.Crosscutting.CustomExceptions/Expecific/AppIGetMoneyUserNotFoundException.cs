using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVBack.Crosscutting.CustomExceptions.Expecific
{
    public class AppIGetMoneyUserNotFoundException : AppIGetMoneyException
    {
        public AppIGetMoneyUserNotFoundException() : base() { }

        public AppIGetMoneyUserNotFoundException(string message) : base(message) { }

        public AppIGetMoneyUserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
