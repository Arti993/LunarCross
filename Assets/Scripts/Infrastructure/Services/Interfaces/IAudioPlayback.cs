using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioPlayback : IService
{
    public void PlayLevelTheme();
    public void PlayMenuTheme();
}
