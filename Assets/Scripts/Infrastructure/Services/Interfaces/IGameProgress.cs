using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameProgress : IService
{
    public void ChangeLevelProgress(int points);

    public int GetCurrentLevelNumber();

    public int GetLevelResult(int levelNumber);

}
