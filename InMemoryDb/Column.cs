using System.Reflection;

namespace InMemoryDb
{
    public class Column
    {
        public Column(string name, bool isAuto, PropertyInfo propertyInfo)
        {
            Name = name;
            IsAuto = isAuto;
            PropertyInfo = propertyInfo;
        }

        public string Name { get; set; }
        public bool IsAuto { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public object GetValue(object item)
        {
            return PropertyInfo.GetValue(item);
        }

        public void SetValue(object item, object value)
        {
            PropertyInfo.SetValue(item, value);
        }
    }
}