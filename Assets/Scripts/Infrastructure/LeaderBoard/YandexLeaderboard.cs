using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    private const string AnonymousName = "Anonymous";

    //переделать в сервис
    [SerializeField] private LeaderboardView _leaderboardView;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

    public void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
    }

    private void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;
        
        SetPlayerScore(AllServicesContainer.Instance.GetService<IGameProgress>().GetTotalScore());
        
        _leaderboardView.gameObject.SetActive(true);

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(name, score));
            }

            _leaderboardView.ConstructiveLeaderboard(_leaderboardPlayers);
        });
    }

    public void Open()
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
        
        if (PlayerAccount.IsAuthorized == false)
            return;
        
        PlayerAccount.RequestPersonalProfileDataPermission();
        
        //движуху с всплыванием окна
        
        Fill();
    }
}