using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labb3DatorGrafik.Service
{
    public class GameService
    {
        public Matrix WorldMatrix { get; set; }
        public GraphicsDevice graphics { get; set; }


        private static GameService instance;

        private GameService(){   }

        public static GameService Instance()
        {
            if (instance == null)
                instance = new GameService();
            return instance;
        }
    }
}
