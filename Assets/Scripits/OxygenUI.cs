using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    [SerializeField] PlayerOxygen playerOxygen;
    [SerializeField] Slider oxygenSlider;
    [SerializeField] Image fillImage;
    void Update()
    {
        if (playerOxygen != null)
        {
            oxygenSlider.value = playerOxygen.GetOxygenPercent();
        }

     
            if (playerOxygen != null)
            {
                float percent = playerOxygen.GetOxygenPercent();
                oxygenSlider.value = percent;

                if (percent > 0.5f)
                    fillImage.color = Color.cyan;
                else if (percent > 0.2f)
                    fillImage.color = Color.yellow;
                else
                    fillImage.color = Color.red;
            }
        
    }


}