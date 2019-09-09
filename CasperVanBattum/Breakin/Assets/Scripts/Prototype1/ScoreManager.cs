using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Prototype1
{
    public interface IScoreManager
    {
        int Score { get; set; }
        event Action OnScoreChanged;
    }
    
    public class ScoreManager : IScoreManager
    {
        private static IScoreManager instance;
        public static IScoreManager Instance => instance ?? (instance = new ScoreManager());

        public event Action OnScoreChanged;

        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnScoreChanged?.Invoke();
            }
        }
    }
}