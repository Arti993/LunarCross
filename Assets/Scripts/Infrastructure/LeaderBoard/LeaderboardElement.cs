using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string name, int score, int rank)
    {
        _playerName.text = name;
        _playerScore.text = score.ToString();
        _playerRank.text = new string($"{rank.ToString()}.");
    }
}
