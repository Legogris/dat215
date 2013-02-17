using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data.Desktop
{
    [Serializable()]
    public class DataHandler
    {
        private List<FavoriteList> favorites;
        private List<ShippingAddress> shippingAddresses;
        private List<CreditCard> creditCards;
        private List<Order> orders;
        private User user;
        private ShoppingCart cart;

        public DataHandler()
        {
            favorites = new List<FavoriteList>();
            shippingAddresses = new List<ShippingAddress>();
            creditCards = new List<CreditCard>();
            orders = new List<Order>();
            cart = new ShoppingCart();
        }

        public static DataHandler ReadFromFile(string path)
        {
            Stream stream = File.Open(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                DataHandler dh = (DataHandler)formatter.Deserialize(stream);
                return dh;
            }
            finally
            {
                stream.Close();
            }
        }

        public void WriteToFile(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
            finally
            {
                stream.Close();
            }
        }

        public List<FavoriteList> GetFavorites()
        {
            return favorites;
        }

        public List<CreditCard> GetCreditCards()
        {
            return creditCards;
        }

        public List<ShippingAddress> GetShippingAddresses() 
        {
            return shippingAddresses; 
        }

        public User GetUser()
        {
            return user;
        }

        public List<Order> GetOrders()
        {
            return orders;
        }
    }
}
