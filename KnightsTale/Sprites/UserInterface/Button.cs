namespace KnightsTale.Sprites
{
    public class Button : Sprite
    {
        private readonly Texture2D HoveredTexture;
        private readonly Texture2D ClickedTexture;
        private readonly PassObject ButtonClicked;
        public bool IsPressed, IsHovered;
        public object Info;
        public float Scale;

        public Button(Texture2D texture, Texture2D hoveredTexture, Texture2D clickedTexture, Vector2 position, PassObject buttonClicked, float scale) : base(texture, position)
        {
            ButtonClicked = buttonClicked;
            HoveredTexture = hoveredTexture;
            ClickedTexture = clickedTexture;
            IsPressed = false;
            Scale = scale;
            Width = (int)(texture.Width * Scale);
            Height = (int)(texture.Height * Scale);
        }

        public override void Update()
        {
            if (Hover())
            {
                if (!IsHovered) { Globals.SoundManager.PlaySound("UIHover"); }
                IsHovered = true;
                if (Globals.Mouse.LeftClick())
                {
                    IsHovered = false;
                    IsPressed = true;
                    Globals.SoundManager.PlaySound("UIClick");
                }
                else if (Globals.Mouse.LeftClickRelease())
                {
                    RunButtonClick();
                }
            }
            else
            {
                IsHovered = false;
            }
            if (!Globals.Mouse.LeftClick() && !Globals.Mouse.LeftClickHold())
            {
                IsPressed = false;
            }
        }

        public virtual void Reset()
        {
            IsPressed = false;
            IsHovered = false;
        }

        public virtual void RunButtonClick()
        {
            ButtonClicked?.Invoke(Info);
            Reset();
        }

        public bool Hover()
        {
            return HoverImage();
        }

        private bool HoverImage()
        {
            Vector2 mousePos = new(Globals.Mouse.NewMousePos.X, Globals.Mouse.NewMousePos.Y);

            if (Rectangle.Contains(mousePos))
            {
                return true;
            }
            return false;
        }

        public override void Draw()
        {
            if (IsPressed)
            {
                Globals.SpriteBatch.Draw(ClickedTexture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1);
            }
            else if (IsHovered)
            {
                Globals.SpriteBatch.Draw(HoveredTexture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1);
            }
            else { Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1); }
        }
    }
}
