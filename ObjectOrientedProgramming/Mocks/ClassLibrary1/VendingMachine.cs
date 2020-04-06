using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SmsNotAvailableException : Exception { }

    public class VendingMachine
    {
        private ISmsGateway smsGateway;
        private int stock = 2;
        
        public VendingMachine(ISmsGateway smsGateway)
        {
            this.smsGateway = smsGateway;
        }

        public void Purchase()
        {
            stock--;

            if (stock == 0)
            {
                bool boss1Result = smsGateway.SendSms("0712345678", "Am ramas fara marfa, boss1!");
                
                if (!boss1Result)
                { 
                    bool boss2Result = smsGateway.SendSms("0722345678", "Am ramas fara marfa, boss2!");
                    
                    if (!boss2Result)
                    {
                        throw new SmsNotAvailableException();
                    }
                }                
            }
        }
    }
}
