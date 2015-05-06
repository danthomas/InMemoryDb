using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InMemoryDb
{
    public class Table<T>
    {
        public Column[] Columns { get; set; }
        private readonly List<T> _items;
        private int _seed;

        public Table(params Column[] columns)
        {
            Columns = columns;
            _items = new List<T>();
            Clear();
        }

        public IEnumerable<T> Items { get { return _items; } }

        public int Count { get { return _items.Count; } }

        public T this[int index]
        {
            get { return _items[index]; }
        }

        public void Insert(T account)
        {
            _items.Add(account);
            var autoColumn = Columns.SingleOrDefault(x => x.IsAuto);
            if (autoColumn != null)
            {
                int seedValue = _seed++;

                if (autoColumn.PropertyInfo.PropertyType == typeof (short))
                {
                    autoColumn.SetValue(account, (short)seedValue);
                }
                else
                {
                    autoColumn.SetValue(account, seedValue);
                }
            }
        }

        public IDataReader Read()
        {
            return new ItemReader<T>(this);
        }

        public void Clear()
        {
            _seed = 1;
            _items.Clear();
        }

        public object GetValue(int column, int row)
        {
            return Columns[column].GetValue(_items[row]);
        }
    }
}