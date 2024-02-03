using System;
using UnityEngine;
using IJunior.TypedScenes;

[RequireComponent(typeof(LevelsSettingsNomenclature))]
public class MainMenu : MonoBehaviour
{
    private LevelsSettingsNomenclature _levelsSettingsNomenclature;

    private void Start()
    {
        _levelsSettingsNomenclature = GetComponent<LevelsSettingsNomenclature>();
    }

    public void OnPlayButtonClick()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        
        SampleScene_1.Load(_levelsSettingsNomenclature.GetLevelSettings(levelReached));
    }

    public void OnLevelsChooseButtonCLick()
    {
        LevelsChoose.Load();
    }
}
