using UnityEngine;

public class ChangeDoors : MonoBehaviour
{
    private GameObject go;
    private int nbChild;
    private Generateurdj _gendj;
    private changeRoom _changeRoom;
    public object PosPlayer;
    private GameObject preciseDoorToBoss;
    private GameObject preciseDoorFromBoss;
    private int sideBossRoomX;
    private int sideBossRoomY;

    // Start is called before the first frame update
    void Awake()
    {
        _gendj = GameObject.FindObjectOfType<Generateurdj>();
        _changeRoom = GameObject.FindObjectOfType<changeRoom>();

    }

    public void ChangeDoorColor()
    {
        GameObject room = _gendj.MainGrid[_changeRoom.x, _changeRoom.y];
        GameObject door = room.transform.Find("Doors").gameObject;
        

            if (_gendj.MainGrid[_gendj._BossX - 1, _gendj._BossY] is GameObject)
            {
                GameObject RoomToBoss = _gendj.MainGrid[_gendj._BossX - 1, _gendj._BossY];
                GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
                preciseDoorToBoss = DoorsToBoss.transform.Find("Top").gameObject;
                sideBossRoomX = _gendj._BossX - 1;
                sideBossRoomY = _gendj._BossY;
                GameObject DoorsBoss = _gendj._BossRoom.transform.Find("Doors").gameObject;
                preciseDoorFromBoss = DoorsBoss.transform.Find("Bottom").gameObject;
            }
            else if (_gendj.MainGrid[_gendj._BossX + 1, _gendj._BossY] is GameObject)
            {
                GameObject RoomToBoss = _gendj.MainGrid[_gendj._BossX + 1, _gendj._BossY];
                GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
                preciseDoorToBoss = DoorsToBoss.transform.Find("Bottom").gameObject;
                sideBossRoomX = _gendj._BossX + 1;
                sideBossRoomY = _gendj._BossY;
                GameObject DoorsBoss = _gendj._BossRoom.transform.Find("Doors").gameObject;
                preciseDoorFromBoss = DoorsBoss.transform.Find("Top").gameObject;
            }
            else if (_gendj.MainGrid[_gendj._BossX, _gendj._BossY - 1] is GameObject)
            {
                GameObject RoomToBoss = _gendj.MainGrid[_gendj._BossX, _gendj._BossY - 1];
                GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
                preciseDoorToBoss = DoorsToBoss.transform.Find("Right").gameObject;
                sideBossRoomX = _gendj._BossX;
                sideBossRoomY = _gendj._BossY - 1;
                GameObject DoorsBoss = _gendj._BossRoom.transform.Find("Doors").gameObject;
                preciseDoorFromBoss = DoorsBoss.transform.Find("Left").gameObject;
            }
            else if (_gendj.MainGrid[_gendj._BossX, _gendj._BossY + 1] is GameObject)
            {
                GameObject RoomToBoss = _gendj.MainGrid[_gendj._BossX, _gendj._BossY + 1];
                GameObject DoorsToBoss = RoomToBoss.transform.Find("Doors").gameObject;
                preciseDoorToBoss = DoorsToBoss.transform.Find("Left").gameObject;
                sideBossRoomX = _gendj._BossX;
                sideBossRoomY = _gendj._BossY + 1;
                GameObject DoorsBoss = _gendj._BossRoom.transform.Find("Doors").gameObject;
                preciseDoorFromBoss = DoorsBoss.transform.Find("Right").gameObject;
                
            }
            for (int i = 0; i < door.transform.childCount; i++)
            {
                if (_changeRoom.x == sideBossRoomX && _changeRoom.y == sideBossRoomY)
                {
                    door.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
                    preciseDoorToBoss.GetComponent<SpriteRenderer>().color = Color.magenta;
                } else if (_changeRoom.x == _gendj._BossX && _changeRoom.y == _gendj._BossY)
                {
                    preciseDoorFromBoss.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
                else 
                {
                    door.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
                }
                _gendj.OpensDoor[_changeRoom.x, _changeRoom.y] = true;
        }
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeDoorColor();
        }
    }
}