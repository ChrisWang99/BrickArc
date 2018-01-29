using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour {

    private float ballVelocityNormal;
    public float ballVelocityReturnStep = 0.1f;

    protected virtual void Start()
    {
        ballVelocityNormal = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 ballVelocity = ballRigidbody2D.velocity;
            if (Mathf.Abs(ballVelocity.magnitude - ballVelocityNormal) <= ballVelocityReturnStep)
                ballRigidbody2D.velocity = ballVelocity.normalized * ballVelocityNormal;
            else if (ballVelocity.magnitude > ballVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude - ballVelocityReturnStep);
            else if(ballVelocity.magnitude < ballVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude + ballVelocityReturnStep);
            BrickEffect();
        }
    }

    protected virtual void BrickEffect()
    {
        Destroy(gameObject);
    }
}
