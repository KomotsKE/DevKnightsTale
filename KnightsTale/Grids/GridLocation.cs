namespace KnightsTale.Grids
{
    public class GridLocation
    {
        public bool Filled, Impassable, UnPathable, HasBeenUsed, IsViewable;
        public float FScore, Cost, CurrentDistance, DistLef;
        public Vector2 Parent, Position;


        public GridLocation(float cost, bool filled)
        {
            this.Cost = cost;
            this.Filled = filled;
            UnPathable = false;
            HasBeenUsed = false;
            IsViewable = false;
        }

        public GridLocation(Vector2 position, float cost, bool filled, float fScore)
        {
            this.Cost = cost;
            this.Filled = filled;
            Impassable = filled;
            UnPathable = false;
            HasBeenUsed = false;
            IsViewable = false;

            Position = position;

            this.FScore = fScore;
        }

        public void SetNode(Vector2 parent, float fScore, float currentDist)
        {
            this.Parent = parent;
            this.FScore = fScore;
            this.CurrentDistance = currentDist;
        }

        public virtual void SwitchGridImassability(bool gridCondition)
        {
            Filled = gridCondition;
            Impassable = gridCondition;
        }
    }
}
