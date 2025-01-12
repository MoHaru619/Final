using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{//room cmaera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    [SerializeField] private Transform player;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        //room camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z),ref velocity, speed);  

        //follow player
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);


    }
    public void MovetoNewRoom(Transform _newRoom)
    { 
       currentPosX = _newRoom.position.x;   
    }
}
