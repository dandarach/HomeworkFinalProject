using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayProcess : IDisposable
    {
        public event Action OnWin;
        public event Action OnDefeat;

        public GameplayProcess()
        {
            Debug.LogWarning("GameplayProcess");
        }

        public void Run()
        {
            Debug.LogWarning("GameplayProcess.Run");
            OnDefeat?.Invoke();
        }

        public void Dispose()
        {
        }
    }
}
