using System;
using System.Collections.Generic;

namespace DrawMIK.DB.Entities;

public partial class Player
{
    public int PlayerId { get; set; }

    public string? NombreJugador { get; set; }

    public string? PlayerLoginId { get; set; }

    public virtual ICollection<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();

    public virtual ICollection<Line> Lines { get; set; } = new List<Line>();
}
