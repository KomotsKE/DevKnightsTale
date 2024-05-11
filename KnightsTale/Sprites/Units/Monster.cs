using KnightsTale.Grids;
using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;
using System.Threading.Tasks;

namespace KnightsTale.Sprites.Units
{
    public class Monster : Unit
    {
        private readonly float AggroRadius = 200f;
        public Timer TimerToAttack;
        private readonly Timer RePathTimer;
        private bool CurrentlyPathing;
        public int Damage;

        public Monster(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position, collisionGroup, doorGroup)
        {
            Damage = 1;
            CurrentlyPathing = false;
            TimerToAttack = new Timer(1000);
            RePathTimer = new Timer(300);
        }

        public virtual void Update(Player player, SquareGrid grid)
        {
            Ai(player, grid);
            Depth = (Position.Y) * Globals.DeepCoef;
            if (InMove)
                AnimationManager.PlayAnimation(WalkAnimation);
            else
                AnimationManager.PlayAnimation(IdleAnimation);
            base.Update();
        }

        public virtual void Ai(Player player, SquareGrid grid)
        {
            if (Vector2.Distance(player.Position, Position) < AggroRadius)
            {
                RePathTimer.UpdateTimer();

                if (PathNodes == null || (PathNodes.Count == 0 && Position.X == MoveTo.X && Position.Y == MoveTo.Y) || RePathTimer.Check())
                {
                    if (!CurrentlyPathing)
                    {
                        Task repathTask = new(() =>
                        {
                            CurrentlyPathing = true;
                            PathNodes = FindPath(grid, grid.GetSlotFromPixel(player.Position));
                            if (PathNodes.Count > 0) { MoveTo = PathNodes[0]; PathNodes.RemoveAt(0); }

                            RePathTimer.ResetToZero();
                            CurrentlyPathing = false;
                        });

                        repathTask.Start();
                    }
                }
                else
                {
                    MoveUnit();
                    InMove = true;
                }
                TimerToAttack.UpdateTimer();
                if (Vector2.Distance(player.Position, Position) < grid.SlotDims.X * 1.5f)
                {
                    InMove = false;
                    if (TimerToAttack.Check())
                    {
                        TimerToAttack.ResetToZero();
                        player.GetHit(Damage);
                        var PushDirection = (player.Position - Position);
                        PushDirection.Normalize();
                        float PushDistance = 3;
                        if (player.DontStuck(PushDirection * PushDistance))
                            player.Position += PushDirection * PushDistance;
                    }
                }
            }
            else
            {
                InMove = false;
            }

        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
