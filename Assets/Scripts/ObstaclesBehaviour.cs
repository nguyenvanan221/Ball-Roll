using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclesBehaviour : MonoBehaviour
{
    //time wait before restarting
    public float waitTime = 2.0f;

    public GameObject explosion;

    private GameObject player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            player = collision.gameObject;
            player.SetActive(false);

            Invoke("ResetGame", waitTime);
        }
    }

    void ResetGame()
    {
        GameController.Instance.EndGame();
    }

    private void PlayerTouch()
    {
        if (explosion != null)
        {
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 1.0f);
        }

        Destroy(this.gameObject);
    }
}
