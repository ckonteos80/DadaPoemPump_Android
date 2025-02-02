using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public List<GameObject> ParticlePrefabs;

    public GameObject CurrentParticle;
    // Start is called before the first frame update
    void Start()
    {
        // SetParticles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetParticles()
    {
        if (CurrentParticle != null)
        {
            Destroy(CurrentParticle);
            CurrentParticle = null;
        }
        CurrentParticle = Instantiate(ParticlePrefabs[Random.Range(0, ParticlePrefabs.Count)]);
        CurrentParticle.transform.SetParent(gameObject.transform);
    }

    public void SetParticlesColor()
    {

        if (CurrentParticle != null)
        {
            // for (int i = 0; i < ParticlePrefabs.Count; i++)
            // {

                ParticleSystem MyParticleSystem = CurrentParticle.GetComponent<ParticleSystem>();
                // float randomValue = Random.value;
                Color randomColor = new Color(Random.value, Random.value, Random.value,  Random.value);

                // Set the color
                var main = MyParticleSystem.main;
                main.startColor = randomColor;
            // }
            // Destroy(CurrentParticle);
            // CurrentParticle = null;
            // CurrentParticle = Instantiate(ParticlePrefabs[Random.Range(0, ParticlePrefabs.Count)]);
            // CurrentParticle.transform.SetParent(gameObject.transform);
        }
    }
}

