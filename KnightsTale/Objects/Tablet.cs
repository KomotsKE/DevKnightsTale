
using System.Diagnostics.CodeAnalysis;
using TiledCS;

namespace KnightsTale.Objects
{
    public class Tablet : TileObject
    {
        public readonly DialogBox TabletDialogBox;
        public readonly float DistanceToUse;

        public Tablet(Rectangle rectangle, string TabletText) : base(rectangle)
        {
            DistanceToUse = 20f;
            TabletDialogBox = new DialogBox
            {
                Text = TabletText
            };
        }

        public override void Update(Player player)
        {
            if (Vector2.Distance(player.Position, Position) < DistanceToUse && !TabletDialogBox.Active)
            {
                TabletDialogBox.Initialize();
                TabletDialogBox.Show();
            }
            else if ((Vector2.Distance(player.Position, Position) > DistanceToUse) && TabletDialogBox.Active) { TabletDialogBox.Hide();  }
            TabletDialogBox.Update();
        }

        public override void Draw()
        {
            if (TabletDialogBox.Active) { TabletDialogBox.Draw(Globals.SpriteBatch); }
        }
    }
}
