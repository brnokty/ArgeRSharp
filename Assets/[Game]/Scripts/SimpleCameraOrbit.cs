using UnityEngine;

public class SimpleCameraOrbit : MonoBehaviour
{
    public Transform target;        
    public float distance = 5.0f;    
    public float xSpeed = 120.0f;    
    public float ySpeed = 120.0f;    

    public float yMinLimit = -20f;   
    public float yMaxLimit = 80f;    

    public float distanceMin = 2f;   
    public float distanceMax = 10f;  

    public float zoomSpeed = 2f;     

    private float x = 0.0f;
    private float y = 0.0f;

    private Quaternion rotation;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rotation = Quaternion.Euler(y, x, 0);
    }

    void LateUpdate()
    {
        if (target)
        {
            // Sağ tık basılıysa kamerayı döndür
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Cursor.lockState = CursorLockMode.Locked; 
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            // Zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, distanceMin, distanceMax);

            rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}