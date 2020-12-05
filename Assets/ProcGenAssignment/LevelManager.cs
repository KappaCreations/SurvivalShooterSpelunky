using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // can't see 2D array in inspector
    public Transform[] roomSpawnersRow0;
    public Transform[] roomSpawnersRow1;
    public Transform[] roomSpawnersRow2;
    public Transform[] roomSpawnersRow3;
    public int[,] roomVals = new int[4, 4];
    public GameObject[] rooms;

    public int testRow = 0;
    public int testColumn = 0;
    public int testType = 0;
    public bool start = true;
    public int prevCol;
    public int prevRow;
    public int Rand;
    public bool finish = false;
    public int finalRow;
    public int finalColumn;
    public bool placedRoom = false;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; i < 4; i++)
            {

                roomVals[i, j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (finish == false)
        {

            // Debug.Log(Rand);
            if (start == true)
            {
                testColumn = 2;
                roomVals[0, testColumn] = 1;
                start = false;
                prevCol = testColumn;
                prevRow = 0;
                testRow = 0;
            }

            placedRoom = false;

            while (placedRoom == false)
            {
                Rand = Random.Range(0, 5) + 1;
                if (testRow != 3)
                {
                    if (Rand <= 2)
                    {
                        
                        if (prevCol == 0 && prevRow==testRow)
                        {
                            if (roomVals[prevRow, prevCol] != 2)
                                roomVals[prevRow, prevCol] = 2;
                            else
                                roomVals[prevRow, prevCol] = 4;

                            testRow++;
                            roomVals[testRow, prevCol] = (int)Random.Range(3, 4) + 1;
                            prevRow = testRow;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + Rand + "at location " + prevRow + " " + prevCol);

                        }
                        else if (roomVals[testRow, prevCol--] == 0)
                        {
                            Debug.Log("going left");
                            testColumn = prevCol--;
                            roomVals[testRow, testColumn] = 1;
                            prevCol = testColumn;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + "1" + "at location " + prevRow + " " + prevCol);
                        }
                        else
                            Rand += 2;

                    }

                    else if (2 < Rand && Rand < 5)
                    {
                        Debug.Log("going right");
                        if (prevCol == 3 && prevRow == testRow)
                        {
                            if (roomVals[prevRow, prevCol] != 2)
                                roomVals[prevRow, prevCol] = 2;
                            else
                                roomVals[prevRow, prevCol] = 4;
                            testRow++;
                            roomVals[testRow, prevCol] = (int)Random.Range(2, 3) + 1;
                            prevRow = testRow;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + "fall down " + "at location " + prevRow + " " + prevCol);
                        }
                        else if (roomVals[testRow, prevCol++] == 0)
                        {

                            testColumn = prevCol++;
                            roomVals[testRow, testColumn] = 1;
                            prevCol = testColumn;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + "1" + "at location " + prevRow + " " + prevCol);
                        }
                        else
                            Rand -= 2;
                    }

                    else if (Rand == 5)
                    {
                        if (roomVals[prevRow, prevCol] != 2)
                            roomVals[prevRow, prevCol] = 2;
                        else
                            roomVals[prevRow, prevCol] = 4;
                        testRow++;
                        roomVals[testRow, prevCol] = (int)Random.Range(2, 3) + 1;
                        prevRow = testRow;
                        placedRoom = true;
                    }
                }

                else
                {
                    if (Rand <= 2)
                    {
                        if (roomVals[prevRow, prevCol--] != 0)
                        {
                            finish = true;
                            placedRoom = true;
                        }

                        else if (roomVals[prevRow, prevCol--] == 0)
                        {
                            Debug.Log("going left");
                            testColumn = prevCol--;
                            roomVals[testRow, testColumn] = 1;
                            prevCol = testColumn;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + "1" + "at location " + prevRow + " " + prevCol);
                        }
                        else
                            Rand += 2;

                    }

                    else if (2 < Rand && Rand < 5)
                    {
                        if (roomVals[prevRow, prevCol++] != 0)
                        {
                            finish = true;
                            placedRoom = true;
                        }


                        else if (roomVals[prevRow, prevCol++] == 0)
                        {
                            Debug.Log("going right");
                            testColumn = prevCol++;
                            roomVals[testRow, testColumn] = 1;
                            prevCol = testColumn;
                            placedRoom = true;
                            Debug.Log("The room that was made was type " + "1" + "at location " + prevRow + " " + prevCol);
                        }
                        else
                            Rand -= 2;
                    }

                    if (Rand == 5)
                    {
                        //roomVals[prevRow,prevCol] = 2;
                        //testRow++;
                        //roomVals[testRow,prevCol] = (int)Random.Range(2, 3) + 1;
                        //prevRow = testRow;
                        finish = true;
                        finalRow = 3;
                        finalColumn = testColumn;
                        placedRoom = true;
                    }
                }

            }

        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Debug.Log("The room on row " + i + " and column " + j + " is " + roomVals[i, j]);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    AddRoom(i, j, roomVals[i, j]);
                }
            }
        }
    }

    public void AddRoom(int row, int column, int roomType)
    {

        Vector3 spawnPos = Vector3.zero;
        // figure out position to spawn at
        switch (row)
        {
            case 0:
                spawnPos = roomSpawnersRow0[column].position;
                break;
            case 1:
                spawnPos = roomSpawnersRow1[column].position;
                break;
            case 2:
                spawnPos = roomSpawnersRow2[column].position;
                break;
            case 3:
                spawnPos = roomSpawnersRow3[column].position;
                break;
        }

        // actually spawn it

        Instantiate(rooms[roomType], spawnPos, transform.rotation);
    }
}
