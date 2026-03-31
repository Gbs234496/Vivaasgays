using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
   
    private AudioSource systemSource;
    private List <AudioSource> activeSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            systemSource = GetComponent<AudioSource>();
            activeSources = new List <AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}