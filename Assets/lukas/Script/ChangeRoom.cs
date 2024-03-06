using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class changeRoom : MonoBehaviour
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

    // Start is called before the first frame update
    public void Awake()
    {
        x = 4;
        y = 4;
        _gendj = GameObject.FindObjectOfType<Generateurdj>();
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (_gendj.OpensDoor[x, y])
        {
            if (collision.gameObject.tag == "TopDoor")
            {
                slidingMain = new Vector3(0, 10, 0);
                slidingSub = new Vector3(0, 1, 0);
                slidingPlayer = new Vector3(0, 4, 0);
                x++;
            }
            if (collision.gameObject.tag == "BottomDoor")
            {
                slidingMain = new Vector3(0, -10, 0);
                slidingSub = new Vector3(0, -1, 0);
                slidingPlayer = new Vector3(0, -4, 0);
                x--;
            }
            if (collision.gameObject.tag == "LeftDoor")
            {
                slidingMain = new Vector3(-18, 0, 0);
                slidingSub = new Vector3(-1, 0, 0);
                slidingPlayer = new Vector3(-4, 0, 0);
                y--;
            }
            if (collision.gameObject.tag == "RightDoor")
            {
                slidingMain = new Vector3(18, 0, 0);
                slidingSub = new Vector3(1, 0, 0);
                slidingPlayer = new Vector3(4, 0, 0);
                y++;
            }
            
            if (collision.gameObject.tag == "RightDoor" || collision.gameObject.tag == "LeftDoor" || collision.gameObject.tag == "BottomDoor" || collision.gameObject.tag == "TopDoor")
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
                if ((x == 4 && y == 4))
                {
                }
                else if (_gendj.MainGrid[x,y].tag == "RandomRoom")
                {
                    _gendj.SubGrid[x, y].GetComponent<SpriteRenderer>().color = Color.yellow;
                    RoomRandomizer scriptInRoom = _gendj.MainGrid[x, y].GetComponent<RoomRandomizer>();
                    scriptInRoom.Lambda();
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
