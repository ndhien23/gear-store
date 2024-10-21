namespace KAMStoreNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InformationUsers", "District", c => c.String());
            AddColumn("dbo.InformationUsers", "Ward", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InformationUsers", "Ward");
            DropColumn("dbo.InformationUsers", "District");
        }
    }
}
