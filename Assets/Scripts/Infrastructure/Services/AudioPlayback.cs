using System;
using UnityEngine;

public class AudioPlayback : IAudioPlayback
{
  private const string MenuTheme = "MenuTheme";
  
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
}
