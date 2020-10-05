using UnityEngine;

[DisallowMultipleComponent]
public class WallOscillator : MonoBehaviour
{
    //member variables
    Vector3 horizontalVector = new Vector3(10f, 0f, 0f);
    Vector3 verticalVector = new Vector3(0f, 8f, 0f);
    Vector3 startingPos;
    bool isVertical;
    float period;

    void Start()
    {
        startingPos = transform.position;
        period = Random.Range(3.7f, 5f);
        SetOscillation();
    }

    private void SetOscillation()
    {
        if (transform.rotation.y == 1)
        {
            isVertical = true;
        }
        else
        {
            isVertical = false;
        }
    }


    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f; // Approx to 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // Sin restricts between -1 to +1
        //float movementFactor = rawSinWave / 2f + 0.5f; // Divide by 2 to clamp b/w -0.5 to +0.5, then add 0.5 to clamp b/w 0 to 1
        Vector3 offsetPos;
        if (isVertical)
        {
            offsetPos = verticalVector * rawSinWave;
        }
        else
        {
            offsetPos = horizontalVector * rawSinWave;
        }

        transform.position = startingPos + offsetPos;
    }
}
