using Infrastructure.Services.GameProgress;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TotalScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private IGameProgress _gameProgress;

        private void Construct()
        {
            _gameProgress = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameProgress>();
        }

        private void Awake()
        {
            Construct();
        }

        private void Start()
        {
            int totalScore = _gameProgress.GetTotalScore();

            _scoreText.text = totalScore.ToString();
        }
    }
}
