using System.Collections;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Utilities.CoroutinesManagement
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}