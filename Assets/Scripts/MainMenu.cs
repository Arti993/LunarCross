using UnityEngine;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    private LevelsSettingsNomenclature _levelsSettingsNomenclature = new LevelsSettingsNomenclature();

    public void OnPlayButtonClick()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        
        SampleScene_1.Load(_levelsSettingsNomenclature.GetLevelProperties(levelReached));
    }

    public void OnLevelsChooseButtonCLick()
    {
        LevelsChoose.Load();
    }
}
