using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    public float tick;

    public int nightStart;
    public int nightEnd;
    public float lightOpenMinute=45f;
    public int dayStart;
    public int dayEnd;
    public float lightCloseMinute=15f;
    public GameObject[] lights;

    private float minutes;
    public int hours;
    private int days;

    private Volume ppv;
    

    public bool isStopped;
    private bool isLightsOn;
    void Start()
    {
        isLightsOn=true;
        isStopped=false;
        minutes=0;
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
        if (hours>=nightStart & hours<nightEnd)
        {
            float var = ((nightEnd-nightStart)*60);
            float lagas = ((float)(hours-nightStart)*60+(float)minutes);
            ppv.weight =  lagas / var;
            if (minutes>=lightCloseMinute && !isLightsOn)
            {
                // open lights
                SetLights(true);
                isLightsOn=true;
            }

        }
        

        if(hours>=dayStart && hours<dayEnd) // Dawn at 6:00 / 6am    -   until 7:00 / 7am
        {
            float var = ((dayEnd-dayStart)*60);
            float lagas = ((float)(hours-dayStart)*60+(float)minutes);
            ppv.weight = 1 - ( lagas / var); // we minus 1 because we want it to go from 1 - 0
            if (minutes>=lightOpenMinute && isLightsOn)
            {
                // close lights
                SetLights(false);
                isLightsOn=false;
            }
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
    private void SetLights(bool status)
    {
        foreach (var item in lights)
        {
            item.SetActive(status);
        }
    }
}
