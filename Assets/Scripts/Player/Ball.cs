using UnityEngine;

public class Ball : MonoBehaviour
{   
    private Rigidbody2D rb;
    private Transform paddlePosition;
    private bool isStickingToPaddle = true;

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall(Vector2 ballLaunchStrength)
    {
        isStickingToPaddle = false;
        rb.velocity = ballLaunchStrength;        
    }
    /// <summary>
    /// Should push ball to left or right depends on paddle movement direction
    /// </summary>
    /// <param name="velocityModification"></param>
    public void ChangeBallVelocity(Vector2  velocityModification)
    {
        //rb.velocity += velocityModification;
        //rb. AddForce (velocityModification);      
    }

    public void StickToPaddle(Vector3 ballPosition)
    {
        if(isStickingToPaddle)
            transform.position = ballPosition;
    }

}
