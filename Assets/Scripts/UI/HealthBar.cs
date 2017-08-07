using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public PlayerMovement player;


    public float maxHealth = 100.0f;

    public Canvas HUD;
    private Image healthCurrent;
    private Image healthWhite;
    private Image healthOutline;

	// Use this for initialization
	void Start () {
		HUD = transform.Find("Canvas").GetComponent<Canvas>();
        healthCurrent = HUD.transform.Find("HealthCurrent").GetComponent<Image>();
        healthWhite = HUD.transform.Find("HealthWhite").GetComponent<Image>();
        healthOutline = HUD.transform.Find("HealthOutline").GetComponent<Image>();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		float health = player.health;

        healthCurrent.transform.position = healthOutline.transform.position;
        healthWhite.transform.position = healthOutline.transform.position;
        
        healthCurrent.transform.localScale = healthOutline.transform.localScale;
        healthWhite.transform.localScale = healthOutline.transform.localScale;
        healthCurrent.rectTransform.sizeDelta = healthOutline.rectTransform.sizeDelta;
        healthWhite.rectTransform.sizeDelta = healthOutline.rectTransform.sizeDelta;

        healthCurrent.fillAmount = health / maxHealth;
        healthWhite.fillAmount = Mathf.Lerp(healthWhite.fillAmount, healthCurrent.fillAmount, Time.deltaTime *  2f);

	}
}
