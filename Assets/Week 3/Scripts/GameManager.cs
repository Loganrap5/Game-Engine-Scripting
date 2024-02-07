using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Battleship
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int[,] grid = new int[,]
        {
            { 1,1,0,0,1 },
            { 0,0,0,0,0 },
            { 0,0,1,0,1 },
            { 1,0,1,0,0 },
            { 1,0,1,0,1 }
        };

        //where player has fired
        private bool[,] hits;

        //total rows and columns 
        private int nRows;
        private int nCols;

        //current row/column we are on
        private int row;
        private int col;

        //correctly hit ships
        private int score;

        private int time;

        //parent of all cells
        [SerializeField] Transform gridRoot;

        //template used to populate grid
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;

        private void Awake()
        {
            //initialize rows/cols to help with operations
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);
            //make identical 2d array for bool
            hits = new bool[nRows, nCols];

            //populate the grid using a loop
            //needs to execute as many times to full up the grid
            //can figure that out by calculating rows * cols
            for (int i = 0; i < nRows * nCols; i++)
            {
                //create an instance of the prefab and child it
                //to the gridRoot
                Instantiate(cellPrefab, gridRoot);
            }

            SelectCurrentCell();
            InvokeRepeating("IncrementTime", 1f, 1f);
        }

        

        Transform GetCurrentCell()
        {
            //you can figure out the child index
            //of the cell that is a part of the grid
            //by calculating(row*Cols) + col
            int index = (row * nCols) + col;

            //return the child by index
            return gridRoot.GetChild(index);
        }

        void SelectCurrentCell()
        {
            //get the current cell
            Transform cell = GetCurrentCell();
            //set the cursor image on
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        void UnselectCurrentCell()
        {
            //get the current cell
            Transform cell = GetCurrentCell();
            //set the Cursor image off
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        public void MoveHorizontal(int amt)
        {
            //since we are moving to a new cell
            //we need to unselect the previous one
            UnselectCurrentCell();

            //update the column
            col += amt;

            //make sure the column stays within the bounds of the grid
            col = Mathf.Clamp(col, 0, nCols - 1);

            SelectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            //unselect previous cell
            UnselectCurrentCell();

            //update the column
            row += amt;

            //make sure the column stays within the bound of the grid
            row = Mathf.Clamp(row, 0, nRows - 1);

            SelectCurrentCell();
        }

        void ShowHit()
        {
            //get the current cell
            Transform cell = GetCurrentCell();
            //set the Hit iamge on
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);
        }

        void ShowMiss()
        {
            //get the current cell
            Transform cell = GetCurrentCell();
            //set the miss image on
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);
        }

        void IncrementScore()
        {
            //add 1 to the score
            score++;

            //update label
            scoreLabel.text = string.Format("Score: {0}", score);
        }

        public void Fire()
        {
            //check if thec ell in the hits data is t or f
            //if its true that means we already fired a shot in the current cell
            //and we should not do anything

            if (hits[row, col]) return;

            //mark this cell as being fired upon
            hits[row, col] = true;

            //if this cell is a ship
            if (grid[row, col] == 1)
            {
                //hit it and increment score
                ShowHit();
                IncrementScore();
            }
            else //miss
            {
                ShowMiss();
            }
        }

        void TryEndGame()
        {
            //check every row
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    //if a cell is not a ship then ignore
                    if (grid[row, col] == 0) continue;
                    //if a cell is a ship and it hasnt been scored
                    //then do a simple return because games not done
                    if (hits[row, col] == false) return;
                }
            }

            //if the loop successfuly completes then all
            //ships ahve beend etryoed and show the winLabel
            winLabel.SetActive(true);
            //stop timer
            CancelInvoke("IncrementTime");
        }

        void IncrementTime()
        {
            //add one to time
            time++;

            //update the time label with current time
            //format it mm:ss where m is the minute and s is seconds
            //ss should always have 2 digits
            //mm should only display as many digits as necessary
            timeLabel.text = string.Format("{0}:{1}", time / 60, (time % 60).ToString("00"));
        }

        public void Restart()
        {
            //unselect the current cell
            UnselectCurrentCell();

            //reset row and column to 0
            row = 0;
            col = 0;

            //reset hit 2D array data to all false
            hits = new bool[nRows, nCols];

            //reset timer and score
            time = 0;
            score = 0;

            //reset the win label
            winLabel.SetActive(false);

            //reset the timer label
            timeLabel.text = "0:00";

            //reset the score label
            scoreLabel.text = "Score: 0";

            

            //reset Hit and Miss objects on each cell
            foreach (Transform child in gridRoot)
            {
                Transform hit = child.Find("Hit");
                hit.gameObject.SetActive(false);

                Transform miss = child.Find("Miss");
                miss.gameObject.SetActive(false);
            }

            //randomize the ships every time player resets
            for (int row = 0; row < nRows; row++)
            {
                for (int col = 0; col < nCols; col++)
                {
                    //generate a random number between 0 and 10
                    int randomNumber = UnityEngine.Random.Range(0, 11);

                    //if the random number is greater than 5, set the cell as a ship (1), otherwise set it as empty (0)
                    if (randomNumber > 5)
                    {
                        grid[row, col] = 1;
                    }
                    else
                    {
                        grid[row, col] = 0;
                    }
                }

                CancelInvoke("IncrementTime");
                //restart timer
                InvokeRepeating("IncrementTime", 1f, 1f);

                SelectCurrentCell();
                
            }

            
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TryEndGame();
        }


    }
}
