using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayFactory : IService
{
    public GameObject CreatePlayer(Vector3 position);

    public GameObject GetLevelsSettingsNomenclature();
}
