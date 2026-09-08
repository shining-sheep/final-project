using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuff : MonoBehaviour
{
    private SpriteRenderer sr;


    [Header("Buff details")]
    [SerializeField] private float buffDuration = 4;//持续时间
    [SerializeField] private bool canBeUsed = true;

    [Header("Floaty movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }
    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeUsed == false)
            return;

        StartCoroutine(BuffCo(buffDuration));
    }
    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        Debug.Log("buff获得:" + duration + "秒");

        yield return new WaitForSeconds(duration);
        Debug.Log("buff移除");
        Destroy(gameObject);
    }
}
