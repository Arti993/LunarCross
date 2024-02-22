using UnityEngine;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SampleScene_1.Load();
    }

    public void OnLevelsChooseButtonCLick()
    {
        LevelsChoose.Load();
    }
}
