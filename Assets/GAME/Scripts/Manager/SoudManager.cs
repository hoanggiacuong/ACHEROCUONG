using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



//Sound on pixabay.com
public class SoudManager : Singleton<SoudManager>
{
    [Header("------AudioSource------")]


    public AudioSource themeSource;

    public AudioSource sfxSource;

    [Header("------AudioClip------")]

    public Sound[] themeAudioClip;
    public Sound[]  sfxAudioClip;






    private void Start()
    {
       

      //  PlayThemSound("themeSound1");
    }


    public void PlayThemSound(string name)
    {
        var s = Array.Find(themeAudioClip, x => x.name == name);
        if (s!=null)
        {
            themeSource.clip = s.audioClip;
            themeSource.Play();
        }
        else
        {
            Debug.Log("Can't find themsMusic name:  "+name);
        }
    }

    public void PlaySfxSound(string name,float time)
    {
        var s = Array.Find(sfxAudioClip, x => x.name == name);
        if (s != null)
        {
            sfxSource.clip = s.audioClip;
            sfxSource.PlayOneShot(s.audioClip);


            StartCoroutine(StopSound(time));
          

        }
        else
        {
            Debug.Log("Can't find themsMusic name:  " + name);
        }
    }

    public void ToggleTheme()
    {
        themeSource.mute = !themeSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void ThemeVolume(float volume)
    {
        themeSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
       sfxSource.volume = volume;
    }


    //private void StartCoroutine(IEnumerable enumerable)
    //{

    //}

    IEnumerator StopSound(float a)
    {
        yield return new WaitForSeconds(a);
        sfxSource.Stop();
    }




}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
}
