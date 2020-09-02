using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dan_LIX_Milos_Peric.ViewModels
{
    public enum SlideCategories
    {
        Pokemon
    }
    public class GameViewModel : ObservableObject
    {
        //Collection of slides we are playing with
        public SlideCollectionViewModel Slides { get; private set; }
        //Game information scores, attempts etc
        public GameInfoViewModel GameInfo { get; private set; }
        //Game timer for elapsed time
        public TimerViewModel Timer { get; private set; }
        //Category we are playing in
        public SlideCategories Category { get; private set; }

        public GameViewModel(SlideCategories category)
        {
            Category = category;
            SetupGame(category);
        }

        //Initialize game essentials
        private void SetupGame(SlideCategories category)
        {

            Slides = new SlideCollectionViewModel();

            GameInfo = new GameInfoViewModel();


            //Set attempts to the maximum allowed
            GameInfo.ClearInfo();

            //Create slides from image folder then display to be memorized
            Slides.CreateSlides("Assets/" + category.ToString());
            Slides.Memorize();


            //Game has started, begin count.
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1), GameInfo, Slides);
            Timer.Start();

            //Slides have been updated
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        //Slide has been clicked
        public void ClickedSlide(object slide)
        {
            if (Slides.canSelect)
            {
                var selected = slide as PictureViewModel;
                Slides.SelectSlide(selected);
            }

            if (!Slides.areSlidesActive)
            {
                Slides.CheckIfMatched();
            }

            GameStatus();
        }

        //Status of the current game
        private void GameStatus()
        {
            if (Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                TimerViewModel.LogVictoryToFile();
                Timer.Stop();
            }
        }

        //Restart game
        public void Restart()
        {
            SetupGame(Category);
        }
    }
}
