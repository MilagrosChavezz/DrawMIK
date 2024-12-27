using System;
using System.Collections.Generic;

namespace DrawMIK.DB.Entities;

public partial class Game
{
    public int GameId { get; set; }

    public string? GameName { get; set; }

    public virtual ICollection<GamePlayer>? GamePlayers { get; set; } = new List<GamePlayer>();

    public virtual ICollection<Line>? Lines { get; set; } = new List<Line>();
}
