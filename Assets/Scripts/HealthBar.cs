using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Transform mainCameraTransform;

    public Slider healthSlider;

    IPawn attachedParent;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;

        attachedParent = GetComponentInParent<IPawn>();

        healthSlider.minValue = 0;

        healthSlider.maxValue = attachedParent.MaxHealth;
    }

    void Update()
    {
        healthSlider.value = attachedParent.MaxHealth;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.forward);
    }
}
