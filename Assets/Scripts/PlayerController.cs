using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;

    private int count;

    // refactor to be set to size of PickUp prefab group
    private const int TOTAL_COUNT = 12;
    private const int N_VOLUME_INCREMENTS = TOTAL_COUNT / 2;
    private const float VOLUME_INCREMENT = 1f / N_VOLUME_INCREMENTS;

    private AudioManager audioManager;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        audioManager = FindObjectOfType<AudioManager>();



        // start score (only layer 1 is audible)
        audioManager.Play("Music1_Layer1");
        audioManager.Play("Music1_Layer2");
        audioManager.Play("Music1_Layer3");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick_Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            
            // SFX/Score
            PlaySound("Pick_Up");
            if (TOTAL_COUNT > 1)
            {
                if (count <= TOTAL_COUNT / 2)
                {
                    //float currentVol = audioManager.GetVolume("Music1_Layer2");
                    audioManager.ChangeVolume("Music1_Layer2",  VOLUME_INCREMENT);
                    
                }
                else if (count < TOTAL_COUNT)
                {
                    audioManager.ChangeVolume("Music1_Layer3", VOLUME_INCREMENT);
                }
                else if (count == TOTAL_COUNT)
                {
                    StopSound("Music1_Layer1");
                    StopSound("Music1_Layer2");
                    StopSound("Music1_Layer3");
                    PlaySound("Music2_FullMix");
                    PlaySound("Success");
                }
            }

        }
    }
    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/" + TOTAL_COUNT.ToString();
        
        if (count >= TOTAL_COUNT)
            winText.text = "You Win!";
    }

    // utility method that plays a sound using AudioManager
    private void PlaySound(string sound)
    {
        audioManager.Play(sound);
    }

    private void StopSound(string sound)
    {
        FindObjectOfType<AudioManager>().Stop(sound);
    }
    // TODO create AudioManager field
    private void SetSoundVolume(string sound, float vol)
    {
        FindObjectOfType<AudioManager>().SetVolume(sound, vol);
    }
}
