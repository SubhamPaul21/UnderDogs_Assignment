using UnityEngine;

[DisallowMultipleComponent]
public class WallOscillator : MonoBehaviour
{
    // state variables
    [SerializeField] Vector3 movementVector = new Vector3(0f, 2f, 0f);
   
    //member variables
    Vector3 startingPos;
    float period;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        period = Random.Range(2f, 4f);
    }

    // Update is called once per frame
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
        Vector3 offsetPos = movementVector * rawSinWave;
        transform.position = startingPos + offsetPos;
    }
}
