using System.Collections.Generic;
using System.Threading.Tasks;
using AnimeTracker.Models;
using JikanDotNet;

namespace AnimeTracker.Interfaces
{
    public interface IDatabaseHelper
    {
        Task SaveUserConfig(KeyValuePair<string,string> userConfigEntry);
        Task SaveUserListAsync(IList<AnimeListEntry> animeListEntries, bool isCleanRequired = false);
        Task<KeyValuePair<string,string>> GetUserConfigAsync(string key);
        Task<IDictionary<string, string>> GetUserConfigListAsync();
        Task<IList<AnimeListEntry>> GetUserListAsync();

    }
}