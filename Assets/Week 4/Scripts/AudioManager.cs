using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //create prefab reference to SoundEffect.cs
    [SerializeField] GameObject soundEfectPrefab;

    private static AudioManager instance;


    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip music;

    
    public enum SoundType
    {
        //always assign values immediatly in enum's to help from making errors later on (especially working in a team)
        ATTACK = 0,
        DAMAGE = 1,
        MUSIC = 3

    }
    private void Awake()
    {
        //when going between levels this object (audiomanager) that are in current level wont destroy
        DontDestroyOnLoad(this);
        instance = this;
        
    }

    public static void PlaySound(SoundType type)
    {

        instance.privPlaySound(type);
    }

    private void privPlaySound(SoundType type)
    {
        //audio.Stop();


        //PlayOneShot will let you use a single audiosoure to play many sound effects
        //but you lose the ability to Pause, REwind, mute, etc, any one of those sounds you played
        //switch (type)
        //{
        //    case SoundType.ATTACK: audio.PlayOneShot(attack); break;
        //    case SoundType.DAMAGE: audio.PlayOneShot(damage); break;
        //    case SoundType.MUSIC: audio.PlayOneShot(music); break;
        //}

        //alternative approach
        //switch (type)
        //{
        //    case SoundType.ATTACK: audio.clip = attack; break;
        //    case SoundType.DAMAGE: audio.clip = damage; break;
        //    case SoundType.MUSIC: audio.clip = music; break;
        //}
        AudioClip clip = null;

        GameObject soundEffectObject = Instantiate(soundEfectPrefab);
        SoundEffect soundEffect = soundEffectObject.GetComponent<SoundEffect>();
        soundEffect.Init(clip);
        soundEffect.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) privPlaySound(SoundType.MUSIC);
        if (Input.GetKeyDown(KeyCode.S)) privPlaySound(SoundType.ATTACK);
    }


}
