using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public float tick;
    public GameObject[] lights;

    private float minutes;
    private int hours;
    private int days;

    private Volume ppv;
    

    public bool isStopped;
    void Start()
    {
        isStopped=false;
        minutes=0;
        hours=0;
        days=0;
        ppv=GetComponent<Volume>();
    }
    void FixedUpdate()
    {
        if (isStopped)
        {
            return;
        }

        minutes += Time.fixedDeltaTime*tick;
        if (minutes>=60)
        {
            minutes=0;
            hours+=1;
        }
        if (hours>=24)
        {
            hours=0;
            days+=1;
        }
        SetVolume();
    }
    private void SetVolume()
    {
        if (hours>=21 & hours<22)
        {
            ppv.weight =  (float)minutes / 60;
        }

        if(hours>=6 && hours<7) // Dawn at 6:00 / 6am    -   until 7:00 / 7am
        {
            ppv.weight = 1 - (float)minutes / 60; // we minus 1 because we want it to go from 1 - 0
        }

    }

    public void SetIsStopped(bool isStopped)
    {
        this.isStopped=isStopped;
    }    
    public int GetDays()
    {
        return days;
    }
    public int GetHours()
    {
        return hours;
    }
    public float GetMinutes()
    {
        return minutes;
    }

}
