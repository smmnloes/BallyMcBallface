using System.Collections.Generic;
using UnityEngine;
using static I18N.Identifier;

public static class I18N
{
    public enum Identifier
    {
        START_GAME,
        PLAYER,
        CHOOSE_PLAYER_FIRST,
        CONGRATULATIONS,
        GAME_COMPLETED,
        SCORE,
        LEVEL_COMPLETED,
        OUCH,
        GAME_OVER
        
    }

    public static string Translate(Identifier identifier)
    {
        return translations[PlayerStats.instance.systemLanguage][identifier];
    }

    private static Dictionary<SystemLanguage, Dictionary<Identifier, string>> translations =
        new Dictionary<SystemLanguage, Dictionary<Identifier, string>>
        {
            {
                SystemLanguage.German,
                new Dictionary<Identifier, string>()
                {
                    {START_GAME, "SPIEL STARTEN"},
                    {PLAYER, "SPIELER"},
                    {CHOOSE_PLAYER_FIRST, "ERST SPIELER AUSWAEHLEN"},
                    {CONGRATULATIONS, "GLUECKWUNSCH"},
                    {GAME_COMPLETED, "SPIEL ABGESCHLOSSEN"},
                    {SCORE, "PUNKTE"},
                    {LEVEL_COMPLETED, "LEVEL ABGESCHLOSSEN"},
                    {OUCH, "AUA"},
                    {GAME_OVER, "SPIEL VORBEI"}
                }
            }
        };
}