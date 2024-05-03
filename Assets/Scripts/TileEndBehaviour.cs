using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{
    //time wait before destroy
    public float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider collider)
    {
        // if collided with player
        if (collider.GetComponent<PlayerBehaviour>())
        {
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            //destroy entire tile
            Destroy(transform.parent.gameObject, destroyTime);
        }
    }

}
