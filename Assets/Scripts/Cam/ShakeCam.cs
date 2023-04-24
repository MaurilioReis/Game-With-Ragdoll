using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public Transform cameraTransform; // Referência ao Transform da câmera
    public float shakeDuration = 0.1f; // Duração da animação de shake
    public float shakeMagnitude = 0.1f; // Magnitude do shake
    public float shakeSpeed = 0.1f; // Velocidade do shake

    private Vector3 originalPosition; // Posição original da câmera
    private float shakeTimer = 0f; // Tempo decorrido do shake

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Se a referência à câmera não for atribuída, utiliza a câmera principal
        }

        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Calcula a nova posição da câmera com base no shake
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            Vector3 newPosition = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0f);

            // Move a câmera suavemente em direção à nova posição
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPosition, shakeSpeed * Time.deltaTime);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reinicia a posição original da câmera quando o shake terminar
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
}