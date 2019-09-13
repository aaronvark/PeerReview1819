using System;

namespace Breakin
{
    public interface IScoreManager
    {
        int Score { get; set; }

        /// <summary>
        /// Invoked each time the Score property is changed
        /// </summary>
        event Action ScoreChanged;
    }

    public class ScoreManager : IScoreManager
    {
        private static IScoreManager instance;
        public static IScoreManager Instance => instance ?? (instance = new ScoreManager());

        public event Action ScoreChanged;

        private int score;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                // Let subscribers know the score was changed
                ScoreChanged?.Invoke();
            }
        }
    }
}