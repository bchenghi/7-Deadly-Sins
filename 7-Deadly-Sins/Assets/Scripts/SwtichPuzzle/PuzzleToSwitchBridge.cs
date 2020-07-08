using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleToSwitchBridge : MonoBehaviour
{
    public GameObject puzzle;
    DragAndDropPuzzle[] puzzlePieces;
    public bool AllPiecesPieced;
    public int numberOfPiecesTobeCompleted;
    private int piecesCompleted;

    private void Start()
    {
        puzzlePieces = puzzle.transform.GetComponentsInChildren<DragAndDropPuzzle>();
        
    }


    private void Update()
    {
        CheckPieces();
        if (AllPiecesPieced)
        {
            Debug.Log("Switches Activated");
        }
    }

    private void CheckPieces()
    {
        if (!AllPiecesPieced)
        {
            for (int i = 0; i < puzzlePieces.Length; i++)
            {
                if (puzzlePieces[i].inRightPosition)
                {
                    piecesCompleted++;
                    if (piecesCompleted == numberOfPiecesTobeCompleted)
                    {
                        AllPiecesPieced = true;
                        
                    }
                }
                else
                {
                    piecesCompleted = 0;
                    break;
                }
            }
        }
    }

}
