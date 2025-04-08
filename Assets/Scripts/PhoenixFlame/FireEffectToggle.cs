using UnityEngine;
using UnityEngine.UI;

public class FireEffectToggle : MonoBehaviour
{
    public Animator fireAnimator;

    public Button toggleButton;

    private bool isFireOn = true;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleFireEffect);
    }

    void ToggleFireEffect()
    {
        isFireOn = !isFireOn;

        fireAnimator.SetBool("IsFireOn", isFireOn);
    }
}
