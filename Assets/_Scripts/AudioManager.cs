using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]

    public AudioSource sfxSource;

    public AudioSource ambientSource;

    [Header("Audio Clips")]

    public AudioClip bounceClip;
    public AudioClip hitClip;
    public AudioClip ambientClip;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        PlayAmbientSound();
    }

    public void PlayAmbientSound()
    {

        if (ambientClip != null && ambientSource != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true;
            ambientSource.Play();


        }
    }

    public void playSound(AudioClip clip)

    {

        if (clip != null)

        {

            sfxSource.PlayOneShot(clip);

        }

    }






}

