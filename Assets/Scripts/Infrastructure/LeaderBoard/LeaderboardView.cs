using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();

    public void ConstructiveLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
    {
        ClearLeaderBoard();

        foreach (LeaderboardPlayer player in leaderboardPlayers)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);
            leaderboardElementInstance.Initialize(player.Name, player.Score);
            
            _spawnedElements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderBoard()
    {
        foreach (var element in _spawnedElements)
        {
            Destroy(element);
        }
        
        _spawnedElements = new List<LeaderboardElement>();
    }

}
