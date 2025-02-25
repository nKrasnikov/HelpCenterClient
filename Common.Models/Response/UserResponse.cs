﻿
namespace Common.Models.Response;

public record UserResponse: BaseResponse
{
    public string? initials { get; set; }
    public string id { get; set; }
    public string userName { get; set; }
    public string normalizedUserName { get; set; }
    public string email { get; set; }
    public string normalizedEmail { get; set; }
    public bool emailConfirmed { get; set; }
    public string passwordHash { get; set; }
    public string securityStamp { get; set; }
    public string concurrencyStamp { get; set; }
    public string? phoneNumber { get; set; }
    public bool phoneNumberConfirmed { get; set; }
    public bool twoFactorEnabled { get; set; }
    public string? lockoutEnd { get; set; }
    public bool lockoutEnabled { get; set; }
    public int accessFailedCount { get; set; }
}
