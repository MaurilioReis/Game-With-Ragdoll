using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public Transform cameraTransform; // Refer�ncia ao Transform da c�mera
    public float shakeDuration = 0.1f; // Dura��o da anima��o de shake
    public float shakeMagnitude = 0.1f; // Magnitude do shake
    public float shakeSpeed = 0.1f; // Velocidade do shake

    private Vector3 originalPosition; // Posi��o original da c�mera
    private float shakeTimer = 0f; // Tempo decorrido do shake

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Se a refer�ncia � c�mera n�o for atribu�da, utiliza a c�mera principal
        }

        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Calcula a nova posi��o da c�mera com base no shake
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            Vector3 newPosition = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0f);

            // Move a c�mera suavemente em dire��o � nova posi��o
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPosition, shakeSpeed * Time.deltaTime);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reinicia a posi��o original da c�mera quando o shake terminar
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
}