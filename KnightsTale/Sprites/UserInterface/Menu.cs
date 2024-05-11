namespace KnightsTale.Sprites.UserInterface
{
    public class Menu : Sprite
    {
        public List<Button> Buttons;
        public bool RestartFlag;
        public Menu(Texture2D texture, Vector2 position) : base(texture, position)
        {
            RestartFlag = false;
        }

        public override void Update()
        {
            foreach (var button in Buttons)
            {
                button.Update();
            }
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color, Rotation, Origin, SpriteEffects.None, 1);
            foreach (var button in Buttons)
            {
                button.Draw();
            }
        }
    }
}
