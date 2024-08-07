using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioPlayback : IService
{
    public void PlayLevelTheme();
    public void PlayMenuTheme();
    public void PlayAlienGrabsAstronautSound();
    public void PlayPickUpAstronautSound();
    public void PlayAstronautGetInSound();
    public void PlayButtonPressSound();
    public void PlayStarCollectingSound();
    public void PlayExplosionSound();
    public void PlayKnockSound();
    public void PlayGravityRaySound();
    public void PlayTornadoSound();
    public void PlayAstronautInRaySound();
}
