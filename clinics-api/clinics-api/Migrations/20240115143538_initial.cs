using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinics_api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentStatuses",
                columns: table => new
                {
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisabal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentStatuses", x => x.AppointmentStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisabal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisabal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationDetailes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fees = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisabal = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                        column: x => x.AppointmentStatusId,
                        principalTable: "AppointmentStatuses",
                        principalColumn: "AppointmentStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppointmentStatuses",
                columns: new[] { "AppointmentStatusId", "IsDisabal", "name" },
                values: new object[,]
                {
                    { 1, false, "booked" },
                    { 2, false, "Disabled" },
                    { 3, false, "done" },
                    { 4, false, "Patient did'nt come" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" },
                    { "doctor", "00000000-0000-0000-0000-000000000000", "Doctor", "DOCTOR" },
                    { "patient", "00000000-0000-0000-0000-000000000000", "Patient", "PATIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "7d08a8ad-3846-4967-aa28-6dedce449eb9", "adminalaa@hotmail.com", true, false, null, "adminalaa@HOTMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEA8lmavICeB/xK4knIfwuOQAC4e683WX85ODBjeTQoEU/sll/lwI/UxE41ReV3O5Sg==", "1234567890", false, "6563fa81-28fa-4f92-9970-76cadd982af0", false, "adminAlaa" },
                    { "2", 0, "f29e365b-fa6a-4576-a378-354e957b57ff", "ahmadjaffal@hotmail.com", true, false, null, "AHMADJAFFAL@HOTMAIL.COM", "AHMAD JAFFAL", "AQAAAAEAACcQAAAAEHHahoJyUjkINClv2AJJq0PrAfvziwD+YBdYk+cxBBzfrM8AhJxnJxUAjqaNxaGgMQ==", "1234567890", false, "6cf6dd7c-c5b3-436a-bf58-7a82538dd2b1", false, "Ahmad Jaffal" },
                    { "3", 0, "78a0537c-a0f3-4fb8-b21a-3338e90ce6f3", "rana@hotmail.com", true, false, null, "RANA@HOTMAIL.COM", "RANA", "AQAAAAEAACcQAAAAEBUCHRu9+A+1J6l71drXoUpD6wn93lw0snZcpbs3q2fujviVrWrX/j7U/3cvSm7rMA==", "1234567890", false, "c31c8e3d-0d7a-4b6f-a9b1-0ef732e94ce1", false, "Rana" },
                    { "4", 0, "0eff093f-5df7-49b8-b413-2bd63da1fd6c", "omar@hotmail.com", true, false, null, "omar@HOTMAIL.COM", "OMAR", "AQAAAAEAACcQAAAAEAn65hzw5wvjhcv5700XNQSa8sHINenCCTsGzbSvY2HyFbt7UZXbraLadmJB5Iwv0Q==", "1234567890", false, "99d5f9ee-01b5-4a69-b0b3-50c078d4878a", false, "Omar" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName", "IsDisabal" },
                values: new object[,]
                {
                    { 1, "Cardiology", false },
                    { 2, "Nephrologist", false },
                    { 3, "Orthopedics", false },
                    { 5, "Gynecology", true },
                    { 6, "Pediatrics", false }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "IsDisabal", "LocationName" },
                values: new object[,]
                {
                    { 1, false, "Amman" },
                    { 2, true, "Amman,Alshmesani" },
                    { 3, false, "Amman,Abdoun" },
                    { 5, false, "Irbid" },
                    { 6, false, "Zarqa" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "admin", "1" },
                    { "doctor", "2" },
                    { "doctor", "3" },
                    { "doctor", "4" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "CloseTime", "DepartmentId", "Description", "Gender", "Img", "IsDisabal", "LocationDetailes", "LocationId", "Name", "OpeningTime", "Phone", "Specialization", "UserId", "fees" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 17, 25, 0, 0), 1, "", "male", "data:image/jpeg;base64,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", false, "Downtown,street 3", 1, "Ahmad", new TimeSpan(0, 8, 0, 0, 0), "123456", "children", "2", "25" },
                    { 2, new TimeSpan(0, 14, 0, 0, 0), 1, "", "female", "data:image/jpeg;base64,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", false, "JabalAmman,street 1", 1, "Rana", new TimeSpan(0, 10, 0, 0, 0), "123456", "children", "3", "45" },
                    { 3, new TimeSpan(0, 18, 15, 0, 0), 1, "", "male", "data:image/jpeg;base64,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", false, "JabalAmman,street 2", 1, "Omar", new TimeSpan(0, 11, 0, 0, 0), "123456", "children", "4", "15" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LocationId",
                table: "Doctors",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AppointmentStatuses");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
