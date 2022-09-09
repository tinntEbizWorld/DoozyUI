using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodButton : MonoBehaviour
{
    public event EventHandler OnUsed;
    [SerializeField] Material enable, disable;
    MeshRenderer buttonMeshRenderer;
    Transform buttonTransform;
    bool canUseButton;
    private void Awake()
    {
        buttonTransform = transform.Find("Button");
        buttonMeshRenderer = buttonTransform.GetComponent<MeshRenderer>();
        canUseButton = true;
    }
    private void Start()
    {
        ResetButton();
    }

    private void ResetButton()
    {
        buttonMeshRenderer.material = enable;
        buttonTransform.localScale = new Vector3(0.6f, 3f, 0.6f);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, UnityEngine.Random.Range(-2f,2f));
        canUseButton = true;
    }
    public bool CanUseButton()
    {
        return canUseButton;
    }
    public void UseButton()
    {
        if (canUseButton)
        {
            buttonMeshRenderer.material = disable;
            buttonTransform.localScale = new Vector3(0.5f, 2f, 0.5f);
            canUseButton = false;

            OnUsed?.Invoke(this, EventArgs.Empty);
        }
    }
    public void CheckMeterial(bool _status)
    {
        buttonMeshRenderer.material = canUseButton == true ? enable : disable;
    }
}
