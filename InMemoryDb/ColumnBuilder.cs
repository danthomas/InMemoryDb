using System.Reflection;

namespace InMemoryDb
{
    public class ColumnBuilder<T>
    {
        private static string _name;
        private static bool _isAuto;
        private static PropertyInfo _propertyInfo;


        public static ColumnBuilder<T> Builder
        {
            get
            {
                _name = "";
                _isAuto = false;
                _propertyInfo = null;
                
                return new ColumnBuilder<T>();
            }
        }

        public ColumnBuilder<T> ForProperty(string name)
        {
            _name = name;
            _propertyInfo = typeof(T).GetProperty(name);
            return this;
        }

        public ColumnBuilder<T> WithAuto()
        {
            _isAuto = true;
            return this;
        }

        public Column Build()
        {
            return new Column(_name, _isAuto, _propertyInfo);
        }
    }
}