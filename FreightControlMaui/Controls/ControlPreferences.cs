using Newtonsoft.Json;

namespace FreightControlMaui.Controls
{
    public static class ControlPreferences
    {
        public static void AddKeyOnPreferences(string key, object contentOfObject)
        {
            var serializeContent = JsonConvert.SerializeObject(contentOfObject);

            Preferences.Set(key, serializeContent);
        }

        public static void RemoveKeyFromPreferences(string key)
        {
            if (Preferences.ContainsKey(key))
            {
                Preferences.Remove(key);
            }
        }

        public static string GetKeyOfPreferences(string key)
        {
            return Preferences.Get(key, "");
        }

        public static void UpdateKeyObjectOfPreference(string key, string value = "", object contentOfObject = null)
        {
            RemoveKeyFromPreferences(key);

            if(contentOfObject is not null)
            {
                var serializeContent = JsonConvert.SerializeObject(contentOfObject);

                Preferences.Set(key, serializeContent);
            }
            else
            {
                Preferences.Set(key, value);
            }            
        }
    }
}