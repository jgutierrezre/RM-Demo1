using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Win : MonoBehaviour
{
    public AudioClip fanfareSound;
    public AudioSource musicSource;
    public GameObject replayButton;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Stop the main music
            if (musicSource != null)
            {
                musicSource.Stop();
            }

            // Play the fanfare sound
            musicSource.PlayOneShot(fanfareSound);

            replayButton.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
