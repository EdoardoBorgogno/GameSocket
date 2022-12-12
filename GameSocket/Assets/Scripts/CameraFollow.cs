using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValue;

    private void FixedUpdate() // Con update ho dei bug.
    {
        Follow();
    }

    private void Awake()
    {
        target = GameObject.Find(PlayerPrefs.GetString("color")).GetComponent<Transform>();
    }

    void Follow()
    {
        // Define minimum x,y,z and Maximum x,y,z values.
        Vector3 targetPosition = target.position + offset;
        // Verifico se la targetPosition è fuori dai bordi
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x,maxValue.x),
            Mathf.Clamp(targetPosition.y + 1, minValues.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValue.z));
        

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
