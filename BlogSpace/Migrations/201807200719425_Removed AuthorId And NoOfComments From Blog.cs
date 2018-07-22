namespace BlogSpace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAuthorIdAndNoOfCommentsFromBlog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blog", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Blog", new[] { "AuthorId" });
            DropColumn("dbo.Blog", "NoOfComments");
            DropColumn("dbo.Blog", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blog", "AuthorId", c => c.String(maxLength: 128));
            AddColumn("dbo.Blog", "NoOfComments", c => c.Int(nullable: false));
            CreateIndex("dbo.Blog", "AuthorId");
            AddForeignKey("dbo.Blog", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
