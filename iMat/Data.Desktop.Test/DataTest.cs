using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Desktop;

namespace Data.Desktop.Test
{
    class DataTest
    {
        private static readonly string path = "test.db";
        static void Main(string[] args)
        {
            DataHandler dh = new DataHandler();

            //Mock credit card
            CreditCard card = new CreditCard();
            card.CardType = CardType.Visa;
            card.CardNumber = "1234";
            card.HoldersName = "Bosse Göransson";
            dh.GetCreditCards().Add(card);

            //Mock favorite
            List<ShoppingItem> favorites = new List<ShoppingItem>();

            dh.WriteToFile(path);

            dh = DataHandler.ReadFromFile(path);
        }
    }
}
