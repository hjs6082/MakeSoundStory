using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EmployPanel : StaffPanel
{
    public override Action init { get; set; } = null;
    public override Button exitButton { get; set; } = null;

    [SerializeField] private Button exitBtn = null;

    [Header("Select Button")]
    [SerializeField] private Button drawButton = null;
    [SerializeField] private Button listButton = null;

    [Header("Draw Staff")]
    [SerializeField] private GameObject startPanel = null;      // 시작시 띄워줄 패널
    [SerializeField] private ResumePanel resumePanel = null;    // 고용 로딩 화면
    [SerializeField] private DrawPanel drawPanel = null;        // 고용 메인 화면
    [SerializeField] private EmployWarningPanel warningPanel = null;

    [Header("Staff List")]
    [SerializeField] private ListPanel listPanel = null;

    protected override void Awake()
    {
        exitButton = exitBtn;
        base.Awake();
    }

    protected override void Update()
    {

    }

    protected override void InitValue()
    {
        // Select Button
        drawButton.onClick.AddListener(() => DrawStaff());
        listButton.onClick.AddListener(() => ListStaff());

        resumePanel.InitValue();
        drawPanel.InitValue();
        listPanel.InitValue();
        warningPanel.InitValue();

        TextUpdate();
        OffPanel();

        base.InitValue();
    }

    protected override void TextUpdate()
    {

    }

    public override void OnPanel()
    {
        base.OnPanel();

        startPanel.SetActive(true);

        TextUpdate();
    }

    public override void OffPanel()
    {
        // TODO : 패널 초기화 스크립트 작성
        startPanel.SetActive(true);
        drawPanel.gameObject.SetActive(false);
        listPanel.OffPanel();
        warningPanel.gameObject.SetActive(false);

        base.OffPanel();
    }

    public override void Warning()
    {
        warningPanel.OnPanel();

        base.Warning();
    }

    #region Draw Staff

    private void DrawStaff()
    {
        // TODO : 이력서 받는 중 트윈
        startPanel.SetActive(false);
        resumePanel.OnPanel();
        StartCoroutine(UIManagement.MethodDelay(() =>
        {
            drawPanel.OnPanel();
        }, 0.5f));
    }
    #endregion

    #region Staff List

    private void ListStaff()
    {
        if (StaffManager.instance.workStaffList.Count > 0)
        {
            startPanel.SetActive(false);
            listPanel.OnPanel();
        }
    }
    #endregion
}
