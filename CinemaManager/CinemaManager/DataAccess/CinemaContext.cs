namespace CinemaManager.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CinemaContext : DbContext
    {
        // Your context has been configured to use a 'CinemaManagerModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CinemaManager.DataAccess.CinemaManagerModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CinemaManagerModel' 
        // connection string in the application configuration file.
        public CinemaContext()
            : base("name=CinemaContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Cinema> Cinemas { get; set; }

        public virtual DbSet<Theater> Theaters { get; set; }




        public virtual DbSet<Showing> Showings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}