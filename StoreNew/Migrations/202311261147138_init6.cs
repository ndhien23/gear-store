namespace KAMStoreNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InformationUsers", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.InformationUsers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.InformationUsers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.InformationUsers", "District", c => c.String(nullable: false));
            AlterColumn("dbo.InformationUsers", "Ward", c => c.String(nullable: false));
            AlterColumn("dbo.InformationUsers", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InformationUsers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.InformationUsers", "Ward", c => c.String());
            AlterColumn("dbo.InformationUsers", "District", c => c.String());
            AlterColumn("dbo.InformationUsers", "City", c => c.String());
            AlterColumn("dbo.InformationUsers", "Address", c => c.String());
            AlterColumn("dbo.InformationUsers", "FullName", c => c.String());
        }
    }
}
