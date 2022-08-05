using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FSM : MonoBehaviour
{
    public enum eState
    {
        WAIT,
        MOVE
    }

    public GameObject npc_Unit_Prefab = null;
    public int npc_Unit_Idx = 0;

    public GameObject npc_Unit = null;
    public RectTransform npc_RectTrm = null;
    public Transform npc_Trm = null;
    public Transform[] move_Points = null;

    public WaitForSeconds stateDelay = new WaitForSeconds(2.0f);

    [Range(0.0f, 1.0f)]
    public float move_Speed = 0.5f;
    public float move_Time = 0.0f;
    public int curPoint = 0;
    public eState curState = default;

    public bool isOnce = false;
    public bool isWait = false;

    public void InitValue()
    {
        print(string.Format("###", npc_Unit_Idx));

        npc_Unit = Instantiate(npc_Unit_Prefab, this.transform.position, Quaternion.identity, this.transform);

        npc_Trm = npc_Unit.transform;

        npc_RectTrm = npc_Unit.GetComponent<RectTransform>();
        npc_RectTrm.localScale = Vector3.one;
        npc_RectTrm.anchoredPosition = Vector2.zero;

        curPoint = 0;
        curState = default;
    }

    private void Update()
    {
        if (isOnce)
        {
            switch (curState)
            {
                case eState.WAIT:
                    if (!isWait) { isWait = true; Wait(); }
                    break;
                case eState.MOVE:
                    Move();
                    break;
            }
        }
    }

    public void Init()
    {
        isOnce = true;
        InitValue();
        StartCoroutine(StateDelay());
    }

    public void Move()
    {
        curPoint %= move_Points.Length;

        npc_Trm.localScale = (npc_Trm.position.x - move_Points[curPoint].position.x < 0) ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        move_Time += Time.deltaTime;
        npc_Trm.position = Vector3.Lerp(npc_Trm.position, move_Points[curPoint].position, move_Time * Time.deltaTime * move_Speed);

        if (Vector2.Distance(npc_Trm.position, move_Points[curPoint].position) <= 0.01f)
        {
            npc_Trm.position = move_Points[curPoint].position;

            move_Time -= move_Time;
            curPoint += Random.Range(1, 5);

            ChangeState();
        }
    }

    public void Wait()
    {
        isWait = true;

        StartCoroutine(StateDelay());
    }

    public eState ChangeState()
    {
        int randIdx = Random.Range(0, 10);
        eState state = (randIdx > 4) ? eState.MOVE : eState.WAIT;

        isWait = false;

        return state;
    }

    public IEnumerator StateDelay()
    {
        yield return stateDelay;

        curState = ChangeState();
    }


}
