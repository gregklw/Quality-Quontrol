using System.Collections;
using UnityEngine;

/// <summary>
/// Script used to trigger an alert effect for a point light.
/// </summary>
public class AlertLight : MonoBehaviour
{
    /// <summary>
    /// The light used for this alert light.
    /// </summary>
    [SerializeField] private Light _targetLight;

    /// <summary>
    /// The sound that plays when the alert starts.
    /// </summary>
    [SerializeField] private AudioSource _alertAudio;

    /// <summary>
    /// Coroutine reference used to make sure only one
    /// coroutine runs at a given time.
    /// </summary>
    private IEnumerator _alertCoroutineRef;

    /// <summary>
    /// Sets the speed factor of how fast the light flashes.
    /// </summary>
    private const int DELTA_TIME_MULTIPLIER = 10;

    /// <summary>
    /// The amount of cycles the alert goes through.
    /// </summary>
    private const int FLASH_AMOUNT = 8;

    /// <summary>
    /// Used to reference the light's intensity right before the alert starts.
    /// </summary>
    private float _oldLightIntensity;

    /// <summary>
    /// Turns on the alert if it's not on already.
    /// </summary>
    public void TurnOnAlert()
    {
        if (_alertCoroutineRef == null)
        {
            _alertAudio.Play();
            _oldLightIntensity = _targetLight.intensity;
            _alertCoroutineRef = AlertCoroutine();
            StartCoroutine(_alertCoroutineRef);
        }
    }

    /// <summary>
    /// Turns off the alert immediately.
    /// </summary>
    public void TurnOffAlert()
    {
        if (_alertCoroutineRef != null)
        {
            StopCoroutine(_alertCoroutineRef);
            TurnOffAlertHelper();
        }
    }

    /// <summary>
    /// Coroutine that creates a red alert effect.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AlertCoroutine()
    {
        var flashCounter = FLASH_AMOUNT;
        bool isIncreasing = true;
        while (flashCounter > 0)
        {
            ///------------decrease the intensity until it reaches a value of 2------------------///
            if (isIncreasing)
            {
                _targetLight.color = Color.red;

                if (_targetLight.intensity < 4)
                {
                    _targetLight.intensity = Mathf.Clamp(_targetLight.intensity + Time.deltaTime * DELTA_TIME_MULTIPLIER, 2, 4);
                }
                else
                {
                    isIncreasing = false;
                    flashCounter--;
                }
            }
            ///---------------------------------------------------------------------------------------///

            //-------------increase the intensity until it reaches a value of 4------------------///
            else
            {
                if (_targetLight.intensity > 2)
                {
                    _targetLight.intensity = Mathf.Clamp(_targetLight.intensity - Time.deltaTime * DELTA_TIME_MULTIPLIER, 2, 4);
                }
                else
                {
                    isIncreasing = true;
                    flashCounter--;
                }
            }
            //---------------------------------------------------------------------------------------///

            yield return null;
        }
        TurnOffAlertHelper();
    }

    /// <summary>
    /// Helper that sets the alert light back to its original state.
    /// </summary>
    private void TurnOffAlertHelper()
    {

        _targetLight.color = Color.white;
        _targetLight.intensity = _oldLightIntensity;
        _alertAudio.Stop();
        _alertCoroutineRef = null;
    }
}
