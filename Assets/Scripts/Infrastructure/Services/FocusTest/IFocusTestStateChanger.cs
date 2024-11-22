namespace Infrastructure.Services.FocusTest
{
    public interface IFocusTestStateChanger : IService
    {
        public bool IsNeedToOpenPauseMenu { get; }
        public bool IsFocused { get; }
        public void EnableFocusTest();
        public void DisableFocusTest();
        public void EnablePauseMenuOpening();
        public void DisablePauseMenuOpening();

    }
}
