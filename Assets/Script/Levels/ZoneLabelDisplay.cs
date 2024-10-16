using System.Collections;
using UnityEngine;
using TMPro;

public class ZoneLabelDisplay : MonoBehaviour
{
    public TextMeshProUGUI zoneText;   
    public string zoneName;           
    public float displayDuration = 3f; 

    void OnEnable()
    {
        StartCoroutine(DisplayZoneLabel());
    }

   
    IEnumerator DisplayZoneLabel()
    {
        // set the zone text to the zone name and make it visible
        zoneText.text = zoneName;
        zoneText.color = Color.red;
        zoneText.gameObject.SetActive(true);

        // wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // hide the zone text after the duration
        zoneText.gameObject.SetActive(false);
    }
}