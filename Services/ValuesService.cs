using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Services
{
    public class ValuesService
    {
        private readonly DataContext _db;

        public List<Value> GetAll()
        {
            var values = _db.Values.ToList();
            return values;
        }

        public ValuesService(DataContext db)
        {
            _db = db;
        }
    }
}