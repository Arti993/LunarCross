using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelEndNextButton : CustomButton
{
    private Button _button;

    protected override void OnEnable()
    {
        _button = gameObject.GetComponent<Button>();
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete += OnScreenFaderDisable;
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }

    public void SetInterractable()
    {
        IsClickable = true;
        _button.interactable = true;
    }
    
    public void SetNotInterractable()
    {
        IsClickable = false;
        _button.interactable = false;
    }
}
