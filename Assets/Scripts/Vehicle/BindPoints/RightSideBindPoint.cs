namespace Vehicle.BindPoints
{
    public class RightSideBindPoint : OutsideBindPoint
    {
        private void Start()
        {
            AngleShift = -AngleShift;
        }
    }
}
