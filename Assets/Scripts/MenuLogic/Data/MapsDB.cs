using System;
using System.Collections.Generic;

public static class MapsDB
{
    public static Dictionary<MapEnum, Map> MapDb = new Dictionary<MapEnum, Map>
    {
        [MapEnum.IceMap] = new Map(_name : "IceMap", _maxPlayers : 2),
        [MapEnum.Detroit] = new Map(_name : "Detroit", _maxPlayers : 2)
    };
}