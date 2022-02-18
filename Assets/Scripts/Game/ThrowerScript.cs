using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class ThrowerScript : MonoBehaviour
{
    [Header("Moving speed of Box")]
    [SerializeField] private float speed;

    [Header("Box Prefabs")]
    [SerializeField ]private List<GameObject> boxesPrefabs;
    
    LineRenderer lineRenderer;
    
    private BoxMove boxMove;
    private bool holdedCube;
    private Vector3 spawnPosition = new Vector3(0f,1f,-5f);
    private float minX = -2f, maxX = 2f;
    private int instantiateCounter = 0;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpawnNewBox();
    }
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            UpdateLine();
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (CheckPosition() && boxMove != null)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    boxMove.Push(new Vector3(touchDeltaPosition.normalized.x, 0f, 0f), 2f);
                    holdedCube = true;
                }
            }
        }
        else
        {
            if (holdedCube)
            {
                boxMove.Push(boxMove.transform.transform.forward, 15f);
                boxMove.GetComponent<Rigidbody>().freezeRotation = false;
                boxMove = null;
                holdedCube = false;
                lineRenderer.enabled = false;
                StartCoroutine(WaitAndSpawn());
            }
        }
    }   
    private void UpdateLine()
    {
       lineRenderer.enabled = true;
       lineRenderer.SetPosition(0, boxMove.transform.position+(Vector3.forward*10f));
       lineRenderer.SetPosition(1, boxMove.transform.position);
    }
    private bool CheckPosition()
    {
        return (transform.position.x < maxX && transform.position.x > minX) ? true : false;
    }
    private void SpawnNewBox()
    {
        boxMove = Instantiate(boxesPrefabs[Random.Range(0, boxesPrefabs.Count)], spawnPosition, Quaternion.identity).GetComponent<BoxMove>();
        boxMove.GetComponent<BoxNumber>().instantiateNumber = instantiateCounter++;
    }
    public IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(2f);
        SpawnNewBox();
    }
}
