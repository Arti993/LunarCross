namespace LevelGeneration.Entities
{
    public interface IPlaceableToVehicle
    {
        public bool TryPlaceToVehicle();

        public void UnplaceFromVehicle();
    }
}
