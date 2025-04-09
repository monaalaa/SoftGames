using UnityEngine;
using UnityEngine.UI;
public class ImageToggler : MonoBehaviour
{
    [SerializeField] 
    private Sprite on;
    [SerializeField] 
    private Sprite off;
    [SerializeField] 
    private Button toggleButton;

    private bool _isOn = true;
    private void Awake()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleImage);
        }
    }

    private void ToggleImage()
    {
        _isOn = !_isOn;
        toggleButton.image.sprite = _isOn ? on : off;
    }

    private void OnDestroy()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.RemoveListener(ToggleImage);
        }
    }
}
