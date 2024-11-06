using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />

    public partial class MakeUpdatedAtNullableForAuditableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Execute raw SQL to modify all tables with 'UpdatedAt' column
            migrationBuilder.Sql(@"
            DECLARE @sql NVARCHAR(MAX) = '';
            SELECT @sql += 'ALTER TABLE ' + QUOTENAME(TABLE_NAME) + ' ALTER COLUMN UpdatedAt DATETIME2 NULL;'
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE COLUMN_NAME = 'UpdatedAt' AND TABLE_SCHEMA = 'dbo';
            EXEC sp_executesql @sql;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Execute raw SQL to revert the 'UpdatedAt' column to non-nullable for all tables
            migrationBuilder.Sql(@"
            DECLARE @sql NVARCHAR(MAX) = '';
            SELECT @sql += 'ALTER TABLE ' + QUOTENAME(TABLE_NAME) + ' ALTER COLUMN UpdatedAt DATETIME2 NOT NULL;'
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE COLUMN_NAME = 'UpdatedAt' AND TABLE_SCHEMA = 'dbo';
            EXEC sp_executesql @sql;
        ");
        }
    }
}
