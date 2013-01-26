using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaUtility
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class AtlasHandler : Microsoft.Xna.Framework.GameComponent
    {
        public Dictionary<string, Rectangle> Dictionary { get; private set; }
        public Dictionary<string, Color[,]> DictOfColorArray { get { return _dictOfColorArray; } }
        public Texture2D Texture { get; private set; }
        public Color[] TexAsColorArray { get; private set; }
        private Dictionary<string, Color[,]> _dictOfColorArray;
        private readonly string tex;
        private readonly string dict;

        public AtlasHandler(Game game, string texName, string dictName)
            : base(game)
        {
            tex = texName;
            dict = dictName;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            Texture = Game.Content.Load<Texture2D>(tex);
            Dictionary = Game.Content.Load<Dictionary<string, Rectangle>>(dict);
            TexAsColorArray = new Color[Texture.Width*Texture.Height];
            Texture.GetData(TexAsColorArray);
            _dictOfColorArray = new Dictionary<string, Color[,]>();
            foreach (KeyValuePair<string, Rectangle> keyValuePair in Dictionary)
            {
                _dictOfColorArray.Add(keyValuePair.Key, UtilityMethods.TextureTo2DArray(this,keyValuePair.Value));
            }
            base.Initialize();
        }
    }
}
