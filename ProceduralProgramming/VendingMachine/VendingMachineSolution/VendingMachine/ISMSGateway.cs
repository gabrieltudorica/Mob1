using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public interface ISMSGateway
    {
        
        void SendSms(string phoneNumber, string message);
        
    }
}
