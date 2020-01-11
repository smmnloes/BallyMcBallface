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
        BACK,
        ENTER_NAME,
        NEXT_LEVEL,
        RESTART_LEVEL,
        TOTAL_SCORE,
        BACK_TO_MENU,
        NEW_HIGHSCORE,
        DONATE,
        DONATE_PLEASE
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
                    {START_GAME, "Spiel starten"},
                    {PLAYER, "Spieler"},
                    {CHOOSE_PLAYER_FIRST, "Spieler wählen"},
                    {CONGRATULATIONS, "Glückwunsch"},
                    {GAME_COMPLETED, "Spiel abgeschlossen"},
                    {SCORE, "Punkte"},
                    {LEVEL_COMPLETED, "Level abgeschlossen"},
                    {OUCH, "Aua"},
                    {GAME_OVER, "Spiel vorbei"},
                    {SELECT_PLAYER, "Spieler wählen"},
                    {SELECT_LEVEL, "Level wählen"},
                    {QUIT_GAME, "Spiel beenden"},
                    {NEW_PLAYER, "Neuer Spieler"},
                    {DELETE_PLAYER, "Spieler löschen"},
                    {BACK, "Zurück"},
                    {ENTER_NAME, "Namen eingeben"},
                    {NEXT_LEVEL, "Nächstes Level"},
                    {RESTART_LEVEL, "Level neustarten"},
                    {TOTAL_SCORE, "Gesamtpunktzahl"},
                    {BACK_TO_MENU, "Zum Hauptmenü"},
                    {NEW_HIGHSCORE, "Neuer Highscore"},
                    {DONATE, "Spenden"},
                    {DONATE_PLEASE, "Spiele zu entwickeln macht mir viel Spaß, aber es ist auch harte Arbeit.\n" +
                                    "Wenn dir mein Spiel gefällt" +
                                    " und du mich unterstützen willst, " +
                                    "würde ich mich über eine kleine Spende sehr freuen.\nEs würde mir auch einen Anreiz geben, " +
                                    "neue Level und anderen Content zu erstellen!\n\nVielen Dank!"}
                }
            },
            {
                SystemLanguage.English,
                new Dictionary<Identifier, string>()
                {
                    {START_GAME, "Start Game"},
                    {PLAYER, "Player"},
                    {CHOOSE_PLAYER_FIRST, "Choose player first"},
                    {CONGRATULATIONS, "Congratulations"},
                    {GAME_COMPLETED, "Game completed"},
                    {SCORE, "Score"},
                    {LEVEL_COMPLETED, "Level completed"},
                    {OUCH, "Ouch"},
                    {GAME_OVER, "Game over"},
                    {SELECT_PLAYER, "Select player"},
                    {SELECT_LEVEL, "Select level"},
                    {QUIT_GAME, "Quit game"},
                    {NEW_PLAYER, "New player"},
                    {DELETE_PLAYER, "Delete player"},
                    {BACK, "Back"},
                    {ENTER_NAME, "Enter name"},
                    {NEXT_LEVEL, "Next level"},
                    {RESTART_LEVEL, "Restart level"},
                    {TOTAL_SCORE, "Total score"},
                    {BACK_TO_MENU, "Back to main menu"},
                    {NEW_HIGHSCORE, "New highscore"},
                    {DONATE, "Donate"},
                    {DONATE_PLEASE, "I love developing games, but it is also hard work.\n" +
                                    "If you like my game and want to support me, I would appreciate a small donation.\n" +
                                    "It would also give me incentive to create more levels and content for you to enjoy!\n\n" +
                                    "Thank you very much!"}
                }
            }
        };
}