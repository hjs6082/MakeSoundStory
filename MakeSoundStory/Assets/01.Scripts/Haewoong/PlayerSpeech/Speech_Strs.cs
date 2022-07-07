using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_Strs
{
    // 기본 소개
    private static readonly string[] tutorial_1_Strs =
    {
        "환영합니다. 여기는 OO 회사입니다.",
        "지금부터 기본적인 시스템을 설명드리겠습니다.",
        "여기서 날짜를 확인할 수 있습니다. 12월 31일까지 회사를 운영하게 됩니다.",
        "현재 소지한 돈입니다. 돈이 모자르면 파산하게 되니 주의해서 운영하세요",
        "각 방에 대해서 소개해드리겠습니다."
    };

    // 직원 고용
    private static readonly string[] tutorial_2_Strs = 
    {
        "이곳은 스태프실입니다. 스태프들을 배치하고, 고용하는 공간입니다.",
        "NPC(이름 미정)에게 말을 걸어 스태프를 고용할 수 있습니다, 스태프를 고용해보세요.",
        "여기서 원하는 고용을 선택하세요.(초급고용, 중급고용, 상급고용창에서)",
        "축하합니다. OOO직원이 입사하였습니다. 당신에게 많은 도움이 될것입니다.",
        "NPC에게 말을 걸어 직원 목록을 확인할 수 있습니다.",
        "여기서 직원 목록을 확인할 수 있습니다.",
        "각 스태프들의 스탯 및 레벨이 적혀있으니 주의해서 확인하세요",
        "닫기버튼을 눌러 종료해주세요",
        "여기서는 직원 배치를 할 수 있습니다. 직원 배치를 통하여 각 실에 스태프를 배치할 수 있습니다."
    };

    // 사무실
    private static readonly string[] tutorial_3_Strs = 
    {
        "여기는 사무실입니다. 스태프들의 독창성을 기를 수 있는 공간입니다.",
        "이곳에 상주하는 스태프들은 독창성이 길러지게 됩니다.",
        "또한 지금까지 만든 곡 제작 리스트들을 확인할 수 잇습니다.",
        "또한 지금까지 만든 곡 제작 리스트들을 확인할 수 잇습니다.",
        "이곳에 상주하는 스태프들은 멜로디컬이 길러지게 됩니다.",
        "이곳은 서브 작업실입니다. 스태프들의 중독성을 기를 수 있는 공간입니다.",
        "이곳에 상주하는 스태프들은 중독성이 길러지게 됩니다.",
        "이곳은 메인 작업실입니다. 스태프들의 대중성을 기를 수 있으며, 곡 제작이 가능한 공간입니다.",
        "곡 제작 NPC에게 말을 걸어 음악 제작을 시작할 수 있습니다.",
        "음악 제작을 시작해보세요"
    };
    
    private static readonly string[] tutorial_4_Strs = 
    {
        "스태프들을 선택하세요. 최소 3명의 스태프가 필요합니다.",
        "장르를 선택해주세요. 스태프들이 싫어하는 장르를 고르지 않게 조심하세요. 패널티를 받게됩니다.",
        "곡 작업을 시작합니다. 작업은 각 위치의 버튼들을 타이밍에 맞게 누르는 것으로 진행됩니다.",
        "40,80 프로마다 다른 악기 작업을 진행하게됩니다.",
        "알맞게 입력하여 나만의 곡을 제작해보세요",
        "이제 곡의 이름을 설정해줄 차례입니다.",
        "곡의 이름을 정해주세요",
        "튜토리얼은 여기까지입니다. 행운을 빕니다"
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
