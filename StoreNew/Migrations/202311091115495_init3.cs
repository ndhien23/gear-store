namespace KAMStoreNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "UserID");
        }
    }
}
