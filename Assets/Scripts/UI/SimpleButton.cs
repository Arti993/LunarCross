using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SimpleButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    protected virtual void Start()
    {
        if (DIServicesContainer.Instance.GetService<IScreenFader>().IsActive() == false)
            _button.interactable = true;

        DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete += OnScreenFaderDisable;
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }
    
    private void OnDisable()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete -= OnScreenFaderDisable;
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart -= OnScreenFaderEnable;
    }

    public void OnClick()
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayButtonPressSound();
    }
    
    protected void OnScreenFaderEnable()
    {
        if (_button != null)
            _button.interactable = false;
    }


    private void OnScreenFaderDisable()
    {
        if (_button != null)
            _button.interactable = true;
    }

}