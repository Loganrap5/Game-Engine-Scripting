using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Week7
{
    public class GameManager : MonoBehaviour
    {
        //event for a quick menu to pop up
        public static UnityEvent endGame = new UnityEvent();

        public static UnityEvent restartGame = new UnityEvent();


        //for the  menu screen
        public static void EndGame()
        {
            endGame.Invoke();
        }

        //for restarting the game     
        public static void RestartGame()
        {
            restartGame.Invoke();
        }


    }
}