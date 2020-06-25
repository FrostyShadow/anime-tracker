using System.Collections.Generic;
using SQLite;

namespace AnimeTracker.Models
{
    public class UserConfigEntity
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }

        public UserConfigEntity()
        {
            
        }

        public UserConfigEntity(KeyValuePair<string,string> userConfig)
        {
            Key = userConfig.Key;
            Value = userConfig.Value;
        }
    }

    public static class UserConfigEntityExtensions
    {
        public static KeyValuePair<string, string> ToKeyValuePair(this UserConfigEntity userConfigEntity)
        {
            return new KeyValuePair<string, string>(userConfigEntity.Key, userConfigEntity.Value);
        }
    }
}