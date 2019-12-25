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
        GAME_OVER,
        SELECT_PLAYER,
        SELECT_LEVEL,
        QUIT_GAME,
        NEW_PLAYER,
        DELETE_PLAYER,
        BACK
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
                    {CHOOSE_PLAYER_FIRST, "ERST SPIELER WAEHLEN"},
                    {CONGRATULATIONS, "GLUECKWUNSCH"},
                    {GAME_COMPLETED, "SPIEL ABGESCHLOSSEN"},
                    {SCORE, "PUNKTE"},
                    {LEVEL_COMPLETED, "LEVEL ABGESCHLOSSEN"},
                    {OUCH, "AUA"},
                    {GAME_OVER, "SPIEL VORBEI"},
                    {SELECT_PLAYER, "SPIELER WAEHLEN"},
                    {SELECT_LEVEL, "LEVEL WAEHLEN"},
                    {QUIT_GAME, "SPIEL BEENDEN"},
                    {NEW_PLAYER, "NEUER SPIELER"},
                    {DELETE_PLAYER, "SPIELER LOESCHEN"},
                    {BACK, "ZURUECK"}
                }
            },
            {
                SystemLanguage.English,
                new Dictionary<Identifier, string>()
                {
                    {START_GAME, "START GAME"},
                    {PLAYER, "PLAYER"},
                    {CHOOSE_PLAYER_FIRST, "CHOOSE PLAYER FIRST"},
                    {CONGRATULATIONS, "CONGRATULATIONS"},
                    {GAME_COMPLETED, "GAME COMPLETED"},
                    {SCORE, "SCORE"},
                    {LEVEL_COMPLETED, "LEVEL COMPLETED"},
                    {OUCH, "OUCH"},
                    {GAME_OVER, "GAME OVER"},
                    {SELECT_PLAYER, "SELECT PLAYER"},
                    {SELECT_LEVEL, "SELECT LEVEL"},
                    {QUIT_GAME, "QUIT GAME"},
                    {NEW_PLAYER, "NEW PLAYER"},
                    {DELETE_PLAYER, "DELETE PLAYER"},
                    {BACK, "BACK"}
                }
            }
        };
}