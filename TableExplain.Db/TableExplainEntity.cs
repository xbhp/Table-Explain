namespace IP_Boss.Dss.Db
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DssEntity : DbContext
    {
        public DssEntity()
            : base("name=DssEntity")
        {
        }

      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         
        }
    }
}
