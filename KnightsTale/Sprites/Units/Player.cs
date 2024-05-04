using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Objects;

namespace KnightsTale.Sprites.Units
{
    public class Player : Unit
    {
        private readonly Weapon weapon;
        private HitBox hitBox { get { return new HitBox((int)position.X - 5, (int)position.Y - 4, 10, 4, Color.Red); ; } }

        public Player(Texture2D texture, Vector2 position, List<Rectangle> collisionGroup, List<Door> doorGroup, Vector2 spawnPoint) : base(texture, position, collisionGroup, doorGroup, spawnPoint)
        {
            this.weapon = new Weapon(Globals.Content.Load<Texture2D>("Weapon/weapon_bow"), position);
            UnitHitBox = new HitBox((int)position.X - 5, (int)position.Y - 4, 10, 4, Color.Red);
        }

        public override void Load()
        {
            walkAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_walk_anim"), 16, 0.1f, true);
            idleAnimation = new Animation(Globals.Content.Load<Texture2D>("Player/knight_m_idle_anim"), 16, 0.1f, true);
        }

        public void Movement()
        {
            inMove = false;
            float changeY = 0;
            if (Globals.MyKeyboard.GetPress("W"))
            { changeY -= 1; inMove = true; }
            if (Globals.MyKeyboard.GetPress("S"))
            { changeY += 1; inMove = true; }

            position.Y += changeY;


            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(hitBox.CollisionHitBox))
                    position.Y -= changeY;

            foreach (var door in DoorGroup)
                if (door.DoorHitBox.CollisionHitBox.Intersects(hitBox.CollisionHitBox) && !door.IsOpened)
                    position.Y -= changeY;

            float changeX = 0;
            if (Globals.MyKeyboard.GetPress("A"))
            { changeX -= 1; inMove = true; direction = Direction.left; }
            if (Globals.MyKeyboard.GetPress("D"))
            { changeX += 1; inMove = true; direction = Direction.right; }

            position.X += changeX;

            foreach (var rectangle in CollisionGroup)
                if (rectangle.Intersects(hitBox.CollisionHitBox))
                    position.X -= changeX;

            foreach (var door in DoorGroup)
                if (door.DoorHitBox.CollisionHitBox.Intersects(hitBox.CollisionHitBox) && !door.IsOpened)
                    position.X -= changeX;

            if (inMove)
                animationManager.PlayAnimation(walkAnimation);
            else
                animationManager.PlayAnimation(idleAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            UnitHitBox = new HitBox((int)position.X - 5, (int)position.Y - 4, 10, 4, Color.Red);
            Depth = (position.Y) * Globals.DeepCoef;
            weapon.Update(this);
            Movement();
            if (Globals.Mouse.LeftClick())
            {
                GameGlobals.PassProjectile(new Arrow(Globals.Content.Load<Texture2D>("Projectiles/weapon_arrow"),position,this, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.newMousePos.X,Globals.Mouse.newMousePos.Y))));
            }
        }

        public override void Draw()
        {
            base.Draw();
            weapon.Draw();
            UnitHitBox.Draw();
        }

    }
}
