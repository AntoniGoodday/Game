using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public GameObject unit;
    
   
    public List<Vector2> positions;
    public List<Vector2> tempUnitPositions = new List<Vector2>();


    public Unit(GameObject newObject, List<Vector2> newPositions)
    {
        unit = newObject;
        positions = newPositions;
    }
	
}
