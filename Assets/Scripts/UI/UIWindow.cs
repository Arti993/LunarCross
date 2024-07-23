using DG.Tweening;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    private const int MainMenuSceneIndex = 1;
    
    [SerializeField] protected RectTransform PanelRect;
    [SerializeField] protected float PanelTopPosY;
    [SerializeField] protected float PanelBottomPosY = -1000f;
    [SerializeField] protected float PanelAnimationDuration = 0.5f;

    public void GoToMainMenu()
    {
        PlayerPrefs.DeleteKey("SelectedLevelNumber");

        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(MainMenuSceneIndex);
    }

    protected void DestroyPauseButton()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        PauseButton pauseButton = uiRootObject.GetComponentInChildren<PauseButton>();

        if (pauseButton != null)
            Destroy(pauseButton.gameObject);
    }
    
    protected void PanelIntro()
    {
        PanelRect.DOAnchorPosY(PanelTopPosY, PanelAnimationDuration).SetUpdate(true);
    }

    protected void PanelOutro()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        sequence.Append(PanelRect.DOAnchorPosY(PanelBottomPosY, PanelAnimationDuration).SetUpdate(true));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}