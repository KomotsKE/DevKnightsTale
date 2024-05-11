using KnightsTale.Managers;
using KnightsTale.Models;
using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites.Weapons
{
    public class Crossbow : Weapon
    {
        public bool IsLoaded;
        public AnimationManager AnimationManager;
        public Timer LoadingTimer;
        public Animation LoadAnimation;
        public Texture2D LoadedTexture;
        public bool LoadSound;
        public Crossbow(Texture2D texture, Texture2D loadedTexture, Vector2 position) : base(texture, position)
        {
            LoadSound = true;
            Origin = new Vector2(Width / 2, Height);
            IsLoaded = true;
            LoadedTexture = loadedTexture;
            LoadAnimation = new Animation(Globals.Content.Load<Texture2D>("Weapon/Crossbow_2_anim"), 32, 0.2f, false);
            LoadingTimer = new Timer((int)(LoadAnimation.FrameCount * LoadAnimation.FrameTime) * 1000);
        }
        public override void Update(Unit Unit)
        {
            base.Update(Unit);
            if (!IsLoaded)
            {
                if (LoadSound)
                {
                    Globals.SoundManager.PlaySound("Loading");
                    LoadSound = false;
                }
                LoadingTimer.UpdateTimer();

                if (LoadingTimer.Check())
                {
                    IsLoaded = true; LoadingTimer.ResetToZero(); Globals.SoundManager.PlaySound("LoadingEnd"); LoadSound = true;
                }
            }

            if (Globals.Mouse.LeftClick() && IsLoaded)
            {
                GameGlobals.PassProjectile(new Arrow(Globals.Content.Load<Texture2D>("Projectiles/Crossbow_bolt"),
                    Position, Unit, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y))));
                IsLoaded = false;
                Globals.SoundManager.PlaySound("Shoot");
            }
        }

        public override void Draw()
        {
            if (!IsLoaded) { AnimationManager.PlayAnimation(LoadAnimation); AnimationManager.Draw(Globals.SpriteBatch, Position, SpriteEffects.None, Rotation, 0.5f, Depth, Color); }
            if (IsLoaded)
            { AnimationManager.RemoveAnimation(); Globals.SpriteBatch.Draw(LoadedTexture, Position, null, Color.White, Rotation, Origin, 0.5f, SpriteEffects.None, Depth); }
        }
    }
}
