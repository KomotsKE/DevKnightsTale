namespace KnightsTale.Managers
{
    public class SceneManager
    {
        public Stack<IScene> sceneStack;

        public SceneManager()
        {
            sceneStack = new Stack<IScene>();
        }

        public void AddScene(IScene scene) { scene.Load(); sceneStack.Push(scene); }

        public void RemoveScene() { sceneStack.Pop(); }

        public IScene GetCurrentScene() { return sceneStack.Peek(); }

        public void SwithSceneTo(IScene scene) { RemoveScene(); AddScene(scene); scene.Update(); }

        public void NextScene() { RemoveScene(); GetCurrentScene().Update(); }
    }
}
