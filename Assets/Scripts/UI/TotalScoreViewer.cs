using Infrastructure;
using Infrastructure.Services.GameProgress;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TotalScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        private void Start()
        {
            int totalScore = DIServicesContainer.Instance.GetService<IGameProgress>().GetTotalScore();

            _scoreText.text = totalScore.ToString();
        }
    }
}
