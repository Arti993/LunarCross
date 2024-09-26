using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LeaderboardView))]
public class YandexLeaderboard : UIWindow
{
    private const string LeaderboardName = "LeaderBoard";
    private const string AnonymousName = "Anonymous";

    private LeaderboardView _leaderboardView;
    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();
    private LeaderboardPlayer _currentPlayer;

    private void Awake()
    {
        _leaderboardView = GetComponent<LeaderboardView>();
    }
    
    public void OpenWindow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();
        
        if (PlayerAccount.IsAuthorized == false)
            return;

        PlayerAccount.RequestPersonalProfileDataPermission();
        
        Fill();
#endif
        
        PanelIntro();
    }

    public void Exit()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentSceneIndex == (int)SceneIndex.MainMenu)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();
        
        if(currentSceneIndex == (int)SceneIndex.Final)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateGameComplete>();
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
