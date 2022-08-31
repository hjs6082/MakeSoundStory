using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Piano
{
    public struct NoteInfo
    {
        public Image _note;
        public Vector2 _notePos;
        public bool _isNull;

        public void SetPos(float x, float y)
        {
            _notePos.x = x;
            _notePos.y = y;
        }
    }

    public class Piano_NoteSpawner : MonoBehaviour
    {
        private const int LINE_COUNT = 8;

        [SerializeField] private Image[] note_Prefabs = null;
        [SerializeField] private Transform note_Parent = null;

        public GameObject linePrefabs = null;
        public Transform lineParent = null;

        public Image[] guideLines = null;
        public List<int> lineIdxs = null;
        public List<Image>[] notes_List = null;

        public void InitValue()
        {
            guideLines = Piano_Management.Instance.guideLine_Parent.GetComponentsInChildren<Image>();

            notes_List = new List<Image>[LINE_COUNT];
            for (int i = 0; i < LINE_COUNT; i++)
            {
                lineIdxs.Add(i);
                notes_List[i] = new List<Image>();
            }

            InitNotes();
        }

        public void InitNotes()
        {
            for (int i = 0; i < lineIdxs.Count; i++)
            {
                for (int j = 0; j < Piano_Management.Instance.lineStackNum; j++)
                {
                    Image note = SpawnNote(i);
                    notes_List[i].Add(note);
                }
            }
        }

        public void NoteTap(int _idx)
        {
            Piano_Note note = notes_List[_idx][0].GetComponent<Piano_Note>();

            note.Tap();
        }

        public void PopNotes()
        {
            Piano_Management.Instance.isCanTap = false;

            for (int i = 0; i < LINE_COUNT; i++)
            {
                Image obj = notes_List[i][0];
                Piano_Note note = obj.GetComponent<Piano_Note>();

                Piano_Management.Instance.p_Music.InputSound(i, note.isTap);








                

                obj.DOComplete();
                obj.rectTransform.DOSizeDelta(Vector2.zero, 0.25f)
                .OnComplete(() =>  { Destroy(obj); });

                if (notes_List[i].Count > 1)
                {
                    for (int j = 0; j < notes_List[i].Count; j++)
                    {
                        RectTransform rect = notes_List[i][j].rectTransform;

                        rect.DOComplete();
                        rect.DOAnchorPosY(rect.anchoredPosition.y - 100.0f, 0.5f);
                    }
                }
                else
                {
                    print("ÀÀ¾Ö");
                    // end
                    Piano_Management.Instance.MakeEnd();
                }

                notes_List[i].RemoveAt(0);
            }

            Piano_Management.Instance.isCanTap = true;
        }

        private Image SpawnNote(int _lineIdx)
        {
            NoteInfo noteInfo;

            if (_lineIdx == 0 || _lineIdx == 1) { noteInfo = SetNote(0, _lineIdx); }
            else if (_lineIdx == 2 || _lineIdx == 3) { noteInfo = SetNote(1, _lineIdx); }
            else if (_lineIdx == 4 || _lineIdx == 5) { noteInfo = SetNote(2, _lineIdx); }
            else { noteInfo = SetNote(3, _lineIdx); }

            Image note = Instantiate<Image>(noteInfo._note, guideLines[_lineIdx].transform);
            RectTransform noteRect = note.GetComponent<RectTransform>();

            noteRect.anchoredPosition = noteInfo._notePos;

            return note;
        }

        private NoteInfo SetNote(int _noteIdx, int _lineIdx)
        {
            NoteInfo noteInfo = new NoteInfo();

            noteInfo._note = note_Prefabs[_noteIdx];
            noteInfo.SetPos(guideLines[_lineIdx].rectTransform.anchoredPosition.x, (100.0f * notes_List[_lineIdx].Count) - 15.0f);

            return noteInfo;
        }
    }
}
