using DatingApp_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Data
{
    public class DataContext : DbContext //make this available as public
    {
        //DataContext constructor
        public DataContext(DbContextOptions<DataContext> options) : base
        (options){} //ctor doesnt need anything inside it.


        //tell our data context class about our entities

        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
        
    }
}