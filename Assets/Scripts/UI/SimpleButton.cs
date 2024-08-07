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
        if (AllServicesContainer.Instance.GetService<IScreenFader>().IsActive() == false)
            _button.interactable = true;

        AllServicesContainer.Instance.GetService<IScreenFader>().FadingComplete += OnScreenFaderDisable;
        AllServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }

    protected void OnScreenFaderEnable()
    {
        _button.interactable = false;
    }
    
    private void OnDisable()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadingComplete -= OnScreenFaderDisable;
        AllServicesContainer.Instance.GetService<IScreenFader>().FadingStart -= OnScreenFaderEnable;
    }

    private void OnScreenFaderDisable()
    {
        _button.interactable = true;
    }

    public void OnClick()
    {
        AllServicesContainer.Instance.GetService<IAudioPlayback>().PlayButtonPressSound();
    }
}
