using UnityEngine;

public class RouletteController : MonoBehaviour
{
    [Header("Spin Settings")]
    [SerializeField] float minStartSpeed = 20f;
    [SerializeField] float maxStartSpeed = 30f;

    [Header("Spin Duration & Decay")]
    [SerializeField] float spinDuration = 2.0f; // 保持高速的時間
    [SerializeField, Range(0.90f, 0.999f)] float decay = 0.97f; // 衰退係數

    float rotSpeed = 0f;
    float timer = 0f;
    bool spinning = false;
    bool decaying = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !spinning)
        {
            StartSpin();
        }

        if (spinning)
        {
            timer += Time.deltaTime;

            // 持續高速轉
            transform.Rotate(0, 0, rotSpeed);

            // 到時間開始衰減
            if (!decaying && timer >= spinDuration)
            {
                decaying = true;
            }

            // 指數衰退
            if (decaying)
            {
                rotSpeed *= decay;

                if (rotSpeed < 0.01f)
                {
                    rotSpeed = 0;
                    spinning = false;
                    decaying = false;
                }
            }
        }
    }

    void StartSpin()
    {
        rotSpeed = Random.Range(minStartSpeed, maxStartSpeed);
        timer = 0f;
        spinning = true;
        decaying = false;
    }
}
