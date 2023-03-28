using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateHealthBar(float maxHp, float curentHp)
    {
        _image.fillAmount = curentHp / maxHp;
    }
}
