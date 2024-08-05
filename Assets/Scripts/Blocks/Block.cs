using System;
using UnityEngine;

public class Block : MonoBehaviour
{    
    public event Action<Block> OnBlockedDestroyedEvent;

    [SerializeField]
    AudioClip hitSound;   

    private void OnCollisionEnter2D(Collision2D collision) => OnBlockGetHit();   

    private void OnBlockGetHit()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        OnBlockedDestroyedEvent?.Invoke(this);
        Destroy(gameObject);
    }
}
