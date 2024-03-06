using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.MemoryProfiler;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Point
{
    public int x;
    public int y;

    public Point(int xx, int yy)
    {
        x = xx;
        y = yy;
    }
}

public class Generateurdj : MonoBehaviour
{

    public int iteration;
    public int maxGenPop = 4;
    public int nbRooms = 5;
    public int seed = 42;
    public GameObject[] tiles;
    private int width, height;
    public GameObject _Start;
    public GameObject _last;
    public GameObject _BossRoom;
    public GameObject[,] MainGrid = new GameObject [9, 9];
    public GameObject[,] SubGrid = new GameObject[9, 9];
    public bool[,] OpensDoor = new bool[9, 9];
    public int _BossX;
    public int _BossY;
    public GameObject RoomToBoss;

    [System.Obsolete]
    void Start()
    {
        width = iteration * 2 + 3;
        height = iteration * 2 + 3;
        UnityEngine.Random.seed = seed;
        int[,] area = new int[height, width];
        int startX = Mathf.RoundToInt(width / 2);
        int startY = Mathf.RoundToInt(height / 2);
        area[startY, startX] = 1;
        List<string> directions = new List<string>(new string[] { "nord", "sud", "est", "ouest" });
        List<Point> points = new List<Point>();
        points.Add(new Point(startX, startY));

        for (int i = 0; i < iteration; i++)
        {
            List<Point> popRooms = new List<Point>();
            foreach (Point point in points)
            {
                List<string> directionsPop = new List<string>(directions);
                System.Random random = new System.Random();
                for (int l = 0; l < directionsPop.Count; l++)
                {
                    string temp = directionsPop[l];
                    int randomIndex = random.Next(directionsPop.Count);
                    directionsPop[l] = directionsPop[randomIndex];
                    directionsPop[randomIndex] = temp;
                }
                if (i == 0)
                {
                    int randomSide = random.Next(1, 3);
                    for (int l = randomSide; l > 0; l--)
                    {
                        directionsPop.Remove(directionsPop[l]);
                    }
                }


                foreach (string direction in directionsPop)
                {
                    if (direction == "nord")
                    {
                        if (area[point.y - 1, point.x] == 0 && area[point.y - 2, point.x] == 0 && area[point.y - 1, point.x - 1] == 0 && area[point.y - 1, point.x + 1] == 0)
                        {
                            if (popRooms.Count < maxGenPop && points.Count + popRooms.Count < nbRooms)
                            {
                                area[point.y - 1, point.x] = 1;
                                popRooms.Add(new Point(point.x, point.y - 1));
                            }
                        }
                    }
                    if (direction == "sud")
                    {
                        if (area[point.y + 1, point.x] == 0 && area[point.y + 2, point.x] == 0 && area[point.y + 1, point.x - 1] == 0 && area[point.y + 1, point.x + 1] == 0)
                        {
                            if (popRooms.Count < maxGenPop && points.Count + popRooms.Count < nbRooms)
                            {
                                area[point.y + 1, point.x] = 1;
                                popRooms.Add(new Point(point.x, point.y + 1));
                            }
                        }
                    }
                    if (direction == "ouest")
                    {
                        if (area[point.y, point.x - 1] == 0 && area[point.y - 2, point.x] == 0 && area[point.y - 1, point.x - 1] == 0 && area[point.y + 1, point.x - 1] == 0)
                        {
                            if (popRooms.Count < maxGenPop && points.Count + popRooms.Count < nbRooms)
                            {
                                area[point.y, point.x - 1] = 1;
                                popRooms.Add(new Point(point.x - 1, point.y));
                            }
                        }
                    }
                    if (direction == "est")
                    {
                        if (area[point.y, point.x + 1] == 0 && area[point.y, point.x + 2] == 0 && area[point.y - 1, point.x + 1] == 0 && area[point.y + 1, point.x + 1] == 0)
                        {
                            if (popRooms.Count < maxGenPop && points.Count + popRooms.Count < nbRooms)
                            {
                                area[point.y, point.x + 1] = 1;
                                popRooms.Add(new Point(point.x + 1, point.y));
                            }
                        }
                    }
                }
            }
            points = new List<Point>(popRooms);
        }
        int[,] indexedArea = IndexAttribute(area);
        TilesInstant(indexedArea);
    }

