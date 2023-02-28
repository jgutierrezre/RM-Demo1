using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBallGame : MonoBehaviour
{
    public GameObject ballPrefab;
    public AudioSource musicSource;
    public GameObject replayButton;

    public float resetYPosition = -1f;

    private GameObject ballObject;

    void Start()
    {
        replayButton.SetActive(false);
        ballObject = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (ballObject != null)
        {
            if (ballObject.transform.position.y < resetYPosition)
            {
                ballObject.transform.position = transform.position;
            }

            if (Mathf.Abs(ballObject.GetComponent<Rigidbody>().velocity.magnitude) > 0.01f)
            {
                if (!ballObject.GetComponent<AudioSource>().isPlaying)
                {
                    ballObject.GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                ballObject.GetComponent<AudioSource>().Stop();
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("reset");
        Start();
        musicSource.Play();
        ballObject.transform.position = transform.position;
    }
}
