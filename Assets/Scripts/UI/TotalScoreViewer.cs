using Infrastructure.Services.GameProgress;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TotalScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private IGameProgress _gameProgress;

        [Inject]
        private void Construct(IGameProgress gameProgress)
        {
            _gameProgress = gameProgress;
        }

        private void Start()
        {
            int totalScore = _gameProgress.GetTotalScore();

            _scoreText.text = totalScore.ToString();
        }
    }
}
