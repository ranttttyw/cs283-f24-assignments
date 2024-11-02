using UnityEngine;
using TMPro;
using System.Collections; // Add this line

public class CollectionGame : MonoBehaviour
{
    public TextMeshProUGUI collectionCounterText;
    private int collectionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            collectionCount++;
            UpdateCounterText();

            // Trigger the rise-up effect
            var effect = other.GetComponent<CollectionEffect>();
            if (effect != null)
            {
                effect.TriggerEffect();
            }
            else
            {
                Debug.LogWarning("CollectionEffect component not found on collected object.");
            }

            // Delay hiding the object to allow the effect to complete
            StartCoroutine(HideObjectAfterDelay(other.gameObject, effect.riseDuration));
        }
    }

    private void UpdateCounterText()
    {
        collectionCounterText.text = "Count: " + collectionCount;
    }

    private IEnumerator HideObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false); // Hide the object after the effect completes
    }
}


