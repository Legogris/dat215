using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [Serializable()]
    public class ShoppingListHandler
    {
        public delegate void ShoppingListsChanged(ShoppingListsChangedEventArgs e);
        [field:NonSerialized()]
        public event ShoppingListsChanged Changed;

        private IList<FavoriteList> favLists;

        public ShoppingListHandler()
        {
            favLists = new List<FavoriteList>();
        }

        public IList<FavoriteList> GetItems()
        {
            return favLists;
        }

        public void Add(FavoriteList l)
        {
            favLists.Add(l);
            if (Changed != null)
            {
                Changed.Invoke(new ShoppingListsChangedEventArgs(ShoppingListsChangedEventArgs.ListEventType.Add, l));
            }
        }

        public void Add(IList<FavoriteList> list)
        {
            foreach (FavoriteList fl in list) {
                favLists.Add(fl);
            }
            if (Changed != null)
            {
                Changed.Invoke(new ShoppingListsChangedEventArgs(ShoppingListsChangedEventArgs.ListEventType.Add, null));
            }
        }

        public void Change(FavoriteList l)
        {
            Changed.Invoke(new ShoppingListsChangedEventArgs(ShoppingListsChangedEventArgs.ListEventType.Change, l));
        }

        public void Remove(FavoriteList l)
        {
            Changed.Invoke(new ShoppingListsChangedEventArgs(ShoppingListsChangedEventArgs.ListEventType.Remove, l));
        }

        public void Clear()
        {
            Changed.Invoke(new ShoppingListsChangedEventArgs(ShoppingListsChangedEventArgs.ListEventType.Clear, null));
        }
    }

    public class ShoppingListsChangedEventArgs
    {
        public enum ListEventType
        {
            Add,
            Change,
            Remove,
            Clear
        }

        public ListEventType Type { get; private set; }
        public FavoriteList List { get; private set; }

        public ShoppingListsChangedEventArgs(ListEventType e, FavoriteList list)
        {
            Type = e;
            List = list;
        }
    }
}
