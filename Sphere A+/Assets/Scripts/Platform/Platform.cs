using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Platform : MonoBehaviour
{
    [SerializeField] int maxPosX = 5;
    [SerializeField] MeshRenderer renderer;
    [SerializeField] GameData data;
    [SerializeField] ParticleSystem splatFx;

    Vector3 childPos;

    public static event Action OnCollideWithPlayer;

    private void Start()
    {
        renderer.material = data.GetRandomMaterial;

        childPos = transform.GetChild(0).transform.localPosition;
        childPos.x = UnityEngine.Random.Range(-maxPosX, maxPosX + 1);
        transform.GetChild(0).transform.localPosition = childPos;

        LeanTween.moveLocalY(renderer.transform.gameObject, -.5f, .5f).setEase(LeanTweenType.easeOutQuad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).normal.y != -1f) return;

        if (collision.collider.CompareTag("Player"))
        {
            OnCollideWithPlayer?.Invoke();
            splatFx.transform.position = collision.GetContact(0).point + (Vector3.up * .01f);
            splatFx.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
