using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour, IAttackable
{
    public int health;
    public Slider healthSlider;

    public delegate void DestroyedObject();
    public event DestroyedObject OnDestroyedObject;
    void Start()
    {
        healthSlider.minValue = 0;
        healthSlider.maxValue = health;
        healthSlider.value = healthSlider.maxValue;
    }
    private void Update()
    {
        if (health <= 0)
        {
            OnDestroyedObject?.Invoke();
            //Destroy(this.gameObject);
        }
    }
    public void ReceiveDamage(int damage = 20)
    {
        health -= damage;
        healthSlider.value = health;
    }
}
