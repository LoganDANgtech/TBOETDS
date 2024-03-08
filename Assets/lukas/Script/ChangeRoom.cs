using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{

    private Vector3 slidingMain;
    private Vector3 slidingSub;
    private Vector3 slidingPlayer;
    private Camera _mainCam;
    private Camera _CamMM;
    private Canvas _Minimap;
    public int x;
    public int y;
    private Generateurdj _gendj;
    private ChangeDoors _doors;

    // Start is called before the first frame update
    public void Awake()
    {
        x = 4;
        y = 4;
        _gendj = GameObject.FindObjectOfType<Generateurdj>();
        _doors = GameObject.FindObjectOfType<ChangeDoors>();
        
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {// && GameObject.FindGameObjectsWithTag("ennemy").Length == 0 --> test
        if (_gendj.OpensDoor[x, y])
        {
            if (collision.gameObject.tag == "TopDoor" && GameObject.FindGameObjectsWithTag("ennemy").Length == 0)
            {
                slidingMain = new Vector3(0, 10, 0);
                slidingSub = new Vector3(0, 1, 0);
                slidingPlayer = new Vector3(0, 4, 0);
                x++;
            }
            if (collision.gameObject.tag == "BottomDoor" && GameObject.FindGameObjectsWithTag("ennemy").Length == 0)
            {
                slidingMain = new Vector3(0, -10, 0);
                slidingSub = new Vector3(0, -1, 0);
                slidingPlayer = new Vector3(0, -4, 0);
                x--;
            }
            if (collision.gameObject.tag == "LeftDoor" && GameObject.FindGameObjectsWithTag("ennemy").Length == 0)
            {
                slidingMain = new Vector3(-18, 0, 0);
                slidingSub = new Vector3(-1, 0, 0);
                slidingPlayer = new Vector3(-4, 0, 0);
                y--;
            }
            if (collision.gameObject.tag == "RightDoor" && GameObject.FindGameObjectsWithTag("ennemy").Length == 0)
            {
                slidingMain = new Vector3(18, 0, 0);
                slidingSub = new Vector3(1, 0, 0);
                slidingPlayer = new Vector3(4, 0, 0);
                y++;
            }
            
            if ((collision.gameObject.tag == "RightDoor" || collision.gameObject.tag == "LeftDoor" || collision.gameObject.tag == "BottomDoor" || collision.gameObject.tag == "TopDoor") && GameObject.FindGameObjectsWithTag("ennemy").Length == 0)
            {
                _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                _CamMM = GameObject.FindGameObjectWithTag("CamMM").GetComponent<Camera>();
                _Minimap = GameObject.FindGameObjectWithTag("Minimap").GetComponent<Canvas>();
                _mainCam.transform.position += slidingMain;
                _CamMM.transform.position += slidingSub;
                _Minimap.transform.position += slidingMain;
                transform.position += slidingPlayer;
                if (x + 1 == _gendj._BossX && y == _gendj._BossY || x - 1 == _gendj._BossX && y == _gendj._BossY || x == _gendj._BossX && y + 1 == _gendj._BossY || x == _gendj._BossX && y - 1 == _gendj._BossY)
                {
                    _gendj.SubGrid[_gendj._BossX, _gendj._BossY].GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (x + 1 == _gendj._TreasureX && y == _gendj._TreasureY || x - 1 == _gendj._TreasureX && y == _gendj._TreasureY || x == _gendj._TreasureX && y + 1 == _gendj._TreasureY || x == _gendj._TreasureX && y - 1 == _gendj._TreasureY)
                {
                    _gendj.SubGrid[_gendj._TreasureX, _gendj._TreasureY].GetComponent<SpriteRenderer>().color = Color.white;
                }
                if ((x == 4 && y == 4))
                {
                }
                else if (_gendj.MainGrid[x,y].tag == "RandomRoom" && !_gendj.OpensDoor[x,y])
                {
                    _gendj.SubGrid[x, y].GetComponent<SpriteRenderer>().color = Color.yellow;
                    RoomRandomizer scriptInRoom = _gendj.MainGrid[x, y].GetComponent<RoomRandomizer>();
                    scriptInRoom.Lambda();
                }
                else if (_gendj.MainGrid[x, y].tag == "TreasureRoom" && !_gendj.OpensDoor[x, y])
                {
                    _gendj.OpensDoor[x, y] = true;
                    RoomRandomizer scriptInRoom = _gendj.MainGrid[x, y].GetComponent<RoomRandomizer>();
                    scriptInRoom.Treasure();
                }
                else
                {
                    RoomRandomizer scriptInRoom = _gendj.MainGrid[x, y].GetComponent<RoomRandomizer>();
                    scriptInRoom.Boss();
                }
            }
        }
    }
}