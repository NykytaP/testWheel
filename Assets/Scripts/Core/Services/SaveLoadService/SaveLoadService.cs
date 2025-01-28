using Infrastructure.Data.Storages.BalanceStorage;
using Newtonsoft.Json;
using UnityEngine;
using static Newtonsoft.Json.JsonConvert;

namespace Core.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        public BalanceStorage LoadBalanceStorage()
        {
            BalanceStorage data;
            string key = typeof(BalanceStorage).ToString();

            if (PlayerPrefs.HasKey(key))
            {
                data = DeserializeObject<BalanceStorage>(PlayerPrefs.GetString(key));
            }
            else
            {
                data = new BalanceStorage();
                SaveBalanceStorage(data);
            }

            return data;
        }

        public void SaveBalanceStorage(BalanceStorage data)
        {
            PlayerPrefs.SetString(typeof(BalanceStorage).ToString(), SerializeData(data));
            PlayerPrefs.Save();
        }

        private string SerializeData(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            return SerializeObject(obj, settings);
        }
    }
}