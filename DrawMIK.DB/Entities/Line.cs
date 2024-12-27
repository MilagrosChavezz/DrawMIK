using System;
using System.Collections.Generic;

namespace DrawMIK.DB.Entities;

public partial class Line
{
    public int LineId { get; set; }

    public int? GameId { get; set; }

    public int? StartX { get; set; }

    public int? StartY { get; set; }

    public int? EndX { get; set; }

    public int? EndY { get; set; }

    public string? Color { get; set; }

    public int? Thickness { get; set; }

    public int? PlayerId { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
