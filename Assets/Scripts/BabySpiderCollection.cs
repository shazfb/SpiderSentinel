using UnityEngine;
using System.Collections.Generic;

public class BabySpiderCollection : MonoBehaviour
{
    public Transform playerFollowPoint; 
    private List<Transform> collectedBabySpiders = new List<Transform>(); 

    public void CollectBabySpider(Transform babySpiderTransform)
    {
        BabySpiderController babySpiderController = babySpiderTransform.GetComponent<BabySpiderController>();
        if (babySpiderController != null)
        {
            babySpiderController.leader = GetPreviousBabySpiderFollowPoint();

            collectedBabySpiders.Add(babySpiderTransform);
        }
    }

    private Transform GetPreviousBabySpiderFollowPoint()
    {
        if (collectedBabySpiders.Count == 0)
        {
            return playerFollowPoint;
        }
        else
        {
            Transform previousBabySpider = collectedBabySpiders[collectedBabySpiders.Count - 1];
            return previousBabySpider.Find("FollowPoint");
        }
    }
}
