using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour {

    public float BallVelocityNormal = 3.0f;
    public float BallVelocityReturnStep = 0.1f;

    protected virtual void Start() {
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 ballVelocity = ballRigidbody2D.velocity;
            //Debug.Log("ballVelocity1: "+ ballRigidbody2D.velocity.ToString()+" "+ ballRigidbody2D.velocity.magnitude.ToString());
            if (Mathf.Abs(ballVelocity.magnitude - BallVelocityNormal) <= BallVelocityReturnStep)
                ballRigidbody2D.velocity = ballVelocity.normalized * BallVelocityNormal;
            else if (ballVelocity.magnitude > BallVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude - BallVelocityReturnStep);
            else if(ballVelocity.magnitude < BallVelocityNormal)
                ballRigidbody2D.velocity = ballVelocity.normalized * (ballVelocity.magnitude + BallVelocityReturnStep);
            //Debug.Log("ballVelocity2: " + ballRigidbody2D.velocity.ToString() + " " + ballRigidbody2D.velocity.magnitude.ToString());
            BrickEffect(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        EmitBreakParticle();
        Destroy(transform.parent.gameObject);
    }

    protected virtual void BrickEffect(GameObject ball)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().BrickNumberReduce();
        Destroy(gameObject);
    }

    protected virtual void EmitBreakParticle() {
        GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>().Emit(0, transform);
    }
}
