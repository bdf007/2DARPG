using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image healthBarFill;

    void OnEnable ()
    {
        character.onTakeDamage += UpdateHealthBar;
        character.onHeal += UpdateHealthBar;
    }

    void OnDisable ()
    {
        character.onTakeDamage -= UpdateHealthBar;
        character.onHeal -= UpdateHealthBar;
    }

    void Start ()
    {
        SetNameText(character.DisplayName);
    }

    void SetNameText (string name)
    {
        nameText.text = name;
    }

    void UpdateHealthBar ()
    {
        float healthPercent = (float)character.CurHp / (float)character.MaxHp;
        healthBarFill.fillAmount = healthPercent;
    }
}
