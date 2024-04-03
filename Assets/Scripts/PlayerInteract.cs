using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionRange = 3f;
    private List<GameObject> nearbyBabySpiders = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject babySpider in nearbyBabySpiders)
            {
                BabySpiderController babySpiderController = babySpider.GetComponent<BabySpiderController>();
                if (babySpiderController != null)
                {
                    babySpiderController.enabled = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BabySpider"))
        {
            nearbyBabySpiders.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BabySpider"))
        {
            nearbyBabySpiders.Remove(other.gameObject);
        }
    }
}
