using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM_Class
{
    public double[] UpBPM = new double[10]
    {
        130, 80, 115, 160, 100,
        70, 140, 140, 125, 200
    };

    public double[] DownBPM = new double[10]
    {
        100, 60, 100, 100, 80,
        50, 60, 110, 120, 140
    };
}

public class BPM_Management : MonoSingleton<BPM_Management>
{
    public struct GenreBPM
    {
        private double upBPM;
        private double downBPM;

        public double UpBPM
        { 
            get { return upBPM; } 
            set { upBPM = value; } 
        }

        public double DownBPM
        { 
            get { return downBPM; } 
            set { downBPM = value; } 
        }
    }

    private BPM_Class      bpm_Class     = null;
    private Dictionary<string, GenreBPM> genreBPM_Dic = null;

    public int curBPM = 0;
    public bool isPlaying = false;

    private void InitValue()
    {
        List<GenreSO> genreSOs = new List<GenreSO>(GenreManager.instance.genreList);
        GenreBPM genreBPM = new GenreBPM();

        for(int i = 0; i < 10; i++)
        {
            genreBPM.UpBPM = bpm_Class.UpBPM[i];
            genreBPM.DownBPM = bpm_Class.DownBPM[i];

            genreBPM_Dic.Add(GenreManager.instance.genreList[i].GenreName, genreBPM);
        }
    }

    public GenreBPM Get_BPM_Dic(string _genreName)
    {
        return genreBPM_Dic[_genreName];
    }

    public void Start_BPM()
    {
        isPlaying = true;

        StartCoroutine(Play_BPM(curBPM));
    }

    public void Stop_BPM()
    {
        isPlaying = false;
    }

    private IEnumerator Play_BPM(int _bpm)
    {
        float delayTime = 60.0f / (float)_bpm;
        var delay = new WaitForSeconds(delayTime);

        while(isPlaying)
        {
            Sound_Management.Instance.PlayMetronome();

            yield return delay;
        }
    }
}
