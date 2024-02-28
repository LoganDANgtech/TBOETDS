using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
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
                    int randomSide = random.Next(0, 4);
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

    private void TilesInstant(int[,] tab)
    {
        int middleX = Mathf.RoundToInt(width / 2);
        int middleY = Mathf.RoundToInt(height / 2);
        int dist = 0;
        GameObject last = new GameObject();
        System.Random random = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tab[y, x] != 0)
                {
                    GameObject go = Instantiate(tiles[tab[y, x] - 1], new Vector3(x - middleX, y - middleY, 0), Quaternion.identity);
                    if (y == middleY && x == middleX)
                    {
                        go.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.5f, 1);
                    }
                    if ( dist < Math.Abs(x - middleX) + Math.Abs(y - middleY)){
                        dist = Math.Abs(x - middleX) + Math.Abs(y - middleY);
                        last = go;
                    }
                    if (dist == Math.Abs(x - middleX) + Math.Abs(y - middleY)) {
                        int rouletteRusse = random.Next(0, 2);
                        if (rouletteRusse == 1) {
                            last = go;
                        }
                    }
                }
            }
        }
        last.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.5f, 1);
    }

    private int[,] IndexAttribute(int[,] tab)
    {
        int[,] indexes = new int[height, width];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
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