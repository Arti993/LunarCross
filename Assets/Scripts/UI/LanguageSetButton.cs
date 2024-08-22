using UnityEngine;

public class LanguageSetButton : MonoBehaviour
{
    [SerializeField] private string _language;

    private MenuWindow _menuWindow;

    private void Awake()
    {
        _menuWindow = GetComponentInParent<MenuWindow>();
    }

    public void ChooseLanguage()
    {
        DIServicesContainer.Instance.GetService<ILocalization>().SetLanguage(_language);
        
        _menuWindow.Exit();
    }
}
