using System.Collections.Generic;

namespace eWolfCommon.Collections
{
    public sealed class StoreUniqueList<T>
    {
        private readonly int _maxItems;
        private readonly List<T> _list = new List<T>();

        public StoreUniqueList(int maxItems)
        {
            _maxItems = maxItems;
        }

        public List<T> Items
        {
            get
            {
                return _list;
            }
        }

        public void Add(T item)
        {
            if (!_list.Contains(item))
                _list.Insert(0, item);

            if (_list.Count > _maxItems)
            {
                _list.RemoveAt(_maxItems);
            }
        }
    }
}