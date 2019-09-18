using Breakin.FSM;
using Breakin.FSM.States;
using UnityEngine;

// Tasks of this class:
// - Keep track of level data
// - Start and setup the game/FSM
//   - Start in menu state
// - Watch events and transition FSM accordingly
//   - Listen for level win/lose
//   - Load new level
//   - Listen for level start
        
// STATES:
// ### Active while in the menu. The player can start a game from here
// Menu -> StartLevel
        
// ### StartLevel: Technically useful state which is active while the level is loading. Allows for loading new levels.
// StartLevel -> GameIdle
        
// ### GameIdle: game is currently playing but the ball is idle. Game is waiting for the player to release the ball and transition to GamePlaying
// GameIdle -> GamePlaying
        
// ### GamePlaying: game is currently playing (ball is moving). Player is playing the game. The game can be lost (GameLost), the ball can lose a life (GameIdle), or the level can be finished and the next one gets loaded (LoadLevel)
// GamePlaying -> GameIdle, GameLost, StartLevel
        
// ### GameLost: game has been lost. Player can start new game or return to main menu
// GameLost -> StartLevel, Menu

namespace Breakin.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        
        private StateMachine gameStateMachine;

        private void Start()
        {
            gameStateMachine = new StateMachine(new StartLevelState());
            
//            EventManager.LevelSetup += levelManager.
        }

        private void Update()
        {
            gameStateMachine.Update();
        }
    }
}