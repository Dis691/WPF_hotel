﻿using System;
using System.Collections.Generic;

namespace hootel.Models;

public partial class Integration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IntegrationDetails { get; set; }
}
