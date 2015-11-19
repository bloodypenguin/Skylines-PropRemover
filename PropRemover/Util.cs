using System.ComponentModel;


namespace PropRemover
{
    public static class Util
    {
        public static string GetPropertyDescription<T>(this T value, string propertyName)
        {
            var fi = value.GetType().GetProperty(propertyName);
            var attributes =
         (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return propertyName;
        }
    }
}