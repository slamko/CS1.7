using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    #region SettingProperties

    public static float DefaultMouseSensetivity { get; } = 1f;

    public static int MaxPlayers { get; } = 4;

    #endregion

    #region CustomPropertyKeys

    #region PlayerProps
    public static string Team { get; } = "t";
    public static string Kills { get;  } = "k";
    public static string Deaths { get; } = "d";
    public static string Health { get; } = "h";
    #endregion

    #endregion
}
