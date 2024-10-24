using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownBarBehavior : MonoBehaviour
{
    public static cooldownBarBehavior instance;
    public Image cooldownBar;
    public Vector3 offSet;

    private void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        cooldownBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //cooldownBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
    }

    public void CooldownBar(float cooldown, float maxCooldown)
    {
        cooldownBar.gameObject.SetActive(cooldown < maxCooldown);

        cooldownBar.fillAmount = cooldown / maxCooldown;

        /* slider.value = cooldown;
        slider.maxValue = maxCooldown;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue); 
        */

    }
}
