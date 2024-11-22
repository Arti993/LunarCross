using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.GameProgress
{
    public class GameProgress : IGameProgress
    {
        private const string SelectedLevelNumber = nameof(SelectedLevelNumber);
        private const string ReachedLevelNumber = nameof(ReachedLevelNumber);
        private const string Level1Result = nameof(Level1Result);
        private const string Level2Result = nameof(Level2Result);
        private const string Level3Result = nameof(Level3Result);
        private const string Level4Result = nameof(Level4Result);
        private const string Level5Result = nameof(Level5Result);
        private const string Level6Result = nameof(Level6Result);
        private const string Level7Result = nameof(Level7Result);
        private const string Level8Result = nameof(Level8Result);
        private const string Level9Result = nameof(Level9Result);
        private const string Level10Result = nameof(Level10Result);
        private const string Level11Result = nameof(Level11Result);
        private const string Level12Result = nameof(Level12Result);

        private Dictionary<int, string> _levelResultsTagsDictionary = new Dictionary<int, string>();

        public GameProgress()
        {
            string[] levelsResultsTags =
            {
                Level1Result, Level2Result, Level3Result, Level4Result, Level5Result,
                Level6Result, Level7Result, Level8Result, Level9Result, Level10Result,
                Level11Result, Level12Result
            };

            int levelNumber;

            for (int i = 0; i < levelsResultsTags.Length; i++)
            {
                levelNumber = i + 1;
                _levelResultsTagsDictionary.Add(levelNumber, levelsResultsTags[i]);
            }

            PlayerPrefs.SetInt(SelectedLevelNumber, 0);
            PlayerPrefs.Save();
        }

        public void SaveLevelProgress(int points)
        {
            int levelNumber = GetCurrentLevelNumber();

            if (_levelResultsTagsDictionary.TryGetValue(levelNumber, out string levelResultTag) == false)
                throw new InvalidOperationException();

            if (PlayerPrefs.GetInt(levelResultTag, 0) < points)
            {
                PlayerPrefs.SetInt(levelResultTag, points);

                SaveTotalScore();

                UnlockNextLevelIfNeed();
            }

            PlayerPrefs.Save();
        }

        public int GetCurrentLevelNumber()
        {
            int levelNumber = PlayerPrefs.GetInt(SelectedLevelNumber, 0);

            if (levelNumber == 0)
                levelNumber = PlayerPrefs.GetInt(ReachedLevelNumber, 1);

            return levelNumber;
        }

        public int GetLevelResult(int levelNumber)
        {
            if (_levelResultsTagsDictionary.TryGetValue(levelNumber, out string levelResultTag) == false)
                throw new InvalidOperationException();

            return PlayerPrefs.GetInt(levelResultTag, 0);
        }

        public int GetTotalScore()
        {
            int totalScore = 0;

            for (int i = 0; i < _levelResultsTagsDictionary.Count; i++)
            {
                totalScore += GetLevelResult(i + 1);
            }

            return totalScore;
        }

        public void ClearSaves()
        {
            foreach (var level in _levelResultsTagsDictionary)
            {
                PlayerPrefs.SetInt(level.Value, 0);
            }

            if (PlayerPrefs.HasKey("GameIsComplete"))
                PlayerPrefs.DeleteKey("GameIsComplete");

            PlayerPrefs.SetInt(ReachedLevelNumber, 1);

            PlayerPrefs.Save();
        }

        public bool IsCurrentLevelLast()
        {
            return GetCurrentLevelNumber() == _levelResultsTagsDictionary.Count;
        }

        public void SelectLevel(int levelNumber)
        {
            PlayerPrefs.SetInt(SelectedLevelNumber, levelNumber);

            PlayerPrefs.Save();
        }

        public void ClearSelectedLevel()
        {
            PlayerPrefs.DeleteKey(SelectedLevelNumber);
        }

        private void UnlockNextLevelIfNeed()
        {
            int completedLevelNumber = GetCurrentLevelNumber();

            int reachedLevelNumber = PlayerPrefs.GetInt(ReachedLevelNumber, 1);

            if (completedLevelNumber == _levelResultsTagsDictionary.Count)
            {
                PlayerPrefs.SetInt("GameIsComplete", 1);

                return;
            }

            if (completedLevelNumber == reachedLevelNumber)
            {
                reachedLevelNumber++;

                PlayerPrefs.SetInt(ReachedLevelNumber, reachedLevelNumber);

                PlayerPrefs.Save();
            }
        }

        private void SaveTotalScore()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
    YandexGame.NewLeaderboardScores("LeaderBoard", GetTotalScore());
#endif
        }
    }
}
