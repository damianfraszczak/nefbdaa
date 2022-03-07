using NEFBDAACommons.Shared.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace NEFBDAACommons.Shared.Models
{
  public class BaseAppConfigViewModel
  {
    public string AppName { get; private set; }
    public string EmailHost { get; private set; }
    public string EmailPort { get; private set; }
    public string EmailUser { get; private set; }
    public string EmailPassword { get; private set; }
    public string EmailDefaultFrom { get; private set; }
    public string EmailSendGridApiKey { get; private set; }
    public string UserPasswordResetTemplate { get; private set; }
    public string UserRegistrationTemplate { get; private set; }
    public IEnumerable<ConfigViewModel> ConfigList { get; private set; }

    public BaseAppConfigViewModel()
    {

    }
    public BaseAppConfigViewModel(IEnumerable<ConfigViewModel> configList)
    {
      InitValues(configList);
    }
    public void InitValues(IEnumerable<ConfigViewModel> configList)
    {
      this.ConfigList = configList;
      configList?.Where(x => !string.IsNullOrEmpty(x.AppKey)).ToList().ForEach(x =>
        {
          var propertyName = x.AppKey.Replace("_", "");
          ReflectionHelper.SetPropValue(this, propertyName, x.Value);
        });
    }

    public ConfigViewModel GetConfig(string appKey)
    {
      return ConfigList.FirstOrDefault(x => x.AppKey == appKey);
    }
    public string GetConfigValue(string appKey)
    {
      return GetConfig(appKey)?.Value;
    }


    public bool IsEmailConfigValid()
    {
      return !(string.IsNullOrEmpty(EmailHost) ||
          string.IsNullOrEmpty(EmailUser) ||
          string.IsNullOrEmpty(EmailPassword) ||
          string.IsNullOrEmpty(EmailPort))
          ||
          !string.IsNullOrEmpty(EmailSendGridApiKey)
          ;
    }
  }
}
