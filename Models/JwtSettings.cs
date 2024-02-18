﻿namespace WebApi_AspNet_Core;

public class JwtSettings
{
    public string? Secret { get; set; }
    public int ExpirationHours { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }


}
