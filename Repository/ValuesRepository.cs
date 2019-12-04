using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Services
{
    //TODO Change this to Repository. This is talking to a DB so it's a Repository.
    public class ValuesRepository
    {
        private readonly DataContext _db;

        public async Task<List<Value>> GetAll()
        {
            var values = await _db.Values.ToListAsync();

            return values;
        }

        public async Task<Value> GetById(int id)
        {
            var value = await _db.Values.FirstOrDefaultAsync(v => v.Id == id);

            return value;
        }

        public ValuesRepository(DataContext db)
        {
            _db = db;
        }
    }
}