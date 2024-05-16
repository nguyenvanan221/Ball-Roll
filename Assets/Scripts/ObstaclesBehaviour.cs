using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclesBehaviour : MonoBehaviour
{
    //time wait before restarting
    public float waitTime = 2.0f;

    public GameObject explosion;

    private PlayerBehaviour player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            player = collision.gameObject.GetComponent<PlayerBehaviour>();
            player.lastScore.value = player.Score;
            if (player.Score > player.bestScore.value)
            {
                player.bestScore.value = player.Score;
            }
            collision.gameObject.SetActive(false);

            Invoke("EndGame", waitTime);
        }
    }

    void EndGame()
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
