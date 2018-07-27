using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour {

    
    private static int xGridSize = 18;
    private static int yGridSize = 10;

    //BoardPrefabs
    [SerializeField]
    private GameObject whiteBoard;
    [SerializeField]
    private GameObject blackBoard;

    public Vector2[,] gameGrid = new Vector2[xGridSize, yGridSize];
    public float xGridPosition = 0;
    public float yGridPosition = 0;
    public int xGridSlot = 0;
    public int yGridSlot = 0;
    public int currentUnit = 0;

    public Vector2 currentPosition;

    public List<Vector2> tempPositions;
    //public List<Vector2> tempList;

    public List<Unit> units = new List<Unit>();
    
    // Use this for initialization
    void Awake () {
        currentPosition = new Vector2(xGridPosition, yGridPosition);
        CreateGameGrid();
        
        tempPositions = new List<Vector2>();
        
        units.Add(new Unit(GameObject.Find("GameCube"), tempPositions));
        
        units.Add(new Unit(GameObject.Find("GameCube2"), tempPositions));
       

        units[0].unit.GetComponent<CubeMover>().canMove = true;
        units[0].unit.GetComponent<CubeMover>().RecordMove();
    }
	
	// Update is called once per frame
	void Update () {
        EndTurn();
        ConfirmMovement();
	}
    void CreateGameGrid()
    {
        bool isWhite = false;
        for (int y = 0; y<yGridSize; y++)
        {
            for(int x = 0; x < xGridSize; x++)
            {
                gameGrid[x,y] = new Vector2(xGridPosition,yGridPosition);
                xGridPosition += 1;
            }
            xGridPosition = 0;
            yGridPosition += 1;
        }
        xGridPosition = 0;
        yGridPosition = 0;
        for (int y = 0; y < yGridSize; y++)
        {
            for (int x = 0; x < xGridSize; x++)
            {
                if (isWhite == false)
                {
                    Instantiate(whiteBoard, gameGrid[x, y], Quaternion.Euler(-90, 0, 0));
                    isWhite = true;
                }
                else
                {
                    Instantiate(blackBoard, gameGrid[x, y], Quaternion.Euler(-90, 0, 0));
                    isWhite = false;
                }
            }
            isWhite = !isWhite;
        }
    }
    public void GetCurrentPosition()
    {
        currentPosition = gameGrid[xGridSlot,yGridSlot];
        Debug.Log(gameGrid[xGridSlot, yGridSlot]);

     }
    void EndTurn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        { 
            StartCoroutine("ResolveTurn");
        }
    }
    void ConfirmMovement()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            while (units[currentUnit].tempUnitPositions.Count < 10)
            {
                units[currentUnit].tempUnitPositions.Insert(0,units[currentUnit].unit.transform.position);
            }
            units[currentUnit].positions = units[currentUnit].tempUnitPositions;
            units[currentUnit].unit.GetComponent<CubeMover>().canMove = false;
            units[currentUnit].unit.GetComponent<CubeMover>().finishedTurn = true;

            tempPositions.Clear();
            currentUnit++;
            if (currentUnit < units.Count&& units[currentUnit].unit.GetComponent<CubeMover>().finishedTurn == false)
            {
                units[currentUnit].unit.GetComponent<CubeMover>().canMove = true;
                Debug.Log(units[currentUnit].unit);
                currentPosition = units[currentUnit].unit.transform.position;
                xGridPosition = currentPosition.x;
                yGridPosition = currentPosition.y;
                xGridSlot = (int)xGridPosition;
                yGridSlot = (int)yGridPosition;
                units[currentUnit].unit.GetComponent<CubeMover>().RecordMove();
            }
            else
            {
                currentUnit = 0;
                while(currentUnit < units.Count && units[currentUnit].unit.GetComponent<CubeMover>().finishedTurn == true)
                {
                    currentUnit++;   
                }
                if (currentUnit >= units.Count)
                {
                    StartCoroutine("ResolveTurn");
                }
                else
                {
                    units[currentUnit].unit.GetComponent<CubeMover>().canMove = true;
                    Debug.Log(units[currentUnit].unit);
                    currentPosition = units[currentUnit].unit.transform.position;
                    xGridPosition = currentPosition.x;
                    yGridPosition = currentPosition.y;
                    xGridSlot = (int)xGridPosition;
                    yGridSlot = (int)yGridPosition;
                    units[currentUnit].unit.GetComponent<CubeMover>().RecordMove();
                }

            }
        }
    }
    IEnumerator ResolveTurn()
    {
        foreach(Unit x in units)
        {
            if (x.unit.GetComponent<CubeMover>().canMove == true)
            {
                x.unit.transform.position = x.unit.GetComponent<CubeMover>().startingPosition;
            }
        }
        tempPositions.Clear();
        for (int i = 0; i < 10;)
        {   
            foreach (Unit h in units)
            {
                if (h.unit.GetComponent<CubeMover>().canMove == false)
                {
                    h.unit.transform.position = h.positions[h.positions.Count - 1];
                    h.positions.RemoveAt(h.positions.Count - 1);
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
        foreach (Unit t in units)
        {
            t.unit.GetComponent<CubeMover>().movesLeft = 10;
        }
        currentUnit = 0;
        units[currentUnit].unit.GetComponent<CubeMover>().canMove = true;
        units[currentUnit].unit.GetComponent<CubeMover>().RecordMove();

    }
}
