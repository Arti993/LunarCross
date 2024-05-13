using Agava.YandexGames;
using TMPro;
using UnityEngine;

public class TotalScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        int totalScore = AllServicesContainer.Instance.GetService<IGameProgress>().GetTotalScore();

        _scoreText.text = totalScore.ToString();
    }
}
