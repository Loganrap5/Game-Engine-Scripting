using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Week11
{
    public class Week11 : MonoBehaviour
    {
        public UnityEvent GameOverEvent;

        [ContextMenu("Do Test GameOverEvent")]
        private void TestGameOverEvent()
        {
            GameOverEvent.Invoke();
        }
    }
}