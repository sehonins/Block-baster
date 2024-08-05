using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    InputControler inputControler;
    [SerializeField]
    GameDataManager gameDataManager;
    [SerializeField]
    Ball ball;
    [SerializeField]
    AudioClip clip;

    private Rigidbody2D rb;

    [Header("Configs")]
    [SerializeField]
    Vector2 ballLaunchStrength = new Vector2(1, 10);
    [SerializeField]
    Vector2 ballVelocityModificatior = new Vector2(1,0);
    //float speedIncreaseFactor = 1.01f;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float leftBoundary = -7f;
    [SerializeField]
    private float rightBoundary = 7f;

    Vector3 differneceBetwenPadleAndBall;
    private bool isBallLounched = false;
    private bool isDataLoaded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
        inputControler.ActionButtonPressed += ShootBall;
        gameDataManager.OnGameDataLoaded += Init;        
    }
   
    private void Init()
    {
        ball.Init();
        differneceBetwenPadleAndBall = ball.transform.position - transform.position;
        isDataLoaded = true;
    }
   
    private void ShootBall()
    {
        if (isBallLounched) return;
        isBallLounched = true;
        ball.LaunchBall(ballLaunchStrength);
    }
    
    private void Update()
    {
        Vector2 velocity = new Vector2(inputControler.XInput * moveSpeed, rb.velocity.y);

        rb.velocity = velocity;
        ballVelocityModificatior.x = velocity.x;
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, leftBoundary, rightBoundary);
        transform.position = newPosition;
        if(isBallLounched ) { return; }
        if(isDataLoaded )
        {
            ball.StickToPaddle(newPosition + differneceBetwenPadleAndBall);
        }        
    }

    private void OnDisable()
    {
        inputControler.ActionButtonPressed -= ShootBall;
        gameDataManager.OnGameDataLoaded -= Init;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
        if (collision.gameObject.GetComponent<Ball>())
        {
            if (!isBallLounched) { return; }
            ball.ChangeBallVelocity(ballVelocityModificatior);
        }
    }
}
