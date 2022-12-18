using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class UserDataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("data Source = DataFile.db");
        }

        public DbSet<User> Users
        {
            get; set; 
        }
        public DbSet<Group> Groups
        {
            get; set;
        }

        public DbSet<Password> passwords
        {
            get; set;
        }
    }
}
