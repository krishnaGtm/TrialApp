﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enza.DataAccess;
using SQLite;
using TrialApp.Common;
using TrialApp.Entities.Transaction;

namespace TrialApp.DataAccess
{
    public class SaveFilterRepository : Repository<SaveFilter>
    {
        public SaveFilterRepository() : base(DbPath.GetTransactionDbPath())
        {
        }

        public SaveFilterRepository(SQLiteAsyncConnection connection) : base(connection)
        {
        }

        public async Task<List<SaveFilter>> GetSaveFilterListAsync()
        {
            var data = await DbContextAsync().QueryAsync<SaveFilter>("Select * from SaveFilter");
            return data;
        }

        public async Task SaveFilterListAsync(List<SaveFilter> savefilterList)
        {
            foreach (var val in savefilterList)
            {
                if (val.FieldValue == "0")
                {
                    await DbContextAsync().ExecuteAsync("delete from SaveFilter where Field = ? ", val.Field);
                }
                else
                    await DbContextAsync().InsertOrReplaceAsync(val);
            }

        }
    }
}
