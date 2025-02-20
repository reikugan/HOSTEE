using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSTEE.Migrations
{
    /// <inheritdoc />
    public partial class UserInheritsIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_User_UserId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramExercise_Exercises_ExerciseId",
                table: "ProgramExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramExercise_TrainingPrograms_TrainingProgramId",
                table: "ProgramExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercise_Exercises_ExerciseId",
                table: "TrainingExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercise_TrainingSessions_TrainingId",
                table: "TrainingExercise");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_User_UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_User_UserId",
                table: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingExercise",
                table: "TrainingExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramExercise",
                table: "ProgramExercise");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TrainingExercise",
                newName: "TrainingExercises");

            migrationBuilder.RenameTable(
                name: "ProgramExercise",
                newName: "ProgramExercises");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingExercise_ExerciseId",
                table: "TrainingExercises",
                newName: "IX_TrainingExercises_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramExercise_ExerciseId",
                table: "ProgramExercises",
                newName: "IX_ProgramExercises_ExerciseId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TrainingSessions",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TrainingPrograms",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Folders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Folders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Folders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Exercises",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises",
                columns: new[] { "TrainingId", "ExerciseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramExercises",
                table: "ProgramExercises",
                columns: new[] { "TrainingProgramId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_UserId",
                table: "Exercises",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramExercises_Exercises_ExerciseId",
                table: "ProgramExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramExercises_TrainingPrograms_TrainingProgramId",
                table: "ProgramExercises",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercises_Exercises_ExerciseId",
                table: "TrainingExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercises_TrainingSessions_TrainingId",
                table: "TrainingExercises",
                column: "TrainingId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_UserId",
                table: "TrainingPrograms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_AspNetUsers_UserId",
                table: "TrainingSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_UserId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramExercises_Exercises_ExerciseId",
                table: "ProgramExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramExercises_TrainingPrograms_TrainingProgramId",
                table: "ProgramExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercises_Exercises_ExerciseId",
                table: "TrainingExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercises_TrainingSessions_TrainingId",
                table: "TrainingExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_UserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSessions_AspNetUsers_UserId",
                table: "TrainingSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramExercises",
                table: "ProgramExercises");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Folders");

            migrationBuilder.RenameTable(
                name: "TrainingExercises",
                newName: "TrainingExercise");

            migrationBuilder.RenameTable(
                name: "ProgramExercises",
                newName: "ProgramExercise");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingExercises_ExerciseId",
                table: "TrainingExercise",
                newName: "IX_TrainingExercise_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramExercises_ExerciseId",
                table: "ProgramExercise",
                newName: "IX_ProgramExercise_ExerciseId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrainingSessions",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrainingPrograms",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingExercise",
                table: "TrainingExercise",
                columns: new[] { "TrainingId", "ExerciseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramExercise",
                table: "ProgramExercise",
                columns: new[] { "TrainingProgramId", "ExerciseId" });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_User_UserId",
                table: "Exercises",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramExercise_Exercises_ExerciseId",
                table: "ProgramExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramExercise_TrainingPrograms_TrainingProgramId",
                table: "ProgramExercise",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercise_Exercises_ExerciseId",
                table: "TrainingExercise",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercise_TrainingSessions_TrainingId",
                table: "TrainingExercise",
                column: "TrainingId",
                principalTable: "TrainingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_User_UserId",
                table: "TrainingPrograms",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSessions_User_UserId",
                table: "TrainingSessions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
