using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public float loc;
    public float locRate;
    public float skill = 1f;
    public float timeToWrite = 1f;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyCost = 1f;
    public float regenAmount = 0.01f;
    public float regenTime = 1f;


    public TMP_Text locText;
    public TMP_Text description;
    //Buttons
    public Button autoCodeBtn;
    //public Button lvl1SkillBtn;
    //Panels
    public GameObject skillPanel;
    public GameObject actionsPanel;

    public Image energyBar;
    
    bool autocoding = false;
    bool isCoding = false;

    private void Start()
    {
        StartCoroutine(WelcomeMsg());
        StartCoroutine(Regen());
        //Disable components by default
        autoCodeBtn.gameObject.SetActive(false);
        skillPanel.gameObject.SetActive(false);
        //Event listeners
        //lvl1SkillBtn.onClick.AddListener(()=>SkillUpgrade(1.10f));

        currentEnergy=maxEnergy;
    }
    private void Update()

    {
        UnlockUpgrades();
        UpdateUI();
        
    }
    void UpdateUI()
    {
        Debug.Log(currentEnergy);
         
        locText.text = loc.ToString("0");
        if(currentEnergy > 70f)
        {
            energyBar.color = Color.green;
        }
        else if(currentEnergy > 40f)
        {
            energyBar.color = Color.yellow;
        }
        else
        {
            energyBar.color = Color.red;
        }


        if (autocoding) //Noooooob :D
        {
            autoCodeBtn.gameObject.SetActive(false);
        }
    }
    public void WriteCode()

    {
        isCoding = true;
        if (isCoding && currentEnergy >= 0f)
        {
            loc += skill;
            //Test
            currentEnergy -= energyCost;
            

        }
        else
        {
            description.text = "Your energy is drained";
        }
            isCoding = false;
    }
    public void EnableAuto()
    {
        autocoding = true;
 
        StartCoroutine(Autocode());
    }
    void UnlockUpgrades()
    {
        if (loc >= 10f)
        {
            autoCodeBtn.gameObject.SetActive(true);
        }
        if (loc >= 50)
        {
            skillPanel.gameObject.SetActive(true);
        }
    }
    public void SkillUpgrade(float skillLvl)
    {
        if(actionsPanel != null)
        {
            actionsPanel.gameObject.SetActive(true);
        }
        skill *= skillLvl;
        
        description.text = "Your LOCrate is now "+skill.ToString("F2");
        StartCoroutine(DescriptionChange());
    }
    public IEnumerator WelcomeMsg()
    {
        description.text = "Welcome to gitKing";
        yield return new WaitForSeconds(2f);
        description.text = "Push Code button to begin writing code";
        yield return new WaitForSeconds(2f);
        description.text = "...but be careful if you code to much without rest you will be exhausted...";
    }
    public IEnumerator Autocode()
    {
        
        while (autocoding) {
            loc += skill;
            yield return new WaitForSeconds(timeToWrite);
        }
        
    }
    public IEnumerator DescriptionChange()
    {
        yield return new WaitForSeconds(5f);
        description.text = String.Empty;
    }
    public IEnumerator Regen()
    {
        while (!isCoding)
        {
            if (autocoding)
            {
                regenAmount = 1f;
            }
            else {
                regenAmount = 2f;
            }
            yield return new WaitForSeconds(regenTime);
            if (currentEnergy < 100f)
            {
                currentEnergy += regenAmount;
            }
            energyBar.fillAmount = currentEnergy / maxEnergy;
        }
    }
    
}
