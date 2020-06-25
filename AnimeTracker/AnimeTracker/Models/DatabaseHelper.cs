using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimeTracker.Interfaces;
using JikanDotNet;
using SQLite;

namespace AnimeTracker.Models
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserConfigEntity>().Wait();
            _database.CreateTableAsync<AnimeListEntity>().Wait();
            InitializeUserConfig();
        }

        private async void InitializeUserConfig()
        {
            var userConfigCount = await _database.Table<UserConfigEntity>().CountAsync();
            if (userConfigCount != 0) return;
            var userConfig = new List<UserConfigEntity>
            {
                new UserConfigEntity
                {
                    Key = "UserName",
                    Value = null
                },
                new UserConfigEntity
                {
                    Key = "LastListUpdateDate",
                    Value = null
                },
                new UserConfigEntity
                {
                    Key = "IsAutomaticListUpdateEnabled",
                    Value = "0"
                }
            };
            await _database.RunInTransactionAsync(db =>
            {
                db.InsertAll(userConfig);
            });
        }

        public async Task SaveUserConfig(KeyValuePair<string,string> userConfigEntry)
        {
            var entity = new UserConfigEntity(userConfigEntry);
            await _database.RunInTransactionAsync(db =>
            {
                db.InsertOrReplace(entity);
            });
        }

        public async Task SaveUserListAsync(IList<AnimeListEntry> animeListEntries, bool isCleanRequired = false)
        {
            var entity = animeListEntries.Select(animeListEntry => new AnimeListEntity(animeListEntry)).ToList();
            await _database.RunInTransactionAsync(db =>
            {
                if (isCleanRequired)
                    db.Table<AnimeListEntity>().Delete();
                db.InsertOrReplace(entity);
            });
        }

        public async Task<KeyValuePair<string,string>> GetUserConfigAsync(string key)
        {
            var entity = await _database.Table<UserConfigEntity>().FirstOrDefaultAsync(c => c.Key == key);
            return entity.ToKeyValuePair();
        }

        public async Task<IDictionary<string, string>> GetUserConfigListAsync()
        {
            var entity = await _database.Table<UserConfigEntity>().ToListAsync();
            return entity.ToDictionary(e => e.Key, e => e.Value);
        }

        public async Task<IList<AnimeListEntry>> GetUserListAsync()
        {
            var entity = await _database.Table<AnimeListEntity>().ToListAsync();
            return entity.Select(animeListEntity => animeListEntity.ToAnimeListEntry()).ToList();
        }
    }
}