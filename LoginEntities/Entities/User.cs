using System;
using System.Collections.Generic;

namespace LoginEntities.Entities;

public partial class User
{
    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
