using TiledCS;

namespace KnightsTale.Models
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;

        private float zoom = 5;
        public float rotation = 0;

        public float CentreX
        {
            get { return centre.X; }
            set { centre.X = value; }
        }

        public float CentreY
        {
            get { return centre.Y; }
            set { centre.Y = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public float OffsetX
        {
            get { return Globals.ScreenWidth / zoom / 2; }
        }

        public float OffsetY
        {
            get { return Globals.ScreenHeight / zoom / 2; }
        }


        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, TiledMap map)
        {
            var dx = MathHelper.Clamp(position.X, OffsetX, map.Width * map.TileWidth - OffsetX);
            var dy = MathHelper.Clamp(position.Y, OffsetY, map.Height * map.TileHeight - OffsetY);
            centre = new Vector2(dx, dy);

            transform = Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)) *
                                                 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))
                                                * Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
