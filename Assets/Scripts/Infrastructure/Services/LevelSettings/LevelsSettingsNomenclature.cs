using Data;
using ScriptableObjects;
using UnityEngine;

namespace Infrastructure.Services.LevelSettings
{
    public class LevelsSettingsNomenclature : ILevelsSettingsNomenclature
    {
        public Level GetLevelSettings(int levelNumber)
        {
            string levelPath = PrefabsPaths.LevelConfig + levelNumber.ToString();

            return Resources.Load<Level>(levelPath);
        }

        public Level GetTutorialLevelSettings()
        {
            return Resources.Load<Level>(PrefabsPaths.TutorialConfig);
        }
    }
}
