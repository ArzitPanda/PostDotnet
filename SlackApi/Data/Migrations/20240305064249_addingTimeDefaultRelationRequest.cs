using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlackApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingTimeDefaultRelationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOTPVerified",
                table: "UserVerification");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "UserVerification");

            migrationBuilder.DropColumn(
                name: "OTPGenerationTimestamp",
                table: "UserVerification");

            migrationBuilder.DropColumn(
                name: "OTPVerificationTimestamp",
                table: "UserVerification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOTPVerified",
                table: "UserVerification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "UserVerification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPGenerationTimestamp",
                table: "UserVerification",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPVerificationTimestamp",
                table: "UserVerification",
                type: "datetime2",
                nullable: true);
        }
    }
}
