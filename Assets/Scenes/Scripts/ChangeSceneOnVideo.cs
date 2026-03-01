using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeSceneOnVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;

    private bool justStartedPlayer;

    void Start()
    {
        justStartedPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isPlaying && !justStartedPlayer)
        {
            SceneManager.LoadScene("Main level");
        }

        if (player.isPlaying)
        {
            justStartedPlayer = false;
        }
    }
}
