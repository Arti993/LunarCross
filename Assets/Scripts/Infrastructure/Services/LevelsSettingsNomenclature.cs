using UnityEngine;

public class LevelsSettingsNomenclature: ILevelsSettingsNomenclature
{
    private const string Path = "Prefabs/LevelsConfigs/Level";

    public Level GetLevelSettings(int levelNumber)
    {
        string levelPath = Path + levelNumber.ToString();

        return Resources.Load<Level>(levelPath);
    }

    public Level GetTutorialLevelSettings()
    {
        return Resources.Load<Level>("Prefabs/LevelsConfigs/Tutorial");
    }
}
