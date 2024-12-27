using System;
using System.Collections.Generic;

namespace DrawMIK.DB.Entities;

public partial class GamePlayer
{
    public int GamePlayerId { get; set; }

    public int? GameId { get; set; }

    public int? PlayerId { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
