using System;
using System.Collections.Generic;

namespace DrawMIK.DB.Entities;

public partial class PlayerLogin
{
    public int PlayerLoginId { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
}
