using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnergyBarUI : MonoBehaviour
{

    [Tooltip("Optional; Either assign a reference in the Editor (that implements IGetEnergySystem) or manually call SetEnergySystem()")]
    [SerializeField] private GameObject getEnergySystemGameObject;

    [Tooltip("Image to show the Energy Bar, should be set as Fill, the script modifies fillAmount")]
    [SerializeField] private Image image;

    private EnergySystem energySystem;

    private void Start()
    {
        if (EnergySystem.TryGetEnergySystem(getEnergySystemGameObject, out EnergySystem energySystem))
        {
            SetEnergySystem(energySystem); 
          //  SetEnergyBarFillAmount(100.0f); 

        }
    }


    public void SetEnergySystem(EnergySystem energySystem)
    {
        if (this.energySystem != null)
        {
            this.energySystem.OnEnergyChanged -= EnergySystem_OnEnergyChanged;
        }
        this.energySystem = energySystem;
        UpdateEnergyBar();
        energySystem.OnEnergyChanged += EnergySystem_OnEnergyChanged;
    }

    private void EnergySystem_OnEnergyChanged(object sender, System.EventArgs e)
    {
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (energySystem != null)
        {
            image.fillAmount = energySystem.GetEnergyNormalized();
        }
    }

    //private void Awake()
    //{
    //    image = GetComponent<Image>();
    //    SetEnergyBarFillAmount(1.0f); // Set the 
    //}

    private void SetEnergyBarFillAmount(float fillAmount)
    {
        image.fillAmount = fillAmount;
    }


    private void OnDestroy()
    {
        if (energySystem != null)
        {
            energySystem.OnEnergyChanged -= EnergySystem_OnEnergyChanged;
        }
    }
}