using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    [HideInInspector] public List<List<GameObject>> particleCollection;
    public GameObject particlePrefab;

    public int particleDepth = 3;
    public bool isRefreshing = false;

    int upZIndex;
    int bottomZIndex = 0;

    int rightIndex;
    int leftIndex = 0;

    float particleBoxDist;
    float maxZPosition = 0;
    float minimumZPosition = 0;

    float maxRightPosition = 0;
    float maxLeftPosition = 0;

    GameManager gameManager;
    CharacterController characterInstance;

    void Start()
    {
        gameManager = GameManager.instance;
        characterInstance = gameManager.characterInstance;

        particleCollection = new List<List<GameObject>>();

        particleBoxDist = particlePrefab.GetComponent<ParticleSystem>().shape.scale.x;
        float tileDistance = particlePrefab.GetComponent<ParticleSystem>().shape.scale.x * particleDepth / 2;

        float xPosition = gameManager.characterInstance.transform.position.x - (tileDistance - particleBoxDist / 2);

        for (int i = 0; i < particleDepth; i++)
        {
            List<GameObject> snowColumn = new List<GameObject>();    
            float zPosition = gameManager.characterInstance.transform.position.z - (tileDistance - particleBoxDist/2);
            for (int a = 0; a < particleDepth; a++)
            {
                Vector3 instancePosition = new Vector3(xPosition, particlePrefab.transform.position.y, zPosition);
                GameObject particles = Instantiate(particlePrefab, instancePosition, Quaternion.identity);
                ParticleTile particle = particles.GetComponent<ParticleTile>();
                particle.generator = this;
                zPosition += particleBoxDist;
                snowColumn.Add(particles);
            }
            xPosition += particleBoxDist;
            particleCollection.Add(snowColumn);
        }

        bottomZIndex = 0;
        upZIndex = particleCollection.Count - 1;

        rightIndex = particleCollection.Count - 1;
        leftIndex = 0;

        List<GameObject> firstcolumn = particleCollection[0];
        List<GameObject> lastcolumn = particleCollection[rightIndex];

        maxZPosition = firstcolumn[upZIndex].transform.position.z;
        minimumZPosition = firstcolumn[bottomZIndex].transform.position.z;

        maxRightPosition = lastcolumn[0].transform.position.x;
        maxLeftPosition = firstcolumn[0].transform.position.x;
    }

    private void FixedUpdate()
    {
        ReOrganiseTiles();
    }

    public void ReOrganiseTiles()
    {
        CheckForwardTiles();
        CheckHorizontalTiles();
    }

    void CheckForwardTiles()
    {
        if (characterInstance.transform.position.z > maxZPosition)
        {
            foreach (List<GameObject> snowList in particleCollection)
            {
                ShiftForwardPositive(snowList);
            }
            upZIndex = bottomZIndex;
            bottomZIndex++;
            if (bottomZIndex == particleDepth) bottomZIndex = 0;

            List<GameObject> column = particleCollection[0];
            maxZPosition = column[upZIndex].transform.position.z;
            minimumZPosition = column[bottomZIndex].transform.position.z;
        }

        if (characterInstance.transform.position.z < minimumZPosition)
        {
            foreach (List<GameObject> snowList in particleCollection)
            {
                ShiftForwardNegative(snowList);
                
            }
            bottomZIndex = upZIndex;
            upZIndex--;
            if (upZIndex < 0) upZIndex = particleDepth - 1;

            List<GameObject> column = particleCollection[0];
            minimumZPosition = column[bottomZIndex].transform.position.z;
            maxZPosition = column[upZIndex].transform.position.z;
        }
    }

    void CheckHorizontalTiles()
    {
        if(characterInstance.transform.position.x > maxRightPosition)
        {
            ShiftRightPositive(particleCollection[leftIndex], particleCollection[rightIndex].First().transform.position.x);

            rightIndex = leftIndex;
            leftIndex++;
            if (leftIndex == particleDepth) leftIndex = 0;

            maxRightPosition = particleCollection[rightIndex].First().transform.position.x;
            maxLeftPosition = particleCollection[leftIndex].First().transform.position.x;
        }

        if(characterInstance.transform.position.x < maxLeftPosition)
        {
            ShiftLeftNegative(particleCollection[rightIndex], particleCollection[leftIndex].First().transform.position.x);

            leftIndex = rightIndex;
            rightIndex--;
            if (rightIndex < 0) rightIndex = particleDepth - 1;

            maxRightPosition = particleCollection[rightIndex].First().transform.position.x;
            maxLeftPosition = particleCollection[leftIndex].First().transform.position.x;
        }
    }

    private void ShiftForwardNegative(List<GameObject> particleColumn)
    {
        Vector3 position = new Vector3(particleColumn[bottomZIndex].transform.position.x, particlePrefab.transform.position.y, particleColumn[bottomZIndex].transform.position.z - particleBoxDist);
        particleColumn[upZIndex].transform.position = position;
        if (isRefreshing) particleColumn[upZIndex].GetComponent<ParticleSystem>().Clear();
    }

    private void ShiftForwardPositive(List<GameObject> particleColumn)
    {
        Vector3 position = new Vector3(1 * particleColumn[upZIndex].transform.position.x, particlePrefab.transform.position.y, particleColumn[upZIndex].transform.position.z + particleBoxDist);
        particleColumn[bottomZIndex].transform.position = position;
        if (isRefreshing) particleColumn[bottomZIndex].GetComponent<ParticleSystem>().Clear();
    }

    private void ShiftLeftNegative(List<GameObject> particleRow, float shiftVal)
    {
        foreach (GameObject item in particleRow)
        {
            Vector3 position = new Vector3(1 * shiftVal - particleBoxDist, particlePrefab.transform.position.y, item.transform.position.z);
            item.transform.position = position;
            if (isRefreshing) item.GetComponent<ParticleSystem>().Clear();
        }
    }

    private void ShiftRightPositive(List<GameObject> particleRow, float shiftVal)
    {
        
        foreach(GameObject item in particleRow)
        {
            Vector3 position = new Vector3(1 * shiftVal + particleBoxDist, particlePrefab.transform.position.y, item.transform.position.z);
            item.transform.position = position;
            if(isRefreshing) item.GetComponent<ParticleSystem>().Clear();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(new Vector3(snowCollection[rightIndex].First().transform.position.x, 5, 0), 0.5f);

        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(new Vector3(snowCollection[leftIndex].First().transform.position.x, 5, 0), 0.5f);
    }
}
