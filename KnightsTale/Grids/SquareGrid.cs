using KnightsTale.Sprites;

namespace KnightsTale.Grids
{
    public class SquareGrid
    {

        public bool ShowGrid;

        public Vector2 SlotDims, GridDims, PhysicalStartPos, TotalPhysicalDims, CurrentHoverSlot;

        public Sprite GridImg;



        public List<List<GridLocation>> slots = new();


        public SquareGrid(Vector2 slotDims, Vector2 startPosition, Vector2 totalDims)
        {
            ShowGrid = false;

            SlotDims = slotDims;

            PhysicalStartPos = new Vector2((int)startPosition.X, (int)startPosition.Y);
            TotalPhysicalDims = new Vector2((int)totalDims.X, (int)totalDims.Y);

            CurrentHoverSlot = new Vector2(-1, -1);

            SetBaseGrid();

            GridImg = new Sprite(Globals.Content.Load<Texture2D>("Map/SpriteSheets/Grid"), SlotDims / 2, (int)slotDims.X - 2, (int)slotDims.Y - 2);
        }

        public virtual void Update()
        {
            CurrentHoverSlot = GetSlotFromPixel(Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y)));
        }


        public virtual Vector2 GetSlotFromPixel(Vector2 pixel)
        {
            Vector2 adjustedPos = pixel - PhysicalStartPos;

            Vector2 tempVec = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / SlotDims.X)), slots.Count - 1),
                Math.Min(Math.Max(0, (int)(adjustedPos.Y / SlotDims.Y)), slots[0].Count - 1));

            return tempVec;
        }

        public virtual void SetBaseGrid()
        {
            GridDims = new Vector2((int)(TotalPhysicalDims.X / SlotDims.X), (int)(TotalPhysicalDims.Y / SlotDims.Y));

            slots.Clear();
            for (int i = 0; i < GridDims.X; i++)
            {
                slots.Add(new List<GridLocation>());

                for (int j = 0; j < GridDims.Y; j++)
                {
                    slots[i].Add(new GridLocation(1, false));
                }
            }
        }

        public virtual Vector2 GetPosFromLoc(Vector2 LOC)
        {
            return PhysicalStartPos + new Vector2((int)LOC.X * SlotDims.X, (int)LOC.Y * SlotDims.Y);
        }

        public List<Vector2> GetPath(Vector2 START, Vector2 END, bool ALLOWDIAGNALS)
        {
            List<GridLocation> viewable = new List<GridLocation>(), used = new List<GridLocation>();

            List<List<GridLocation>> masterGrid = MasterGridUpdate(END);

            viewable.Add(masterGrid[(int)START.X][(int)START.Y]);

            while (viewable.Count > 0 && !(viewable[0].Position.X == END.X && viewable[0].Position.Y == END.Y))
            {
                TestAStarNode(masterGrid, viewable, used, END, ALLOWDIAGNALS);
            }


            List<Vector2> path = new List<Vector2>();

            if (viewable.Count > 0)
            {
                GridLocation currentNode = viewable[0];
                path.Clear();
                Vector2 tempPos;
                while (true)
                {
                    tempPos = GetPosFromLoc(currentNode.Position) + SlotDims / 2;
                    path.Add(new Vector2(tempPos.X, tempPos.Y));

                    if (currentNode.Position == START)
                    {
                        break;
                    }
                    currentNode = masterGrid[(int)currentNode.Parent.X][(int)currentNode.Parent.Y];
                }
                path.Reverse();
                if (path.Count > 1)
                {
                    path.RemoveAt(0);
                }
            }
            return path;
        }

        public List<List<GridLocation>> MasterGridUpdate(Vector2 end)
        {
            List<List<GridLocation>> masterGrid = new();
            for (int i = 0; i < slots.Count; i++)
            {
                masterGrid.Add(new List<GridLocation>());
                for (int j = 0; j < slots[i].Count; j++)
                {
                    bool impassable = slots[i][j].Impassable;

                    if (slots[i][j].Impassable || slots[i][j].Filled)
                    {
                        if (i != (int)end.X || j != (int)end.Y) impassable = true;
                    }

                    float cost = slots[i][j].Cost;

                    masterGrid[i].Add(new GridLocation(new Vector2(i, j), cost, impassable, 99999999));
                }
            }
            return masterGrid;
        }

        public void TestAStarNode(List<List<GridLocation>> masterGrid, List<GridLocation> viewable, List<GridLocation> used, Vector2 end, bool ALLOWDIAGNALS)
        {
            GridLocation currentNode;

            int[] dx = { 0, 0, -1, 1, -1, 1, -1, 1 };
            int[] dy = { -1, 1, 0, 0, -1, -1, 1, 1 };
            float[] cost = { 1, 1, 1, 1, (float)Math.Sqrt(2), (float)Math.Sqrt(2), (float)Math.Sqrt(2), (float)Math.Sqrt(2) };

            for (int i = 0; i < dx.Length; i++)
            {
                int newX = (int)viewable[0].Position.X + dx[i];
                int newY = (int)viewable[0].Position.Y + dy[i];

                if (newX >= 0 && newX < masterGrid.Count && newY >= 0 && newY < masterGrid[0].Count &&
                    !masterGrid[newX][newY].Impassable && !used.Any(g => g.Position.X == newX && g.Position.Y == newY))
                {
                    currentNode = masterGrid[newX][newY];
                    SetAStarNode(viewable, used, currentNode, new Vector2(viewable[0].Position.X, viewable[0].Position.Y), viewable[0].CurrentDistance, end, cost[i]);
                }
            }

            viewable[0].HasBeenUsed = true;
            used.Add(viewable[0]);
            viewable.RemoveAt(0);
        }

        public void SetAStarNode(List<GridLocation> viewable, List<GridLocation> used, GridLocation nextNode, Vector2 nextParent, float d, Vector2 target, float DISTMULT)
        {
            float f = d;
            float addedDist = (nextNode.Cost * DISTMULT);


            if (!nextNode.IsViewable && !nextNode.HasBeenUsed)
            {
                nextNode.SetNode(nextParent, f, d + addedDist);
                nextNode.IsViewable = true;

                SetAStarNodeInsert(viewable, nextNode);
            }
            else if (nextNode.IsViewable)
            {

                if (f < nextNode.FScore)
                {
                    nextNode.SetNode(nextParent, f, d + addedDist);
                }
            }
        }

        public virtual void SetAStarNodeInsert(List<GridLocation> LIST, GridLocation NEWNODE)
        {
            bool added = false;
            for (int i = 0; i < LIST.Count; i++)
            {
                if (LIST[i].FScore > NEWNODE.FScore)
                {
                    LIST.Insert(Math.Max(1, i), NEWNODE);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                LIST.Add(NEWNODE);
            }
        }

        public virtual void DrawGrid()
        {
            if (ShowGrid)
            {
                Color color;
                Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0));
                Vector2 botRight = GetSlotFromPixel(new Vector2(Globals.ScreenWidth, Globals.ScreenHeight));

                for (int j = (int)topLeft.X; j <= botRight.X && j < slots.Count; j++)
                {
                    for (int k = (int)topLeft.Y; k <= botRight.Y && k < slots[0].Count; k++)
                    {
                        if (CurrentHoverSlot.X == j && CurrentHoverSlot.Y == k) color = Color.Red;
                        else if (slots[j][k].Filled) color = Color.DarkGray;
                        else color = Color.White;

                        GridImg.Draw(PhysicalStartPos + new Vector2(j * SlotDims.X, k * SlotDims.Y) - new Vector2(SlotDims.X / 2 - 1, SlotDims.Y / 2 - 1), color);
                    }
                }
            }
        }
    }
}
