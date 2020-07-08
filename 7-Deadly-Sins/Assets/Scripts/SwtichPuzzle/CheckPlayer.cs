using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    public bool playerDetected;
    public GameObject Puzzle;
    public GameObject PuzzleTemplate;
    public InventoryCheckPuzzle inventoryChecker;
    public PuzzleToSwitchBridge bridge;
    private bool puzzleShown;

    private void Start()
    {
        Puzzle.SetActive(false);
        PuzzleTemplate.SetActive(false);
        puzzleShown = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
           
                playerDetected = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerDetected = false;
        }
    }


    private void Update()
    {
        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.N) && !puzzleShown)
            {
                if (inventoryChecker.hasAllClues)
                {
                    inventoryChecker.RemovePuzzles();
                    Puzzle.SetActive(true);
                    PuzzleTemplate.SetActive(true);
                    puzzleShown = true;
                }else if (bridge.AllPiecesPieced)
                {
                    Puzzle.SetActive(true);
                    PuzzleTemplate.SetActive(true);
                    puzzleShown = true;
                }
                
            }

            if (puzzleShown)
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Puzzle.SetActive(false);
                    PuzzleTemplate.SetActive(false);
                    puzzleShown = false;
                }
            }
            
        }



        
        

        
            
        
    }
}
