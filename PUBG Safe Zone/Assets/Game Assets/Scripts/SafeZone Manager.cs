using System.Collections;
using UnityEngine;

public class SafeZoneManager : MonoBehaviour
{
    public Transform safeZoneObj;
    public float safeZoneShrinkFactor;
    public float cooldown;
    public float timeElapsed = 0f;
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > cooldown)
        {
            timeElapsed = 0f;
            StartCoroutine(ShrinkSafeZone());
            cooldown = timeElapsed + cooldown;
        }
    }

    IEnumerator ShrinkSafeZone()
    {
        Vector3 startScale = new Vector3(safeZoneObj.transform.localScale.x, safeZoneObj.transform.localScale.y, safeZoneObj.transform.localScale.z);
        Vector3 desiredScale = new Vector3(safeZoneObj.transform.localScale.x * safeZoneShrinkFactor, safeZoneObj.transform.localScale.y, safeZoneObj.transform.localScale.z * safeZoneShrinkFactor);
        float startTime = 0f;
        float endTime = 4f;

        while(endTime > startTime)
        {
            safeZoneObj.localScale = Vector3.Lerp(startScale, desiredScale, startTime / endTime);
            startTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"WELCOME {other}");
    }
}