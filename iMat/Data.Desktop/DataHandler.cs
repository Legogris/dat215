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
        private ShoppingListHandler favorites;
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
            favorites = new ShoppingListHandler();
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
            rootCategory = new ConcreteProductCategory(string.Empty, "Alla", null);
            ProductCategory greens = new ConcreteProductCategory("GREENS", "Frukt & Grönt", rootCategory);
            ProductCategory drinks = new ConcreteProductCategory("DRINKS", "Drycker", rootCategory);
            ProductCategory pantry = new ConcreteProductCategory("PANTRY", "Skafferi", rootCategory);

            ProductCategory berry = new ConcreteProductCategory("BERRY", "Bär", greens);
            ProductCategory bread = new ConcreteProductCategory("BREAD", "Bröd", rootCategory);
            ProductCategory cabbage = new ConcreteProductCategory("CABBAGE", "Kål", greens);
            ProductCategory citrus = new ConcreteProductCategory("CITRUS_FRUIT", "Citrusfrukter", greens);
            ProductCategory coldDrinks = new ConcreteProductCategory("COLD_DRINKS", "Kalla drycker", drinks);
            ProductCategory dairies = new ConcreteProductCategory("DAIRIES", "Mejeriprodukter", rootCategory);
            ProductCategory exoticFruit = new ConcreteProductCategory("EXOTIC_FRUIT", "Exotiska frukter", greens);
            ProductCategory fish = new ConcreteProductCategory("FISH", "Fisk", rootCategory);
            ProductCategory dries = new ConcreteProductCategory("FLOUR_SUGAR_SALT", "Mjöl, socker & salt", pantry);
            ProductCategory stoneFruit = new ConcreteProductCategory("FRUIT", "Stenfrukter", greens);
            ProductCategory herb = new ConcreteProductCategory("HERB", "Örter", greens);
            ProductCategory hotDrinks = new ConcreteProductCategory("HOT_DRINKS", "Varma drycker", drinks);
            ProductCategory meat = new ConcreteProductCategory("MEAT", "Kött", rootCategory);
            ProductCategory melons = new ConcreteProductCategory("MELONS", "Meloner", greens);
            ProductCategory nutsSeeds = new ConcreteProductCategory("NUTS_AND_SEEDS", "Nötter & frön", pantry);
            ProductCategory pasta = new ConcreteProductCategory("PASTA", "Pasta", pantry);
            ProductCategory pod = new ConcreteProductCategory("POD", "Baljväxter", pantry);
            ProductCategory rice = new ConcreteProductCategory("RICE", "Ris", pantry);
            ProductCategory roots = new ConcreteProductCategory("ROOT_VEGETABLE", "Rotfrukter", greens);
            ProductCategory sweet = new ConcreteProductCategory("SWEET", "Sött & snacks", rootCategory);
            ProductCategory veggieFruit = new ConcreteProductCategory("VEGETABLE_FRUIT", "Grönsaksfrukter", greens);

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
                    string[] products = ds[1].Split(',');
                    foreach (string ps in products)
                    {
                        ConcreteProductCategory cat = (ConcreteProductCategory)rootCategory.GetCategoryByID(ps);
                        cat.AddProduct(p);
                    }
                }
            }
            catch (Exception e)  {
            }
            finally { sr.Close(); }

            //Proper products for shoppingitems
            /*ShoppingCart newCart = new ShoppingCart();
            foreach (ShoppingItem si in cart.GetItems())
            {
                newCart.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
            }
            cart = newCart;
            */
            //List<FavoriteList> newFavs = new List<FavoriteList>();
            //foreach (FavoriteList list in favorites.GetItems())
            //{
            //    FavoriteList newList = new FavoriteList(list.Name);
            //    foreach (ShoppingItem si in list)
            //    {
            //        newList.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
            //    }
            //    newFavs.Add(newList);
            //}
            //favorites.Add(newFavs);

            /*List<Order> newOrders = new List<Order>();
            foreach (Order o in orders)
            {
                List<ShoppingItem> newItems = new List<ShoppingItem>();
                foreach (ShoppingItem si in o.Items)
                {
                    newItems.Add(new ShoppingItem(GetProduct(si.Product.ProductID), si.Amount));
                }
                newOrders.Add(new Order(newItems, o.Date, o.OrderNumber));
            }
            orders = newOrders;*/
                
        }

        public void WriteToFile(string path)
        {
            Stream stream = File.Open(path, FileMode.Create);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
            catch {
                
            }
            finally
            {
                stream.Close();
            }
        }

        public ShoppingListHandler GetFavorites()
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

        public ProductCategory GetRootCategory()
        {
            return rootCategory;
        }

        public ShoppingCart GetCart()
        {
            return cart;
        }
    }
}
