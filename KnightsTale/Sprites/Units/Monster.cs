using System.Threading.Tasks;

namespace KnightsTale.Sprites.Units
{
    public class Monster : Unit
    {
        private readonly float AggroRadius = 200f;
        public Timer TimerToAttack;
        private readonly Timer RePathTimer;
        private bool CurrentlyPathing;
        public Vector2 LastKnownPlayerLocatuin;
        public int Damage;
        public bool Chasing;
        public Monster(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, string type) : base(texture, position, collisionGroup, doorGroup,type)
        {
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
            RePathTimer.UpdateTimer();
            TimerToAttack.UpdateTimer();
            Chase(player, grid);
            MeleeAtack(player, grid);
            MoveUnit();
        }

        public virtual void MeleeAtack(Player player, SquareGrid grid)
        {
            if (Vector2.Distance(player.Position, Position) < grid.SlotDims.X)
            {
                InMove = false;
                if (TimerToAttack.Check())
                {
                    TimerToAttack.ResetToZero();
                    player.GetHit(Damage); PushPlayer(player);
                }
            }
        }

        public virtual void Chase(Player player, SquareGrid grid)
        {
            if (CanSeePlayer(player))
            {
                LastKnownPlayerLocatuin = player.Position;
                if (PathNodes == null || (PathNodes.Count == 0 && Position.X == MoveTo.X && Position.Y == MoveTo.Y) || RePathTimer.Check() && !CurrentlyPathing)
                {
                    Task repathTask = new(() =>
                    {
                        CurrentlyPathing = true;
                        PathNodes = FindPath(grid, grid.GetSlotFromPixel(player.Position));
                        if (PathNodes.Count > 0) { MoveTo = PathNodes[0]; PathNodes.RemoveAt(0); }

                        RePathTimer.ResetToZero();
                        CurrentlyPathing = false;
                    });
                    Chasing = false;
                    repathTask.Start();
                }
            }
            else if (!CanSeePlayer(player) && LastKnownPlayerLocatuin != Vector2.Zero)
            {
                PathNodes = FindPath(grid, grid.GetSlotFromPixel(LastKnownPlayerLocatuin));
                if (PathNodes.Count > 0) { MoveTo = PathNodes[0]; PathNodes.RemoveAt(0); }
                LastKnownPlayerLocatuin = Vector2.Zero;
                Chasing = false;
            }
            else
            {
                Chasing =false;
            }
        }

        public virtual bool CanSeePlayer(Player player)
        {
            Vector2 middleOfPlayer = new(player.Position.X, player.Position.Y);
            Vector2 middleOfEnemy = new(Position.X, Position.Y);

            Vector2 direction = middleOfPlayer - middleOfEnemy;
            float distanceToPlayer = Vector2.Distance(middleOfEnemy, middleOfPlayer);

            if (AggroRadius > distanceToPlayer)
            {
                if (direction != Vector2.Zero) direction.Normalize();

                Vector2 currentPos = middleOfEnemy;
                float lengthOfLine = 0.0f;

                while (lengthOfLine < distanceToPlayer + 1.0f)
                {
                    currentPos += direction;
                    foreach (var collison in CollisionGroup)
                        if (collison.Contains(currentPos))
                            return false;
                    foreach (var door in DoorGroup)
                        if (door.DoorHitBox.CollisionHitBox.Contains(currentPos) && !door.IsOpened)
                            return false;
                    lengthOfLine = Vector2.Distance(middleOfEnemy, currentPos);
                }
                return true;
            }
            return false;
        }

        public virtual void PushPlayer(Player player)
        {
            var PushDirection = (player.Position - Position);
            PushDirection.Normalize();
            float PushDistance = 5;
            if (player.DontStuck(PushDirection * PushDistance))
                player.Position += PushDirection * PushDistance;
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
