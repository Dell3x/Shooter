using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    [SerializeField] private Destructable owner;
    public bool rotateBar = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponent<Image>();

        if (owner.gameObject.GetComponent<CharacterController>() != null)
        {
            rotateBar = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.InverseLerp(0.0f, owner.hitPoints, owner.hitPointsCurrent);
        

        if (rotateBar)
        {
            transform.forward = Camera.main.transform.forward;

        }
    }
}
