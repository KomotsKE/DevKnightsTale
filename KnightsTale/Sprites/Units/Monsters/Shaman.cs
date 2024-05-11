using KnightsTale.Grids;
using KnightsTale.Models;
using KnightsTale.Objects;
using KnightsTale.Sprites.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Sprites.Units.Monsters
{
    public class Shaman : Monster
    {
        public float AttackRange;
        public bool IsAttacking;
        public Shaman(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup) : base(texture, position, collisionGroup, doorGroup)
        {
            Speed = 1.2f;
            WalkAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Shaman_run_anim"), 16, 0.1f, true);
            IdleAnimation = new Animation(Globals.Content.Load<Texture2D>("Monsters/Shaman_idle_anim"), 16, 0.1f, true);
            AttackRange = 50f;
            weapon = new Crossbow(Globals.Content.Load<Texture2D>("Weapon/Crossbow_2"), Globals.Content.Load<Texture2D>("Weapon/Loaded_Crossbow_2"), this.Position);
        }

        public override void Ai(Player player, SquareGrid grid)
        {
            if (player != null && Vector2.Distance(Position,player.Position) < AttackRange || IsAttacking)
            {
                IsAttacking = true;

                TimerToAttack.UpdateTimer();
                
                if (TimerToAttack.Check())
                {
                    GameGlobals.PassProjectile(new Arrow(Globals.Content.Load<Texture2D>("Projectiles/Crossbow_bolt"), Position, this, player.Position));
                    TimerToAttack.ResetToZero();
                    IsAttacking = false;
                }
            }
            else
            {
                base.Ai(player, grid);
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
