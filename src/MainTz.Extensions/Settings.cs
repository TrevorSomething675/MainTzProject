using Microsoft.Extensions.Configuration;

namespace MainTz.Extensions
{
    /// <summary>
    /// Объект, с помощью которого мы достаём конфигурацию из appsettings и заносим её в дженерик T.
    /// </summary>
	public abstract class Settings
    {
        public static T Load<T>(string key, IConfiguration configuration = null)
        {
            var settings = (T)Activator.CreateInstance(typeof(T));

            SettingsFactory.Create(configuration).GetSection(key).Bind(settings, (x) => { x.BindNonPublicProperties = true; });

            return settings;
        }
    }
}
