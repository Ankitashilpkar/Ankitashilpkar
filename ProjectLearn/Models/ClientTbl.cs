using System;
using System.Collections.Generic;

namespace ProjectLearn.Models;

public partial class ClientTbl
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ConfirmPassword { get; set; }

    public int Clientid { get; set; }
}
