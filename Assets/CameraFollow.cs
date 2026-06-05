using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform alvo;
    [SerializeField] private float followSpeed = 5f;
    public void LateUpdate()
    {
        MoveCam();
    }
    public void MoveCam()
    {
        Vector3 newPos = new Vector3
        (
            alvo.position.x,
            transform.position.y,
            transform.position.z
        );
        transform.position = Vector3.Lerp
        (
            transform.position,
            newPos,
            followSpeed * Time.deltaTime
        );
    }
}
