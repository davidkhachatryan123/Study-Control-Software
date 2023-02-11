using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyControlSoftwareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedConfigToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturer_LecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturer_AspNetUsers_LecturerId",
                table: "Lecturer");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_StudentId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Facultys_FacultyId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturer",
                table: "Lecturer");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Lecturer",
                newName: "Lecturers");

            migrationBuilder.RenameIndex(
                name: "IX_Student_FacultyId",
                table: "Students",
                newName: "IX_Students_FacultyId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Lecturers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Lecturers",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_LecturerId",
                table: "Courses",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_AspNetUsers_LecturerId",
                table: "Lecturers",
                column: "LecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_StudentId",
                table: "Students",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Facultys_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Facultys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_LecturerId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_AspNetUsers_LecturerId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_StudentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Facultys_FacultyId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lecturers",
                table: "Lecturers");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Lecturers",
                newName: "Lecturer");

            migrationBuilder.RenameIndex(
                name: "IX_Students_FacultyId",
                table: "Student",
                newName: "IX_Student_FacultyId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Lecturer",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Lecturer",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lecturer",
                table: "Lecturer",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturer_LecturerId",
                table: "Courses",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturer_AspNetUsers_LecturerId",
                table: "Lecturer",
                column: "LecturerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_StudentId",
                table: "Student",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Facultys_FacultyId",
                table: "Student",
                column: "FacultyId",
                principalTable: "Facultys",
                principalColumn: "Id");
        }
    }
}
