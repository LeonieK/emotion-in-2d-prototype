using UnityEngine;
using UnityEngine.UI;

public class ToggleBehavior : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Toggle toggle;

    void Start()
    {
        toggle.onValueChanged.AddListener(delegate{
                ToggleValueChanged(toggle);
            });
    }

    public void ToggleValueChanged(Toggle change)
    {
        if(change.isOn)
        {
            button.SetActive(true);
        } else {
            button.SetActive(false);
        }
        
    }
}
