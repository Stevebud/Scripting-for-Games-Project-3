using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider _energyBar = null;
    [SerializeField] Text _energyText = null;
    [SerializeField] Slider _speedBar = null;
    [SerializeField] Text _speedText = null;
    [SerializeField] Slider _speedBarR = null;
    [SerializeField] Text _speedTextR = null;
    [SerializeField] FPSMotor _fpsMotor = null;

    public float speedMultiplier = 1f;
    //public 
    public float energy = 100f;
    public float energyRegen = 0.05f;

    private void Start()
    {
        _speedBar.gameObject.SetActive(false);
        _speedBarR.gameObject.SetActive(false);
        _speedText.gameObject.SetActive(false);
        _speedTextR.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (energy < 100f && !Input.GetMouseButton(1))
        {
            energy += energyRegen;
        }
        updateSpeed();
        updateEnergy();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _speedBar.gameObject.SetActive(true);
            _speedBarR.gameObject.SetActive(true);
            _speedText.gameObject.SetActive(true);
            _speedTextR.gameObject.SetActive(true);
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            _speedBar.gameObject.SetActive(false);
            _speedBarR.gameObject.SetActive(false);
            _speedText.gameObject.SetActive(false);
            _speedTextR.gameObject.SetActive(false);
        }
    }

    private void updateEnergy()
    {
        _energyBar.value = energy;
        _energyText.text = energy-(energy%1) + " ";
    }

    private void updateSpeed()
    {
        _speedBar.value = _fpsMotor.speed * speedMultiplier;
        _speedBarR.value = _speedBar.value;
        _speedText.text = (_fpsMotor.speed * speedMultiplier) - (_fpsMotor.speed * speedMultiplier)%1 + " ";
        _speedTextR.text = _speedText.text;
    }
}
