using UnityEngine;
using System.Collections;

public class PulseLightAttack : MonoBehaviour
{

    public float lerpTime = 0.5f;
    private float i = 0;

    float maxDist = 100;
    public float speed = 100.0f;
    float timer;
    Light searchLight;
    // Use this for initialization
    void Start()
    {
        searchLight = this.GetComponent<Light>();
        searchLight.range = 0;
        maxDist = GetComponentInParent<Backstab>().AttackRange;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        searchLight.range = timer * speed;
        if (searchLight.range > maxDist)
        {
            searchLight.range = 1;
            timer = 0;
        }
        i += Time.deltaTime;
    }

}