using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext : DbContext  // crtl . (autocomplete) to select using statements
    // dbcontext: responsible for managing the connection
    // with the relational database, contains one or more dbsets.
    // the object relational manager, (ORM), accepts LINQ commands and 
    // translates them into sql, grabs the data, and then maps them back 
    // into the entities.
    {
        // constructor that allows options to be passed as options when an instance
        // of the store context is created, options are passed up to the base 
        // class, in this case db context

        // the options for this constructor will be the database connection string
        public StoreContext(DbContextOptions options) : base(options){}

        // every entity needs a dbset property

        //         //<product> is pulled in from entities/product.cs
        //                   // products is the name of the table that will be created
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
    }
} 

// ctrl shift p, >sqlite: open database
// dotnet ef migrations add InitialCreate -o Data/Migrations
// dotnet ef database update
// dotnet ef database drop, y
// dotnet watch run