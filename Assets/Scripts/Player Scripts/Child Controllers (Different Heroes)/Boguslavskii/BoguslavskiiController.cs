using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoguslavskiiController : PlayerController
{
    [SerializeField] private GameObject xboxPrefab;

    protected override void Ability()
    {     
        if (!isPrefab) {
            float distanceToXbox = isLookingRight ? 5f : -5f;

            Vector2 newXboxPosition = new Vector2(transform.position.x + distanceToXbox, transform.position.y + 6);
            GameObject newXbox = Instantiate(xboxPrefab, newXboxPosition, Quaternion.identity, projectilePackage.transform);

            newXbox.SetActive(true);      
        }         
    }
}
