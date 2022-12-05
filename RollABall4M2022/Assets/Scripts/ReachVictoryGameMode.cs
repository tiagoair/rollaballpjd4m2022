using UnityEngine;

namespace UnityTemplateProjects
{
    [CreateAssetMenu(fileName = "ReachVictoryGameMode", menuName = "GameModes/Reach Victory", order = 0)]
    public class ReachVictoryGameMode : GameMode
    {
        public override void UpdateGameMode(int coins = 0, float time = 0, bool arrivedAtLocation = false)
        {
            if (arrivedAtLocation)
            {
                gameState = GameState.Victory;
                OnGameModeStateChanged?.Invoke(gameState);
            }
        }
    }
}