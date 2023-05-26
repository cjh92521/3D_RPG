using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateBarUI : MonoBehaviour
{
    readonly string HP_FORMAT = "<size=70>{0}</size> / {1}";
    readonly string Energy_FORMAT = "<size=25>{0}</size> / {1}";

    [SerializeField] private Stateable playerStatus;
    
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image energyBar;
    [SerializeField] private TMP_Text energyText;

    public void Update()
    {
        hpBar.fillAmount = playerStatus.hpFill;
        int maxHp = Mathf.RoundToInt(playerStatus.MaxHp);
        int hp = Mathf.RoundToInt(playerStatus.Hp);
        hpText.text = string.Format(HP_FORMAT, hp, maxHp);

        energyBar.fillAmount = playerStatus.energyFill;
        int maxEnergy = Mathf.RoundToInt(playerStatus.MaxEnergy);
        int energy = Mathf.RoundToInt(playerStatus.Energy);
        energyText.text = string.Format(Energy_FORMAT, energy, maxEnergy);
    }

}
