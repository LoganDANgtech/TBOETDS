using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRandomizer : MonoBehaviour
{

    public List<GameObject> listGrids;

    public List<GameObject> listMobGrid;
    public List<GameObject> listObjectGrid;
    public List<GameObject> listInterGrid;


    public List<GameObject> listBossGrids;
    public List<GameObject> listTreasureGrids;

    public List<GameObject> listInterTreasureGrid;
    public List<GameObject> listBossMobs;
    public List<GameObject> listBossObjects;


    public List<GameObject> listMobs;
    public List<GameObject> listObjects;
    public List<GameObject> listInters;

    private int PosMob;
    private int PosObj;
    private int PosInter;


    public int numberMobs = 7;

    public void Lambda()
    {
        int a = Random.Range(0, listGrids.Count);
        //listGrids[a].SetActive(true);
        GameObject ActiveGrid = listGrids[a];

        for (int i = 0; i < ActiveGrid.transform.GetChild(0).childCount; i++)
        {
            listMobGrid.Add(ActiveGrid.transform.GetChild(0).GetChild(i).gameObject);
        }

        for (int i = 0; i < ActiveGrid.transform.GetChild(1).childCount; i++)
        {
            listObjectGrid.Add(ActiveGrid.transform.GetChild(1).GetChild(i).gameObject);
        }

        for (int i = 0; i < ActiveGrid.transform.GetChild(2).childCount; i++)
        {
            listInterGrid.Add(ActiveGrid.transform.GetChild(2).GetChild(i).gameObject);
        }


        int currentNumberMobs = 0;

        for (int i = 0; currentNumberMobs < numberMobs; i++)
        {
            GameObject MobToSpawn = listMobs[Random.Range(0, listMobs.Count)];

            PosMob = Random.Range(0, listMobGrid.Count);
            Instantiate(MobToSpawn, listMobGrid[PosMob].transform.position, Quaternion.identity);
            listMobGrid.Remove(listMobGrid[PosMob]);
            currentNumberMobs++;
        }

        int currentNumberObjects = 0;

        for (int i = 0; currentNumberObjects < ActiveGrid.transform.GetChild(1).childCount; i++)
        {
            GameObject ObjectToSpawn = listObjects[Random.Range(0, listObjects.Count)];

            PosObj = Random.Range(0, listObjectGrid.Count);
            Instantiate(ObjectToSpawn, listObjectGrid[PosObj].transform.position, Quaternion.identity);
            listObjectGrid.Remove(listObjectGrid[PosObj]);
            currentNumberObjects++;
        }

        int currentNumberInters = 0;
        
        for (int i = 0; currentNumberInters < ActiveGrid.transform.GetChild(2).childCount; i++)
        {
            GameObject InterToSpawn = listInters[Random.Range(0, listInters.Count)];

            PosInter = Random.Range(0, listInterGrid.Count);
            Instantiate(InterToSpawn, listInterGrid[PosInter].transform.position, Quaternion.identity);
            listInterGrid.Remove(listInterGrid[PosInter]);
            currentNumberInters++;
        }
    }

        public void Boss()
        {
            //listBossGrids[0].SetActive(true);
            GameObject ActiveGrid = listBossGrids[0];

            for (int i = 0; i < ActiveGrid.transform.GetChild(0).childCount; i++)
            {
                listMobGrid.Add(ActiveGrid.transform.GetChild(0).GetChild(i).gameObject);
            }

            for (int i = 0; i < ActiveGrid.transform.GetChild(1).childCount; i++)
            {
                listObjectGrid.Add(ActiveGrid.transform.GetChild(1).GetChild(i).gameObject);
            }


            GameObject MobToSpawn = listBossMobs[Random.Range(0, listBossMobs.Count)];
            Instantiate(MobToSpawn, listMobGrid[Random.Range(0, listMobGrid.Count)].transform.position, Quaternion.identity);


            int currentNumberObjects = 0;

            for (int i = 0; currentNumberObjects < ActiveGrid.transform.GetChild(1).childCount; i++)
            {
                GameObject ObjectToSpawn = listBossObjects[Random.Range(0, listBossObjects.Count)];

                Instantiate(ObjectToSpawn, listObjectGrid[Random.Range(0, listObjectGrid.Count)].transform.position, Quaternion.identity);
                currentNumberObjects++;
            }
        }
    
        public void Treasure()
    {
        //listTreasureGrids[0].SetActive(true);
        GameObject ActiveGrid = listTreasureGrids[0];

        for (int i = 0; i < ActiveGrid.transform.GetChild(2).childCount; i++)
        {
            listInterGrid.Add(ActiveGrid.transform.GetChild(2).GetChild(i).gameObject);
        }

        int currentNumberInters = 0;

        for (int i = 0; currentNumberInters < ActiveGrid.transform.GetChild(2).childCount; i++)
        {
            GameObject InterToSpawn = listInterTreasureGrid[Random.Range(0, listInterTreasureGrid.Count)];

            Instantiate(InterToSpawn, listTreasureGrids[PosInter].transform.position, Quaternion.identity);
            currentNumberInters++;
        }
    }
}
