using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayCycle : IDisposable
    {
        public GameplayCycle()
        {
            Debug.LogWarning("GameplayCycle");
        }

        //public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

        public void Run()
        {
            Debug.LogWarning("GameplayCycle.Run");
        }

        public void Dispose()
        {
            OnGameModeEnded();
        }

        private void OnGameModeEnded()
        {
            //if (_gameMode != null)
            //{
            //    _gameMode.Win -= OnGameModeWin;
            //    _gameMode.Defeat -= OnGameModeDefeat;
            //}
        }

        private void OnGameModeWin()
        {
            Debug.LogWarning("*** WIN ***");

            OnGameModeEnded();
            SceneManager.LoadScene("Menu");
        }

        private void OnGameModeDefeat()
        {
            Debug.LogWarning("*** DEFEAT ***");

            OnGameModeEnded();
            SceneManager.LoadScene("Menu");
        }
    }
}
