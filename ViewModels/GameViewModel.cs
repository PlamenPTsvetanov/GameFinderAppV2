using GameFinderAppV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace GameFinderAppV2.ViewModels
{
    public class GameViewModel : FilterViewModel
    {
        private GameModel _game;
        private static DatabaseModel _db = new DatabaseModel();

        public GameViewModel()
        {
            _game = new GameModel();
        }

        public GameViewModel(GameModel g)
        {
            _game = g;
        }

        public string Title { get { return _game.Title; } }
        public short ReleaseYear { get{ return _game.ReleaseYear; } }
        public String Genres { get{ return _game.Genres; } }
        public String Platforms { get{ return _game.Platforms; } }
        public double Price { get{ return _game.Price; } }
        public double? Rating { get{ return _game.Rating; } }
        public String? AgeCategory { get{ return _game.AgeCategory; } }
        public String? Edition { get{ return _game.Edition; } }

        public static List<GameViewModel> filter(ref List<TextBox> generatedTextBoxes)
        {
            List<GameModel> gameModels = _db.Games.ToList();
            List<GameModel> filtered = FilterViewModel.filter<GameModel>(ref generatedTextBoxes, ref gameModels);

            List<GameViewModel> ret = new List<GameViewModel>();
            foreach (GameModel game in filtered)
            {
                ret.Add(new GameViewModel(game));
            }

            return ret;
        }
    }
}
