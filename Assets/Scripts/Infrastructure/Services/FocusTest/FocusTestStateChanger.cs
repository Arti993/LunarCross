using Data;
using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Services.FocusTest
{
    public class FocusTestStateChanger : IFocusTestStateChanger
    {
        private IAssetsProvider _provider;
        private GameObject _focusTestObject;
        private FocusTest _focusTest;
        private bool _isNeedToOpenPauseMenu;

        public FocusTestStateChanger(IAssetsProvider provider)
        {
            _provider = provider;
        }

        public bool IsNeedToOpenPauseMenu => _isNeedToOpenPauseMenu;
        public bool IsFocused => _focusTest.IsFocused();
        

        public void EnableFocusTest()
        {
            if (_focusTestObject == null)
            {
                _focusTestObject = _provider.Instantiate(PrefabsPaths.FocusTest);
                _focusTest = _focusTestObject.GetComponent<FocusTest>();
            }
            
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
