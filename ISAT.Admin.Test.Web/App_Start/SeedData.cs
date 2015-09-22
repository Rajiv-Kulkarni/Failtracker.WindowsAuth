using System;
using System.Linq;
using ISAT.Admin.Test.Web.Data;
using ISAT.Admin.Test.Web.Domain;
using ISAT.Admin.Test.Web.Infrastructure.Tasks;
using AutoMapper;
using Heroic.AutoMapper;
using AutoMapper.QueryableExtensions;



namespace ISAT.Admin.Test.Web
{
    public class SeedData : IRunAtStartup
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            //foreach (var user in _context.Users.Where(u => u.Id == "rskulka1").OrderBy(u => u.Id).Skip(1))
            //{
            //    _context.Users.Remove(user);
            //}

            //_context.SaveChanges();

            //var user1 = _context.Users.FirstOrDefault() ??
            //            _context.Users.Add(new User {Id="test", UserName = "Test User", Discriminator = "test"});

            //_context.SaveChanges();

        }
    }
}