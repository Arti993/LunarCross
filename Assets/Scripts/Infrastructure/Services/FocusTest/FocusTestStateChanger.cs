using Data;
using Infrastructure.Services.AssetsProvider;
using Reflex.Attributes;
using UnityEngine;

namespace Infrastructure.Services.FocusTest
{
    public class FocusTestStateChanger : IFocusTestStateChanger
    {
        private GameObject _focusTestObject;
        private FocusTest _focusTest;
        private bool _isNeedToOpenPauseMenu;

        public FocusTestStateChanger(IAssetsProvider provider)
        {
            _focusTestObject = provider.Instantiate(PrefabsPaths.FocusTest);
            _focusTest = _focusTestObject.GetComponent<FocusTest>();
        }

        public bool IsNeedToOpenPauseMenu => _isNeedToOpenPauseMenu;
        public bool IsFocused => _focusTest.IsFocused();
        

        public void EnableFocusTest()
        {
            _focusTestObject.SetActive(true);
        }

        public void DisableFocusTest()
        {
            _focusTestObject.SetActive(false);
        }

        public void EnablePauseMenuOpening()
        {
            _isNeedToOpenPauseMenu = true;
        }

        public void DisablePauseMenuOpening()
        {
            _isNeedToOpenPauseMenu = false;
        }
    }
}
