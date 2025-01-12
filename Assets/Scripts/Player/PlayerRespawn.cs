using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    //[SerializeField] private CameraController cam;
    private Transform currentCheckpoint;
    private Transform finishCheckpoint;

    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();

    }
    public void CheckRespawn()
    {

        //check if check point available

        if (currentCheckpoint == null)
        {
            //show game over
            uiManager.GameOver();

            return;

        }




        transform.position = currentCheckpoint.position;

        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MovetoNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Checkpoint")
        

            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;


        
    }
}
