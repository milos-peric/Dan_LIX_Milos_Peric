using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dan_LIX_Milos_Peric.ViewModels
{
    public class GameInfoViewModel : ObservableObject
    {
        private bool _gameLost;
        private bool _gameWon;

        public Visibility LostMessage
        {
            get
            {
                if (_gameLost)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        public Visibility WinMessage
        {
            get
            {
                if (_gameWon)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }

        public void GameStatus(bool win)
        {
            if (!win)
            {
                _gameLost = true;
                OnPropertyChanged("LostMessage");
            }

            if (win)
            {
                _gameWon = true;
                OnPropertyChanged("WinMessage");
            }
        }

        public void ClearInfo()
        {
            _gameLost = false;
            _gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }
    }
}
