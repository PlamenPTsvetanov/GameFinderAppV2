using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace GameFinderAppV2.Utils
{
    public class GameUtil
    {
        private GameModel _game;
        private static DatabaseModel _db = new DatabaseModel();

        public GameUtil()
        {
            _game = new GameModel();
        }

        public GameUtil(GameModel g)
        {
            _game = g;
        }

        public string Title { get { return _game.Title; } }
        public short ReleaseYear { get { return _game.ReleaseYear; } }
        public string Genres { get { return _game.Genres; } }
        public string Platforms { get { return _game.Platforms; } }
        public double Price { get { return _game.Price; } }
        public double? Rating { get { return _game.Rating; } }
        public string? AgeCategory { get { return _game.AgeCategory; } }
        public string? Edition { get { return _game.Edition; } }

        public static List<GameUtil> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<GameModel> gameModels = _db.Games.ToList();
            List<GameModel> filtered = FilterUtil.filter(ref generatedTextBoxes, ref gameModels);

            List<GameUtil> ret = new List<GameUtil>();
            foreach (GameModel game in filtered)
            {
                ret.Add(new GameUtil(game));
            }

            return ret;
        }
    }
}
