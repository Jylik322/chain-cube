using UnityEngine;
[RequireComponent(typeof(BoxNumber))]
[RequireComponent(typeof(BoxMove))]
[RequireComponent(typeof(TrailRenderer))]
public class BoxColliderDetector : MonoBehaviour
{
    private BoxMove boxMove;
    private TrailRenderer trailRenderer;
    private BoxNumber boxNumber;
    private ParticleSystem particle;
    private bool destroyable = false;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        boxMove = GetComponent<BoxMove>();
        boxNumber = GetComponent<BoxNumber>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    private void SendScore()
    {
        Score.ScoreChanged.Invoke(boxNumber.Number);
    }
    private void UpdateBox()
    {
        boxMove.Push(Vector3.up, 4f);
        boxNumber.Number = boxNumber.Number * 2;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxNumber>() != null)
        {
            trailRenderer.enabled = false;
            BoxNumber collisionBoxNumber = collision.gameObject.GetComponent<BoxNumber>();
            if (boxNumber.Number == collisionBoxNumber.Number && collisionBoxNumber.instantiateNumber > boxNumber.instantiateNumber)
            {
                Destroy(collision.gameObject);
                particle.Play();
                UpdateBox();
                SendScore();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("REDLINE")){
            if (!destroyable)
            {
                destroyable = true;
                SendScore();
            }
            else
            {
                Score.OnGameOver.Invoke();
            }
        }
    }
}