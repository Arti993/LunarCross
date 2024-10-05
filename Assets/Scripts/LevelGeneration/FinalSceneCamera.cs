using Ami.BroAudio;
using UnityEngine;

public class FinalSceneCamera : MonoBehaviour
{
    [SerializeField] private Transform _rocket;
    [SerializeField] private GameObject _designObjectsContainer;

    private Transform _transform;
    private float _startRocketPositionY;
    private float _startCameraPositionY;
    private float _maxCameraOffset = 28;
    private float _maxRocketOffset = 55;

    private void Start()
    {
        _transform = transform;
        _startRocketPositionY = _rocket.position.y;
        _startCameraPositionY = _transform.position.y;

        SoundID finalMusicTheme = DIServicesContainer.Instance.GetService<IAudioPlayback>().MusicContainer.FinalTheme;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMusic(finalMusicTheme);
    }
    
    private void FixedUpdate()
    {
        float rocketOffset = _rocket.position.y - _startRocketPositionY;

        if (rocketOffset < _maxCameraOffset)
        {
            Vector3 newCameraPosition = _transform.position;
            newCameraPosition.y = _startCameraPositionY + rocketOffset;
            _transform.position = newCameraPosition;
        }
        else if (rocketOffset > _maxRocketOffset)
        {
            StopRocketSounds();
            
            Destroy(_rocket.gameObject);
            
            Destroy(_designObjectsContainer);

            ShowCompleteGameWindow();
            
            Destroy(this);
        }
    }

    private void ShowCompleteGameWindow()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateGameComplete>();
    }

    private void StopRocketSounds()
    {
        SoundID rocketEngine = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.RocketEngine;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Stop(rocketEngine);
        
        SoundID rocketTurbine = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.RocketTurbine;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Stop(rocketTurbine);
    }
}
