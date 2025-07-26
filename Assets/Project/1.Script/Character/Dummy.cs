using UnityEngine;

public class Dummy : MonoBehaviour
{
    private bool pulling;
    private float pullSpeed;
    private Vector3 dest;

    public void PullDummy(Vector3 castedPosition, Vector3 castDir, float pullSpeed, float pullRatioDist)
    {
        pulling = true;
        this.pullSpeed = pullSpeed;
        
        dest = Vector3.Lerp(transform.position, castedPosition + castDir * 1.5f, pullRatioDist);
    }

    private void FixedUpdate()
    {
        if (pulling)
        {
            if (dest != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, dest, pullSpeed * Time.deltaTime);
            }
            else
            {
                pulling = false;
            }
        }
    }
}