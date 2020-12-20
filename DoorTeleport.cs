using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public bool isOpen = false;
    public Transform door;

    public void Unlock()
    {
        isOpen = true;
    }

    public void Teleport(GameObject player)
    {
        player.transform.position = new Vector3(door.position.x, door.position.y, player.transform.position.z);
    }
}
