using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piano_Music : MonoBehaviour
{
    public List<List<bool>> musicPlaySound = null;
    public AudioSource musicPlaySource = null;

    public GameObject savePanel = null;
    private InputField inputField = null;
    private Button saveButton = null;
    private Button playButton = null;

    public void InitValue()
    {
        musicPlaySound = new List<List<bool>>();

        for(int i = 0; i < 8; i++)
        {
            musicPlaySound.Add(new List<bool>());
        }

        musicPlaySource = this.gameObject.AddComponent<AudioSource>();

        InitSavePanel();
    }

    private void InitSavePanel()
    {
        inputField = savePanel.transform.GetChild(1).GetComponentInChildren<InputField>();
        print(inputField.name);
        saveButton = savePanel.transform.GetChild(2).GetComponent<Button>();
        print(saveButton.name);
        playButton = savePanel.transform.GetChild(3).GetComponent<Button>();
        print(playButton.name);

        saveButton.onClick.AddListener(() => SaveMusic(inputField.text));
        playButton.onClick.AddListener(() => StartCoroutine(PlayMusic(inputField.text)));

        savePanel.SetActive(false);
    }

    public void InputSound(int _idx, bool _isTap)
    {
        musicPlaySound[_idx].Add(_isTap);
    }

    public IEnumerator PlayMusic(string _name)
    {
        var bpm = new WaitForSeconds(Piano.Piano_Management.Instance.delayTime);

        MusicData data = SaveSystem.Load<MusicData>("Music_" + _name);
        List<List<bool>> music = new List<List<bool>>();
        for(int i = 0; i < 8; i++)
        {
            music.Add(new List<bool>());
            for(int j = 0; j < data.music.Count / 8; j++)
            {
                music[i].Add(data.music[j]);
            }
        }

        while (music[0].Count > 0)
        {
            for (int i = 0; i < musicPlaySound.Count; i++)
            {
                if(music[i][0]) Sound_Management.Instance.PlayClip(i);

                music[i].RemoveAt(0);
            }

            yield return bpm;
        }
    }

    public void SaveMusic(string _name)
    {
        MusicData music = new MusicData(_name, musicPlaySound);

        SaveSystem.Save<MusicData>(music, "Music_"+ _name);
    }
}
