using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Speech_Func : MonoBehaviour
{
    private Coroutine tmpTween = null;
    private bool isWaitting = false;
    public string tmpText = null;

    public void Talk(TextMeshProUGUI _tmp, string _str)
    {
        Speech_Management.isTalking = true;
        StartCoroutine(TweeningTMP(_tmp, _str));
    }

    private IEnumerator TweeningTMP(TextMeshProUGUI _tmp, string _str)
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);
        StringBuilder sb = new StringBuilder().Clear();
        string str = _str + " ¡å";

        while(Speech_Management.isTalking)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(Speech_Management.isTalking)
                {
                    sb.Append(str[i]);
                    _tmp.text = sb.ToString();
                    
                    yield return delay;
                }
                else
                {
                    break;
                }
            }

            Speech_Management.isTalking = false;
        }

        isWaitting = true;  
        StartCoroutine(Waitting(_tmp));
        Debug.Log(_tmp.textInfo.characterCount);
        Debug.Log(_tmp.textInfo.characterInfo.Length);

        for(int i = 0; i < _tmp.textInfo.characterCount; i++)
        {
            Debug.Log(_tmp.textInfo.characterInfo[i].character);
        }
    }

    private IEnumerator Waitting(TextMeshProUGUI _tmp)
    {
        WaitForSeconds delay = new WaitForSeconds(0.75f);

        int tmpCharIdx = _tmp.textInfo.characterCount;
        bool alphaZero = false;
        TMP_CharacterInfo info = _tmp.textInfo.characterInfo[tmpCharIdx - 1];
        Debug.Log(info.character);
        int meshIndex = info.materialReferenceIndex;
        int vertexIndex = info.vertexIndex;

        while(isWaitting)
        {
            yield return delay;

            alphaZero = !alphaZero;
            Color32[] vertexColors = _tmp.textInfo.meshInfo[meshIndex].colors32;
            byte alpha = (alphaZero) ? (byte)0 : (byte)255;

            vertexColors[vertexIndex + 0].a = alpha;
            vertexColors[vertexIndex + 1].a = alpha;
            vertexColors[vertexIndex + 2].a = alpha;
            vertexColors[vertexIndex + 3].a = alpha;

            _tmp.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

        }
    }

    private void SkipTween()
    {
        Speech_Management.isTalking = false;
    }
}
