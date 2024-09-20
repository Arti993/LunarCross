using UnityEngine;

public class FocusTestStateChanger : IFocusTestStateChanger
{
    private GameObject _focusTestObject;
    
    public FocusTestStateChanger(IAssetsProvider provider)
    {
        _focusTestObject = provider.Instantiate(PrefabsPaths.FocusTest);
    }
    
    public void EnableFocusTest()
    {
        _focusTestObject.SetActive(true);
    }

    public void DisableFocusTest()
    {
        _focusTestObject.SetActive(false);
    }
}
