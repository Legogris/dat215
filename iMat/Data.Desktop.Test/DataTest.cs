using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Desktop;
using System.Diagnostics;

namespace Data.Desktop.Test
{
    class DataTest
    {
        private static readonly string dbPath = "config.db";
        private static readonly string productsPath = "products.txt";
        static void Main(string[] args)
        {
            DataHandler dh = new DataHandler(productsPath);

            //Mock credit card
            CreditCard card = new CreditCard();
            card.CardType = CardType.Visa;
            card.CardNumber = "1234";
            card.HoldersName = "Bosse Göransson";
            dh.GetCreditCards().Add(card);

            //Mock favorite
            FavoriteList fl = new FavoriteList("bananer i pyjamas");
            dh.GetFavorites().Add(fl);
            dh.WriteToFile(dbPath);
            dh = DataHandler.ReadFromFile(dbPath, productsPath);
        }
    }
}
