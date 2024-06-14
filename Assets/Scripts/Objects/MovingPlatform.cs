using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField]
    Transform[] wayPoints;
    [SerializeField]
    float speed=5f;
    #endregion
    #region Private variables for moving
    int _currentTargetPosition = 0;
    int _nextTargetPosition = 1;
    float _minTargetDistance = 0.01f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = this.wayPoints[0].transform.position;
        StartCoroutine(MovePlatform());
    }
    IEnumerator MovePlatform()
    {
        float alpha = 0.0f;
        while(true)
        {
            alpha += Time.fixedDeltaTime * speed;
			
            if((1.0f - alpha) < _minTargetDistance)
            {
                _nextTargetPosition++;
                _currentTargetPosition++;
                _nextTargetPosition %= wayPoints.Length;
                _currentTargetPosition %= wayPoints.Length;
                alpha = 0.0f;
            }
            this.transform.position = Vector3.Lerp(this.wayPoints[_currentTargetPosition].transform.position, this.wayPoints[_nextTargetPosition].transform.position, alpha);
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.GetComponent<PlayerController>())
        {
            other.collider.GetComponent<PlayerController>().AttachToParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider.GetComponent<PlayerController>())
        {
            other.collider.GetComponent<PlayerController>().AttachToParent(null);
        }
    }
}