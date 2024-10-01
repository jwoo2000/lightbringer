using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject FireflyPrefab;

    [SerializeField]
    private Transform fireflyAttractor;

    [SerializeField]
    private float trailDensity = 10.0f; // firefly object instances per 10 units

    private List<Vector3> POIAnchorPositions;

    // Start is called before the first frame update
    void Start()
    {
        // find poi anchor locations and append their transform positions to list
        GameObject[] POIAnchors = GameObject.FindGameObjectsWithTag("POIAnchor");
        POIAnchorPositions = new List<Vector3>(POIAnchors.Length);
        foreach (GameObject currAnchor in POIAnchors) {
            POIAnchorPositions.Add(currAnchor.transform.position);
        }

        // instantiate trails between poi anchors
        GenerateTrails();
    }

    private void GenerateTrails()
    {
        for (int currAnchor = 0; currAnchor < POIAnchorPositions.Count-1; currAnchor++)
        {
            // get the start and end points for curr trail
            Vector3 startPoint = POIAnchorPositions[currAnchor];
            Vector3 endPoint = POIAnchorPositions[currAnchor + 1];

            // calculate direction and distance between anchors
            Vector3 direction = (endPoint - startPoint).normalized;
            float distance = Vector3.Distance(startPoint, endPoint);

            // calculate the number of fireflies to spawn based on the distance and trail density
            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            // instantiate fireflies along trail
            for (int i = 0; i <= numFireflies; i++)
            {
                // calculate position of curr firefly instance along the trail
                Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)i / (float)numFireflies);

                // instantiate and set firefly attractor
                GameObject firefly = Instantiate(FireflyPrefab, spawnPosition, Quaternion.identity);
                firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;
            }
        }
    }
}
