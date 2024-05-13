using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(string name, int score)
    {
        _playerName.text = name;
        _playerScore.text = score.ToString();
    }
}
