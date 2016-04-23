namespace DNSimple.Common.Json
{
    using Newtonsoft.Json.Serialization;

    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        // https://gist.github.com/jarroda/1288a6edd5754944fbe2

        protected override string ResolvePropertyName(string propertyName)
        {
            for (var i = propertyName.Length - 1; i > 1; i--)
            {
                if (char.IsUpper(propertyName[i]))
                {
                    propertyName = propertyName.Insert(i, "_");
                }
            }
            return propertyName.ToLower();
        }
    }
}