using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    private const int MaxRowsCount = 10;
    
    [SerializeField] private Transform _mainContainer;
    [SerializeField] private Transform _additionalContainer;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();
    private bool _isCurrentScoreInTop;

    private void Awake()
    {
        _additionalContainer.gameObject.SetActive(false);
    }

    public void ConstructiveLeaderboard(List<LeaderboardPlayer> leaderboardPlayers, LeaderboardPlayer currentPlayer)
    {
        ClearLeaderBoard();

        for (int i = 0; i < MaxRowsCount; i++)
        {
            LeaderboardPlayer player = leaderboardPlayers[i];
            
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _mainContainer);
            
            leaderboardElementInstance.Initialize(player.Name, player.Score, player.Rank);
            
            _spawnedElements.Add(leaderboardElementInstance);

            if (currentPlayer.Score >= player.Score)
                _isCurrentScoreInTop = true;
        }

        if (_isCurrentScoreInTop == false)
        {
            _additionalContainer.gameObject.SetActive(true);
            
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _additionalContainer);
            
            leaderboardElementInstance.Initialize(currentPlayer.Name, currentPlayer.Score, currentPlayer.Rank);
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
