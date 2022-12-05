using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityTemplateProjects
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public abstract class GameMode : ScriptableObject
    {
        public Action<GameState> OnGameModeStateChanged;
        
        protected GameState gameState;
        public abstract void UpdateGameMode([Optional] int coins, [Optional] float time, 
            [Optional] bool arrivedAtLocation);

        public virtual GameState GetGameModeState()
        {
            return gameState;
        }
    }
}