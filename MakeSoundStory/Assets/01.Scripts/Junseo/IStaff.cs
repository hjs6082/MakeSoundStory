using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStaff
{
    enum staffStatus
    {
        none,
        talk
    };
    void Say(StaffSO staff); // �������� ���ϴ°�
                             // �������� �ൿ�Ұ�
                             // �������� ��Ź�ϴ� ��
                             // ������ �̺�Ʈ
}
