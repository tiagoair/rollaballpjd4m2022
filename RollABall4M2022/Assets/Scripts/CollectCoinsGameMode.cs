using UnityEngine;

namespace UnityTemplateProjects
{
    [CreateAssetMenu(fileName = "CollectCoinsGameMode", menuName = "GameModes/Collect Coins", order = 0)]
    public class CollectCoinsGameMode : GameMode
    {
        public int coinsToCollect;

        public float timeToLose;
        
        public override void UpdateGameMode(int coins = 0, float time = 0, bool arrivedAtLocation = false)
        {
            if (coins >= coinsToCollect)
            {
                gameState = GameState.Victory;
                OnGameModeStateChanged?.Invoke(gameState);
            }

            if (timeToLose > 0 && time >= timeToLose)
            {
                gameState = GameState.GameOver;
                OnGameModeStateChanged?.Invoke(gameState);
            }
        }
    }
}