using System.Collections.Generic;
using UnityEngine;

public class LevelsSettingsNomenclature : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    public Level GetLevelSettings(int levelNumber)
    {
        return _levels[levelNumber - 1];
    }
}
