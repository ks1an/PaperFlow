using UnityEngine;

public sealed class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] InputSeed _inputSeedField;

    public void SetActiveMenu(bool isActive)
    {
        if(!isActive)
            _inputSeedField.DisableInputSeed();

        _menu.SetActive(isActive);
    }

    public void SetActiveInputSeedField()
    {
        if(_inputSeedField.gameObject.activeSelf)
            _inputSeedField.DisableInputSeed();
        else
            _inputSeedField.gameObject.SetActive(true);
    }
}