namespace KnightsTale.Models
{
    public interface IScene
    {
        public void Load();
        public void Update(GameTime gameTime);
        public void Draw();
    }
}
