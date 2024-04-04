using Battleship;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Week7
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] public Button restartButton;

        [SerializeField] public Canvas menuScreen;

        [SerializeField] public TextMeshProUGUI youDiedText;

        [SerializeField] Player player;

        private void Awake()
        {
            menuScreen.enabled = false;
        }

        private void OnEnable()
        {
            restartButton.onClick.AddListener(RestartGame);

            GameManager.endGame.AddListener(ShowMenu);
        }
        private void OnDisable()
        {
            restartButton.onClick.RemoveListener(RestartGame);

            GameManager.endGame.RemoveListener(ShowMenu);
        }

        //For restart game button
        private void RestartGame()
        {
            GameManager.RestartGame();

            //hide menu
            menuScreen.enabled = false;
        }


        //show menu without you died text, and if players health = 0 do it with the text
        private void ShowMenu()
        {
            menuScreen.enabled = true;
            youDiedText.enabled = false;
            if(player.health <= 0)
            {
                menuScreen.enabled = true;
                youDiedText.enabled = true;
            }
        }


    }
}
