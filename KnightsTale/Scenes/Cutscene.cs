namespace KnightsTale.Scenes
{
    public class Cutscene : IScene
    {
        public Sprite BackGround;
        public DialogBox CutsceneDialogBox;

        public void Load()
        {
            BackGround = new(Globals.Content.Load<Texture2D>("Backgrounds/felix_by_asstika_ddevu5v-pre"), Vector2.Zero,1920,1080);
        }

        public virtual void Update()
        {
            CutsceneDialogBox.Update();
            if (Globals.MyKeyboard.GetSinglePress("X") || CutsceneDialogBox.Active == false)
            {
                if (Globals.SceneManager.sceneStack.Count > 1)
                {
                    Globals.SceneManager.NextScene();
                }
            }
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            BackGround.Draw();
            CutsceneDialogBox.Draw(Globals.SpriteBatch);
            Globals.SpriteBatch.End();
        }
    }
}
