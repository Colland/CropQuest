using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpController : MonoBehaviour
{
    public static ExpController instance;

    [SerializeField] private Text levelText;
    [SerializeField] private Text experienceText;
    [SerializeField] private float level;
    public float currentExp;
    [SerializeField] private float targetExp;
    [SerializeField] private Image progressBar;

    void Awake() {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        experienceText.text = currentExp + " / " + targetExp;
        XP();
    }

    public void XP() {
        levelText.text = "Level : " + level.ToString();
        progressBar.fillAmount = (currentExp / targetExp);

        if (currentExp >= targetExp) {
            currentExp = currentExp - targetExp;
            level++;
            targetExp += 50;
        }
    } 
}
