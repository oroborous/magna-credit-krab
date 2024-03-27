using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text spent;

    public int deposited;
    public Text bill;

    public bool isPlaying = true;

    public GameObject[] SpriteOb;

    private AudioSource musicSource;
    private AudioSource musicSource1;


    private AudioSource sfxSource;

    public AudioClip grab;
    public AudioClip walk;
    public AudioClip zip;
    public AudioClip music;
    public AudioClip[] hit;
    public AudioClip grumble;
    public AudioClip happy;

    public AudioClip paid;
    public AudioClip bum;
    public AudioClip cost;

    public AudioClip warning;

    public const int CASH = 0;
    public const int COST = 1;
    public const int BUM = 2;
    public const int LIGHTNING = 3;
    public const int SAD = 4;
    public const int HAPPY = 5;
    public const int DANGER = 6;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        isPlaying = true;

        PlayMusic(music, .1f);
    }


    private void Update()
    {
       
        spent.text = "SPENT: $" + score;

        if (!isPlaying)
        {
            bill.text = "Press ESC to Restart";
        }
        else
        {
            bill.text = "Bill: $" + deposited;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void Spend(int amount)
    {
        if(deposited != 0)
        {
            int n;
            n = deposited - amount; //Difference

            if(n > 0)
            {
                score += amount;
                deposited = n;
            }
            else
            {
                score += deposited;
                deposited = 0;
            }
            
        }
       
    }

    public void spawnSprite(int Sprite, Transform t)
    {
        GameObject n = Instantiate(SpriteOb[Sprite]);
        n.transform.position = t.transform.position;
    }


    public void PlayMusic(AudioClip musicClip, float vol)
    {
        musicSource.clip = musicClip;
        musicSource.volume = vol;
        musicSource.Play();
    }

    public void PlayWarning()
    {
        musicSource1.clip = warning;
        musicSource1.Play();
    }

    public void StopWarning()
    {
        musicSource1.Stop();
    }

    public void PlaySFX(AudioClip clip, float vol)
    {
        sfxSource.PlayOneShot(clip, vol);
    }

}
