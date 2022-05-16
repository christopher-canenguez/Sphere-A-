using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Variables.
    [SerializeField] int maxPosX = 5;
    [SerializeField] MeshRenderer _renderer;
    [SerializeField] GameData _data;
    [SerializeField] ParticleSystem _splatFx;

    public AudioSource _bounceSound;

    Vector3 childPos;

    public static event Action OnCollideWithPlayer;

    // Initiated.
    private void Start()
    {
        _renderer.material = _data.GetRandomMaterial;

        childPos = transform.GetChild(0).transform.localPosition;

        childPos.x = UnityEngine.Random.Range(-maxPosX, maxPosX + 1);

        transform.GetChild(0).transform.localPosition = childPos;

        // Animations.
        LeanTween.moveLocalY(_renderer.transform.gameObject, -.5f, .5f).setEase(LeanTweenType.easeOutQuad);

        _bounceSound = GetComponent<AudioSource>();
    } // End Start.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).normal.y != -1f)
        {
            return;
        } // End if.

        // When player comes into contact with platform, increase score and bounce.
        if (collision.collider.CompareTag("Player"))
        {
            OnCollideWithPlayer?.Invoke();
            _splatFx.transform.position = collision.GetContact(0).point + (Vector3.up * .05f);
            _splatFx.Play();
            _bounceSound.Play();
        } // End if.
    } // End OnCollisionEnter.

    // When the platform comes into contact with wall, destroy plaatform object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        } // End if.
    } // End OnTriggerEnter.
} // End script.
