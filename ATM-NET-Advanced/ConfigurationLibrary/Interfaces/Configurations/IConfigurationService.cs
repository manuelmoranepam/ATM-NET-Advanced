using Microsoft.Extensions.Configuration;

namespace ConfigurationLibrary.Interfaces.Configurations;

public interface IConfigurationService
{
	IConfiguration GetConfiguration();
	T GetConfigurationSection<T>(string sectionName);
	T GetConfigurationValue<T>(string key);
}
