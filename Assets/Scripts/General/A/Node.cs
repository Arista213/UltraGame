using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public readonly int GridX;//X Position in the Node Array
    public readonly int GridY;//Y Position in the Node Array

    public readonly bool IsWall;//Tells the program if this node is being obstructed.
    public Vector3 Position;//The world position of the node.

    public Node ParentNode;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

    public int IgCost;//The cost of moving to the next square.
    public int IhCost;//The distance to the goal from this node.

    public int FCost { get { return IgCost + IhCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.

    public Node(bool aIsWall, Vector3 aPos, int gridX, int gridY)//Constructor
    {
        IsWall = aIsWall;
        Position = aPos;
        GridX = gridX;
        GridY = gridY;
    }

}