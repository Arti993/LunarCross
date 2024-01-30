using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelsPropertiesNomenclature _levelsPropertiesNomenclature;

    public void OnPlayButtonClick()
    {
        SampleScene_1.Load(_levelsPropertiesNomenclature.GetLevelProperties(1));
    }
}
