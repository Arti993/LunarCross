using ScriptableObjects;

namespace Infrastructure.Services.LevelSettings
{
    public interface ILevelsSettingsNomenclature : IService
    {
        public Level GetLevelSettings(int levelNumber);

        public Level GetTutorialLevelSettings();
    }
}
