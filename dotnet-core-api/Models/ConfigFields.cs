namespace TodoApi.Models
{
    public enum ConfigFields
    {
        MY_KEY,
        POSITION_TITLE,
        POSITION_NAME,
        DEFAULT_LOGLEVEL,
        HORSE,
        MOESTUIN_GROENTE_AARDAPPEL,
        MOESTUIN_FRUIT_FRAMBOOS
    }

    public static class ConfigFieldsExtensions
    {
        private static readonly Dictionary<ConfigFields, string> configFieldValues = new Dictionary<ConfigFields, string>
            {
                { ConfigFields.MY_KEY, "MyKey" },
                { ConfigFields.POSITION_TITLE, "Position:Title" },
                { ConfigFields.POSITION_NAME, "Position:Name" },
                { ConfigFields.DEFAULT_LOGLEVEL, "Logging:LogLevel:Default" },
                { ConfigFields.HORSE, "Horse" },
                { ConfigFields.MOESTUIN_GROENTE_AARDAPPEL, "Moestuin:Groente:Aardappel" },
                { ConfigFields.MOESTUIN_FRUIT_FRAMBOOS, "Moestuin:Fruit:Framboos" }
            };

        public static string GetValue(this ConfigFields constant)
        {
            return configFieldValues[constant];
        }
    }
}