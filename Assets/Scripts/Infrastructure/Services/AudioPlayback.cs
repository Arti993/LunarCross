using System;
using UnityEngine;

public class AudioPlayback : IAudioPlayback
{
  private const string MenuTheme = "MenuTheme";
  private const string AlienGrabsAstronaut = "AlienGrabsAstronaut";
  private const string PickUpAstronaut = "PickUpAstronaut";
  private const string AstronautGetIn = "AstronautGetIn";
  private const string ButtonPress = "ButtonPress";
  private const string StarCollecting = "StarCollecting";
  private const string Explosion = "Explosion";
  private const string Knock = "Knock";
  private const string GravityRay = "GravityRay";
  private const string Tornado = "Tornado";
  private const string AstronautInRay = "AstronautInRay";
  
  
  private readonly SoundsCollection _soundsCollection;
  
  public AudioPlayback(IAssets provider)
  {
    GameObject soundsCollectionObject = provider.Instantiate("Prefabs/SoundsCollection");

    if(soundsCollectionObject.TryGetComponent(out SoundsCollection soundsCollection) == false)
      throw new InvalidOperationException();

    _soundsCollection = soundsCollection;
  }

  public void PlayLevelTheme()
  {
    int levelNumber = AllServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();
    
    _soundsCollection.PlayMusic($"Level{levelNumber}Theme");
  }

  public void PlayMenuTheme()
  {
    _soundsCollection.PlayMusic(MenuTheme);
  }

  public void PlayAlienGrabsAstronautSound()
  {
    _soundsCollection.PlaySound(AlienGrabsAstronaut);
  }

  public void PlayPickUpAstronautSound()
  {
    _soundsCollection.PlaySound(PickUpAstronaut);
  }

  public void PlayAstronautGetInSound()
  {
    _soundsCollection.PlaySound(AstronautGetIn);
  }

  public void PlayButtonPressSound()
  {
    _soundsCollection.PlaySound(ButtonPress);
  }

  public void PlayStarCollectingSound()
  {
    _soundsCollection.PlaySound(StarCollecting);
  }

  public void PlayExplosionSound()
  {
    _soundsCollection.StopAllSounds();
    
    _soundsCollection.PlaySound(Explosion);
  }

  public void PlayKnockSound()
  {
    _soundsCollection.PlaySound(Knock);
  }

  public void PlayGravityRaySound()
  {
    _soundsCollection.PlaySound(GravityRay);
  }

  public void PlayTornadoSound()
  {
    _soundsCollection.PlaySound(Tornado);
  }

  public void PlayAstronautInRaySound()
  {
    _soundsCollection.PlaySound(AstronautInRay);
  }
}
