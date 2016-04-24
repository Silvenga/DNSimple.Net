namespace DNSimple.Net.Common.Json
{
    using System;
    using System.Reflection;

    public static class CopierHelper
    {
        // http://stackoverflow.com/a/8724150

        public static void CopyPropertiesTo(this object source, object destination)
        {
            if (source == null || destination == null)
            {
                throw new Exception("Source or/and Destination Objects are null");
            }
            var typeDest = destination.GetType();
            var typeSrc = source.GetType();

            var srcProps = typeSrc.GetProperties();
            foreach (var srcProp in srcProps)
            {
                if (!srcProp.CanRead)
                {
                    continue;
                }
                var targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }

                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }
    }
}