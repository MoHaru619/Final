using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    //[SerializeField] private CameraController cam;
    private Transform currentCheckpoint;
    private Transform finishCheckpoint;

   // private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        //playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();

    }


    public void FinishedRespawn()
    {

        //check if check point available

        if (finishCheckpoint == true)

            uiManager.NextLevel();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "FinishedLevel")
        {
            finishCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
          
        }

    }
}
