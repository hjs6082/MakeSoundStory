using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_Strs
{
    // �⺻ �Ұ�
    private static readonly string[] tutorial_1_Strs =
    {
        "ȯ���մϴ�. ����� OO ȸ���Դϴ�.",
        "���ݺ��� �⺻���� �ý����� ����帮�ڽ��ϴ�.",
        "���⼭ ��¥�� Ȯ���� �� �ֽ��ϴ�. 12�� 31�ϱ��� ȸ�縦 ��ϰ� �˴ϴ�.",
        "���� ������ ���Դϴ�. ���� ���ڸ��� �Ļ��ϰ� �Ǵ� �����ؼ� ��ϼ���",
        "�� �濡 ���ؼ� �Ұ��ص帮�ڽ��ϴ�."
    };

    // ���� ���
    private static readonly string[] tutorial_2_Strs = 
    {
        "�̰��� ���������Դϴ�. ���������� ��ġ�ϰ�, ����ϴ� �����Դϴ�.",
        "NPC(�̸� ����)���� ���� �ɾ� �������� ����� �� �ֽ��ϴ�, �������� ����غ�����.",
        "���⼭ ���ϴ� ����� �����ϼ���.(�ʱް��, �߱ް��, ��ް��â����)",
        "�����մϴ�. OOO������ �Ի��Ͽ����ϴ�. ��ſ��� ���� ������ �ɰ��Դϴ�.",
        "NPC���� ���� �ɾ� ���� ����� Ȯ���� �� �ֽ��ϴ�.",
        "���⼭ ���� ����� Ȯ���� �� �ֽ��ϴ�.",
        "�� ���������� ���� �� ������ ���������� �����ؼ� Ȯ���ϼ���",
        "�ݱ��ư�� ���� �������ּ���",
        "���⼭�� ���� ��ġ�� �� �� �ֽ��ϴ�. ���� ��ġ�� ���Ͽ� �� �ǿ� �������� ��ġ�� �� �ֽ��ϴ�."
    };

    // �繫��
    private static readonly string[] tutorial_3_Strs = 
    {
        "����� �繫���Դϴ�. ���������� ��â���� �⸦ �� �ִ� �����Դϴ�.",
        "�̰��� �����ϴ� ���������� ��â���� �淯���� �˴ϴ�.",
        "���� ���ݱ��� ���� �� ���� ����Ʈ���� Ȯ���� �� �ս��ϴ�.",
        "���� ���ݱ��� ���� �� ���� ����Ʈ���� Ȯ���� �� �ս��ϴ�.",
        "�̰��� �����ϴ� ���������� ��ε����� �淯���� �˴ϴ�.",
        "�̰��� ���� �۾����Դϴ�. ���������� �ߵ����� �⸦ �� �ִ� �����Դϴ�.",
        "�̰��� �����ϴ� ���������� �ߵ����� �淯���� �˴ϴ�.",
        "�̰��� ���� �۾����Դϴ�. ���������� ���߼��� �⸦ �� ������, �� ������ ������ �����Դϴ�.",
        "�� ���� NPC���� ���� �ɾ� ���� ������ ������ �� �ֽ��ϴ�.",
        "���� ������ �����غ�����"
    };
    
    private static readonly string[] tutorial_4_Strs = 
    {
        "���������� �����ϼ���. �ּ� 3���� �������� �ʿ��մϴ�.",
        "�帣�� �������ּ���. ���������� �Ⱦ��ϴ� �帣�� ���� �ʰ� �����ϼ���. �г�Ƽ�� �ްԵ˴ϴ�.",
        "�� �۾��� �����մϴ�. �۾��� �� ��ġ�� ��ư���� Ÿ�ֿ̹� �°� ������ ������ ����˴ϴ�.",
        "40,80 ���θ��� �ٸ� �Ǳ� �۾��� �����ϰԵ˴ϴ�.",
        "�˸°� �Է��Ͽ� ������ ���� �����غ�����",
        "���� ���� �̸��� �������� �����Դϴ�.",
        "���� �̸��� �����ּ���",
        "Ʃ�丮���� ��������Դϴ�. ����� ���ϴ�"
    };

    public static List<string> GetTurotialStrsToList(int _idx)
    {
        List<string> str_List = null;

        switch (_idx)
        {
            case 1: str_List = new List<string>(tutorial_1_Strs); break;
            case 2: str_List = new List<string>(tutorial_2_Strs); break;
            case 3: str_List = new List<string>(tutorial_3_Strs); break;
        }

        return str_List;
    }

    public static Queue<string> GetTurotialStrsToQueue(int _idx)
    {
        Queue<string> str_Queue = new Queue<string>();
        string[] strs = null;

        switch (_idx)
        {
            case 1: strs = tutorial_1_Strs; break;
            case 2: strs = tutorial_2_Strs; break;
            case 3: strs = tutorial_3_Strs; break;
        }

        for(int i = 0; i < strs.Length; i++)
        {
            str_Queue.Enqueue(strs[i]);
        }

        return str_Queue;
    }
}
