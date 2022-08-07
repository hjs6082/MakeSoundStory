/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    [Header("Data Time Setting")]
    [Range(1, 28)]
    public int dateInMonth;
    [Range(1, 4)]
    public int season;
    [Range(1, 99)]
    public int year;
    [Range(0, 24)]
    public int hour;
    [Range(0, 6)]
    public int minutes;

    private DateTime dateTime;

    [Header("Tick Settings")]
    public int tickSecondsIncrease = 10;
    public float TimeBetweenTicks = 1;
    private float currentTimeBetweenTicks = 0;

    public static UnityAction<DateTime> onDateTimeChanged;

    private void Awake()
    {
        dateTime = new DateTime(dateInMonth, season - 1, year, hour, minutes * 10);

        Debug.Log($"New Years Day: {DateTime.NewYearsDay(2)}");
    }

    // Start is called before the first frame update
    void Start()
    {
        onDateTimeChanged?.Invoke(dateTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct DateTime
    {
        #region
        private Days day;
        private int date;
        private int year;

        private int hour;
        private int minutes;

        private Season season;

        private int totalNumDays;
        private int totalNumWeeks;
    }
}
*/