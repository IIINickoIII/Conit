namespace Conit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCascadeDeletingDependensiesBetweenEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstructionPages", "InstructionId", "dbo.Instructions");
            DropForeignKey("dbo.InstructionPages", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Instructions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.PartProducts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.PartProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.InstructionPages", new[] { "InstructionId" });
            DropIndex("dbo.InstructionPages", new[] { "PartId" });
            DropIndex("dbo.Instructions", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.PartProducts", new[] { "ProductId" });
            DropIndex("dbo.PartProducts", new[] { "PartId" });
            AlterColumn("dbo.InstructionPages", "InstructionId", c => c.Int());
            AlterColumn("dbo.InstructionPages", "PartId", c => c.Int());
            AlterColumn("dbo.Instructions", "ProductId", c => c.Int());
            AlterColumn("dbo.Products", "CompanyId", c => c.Int());
            AlterColumn("dbo.PartProducts", "ProductId", c => c.Int());
            AlterColumn("dbo.PartProducts", "PartId", c => c.Int());
            CreateIndex("dbo.InstructionPages", "InstructionId");
            CreateIndex("dbo.InstructionPages", "PartId");
            CreateIndex("dbo.Instructions", "ProductId");
            CreateIndex("dbo.Products", "CompanyId");
            CreateIndex("dbo.PartProducts", "ProductId");
            CreateIndex("dbo.PartProducts", "PartId");
            AddForeignKey("dbo.InstructionPages", "InstructionId", "dbo.Instructions", "Id");
            AddForeignKey("dbo.InstructionPages", "PartId", "dbo.Parts", "Id");
            AddForeignKey("dbo.Instructions", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Products", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.PartProducts", "PartId", "dbo.Parts", "Id");
            AddForeignKey("dbo.PartProducts", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PartProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PartProducts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Instructions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InstructionPages", "PartId", "dbo.Parts");
            DropForeignKey("dbo.InstructionPages", "InstructionId", "dbo.Instructions");
            DropIndex("dbo.PartProducts", new[] { "PartId" });
            DropIndex("dbo.PartProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.Instructions", new[] { "ProductId" });
            DropIndex("dbo.InstructionPages", new[] { "PartId" });
            DropIndex("dbo.InstructionPages", new[] { "InstructionId" });
            AlterColumn("dbo.PartProducts", "PartId", c => c.Int(nullable: false));
            AlterColumn("dbo.PartProducts", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CompanyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructions", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.InstructionPages", "PartId", c => c.Int(nullable: false));
            AlterColumn("dbo.InstructionPages", "InstructionId", c => c.Int(nullable: false));
            CreateIndex("dbo.PartProducts", "PartId");
            CreateIndex("dbo.PartProducts", "ProductId");
            CreateIndex("dbo.Products", "CompanyId");
            CreateIndex("dbo.Instructions", "ProductId");
            CreateIndex("dbo.InstructionPages", "PartId");
            CreateIndex("dbo.InstructionPages", "InstructionId");
            AddForeignKey("dbo.PartProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PartProducts", "PartId", "dbo.Parts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Instructions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InstructionPages", "PartId", "dbo.Parts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InstructionPages", "InstructionId", "dbo.Instructions", "Id", cascadeDelete: true);
        }
    }
}
