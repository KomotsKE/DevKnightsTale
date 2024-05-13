using KnightsTale.Sprites.Units;

namespace KnightsTale.Sprites.Weapons
{
    public class Crossbow : PlayerWeapon
    {
         
        public Timer LoadingTimer;
        public AnimationManager RangeAnimationManager;
        public Animation LoadingAnimation;
        public Texture2D LoadedTexture;
        public bool LoadSound;
       
        public bool IsLoaded;

        public AnimationManager MeleeAnimationManager;
        public Animation MeleeAnimation;
        public Timer MeleeAtackChargeTimer;
        public bool MeleeAtackCharged;

        public bool Arrowsavailability;




        public Crossbow(Texture2D texture, Texture2D loadedTexture, Vector2 position) : base(texture, position)
        {
            WeaponOffset = new Vector2(0, -5);
            LoadSound = true;
            Origin = new Vector2(Width / 2, Height);
            IsLoaded = true;
            MeleeAtackCharged = true;
            LoadedTexture = loadedTexture;
            LoadingAnimation = new Animation(Globals.Content.Load<Texture2D>("Weapon/Crossbow_2_anim"), 32, 0.2f, false);
            Texture2D MeleeAnimationTexture = Globals.Content.Load<Texture2D>("Weapon/small_sting");
            MeleeAnimation = new Animation(MeleeAnimationTexture,88,0.2f,false, new Vector2(15,MeleeAnimationTexture.Height/2));
            LoadingTimer = new Timer((int)(LoadingAnimation.FrameCount * LoadingAnimation.FrameTime) * 1000);
            MeleeAtackChargeTimer = new Timer(500);
        }
        public override void Update(Player player)
        {
            base.Update(player);
            RangeAtack(player);
            MeleeAtack(player);
        }

        public void RangeAtack(Player player)
        {
            if (player.ArrowsCount == 0) { Arrowsavailability = false; }
            else { Arrowsavailability = true; }
            if (!IsLoaded && Arrowsavailability)
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

            if (Globals.Mouse.LeftClick() && IsLoaded && Arrowsavailability)
            {
                GameGlobals.PassProjectile(new Arrow(Globals.Content.Load<Texture2D>("Projectiles/Crossbow_bolt"),
                    Position, player, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y))));
                IsLoaded = false;
                Globals.SoundManager.PlaySound("Shoot");
                player.ArrowsCount--;
            }
        }

        public void MeleeAtack(Player player)
        {
            if (!MeleeAtackCharged)
            {
                MeleeAtackChargeTimer.UpdateTimer();

                if (MeleeAtackChargeTimer.Check())
                {
                    MeleeAtackChargeTimer.ResetToZero(); MeleeAtackCharged = true;
                }
            }
            if (Globals.Mouse.RightClick() && MeleeAtackCharged && player.Stamina > 20)
            {
                GameGlobals.PassProjectile(new InvisibleMeleeAtack(Globals.Content.Load<Texture2D>("Projectiles/Crossbow_bolt"),
                    Position, player, Globals.ScreenToWorldSpace(new Vector2(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y))));
                MeleeAtackCharged = false;
                player.Stamina -= 20;
            }
        }
        public override void Draw()
        {
            if (!MeleeAtackCharged) { MeleeAnimationManager.PlayAnimation(MeleeAnimation); MeleeAnimationManager.Draw(Globals.SpriteBatch, Position, SpriteEffects.None, Rotation + MathHelper.ToRadians(-90), 0.4f, Depth, Color); }
            if (MeleeAtackCharged) { MeleeAnimationManager.RemoveAnimation(); }
            if (!Arrowsavailability) 
            { 
                base.Draw();
            }
            else
            {
                
                if (!IsLoaded) { RangeAnimationManager.PlayAnimation(LoadingAnimation); RangeAnimationManager.Draw(Globals.SpriteBatch, Position, SpriteEffects.None, Rotation, 0.5f, Depth, Color); }
                if (IsLoaded) { RangeAnimationManager.RemoveAnimation(); Globals.SpriteBatch.Draw(LoadedTexture, Position, null, Color.White, Rotation, Origin, 0.5f, SpriteEffects.None, Depth); }
                if (!Arrowsavailability) { base.Draw(); }
            }
            
        }
    }
}
