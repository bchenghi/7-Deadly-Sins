using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleToSwitchBridge : MonoBehaviour
{
    public Transform text;
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
            GetComponent<InventoryCheckPuzzle>().enabled = false;
            text.GetComponent<TextMeshProUGUI>().text = "Solve the Switches to escape!";
            
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
