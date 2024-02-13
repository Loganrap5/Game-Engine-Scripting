using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HanoiTower : MonoBehaviour
{
    [SerializeField] private Transform peg1Transform;
    [SerializeField] private Transform peg2Transform;
    [SerializeField] private Transform peg3Transform;

    //win message
    [SerializeField] private GameObject winMessage;

    [SerializeField] private int[] peg1 = { 1, 2, 3, 4 };
    [SerializeField] private int[] peg2 = { 0, 0, 0, 0 };
    [SerializeField] private int[] peg3 = { 0, 0, 0, 0 };

    [SerializeField] private int currentPeg = 1;

    //check win condition and see if peg 3 has the correct order
    private bool WinCondition()
    {
        for(int i = 0; i < peg3.Length; i++)
        {
            if (peg3[i] != (i + 1))
            {
                return false;
            }
        }
        
        return true;

    }
    
    public void CheckWinCondition()
    {
        if(WinCondition())
        {
            if(true)
            {
                winMessage.SetActive(true);
            }
            else
            {
                
            }
        }
    }

    //create 3 image instances to change peg colors based on which peg is selected
    public Image pegOne;
    public Image pegTwo;
    public Image pegThree;

    //check what peg is selected and change colors accordingly
    public void SelectedPegColor()
    {
        if (currentPeg == 1)
        {
            pegOne.color = Color.gray;
            pegTwo.color = Color.black;
            pegThree.color = Color.black;
        }
        if(currentPeg == 2)
        {
            pegOne.color = Color.black;
            pegTwo.color = Color.gray;
            pegThree.color = Color.black;
        }
        if (currentPeg == 3)
        {
            pegOne.color = Color.black;
            pegTwo.color = Color.black;
            pegThree.color = Color.gray;
        }
    }

    //have this method run every frame to check what the selected peg is and change the color to gray
    //also checking for the win condition
    public void Update()
    {
        SelectedPegColor();
        CheckWinCondition();
    }

    


    [ContextMenu("Move Right")]
    public void MoveRight()
    {
        //Make sure we aren't the right most peg
        if (CanMoveRight() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg + 1);
        int toIndex = GetBottomNumberIndex(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg + 1);

        disc.SetParent(toPeg);
    }

    [ContextMenu("Move Left")]
    public void MoveLeft()
    {
        //Make sure we aren't the left most peg
        if (CanMoveLeft() == false) return;

        //Check to see what index and number we are moving from THIS peg
        int[] fromArray = GetPeg(currentPeg);
        int fromIndex = GetTopNumberIndex(fromArray);

        //If there wasn't anything to move then don't try to move
        if (fromIndex == -1) return;

        //Check to see where in the peg we are moving to that the number
        //should be placed into
        int[] toArray = GetPeg(currentPeg - 1);
        int toIndex = GetBottomNumberIndex(toArray);

        //If the adjacent peg is FULL then we cannot move anything into it
        //This probably will never happen since the max number of numbers
        //we have is the size of each peg
        if (toIndex == -1) return;

        //Lastly check to verify the number we are moving is not larger
        //than whatever number we may be placing this number on top of
        //on the adjacent peg
        if (CanAddToPeg(fromArray[fromIndex], toArray) == false) return;

        //If all checks PASS then go aheand and move the number
        //out of THIS array into the adjacent array
        MoveNumber(fromArray, fromIndex, toArray, toIndex);

        Transform disc = PopDiscFromCurrentPeg();
        Transform toPeg = GetPegTransform(currentPeg - 1);

        disc.SetParent(toPeg);
    }

    public void IncrementPegNumber()
    {
        currentPeg++;

        //make sure we cannot go out of bounds for the amount of pegs we have
        if (currentPeg == 4)
        {
            currentPeg = 3;
        }
    }

    public void DecrementPegNumber()
    {
        currentPeg--;

        //make sure we cannot go out of bounds for the amount of pegs we have
        if (currentPeg == 0)
        {
            currentPeg = 1;
        }
        
    }

    Transform PopDiscFromCurrentPeg()
    {
        Transform currentPegTransform = GetPegTransform(currentPeg);
        int index = currentPegTransform.childCount - 1;
        Transform disk = currentPegTransform.GetChild(index);
        return disk;
    }

    Transform GetPegTransform(int pegNumber)
    {
        if (pegNumber == 1) return peg1Transform;

        if (pegNumber == 2) return peg2Transform;

        return peg3Transform;
    }
    void MoveNumber(int[] fromArr, int fromIndex, int[] toArr, int toIndex)
    {
        int value = fromArr[fromIndex];
        fromArr[fromIndex] = 0;

        toArr[toIndex] = value;
    }

    bool CanMoveRight()
    {
        //If peg 1 or 2 then can move right
        return currentPeg < 3;
    }

    bool CanAddToPeg(int value, int[] peg)
    {
        int topNumberIndex = GetTopNumberIndex(peg);
        if(topNumberIndex == -1) return true;

        int topNumber = peg[topNumberIndex];
        return topNumber > value;
    }

    bool CanMoveLeft()
    {
        //If peg 2 or 3 then can move right
        return currentPeg > 1;
    }

    int[] GetPeg(int pegNumber)
    {
        

        if (pegNumber == 1) return peg1;

        if (pegNumber == 2) return peg2;

        return peg3;
    }

    int GetTopNumberIndex(int[] peg)
    {
        for (int i = 0; i < peg.Length; i++)
        {
            if (peg[i] != 0) return i;
        }

        return -1;
    }

    int GetBottomNumberIndex(int[] peg)
    {
        for (int i = peg.Length - 1; i >= 0; i--)
        {
            if (peg[i] == 0) return i;
        }

        return -1;
    }
}
