using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    const string PLAYER_TAG = "Player";
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER_TAG)
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene == SceneManager.sceneCount)
            {
                Debug.Log("Game over");
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }

            InterstitialAD.singleton.ShowAD();
        }
    }
}
