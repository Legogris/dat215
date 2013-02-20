using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

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

        [NonSerialized()]
        private ProductCategory rootCategory;
        [NonSerialized()]
        private ISet<Product> products;

        public DataHandler(string productsPath)
        {
            favorites = new List<FavoriteList>();
            shippingAddresses = new List<ShippingAddress>();
            creditCards = new List<CreditCard>();
            orders = new List<Order>();
            cart = new ShoppingCart();

            LoadProducts(productsPath);
        }

        public static DataHandler ReadFromFile(string configPath, string productsPath)
        {
            DataHandler dh = null;
            FileStream stream = File.Open(configPath, FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                dh = (DataHandler)formatter.Deserialize(stream);

            }
            catch {
                dh = new DataHandler(productsPath);
            }
            finally
            {
                stream.Close();
            }
            dh.LoadProducts(productsPath);
            return dh;
        }

        private void LoadProducts(string productsPath)
        {
            rootCategory = new ProductCategory(string.Empty, "Alla", null);
            ProductCategory greens = new ProductCategory("GREENS", "Frukt & Grönt", rootCategory);
            ProductCategory drinks = new ProductCategory("DRINKS", "Drycker", rootCategory);
            ProductCategory pantry = new ProductCategory("PANTRY", "Skafferi", rootCategory);

            ProductCategory berry = new ProductCategory("BERRY", "Bär", greens);
            ProductCategory bread = new ProductCategory("BREAD", "Bröd", rootCategory);
            ProductCategory cabbage = new ProductCategory("CABBAGE", "Kål", greens);
            ProductCategory citrus = new ProductCategory("CITRUS_FRUIT", "Citrusfrukter", greens);
            ProductCategory coldDrinks = new ProductCategory("COLD_DRINKS", "Kalla drycker", drinks);
            ProductCategory dairies = new ProductCategory("DAIRIES", "Mejeriprodukter", rootCategory);
            ProductCategory exoticFruit = new ProductCategory("EXOTIC_FRUIT", "Exotiska frukter", greens);
            ProductCategory fish = new ProductCategory("FISH", "Fisk", rootCategory);
            ProductCategory dries = new ProductCategory("FLOUR_SUGAR_SALT", "Mjöl, socker & salt", pantry);
            ProductCategory stoneFruit = new ProductCategory("FRUIT", "Stenfrukter", greens);
            ProductCategory herb = new ProductCategory("HERB", "Örter", greens);
            ProductCategory hotDrinks = new ProductCategory("HOT_DRINKS", "Varma drycker", drinks);
            ProductCategory meat = new ProductCategory("MEAT", "Kött", rootCategory);
            ProductCategory melons = new ProductCategory("MELONS", "Meloner", greens);
            ProductCategory nutsSeeds = new ProductCategory("NUTS_AND_SEEDS", "Nötter & frön", pantry);
            ProductCategory pasta = new ProductCategory("PASTA", "Pasta", pantry);
            ProductCategory pod = new ProductCategory("POD", "Baljväxter", pantry);
            ProductCategory rice = new ProductCategory("RICE", "Ris", pantry);
            ProductCategory roots = new ProductCategory("ROOT_VEGETABLE", "Rotfrukter", greens);
            ProductCategory sweet = new ProductCategory("SWEET", "Sött & snacks", rootCategory);
            ProductCategory veggieFruit = new ProductCategory("VEGETABLE_FRUIT", "Grönsaksfrukter", greens);

            StreamReader sr = File.OpenText(productsPath);
            try
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] ds = line.Split(';');
                    Product p = new Product()
                    {
                        ProductID = int.Parse(ds[0]),
                        Name = ds[2],
                        Price = double.Parse(ds[3], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture.NumberFormat),
                        Unit = ds[4],
                        UnitSuffix = ds[4].Split('/')[1],
                        ImageName = ds[5]
                    };
                    ProductCategory cat = rootCategory.GetCategoryByID(ds[1]);
                    cat.AddProduct(p);
                }
            }
            finally { sr.Close(); }

            //Proper products for shoppingitems
            ShoppingCart newCart = new ShoppingCart();
            foreach (ShoppingItem si in cart.GetItems())
            {
                newCart.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
            }
            cart = newCart;
            
            List<FavoriteList> newFavs = new List<FavoriteList>();
            foreach (FavoriteList list in favorites)
            {
                FavoriteList newList = new FavoriteList(list.Name);
                foreach (ShoppingItem si in list)
                {
                    newList.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
                }
                newFavs.Add(newList);
            }
            favorites = newFavs;

            List<Order> newOrders = new List<Order>();
            foreach (Order o in orders)
            {
                List<ShoppingItem> newItems = new List<ShoppingItem>();
                foreach (ShoppingItem si in o.Items)
                {
                    newItems.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
                }
                newOrders.Add(new Order(newItems, o.Date, o.OrderNumber));
            }
            orders = newOrders;
                
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

        public Product GetProduct(int id)
        {
            return rootCategory.GetProduct(id);
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

        public ProductCategory GetRootCategory()
        {
            return rootCategory;
        }
    }
}
