using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.Model.Migrations
{
    public partial class QuestionOptionsAndQuestionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateValue",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionaryId",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueType",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Questionaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionaryId",
                table: "Answers",
                column: "QuestionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questionaries_QuestionaryId",
                table: "Answers",
                column: "QuestionaryId",
                principalTable: "Questionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql ( "SET IDENTITY_INSERT [Questions] ON" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 1, 'Введите имя' , 1 )" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 2, 'Введите возраст' , 2 )" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 3, 'Введите пол' , 3 )" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 4, 'Введите дату рождения' , 5 )" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 5, 'Введите семейное положение' , 3 )" );
            migrationBuilder.Sql ( "INSERT INTO [Questions] ([Id], [Title], [ValueType] ) VALUES ( 6, 'Любите ли вы программировать' , 4 )" );
            migrationBuilder.Sql ( "SET IDENTITY_INSERT [Questions] OFF" );

            migrationBuilder.Sql ( "INSERT INTO [QuestionOptions] ([Title], [QuestionId] ) VALUES ( 'Мужской' , 3 )" );
            migrationBuilder.Sql ( "INSERT INTO [QuestionOptions] ([Title], [QuestionId] ) VALUES ( 'Женский' , 3 )" );
            migrationBuilder.Sql ( "INSERT INTO [QuestionOptions] ([Title], [QuestionId] ) VALUES ( 'В браке' , 5 )" );
            migrationBuilder.Sql ( "INSERT INTO [QuestionOptions] ([Title], [QuestionId] ) VALUES ( 'Не в браке' , 5 )" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questionaries_QuestionaryId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "Questionaries");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionaryId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "DateValue",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionaryId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Answers");
        }
    }
}
