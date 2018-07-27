using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour {

    public Vector2 startingPosition;
    public Grid gridManager;
    public int movesLeft = 10;
    public bool canMove = false;
    public bool finishedTurn = false;
    // Use this for initialization
    private void Awake()
    {
        startingPosition = transform.position;
    }
    void Start () {
        //gameObject.transform.position = gridManager.currentPosition;
        //RecordMove();
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {

                if (gridManager.xGridSlot < gridManager.gameGrid.GetLength(0) - 1)
                {
                    if (movesLeft > 0)
                    {
                        movesLeft -= 1;
                        gridManager.xGridSlot += 1;
                        gridManager.GetCurrentPosition();
                        gameObject.transform.position = gridManager.currentPosition;
                        RecordMove();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

                if (gridManager.xGridSlot > 0)
                {
                    if (movesLeft > 0)
                    {
                        movesLeft -= 1;
                        gridManager.xGridSlot -= 1;
                        gridManager.GetCurrentPosition();
                        gameObject.transform.position = gridManager.currentPosition;
                        RecordMove();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {

                if (gridManager.yGridSlot < gridManager.gameGrid.GetLength(1) - 1)
                {
                    if (movesLeft > 0)
                    {
                        movesLeft -= 1;
                        gridManager.yGridSlot += 1;
                        gridManager.GetCurrentPosition();
                        gameObject.transform.position = gridManager.currentPosition;
                        RecordMove();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {

                if (gridManager.yGridSlot > 0)
                {
                    if (movesLeft > 0)
                    {
                        movesLeft -= 1;
                        gridManager.yGridSlot -= 1;
                        gridManager.GetCurrentPosition();
                        gameObject.transform.position = gridManager.currentPosition;
                        RecordMove();
                    }
                }
            }
        }
        
}

    public void RecordMove()
    {
        gridManager.tempPositions.Insert(0, transform.position);
        gridManager.units[gridManager.currentUnit].tempUnitPositions.Insert(0, transform.position);
    }

    
}