    void TilesInstant(int[,] tab)
    {
        int middleX = Mathf.RoundToInt(width / 2);
        int middleY = Mathf.RoundToInt(height / 2);
        int dist = 0;
        System.Random random = new System.Random();
        

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tab[y, x] != 0)
                {
                    GameObject goMap = Instantiate(tiles[tab[y, x] - 1], new Vector3(x - middleX, y - middleY-50, 0), Quaternion.identity);
                    GameObject goMain = Instantiate(tiles[tab[y, x] +14], new Vector3((float)(x - 17 * middleX + (x * 17)), y - 9 * middleY + y * 9, 0), Quaternion.identity);
                    MainGrid[y, x] = goMain;
                    SubGrid[y, x] = goMap;
                    OpensDoor[y, x] = false;
                    goMain.tag = "RandomRoom";

                    if (y == middleY && x == middleX)
                    {
                        goMap.GetComponent<SpriteRenderer>().color = Color.green;
                        GameObject door = goMain.transform.Find("Doors").gameObject;
                        _Start = goMain;

                        for (int i = 0; i < door.transform.childCount; i++)
                        {
                            door.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        OpensDoor[y, x] = true;
                    }
                    else
                    {
                        goMap.GetComponent<SpriteRenderer>().color = Color.grey;
                    }
                    if ( dist < Math.Abs(x - middleX) + Math.Abs(y - middleY)){
                        dist = Math.Abs(x - middleX) + Math.Abs(y - middleY);
                        _last = goMap;
                        _BossRoom = goMain;
                        _BossX = y;
                        _BossY = x;
                    }
                    if (dist == Math.Abs(x - middleX) + Math.Abs(y - middleY)) {
                        int rouletteRusse = random.Next(0, 2);
                        if (rouletteRusse == 1) {
                            _last = goMap;
                            _BossRoom = goMain;
                            _BossX = y;
                            _BossY = x;
                        }
                    }
                }
            }
        }
        _Start.tag = "StartRoom";
        _BossRoom.tag = "BossRoom";
        if (MainGrid[_BossX - 1, _BossY] is GameObject)
        {
            GameObject RoomToBoss = MainGrid[_BossX - 1, _BossY];
            GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
            GameObject preciseDoorToBoss = DoorsToBoss.transform.Find("Top").gameObject;
            preciseDoorToBoss.GetComponent<SpriteRenderer>().color = Color.grey;
            GameObject DoorsBoss = _BossRoom.transform.Find("Doors").gameObject;
            GameObject preciseDoorFromBoss = DoorsBoss.transform.Find("Bottom").gameObject;
            preciseDoorFromBoss.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (MainGrid[_BossX + 1, _BossY] is GameObject)
        {
            GameObject RoomToBoss = MainGrid[_BossX + 1, _BossY];
            GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
            GameObject preciseDoorToBoss = DoorsToBoss.transform.Find("Bottom").gameObject;
            preciseDoorToBoss.GetComponent<SpriteRenderer>().color = Color.grey;
            GameObject DoorsBoss = _BossRoom.transform.Find("Doors").gameObject;
            GameObject preciseDoorFromBoss = DoorsBoss.transform.Find("Top").gameObject;
            preciseDoorFromBoss.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (MainGrid[_BossX, _BossY - 1] is GameObject)
        {
            GameObject RoomToBoss = MainGrid[_BossX, _BossY - 1];
            GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
            GameObject preciseDoorToBoss = DoorsToBoss.transform.Find("Right").gameObject;
            preciseDoorToBoss.GetComponent<SpriteRenderer>().color = Color.grey;
            GameObject DoorsBoss = _BossRoom.transform.Find("Doors").gameObject;
            GameObject preciseDoorFromBoss = DoorsBoss.transform.Find("Left").gameObject;
            preciseDoorFromBoss.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (MainGrid[_BossX, _BossY + 1] is GameObject)
        {
            GameObject RoomToBoss = MainGrid[_BossX, _BossY + 1];
            GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
            GameObject preciseDoorToBoss = DoorsToBoss.transform.Find("Left").gameObject;
            preciseDoorToBoss.GetComponent<SpriteRenderer>().color = Color.grey;
            GameObject DoorsBoss = _BossRoom.transform.Find("Doors").gameObject;
            GameObject preciseDoorFromBoss = DoorsBoss.transform.Find("Right").gameObject;
            preciseDoorFromBoss.GetComponent<SpriteRenderer>().color = Color.grey;
        }

    }

    private int[,] IndexAttribute(int[,] tab)
    {
        //creation de tab
        int[,] indexes = new int[height, width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tab[y, x] == 1)
                {
                    if (tab[y + 1, x] == 1)
                    {
                        indexes[y, x] += 8;
                    }
                    if (tab[y - 1, x] == 1)
                    {
                        indexes[y, x] += 2;
                    }
                    if (tab[y, x - 1] == 1)
                    {
                        indexes[y, x] += 1;
                    }
                    if (tab[y, x + 1] == 1)
                    {
                        indexes[y, x] += 4;
                    }
                }
            }
        }

        return indexes;

    }
}