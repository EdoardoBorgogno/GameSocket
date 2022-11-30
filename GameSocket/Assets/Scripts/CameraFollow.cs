using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValue;

    private void FixedUpdate() // Con update ho dei bug.
    {
        Follow();
    }

    void Follow()
    {
        // Define minimum x,y,z and Maximum x,y,z values.
        Vector3 targetPosition = target.position + offset;
        // Verifico se la targetPosition è fuori dai bordi
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x,maxValue.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));
        

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
