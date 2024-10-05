using UnityEngine;

public class LevelsSettingsNomenclature: ILevelsSettingsNomenclature
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
