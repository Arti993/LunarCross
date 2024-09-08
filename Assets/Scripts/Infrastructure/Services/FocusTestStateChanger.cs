using UnityEngine;

public class FocusTestStateChanger : IFocusTestStateChanger
{
    private const string FocusTestPrefabPath = "Prefabs/FocusTest";

    private GameObject _focusTestObject;
    
    public FocusTestStateChanger(IAssetsProvider provider)
    {
        _focusTestObject = provider.Instantiate(FocusTestPrefabPath);
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
