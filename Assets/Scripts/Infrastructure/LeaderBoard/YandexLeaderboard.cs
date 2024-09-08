using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(LeaderboardView))]
public class YandexLeaderboard : MenuWindow
{
    private const string LeaderboardName = "LeaderBoard";
    private const string AnonymousName = "Anonymous";

    private LeaderboardView _leaderboardView;
    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();
    private LeaderboardPlayer _currentPlayer;

    protected override void Awake()
    {
        _leaderboardView = GetComponent<LeaderboardView>();
        
        base.Awake();
    }
    
    public void OpenWindow()
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
        
        if (PlayerAccount.IsAuthorized == false)
            return;

        PlayerAccount.RequestPersonalProfileDataPermission();

        Fill();
    }
    
    private void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;
        
        SetPlayerScore(DIServicesContainer.Instance.GetService<IGameProgress>().GetTotalScore());
        
        _leaderboardView.gameObject.SetActive(true);

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(name, score, rank));
            }

            _leaderboardView.ConstructiveLeaderboard(_leaderboardPlayers, _currentPlayer);
        });
    }

    private void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;
        
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result.score < score)
            {
                Leaderboard.SetScore(LeaderboardName, score);
                
                _currentPlayer = new LeaderboardPlayer(result.player.publicName, score, result.rank);
            }
            else
            {
                _currentPlayer = new LeaderboardPlayer(result.player.publicName, result.score, result.rank);
            }
        });
    }
}
