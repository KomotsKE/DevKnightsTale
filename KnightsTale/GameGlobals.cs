namespace KnightsTale
{
    public class GameGlobals
    {
        private static bool isOver;
        private static bool isPaused;
        private static int score;
        private static PassObject passMonster;
        private static PassObject passProjectile;

        public static PassObject PassProjectile { get => passProjectile; set => passProjectile = value; }
        public static PassObject PassMonster { get => passMonster; set => passMonster = value; }
        public static int Score { get => score; set => score = value; }
        public static bool IsPaused { get => isPaused; set => isPaused = value; }
        public static bool IsOver { get => isOver; set => isOver = value; }
    }
}
