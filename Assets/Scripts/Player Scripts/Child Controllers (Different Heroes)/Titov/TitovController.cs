using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitovController : PlayerController
{
    [SerializeField] private Image titovLight; 
    [SerializeField] private float lightPrepare;
    [SerializeField] private float lightTime;
    private bool isLightActivated = false;

    private void Start() {
        titovLight.gameObject.SetActive(true);
        titovLight.CrossFadeAlpha(0, 0, false);
    }

    protected override void Ability()
    {        
        if (!isPrefab) StartCoroutine(LightActivation());
    }

    private IEnumerator LightActivation() {
        titovLight.CrossFadeAlpha(1, lightPrepare, false);
        yield return new WaitForSeconds(lightPrepare);
        isLightActivated = true;
        yield return new WaitForSeconds(1);
        isLightActivated = false;
        titovLight.CrossFadeAlpha(0, lightPrepare / 2, false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isLightActivated && other.gameObject.GetComponent<EnemyBehaviour>() != null) {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (isLightActivated && other.gameObject.GetComponent<EnemyBehaviour>() != null) {
            Destroy(other.gameObject);
        }
    }
}
