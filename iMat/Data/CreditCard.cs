using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum CardType
    {
        Visa,
        MasterCard,
        Amex
    }
    [Serializable()]
    public class CreditCard
    {
        public CardType CardType { get; set; }
        public string CardNumber { get; set; }
        public string HoldersName { get; set; }
        public int ValidMonth { get; set; }
        public int ValidYear { get; set; }
        public int VerificationCode { get; set; }
    }
}
