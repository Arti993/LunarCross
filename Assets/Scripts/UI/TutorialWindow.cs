using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TutorialWindow : UIWindow
{
    private const float TopPosY = 0f;
    private const float BottomPosY = -1000; 
    private const float AnimationDuration = 0.5f;
    private const float DelayTimeBeforShowWindow = 2.3f;
    
    private RectTransform _rectTransform;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        Vector3 startPosition = _rectTransform.localPosition;

        startPosition.y = BottomPosY;

        _rectTransform.localPosition = startPosition;
        
        StartCoroutine(DelayBeforeShowWindow());
    }

    protected virtual void Continue()
    {
        PausePanelOutro();

        StartCoroutine(Destroy(AnimationDuration));

        Time.timeScale = 1f;
        
        GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseButton(uiRoot);
    }
    
    protected virtual void PauseGame()
    {
        DestroyPauseButton();

        PausePanelIntro();

        Time.timeScale = 0f;
    }


    private void PausePanelIntro()
    {
        _rectTransform.DOAnchorPosY(TopPosY, AnimationDuration).SetUpdate(true);
    }

    private void PausePanelOutro()
    {
        _rectTransform.DOAnchorPosY(BottomPosY, AnimationDuration).SetUpdate(true);
    }

    private IEnumerator DelayBeforeShowWindow()
    {
        yield return new WaitForSeconds(DelayTimeBeforShowWindow);

        PauseGame();
    }
}