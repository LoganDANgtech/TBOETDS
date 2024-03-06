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

    public List<GameObject> listBossMobs;
    public List<GameObject> listBossObjects;


    public List<GameObject> listMobs;
    public List<GameObject> listObjects;
    public List<GameObject> listInters;


    public int numberMobs = 8;

    public void Lambda()
    {
        int a = Random.Range(0, listGrids.Count);
        listGrids[a].SetActive(true);
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

            Instantiate(MobToSpawn, listMobGrid[Random.Range(0, listMobGrid.Count)].transform.position, Quaternion.identity);
            currentNumberMobs++;
        }


        int currentNumberObjects = 0;

        for (int i = 0; currentNumberObjects < listObjectGrid.Count; i++)
        {
            GameObject ObjectToSpawn = listObjects[Random.Range(0, listObjects.Count)];

            Instantiate(ObjectToSpawn, listObjectGrid[Random.Range(0, listObjectGrid.Count)].transform.position, Quaternion.identity);
            currentNumberObjects++;
        }


        int currentNumberInters = 0;

        for (int i = 0; currentNumberInters < listInterGrid.Count; i++)
        {
            GameObject InterToSpawn = listInters[Random.Range(0, listInters.Count)];

            Instantiate(InterToSpawn, listInterGrid[Random.Range(0, listInterGrid.Count)].transform.position, Quaternion.identity);
            currentNumberInters++;
        }
    }

        public void Boss()
        {
            listBossGrids[0].SetActive(true);
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

            for (int i = 0; currentNumberObjects < listObjectGrid.Count; i++)
            {
                GameObject ObjectToSpawn = listBossObjects[Random.Range(0, listBossObjects.Count)];

                Instantiate(ObjectToSpawn, listObjectGrid[Random.Range(0, listObjectGrid.Count)].transform.position, Quaternion.identity);
                currentNumberObjects++;
            }
        }
}
