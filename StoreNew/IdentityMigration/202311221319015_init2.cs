namespace KAMStoreNew.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "District", c => c.String());
            AddColumn("dbo.AspNetUsers", "Ward", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Ward");
            DropColumn("dbo.AspNetUsers", "District");
        }
    }
}
