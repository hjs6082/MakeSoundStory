using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_MusicSave : MonoBehaviour
{
    public List<double> musicPlayTime = null;
    public List<AudioClip> musicPlaySound = null;
    public double curTime = 0.0;

    public AudioSource musicPlaySource = null;

    public void MakingMusic()
    {
        curTime = Time.time;
    }

    public void InputSound(AudioClip _sound)
    {
        musicPlaySource.Stop();
        musicPlaySource.PlayOneShot(_sound);

        musicPlayTime.Add(curTime);
        musicPlaySound.Add(_sound);
    }

    public IEnumerator PlayMusic()
    {
        List<double> _playTime = new List<double>(musicPlayTime);
        List<AudioClip> _playSound = new List<AudioClip>(musicPlaySound);

        for(int i = 0; i < _playTime.Count; i++)
        {
            musicPlaySource.Stop();
            musicPlaySource.PlayOneShot(_playSound[0]);

            yield return new WaitForSeconds((float)(_playTime[1] - _playTime[0]));
            
            _playTime.RemoveAt(0);
            _playSound.RemoveAt(0);
        }
    }
}
