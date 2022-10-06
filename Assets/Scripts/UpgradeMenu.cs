using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Text dameText;

    [SerializeField]
    private float healthMultiplier =1.3f;

    [SerializeField]
    private float movementspeedMultiplier =1.3f;

    [SerializeField]
    private float dameMultiplier =1.3f;

    [SerializeField]
    private int upgradeCost=50;

    private PlayerStarts starts;

    private Weapon weapon;

    void OnEnable(){
        starts=PlayerStarts.instance;
        weapon=Weapon.instance;
        UpdateValues();
    }

    void UpdateValues(){
        healthText.text="HEALTH: " + starts.maxHealth.ToString();
        speedText.text="SPEED: " + starts.movementSpeed.ToString();
        dameText.text="DAME: " + weapon.Damage.ToString();
    }

    public void UpgradeHealth(){
        if(GameMaster.Money < upgradeCost){
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        starts.maxHealth =(int)(starts.maxHealth *healthMultiplier);
        GameMaster.Money-=upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
    public void UpgradeSpeed(){
        if(GameMaster.Money < upgradeCost){
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        starts.movementSpeed =Mathf.Round(starts.movementSpeed *movementspeedMultiplier);
        GameMaster.Money-=upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
     public void UpgradeDame(){
        if(GameMaster.Money < upgradeCost){
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        weapon.Damage =(int)(weapon.Damage *dameMultiplier);
        GameMaster.Money-=upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
}
