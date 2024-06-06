using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Reflection.Metadata;

namespace ATLANT.Models
{
    public class FitnesContext: IdentityDbContext<User, IdentityRole<int>, int>
    {
        //////// Информация о подключении к БД
        protected readonly IConfiguration Configuration;
        public FitnesContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        ////////////////

        #region Constructor
        /*public FitnesContext(DbContextOptions<FitnesContext> options) : base(options)
        { }*/
        #endregion
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Coach> Coachs { get; set; }
        public virtual DbSet<Abonement> Abonement { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<TimeTable> TimeTable { get; set; }
        public virtual DbSet<VisitRegister> VisitRegister { get; set; }
        public virtual DbSet<TypeTraining> TypeTraining { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<Shedule> Shedule { get; set; }
        public virtual DbSet<DayWeek> DayWeek { get; set; }
        public virtual DbSet<PaymentVisit> PaymentVisit { get; set; }
        public virtual DbSet<VisitRegisterTimeTable> VisitRegisterTimeTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
