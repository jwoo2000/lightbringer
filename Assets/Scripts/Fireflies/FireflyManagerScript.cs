using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject FireflyPrefab;

    [SerializeField]
    private GameObject MidFireflyPrefab;

    [SerializeField]
    private GameObject HighFireflyPrefab;

    [SerializeField]
    private Transform fireflyAttractor;

    [SerializeField]
    private float trailDensity = 6.0f; // firefly object instances per 10 units

    private List<Vector3> POIAnchorPositions;

    [SerializeField]
    private POIManagerScript POIManager;

    private int numLowTierPOI;
    private int numMidTierPOI;
    private int numHighTierPOI;

    [SerializeField]
    private float connectingTrailRatio = 0.4f; // proportion of POIs that have trails to the next tier

    [SerializeField]
    private float POINoFireflyRadius;

    [SerializeField]
    private float playerSpawnNoFireflyRadius = 3f; // radius in which no fireflies are placed around the plaeyr when spawning (min: 2.3f)

    [SerializeField]
    private float pauseSystemOutsideRadius = 30f; // pause particle systems outside this radius from player (performance)

    [SerializeField]
    private List<ParticleSystem> fireflyParticleSystems = new List<ParticleSystem>();

    private void Awake()
    {
        POINoFireflyRadius = Mathf.Max(0, POIManager.discoverDist-7.0f);
    }

    public void startManager()
    {
        //Debug.Log("starting fireflymanager");
        numLowTierPOI = POIManager.numLowTierPOI;
        numMidTierPOI = POIManager.numMidTierPOI;
        numHighTierPOI = POIManager.numHighTierPOI;

        // find poi anchor locations and append their transform positions to list
        GameObject[] POIAnchors = GameObject.FindGameObjectsWithTag("POIAnchor");
        POIAnchorPositions = new List<Vector3>(POIAnchors.Length);
        foreach (GameObject currAnchor in POIAnchors)
        {
            POIAnchorPositions.Add(currAnchor.transform.position);
        }

        // instantiate trails between poi anchors
        GenerateTrails();
        GenerateConnectingTrails();
        GeneratePlayerToFirstLowTrail();
        //Debug.Log("finished generating all trails");
        InvokeRepeating("checkParticleSystems", 0, 1f); // Check every 1 second
    }

    private void checkParticleSystems()
    {
        for (int i = fireflyParticleSystems.Count - 1; i >= 0; i--)
        {
            ParticleSystem fireflySystem = fireflyParticleSystems[i];
            if (fireflySystem != null)
            {
                float distToPlayer = Vector3.Distance(fireflyAttractor.position, fireflySystem.transform.position);
                if ((distToPlayer > pauseSystemOutsideRadius) && fireflySystem.isPlaying)
                {
                    fireflySystem.Pause();
                } 
                else if ((distToPlayer <= pauseSystemOutsideRadius) && fireflySystem.isPaused)
                {
                    fireflySystem.Play();
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawWireSphere(fireflyAttractor.position, pauseSystemOutsideRadius);
    //}

    private void GenerateTrails()
    {
        // generate trails between low tier POIs ===========================
        for (int currAnchor = 0; currAnchor < numLowTierPOI; currAnchor++)
        {
            // get the start and end points for curr trail
            Vector3 startPoint = POIAnchorPositions[currAnchor];
            Vector3 endPoint = (currAnchor == numLowTierPOI - 1)
                       ? POIAnchorPositions[0]  // if last anchor, connect to first
                       : POIAnchorPositions[currAnchor + 1];  // else connect to next POI

            // generate two random control points along trail for bezier cubic curve
            Vector3 controlPoint1 = GetOffsetControlPoint(startPoint, endPoint, 0.2f, 0.4f, 30.0f);
            Vector3 controlPoint2 = GetOffsetControlPoint(startPoint, endPoint, 0.6f, 0.8f, 30.0f);

            // calculate the number of fireflies to spawn based on the distance and trail density
            float distance = Vector3.Distance(startPoint, endPoint);
            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            // instantiate fireflies along trail
            for (int i = 0; i <= numFireflies; i++)
            {
                float t = (float) i / numFireflies;

                // calculate position of curr firefly instance along the trail
                //Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)i / (float)numFireflies);  // linear trail
                Vector3 spawnPosition = GetPointOnCurve(t, startPoint, controlPoint1, controlPoint2, endPoint);

                if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius) && (Vector3.Distance(spawnPosition, endPoint) > POINoFireflyRadius)) {
                    // instantiate and set firefly attractor
                    GameObject firefly = Instantiate(FireflyPrefab, spawnPosition, Quaternion.identity);
                    firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                    ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                    if (fireflySystem != null)
                    {
                        fireflyParticleSystems.Add(fireflySystem);
                    }
                }
            }
        }

        // generate trails between mid tier POIs ===========================
        for (int currAnchor = numLowTierPOI; currAnchor < numLowTierPOI+numMidTierPOI; currAnchor++)
        {
            // get the start and end points for curr trail
            Vector3 startPoint = POIAnchorPositions[currAnchor];
            Vector3 endPoint = (currAnchor == numLowTierPOI + numMidTierPOI - 1)
                       ? POIAnchorPositions[numLowTierPOI]  // if last anchor, connect to first
                       : POIAnchorPositions[currAnchor + 1];  // else connect to next POI

            // generate two random control points along trail for bezier cubic curve
            Vector3 controlPoint1 = GetOffsetControlPoint(startPoint, endPoint, 0.2f, 0.4f, 30.0f);
            Vector3 controlPoint2 = GetOffsetControlPoint(startPoint, endPoint, 0.6f, 0.8f, 30.0f);


            // calculate the number of fireflies to spawn based on the distance and trail density
            float distance = Vector3.Distance(startPoint, endPoint);
            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            // instantiate fireflies along trail
            for (int i = 0; i <= numFireflies; i++)
            {
                float t = (float)i / numFireflies;

                // calculate position of curr firefly instance along the trail
                //Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)i / (float)numFireflies); // linear trail
                Vector3 spawnPosition = GetPointOnCurve(t, startPoint, controlPoint1, controlPoint2, endPoint);

                if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius) && (Vector3.Distance(spawnPosition, endPoint) > POINoFireflyRadius))
                {
                    // instantiate and set firefly attractor
                    GameObject firefly = Instantiate(MidFireflyPrefab, spawnPosition, Quaternion.identity);
                    firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                    ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                    if (fireflySystem != null)
                    {
                        fireflyParticleSystems.Add(fireflySystem);
                    }
                }
            }
        }

        // generate trails between high tier POIs ===========================
        for (int currAnchor = numLowTierPOI + numMidTierPOI; currAnchor < numLowTierPOI + numMidTierPOI + numHighTierPOI; currAnchor++)
        {
            // get the start and end points for curr trail
            Vector3 startPoint = POIAnchorPositions[currAnchor];
            Vector3 endPoint = (currAnchor == numLowTierPOI + numMidTierPOI + numHighTierPOI - 1)
                       ? POIAnchorPositions[numLowTierPOI + numMidTierPOI]  // if last anchor, connect to first
                       : POIAnchorPositions[currAnchor + 1];  // else connect to next POI

            // generate two random control points along trail for bezier cubic curve
            Vector3 controlPoint1 = GetOffsetControlPoint(startPoint, endPoint, 0.2f, 0.4f, 30.0f);
            Vector3 controlPoint2 = GetOffsetControlPoint(startPoint, endPoint, 0.6f, 0.8f, 30.0f);

            // calculate the number of fireflies to spawn based on the distance and trail density
            float distance = Vector3.Distance(startPoint, endPoint);
            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            // instantiate fireflies along trail
            for (int i = 0; i <= numFireflies; i++)
            {
                float t = (float)i / numFireflies;

                // calculate position of curr firefly instance along the trail
                //Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)i / (float)numFireflies); // linear trail
                Vector3 spawnPosition = GetPointOnCurve(t, startPoint, controlPoint1, controlPoint2, endPoint);

                if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius) && (Vector3.Distance(spawnPosition, endPoint) > POINoFireflyRadius))
                {
                    // instantiate and set firefly attractor
                    GameObject firefly = Instantiate(HighFireflyPrefab, spawnPosition, Quaternion.identity);
                    firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                    ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                    if (fireflySystem != null)
                    {
                        fireflyParticleSystems.Add(fireflySystem);
                    }
                }

            }
        }
    }

    // returns a point somewhere between start and end [minT, maxT], offset by given value
    private Vector3 GetOffsetControlPoint(Vector3 startPoint, Vector3 endPoint, float minT, float maxT, float offset)
    {
        float t = Random.Range(minT, maxT);
        Vector3 dir = endPoint - startPoint; // vector from start to end
        Vector3 pointOnLine = startPoint + dir * t; // point somewhere between start and end based on given minT and maxT
        Vector3 perp = Vector3.Cross(dir, Vector3.up).normalized; // vector facing side to side of trail, cross of trail dir and up
        float pointOffset = Random.Range(-offset, offset);

        return pointOnLine + (perp * pointOffset);
    }

    // returns a point on cubic bezier curve
    private Vector3 GetPointOnCurve(float t, Vector3 startPoint, Vector3 cp1, Vector3 cp2, Vector3 endPoint)
    {
        // cubic bezier formula: (1 - t)^3 * start    +    3(1 - t)^2 * t * cp1    +    3(1 - t) * t^2 * cp2    +    t^3 * end
        float oneMinT = 1 - t;
        float Tsq = t * t;
        float oneMinTsq = oneMinT * oneMinT;
        float oneMinTcb = oneMinTsq * oneMinT;
        float Tcb = Tsq * t;

        Vector3 point = oneMinTcb * startPoint;
        point += 3.0f * oneMinTsq * t * cp1;
        point += 3.0f * oneMinT * Tsq * cp2;
        point += Tcb * endPoint;

        return point;
    }

    private void GenerateConnectingTrails()
    {
        // generate connecting trails between low tier and mid tier ===========================
        int numConnectingTrails = Mathf.FloorToInt(numLowTierPOI * connectingTrailRatio);

        // store all possible trail connections with distances
        List<(Vector3 lowTierPOI, Vector3 midTierPOI, float distance)> trailCandidatesLM = new List<(Vector3, Vector3, float)>();

        // calculate dist between all low tier and mid tier POIs
        for (int lowIndex = 0; lowIndex < numLowTierPOI; lowIndex++)
        {
            for (int midIndex = numLowTierPOI; midIndex < numLowTierPOI + numMidTierPOI; midIndex++)
            {
                Vector3 lowTierPOI = POIAnchorPositions[lowIndex];
                Vector3 midTierPOI = POIAnchorPositions[midIndex];
                float distance = Vector3.Distance(lowTierPOI, midTierPOI);

                trailCandidatesLM.Add((lowTierPOI, midTierPOI, distance));
            }
        }

        // sort by trail length
        trailCandidatesLM.Sort((a, b) => a.distance.CompareTo(b.distance));

        // generate trails for the shortest distances up to numConnectingTrails
        for (int i = 0; i < numConnectingTrails; i++)
        {
            Vector3 startPoint = trailCandidatesLM[i].lowTierPOI;
            Vector3 endPoint = trailCandidatesLM[i].midTierPOI;
            float distance = trailCandidatesLM[i].distance;

            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            for (int j = 0; j <= numFireflies; j++)
            {
                Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)j / (float)numFireflies);

                if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius) && (Vector3.Distance(spawnPosition, endPoint) > POINoFireflyRadius))
                {
                    GameObject firefly = Instantiate(MidFireflyPrefab, spawnPosition, Quaternion.identity);
                    firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                    ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                    if (fireflySystem != null)
                    {
                        fireflyParticleSystems.Add(fireflySystem);
                    }
                }
            }
        }

        // generate connecting trails between mid tier and high tier ===========================
        numConnectingTrails = Mathf.FloorToInt(numMidTierPOI * connectingTrailRatio);

        // store all possible trail connections with distances
        List<(Vector3 midTierPOI, Vector3 highTierPOI, float distance)> trailCandidatesMH = new List<(Vector3, Vector3, float)>();

        // calculate dist between all low tier and mid tier POIs
        for (int midIndex = numLowTierPOI; midIndex < numLowTierPOI + numMidTierPOI; midIndex++)
        {
            for (int highIndex = numLowTierPOI + numMidTierPOI; highIndex < numLowTierPOI + numMidTierPOI + numHighTierPOI; highIndex++)
            {
                Vector3 midTierPOI = POIAnchorPositions[midIndex];
                Vector3 highTierPOI = POIAnchorPositions[highIndex];
                float distance = Vector3.Distance(midTierPOI, highTierPOI);

                trailCandidatesMH.Add((midTierPOI, highTierPOI, distance));
            }
        }

        // sort by trail length
        trailCandidatesMH.Sort((a, b) => a.distance.CompareTo(b.distance));

        // generate trails for the shortest distances up to numConnectingTrails
        for (int i = 0; i < numConnectingTrails; i++)
        {
            Vector3 startPoint = trailCandidatesMH[i].midTierPOI;
            Vector3 endPoint = trailCandidatesMH[i].highTierPOI;
            float distance = trailCandidatesMH[i].distance;

            int numFireflies = Mathf.FloorToInt(distance * trailDensity / 10.0f);

            for (int j = 0; j <= numFireflies; j++)
            {
                Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)j / (float)numFireflies);

                if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius) && (Vector3.Distance(spawnPosition, endPoint) > POINoFireflyRadius))
                {
                    GameObject firefly = Instantiate(HighFireflyPrefab, spawnPosition, Quaternion.identity);
                    firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                    ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                    if (fireflySystem != null)
                    {
                        fireflyParticleSystems.Add(fireflySystem);
                    }
                }

            }
        }
    }

    private void GeneratePlayerToFirstLowTrail()
    {
        //Debug.Log("generating player to first poi trail");
        // store all possible trail connections with distances
        List<(Vector3 lowTierPOI, float distance)> distToLow = new List<(Vector3,  float)>();

        // calculate dist between all low tier and mid tier POIs
        for (int lowIndex = 0; lowIndex < numLowTierPOI; lowIndex++)
        {
            Vector3 lowTierPOI = POIAnchorPositions[lowIndex];
            float distance = Vector3.Distance(lowTierPOI, fireflyAttractor.position);

            distToLow.Add((lowTierPOI, distance));
        }

        // sort by trail length
        distToLow.Sort((a, b) => a.distance.CompareTo(b.distance));

        // generate trails for the shortest distances up to numConnectingTrails
        Vector3 startPoint = distToLow[0].lowTierPOI;
        Vector3 dir = (fireflyAttractor.position - startPoint).normalized;
        Vector3 endPoint = fireflyAttractor.position - (dir*playerSpawnNoFireflyRadius);

        float trailDistance = distToLow[0].distance;

        int numFireflies = Mathf.FloorToInt(trailDistance * trailDensity / 10.0f);

        for (int i = 0; i <= numFireflies; i++)
        {
            Vector3 spawnPosition = Vector3.Lerp(startPoint, endPoint, (float)i / (float)numFireflies);
            if ((Vector3.Distance(spawnPosition, startPoint) > POINoFireflyRadius))
            {
                GameObject firefly = Instantiate(FireflyPrefab, spawnPosition, Quaternion.identity);
                firefly.GetComponent<AbsorbFireflies>().attractor = fireflyAttractor;

                ParticleSystem fireflySystem = firefly.GetComponent<ParticleSystem>();
                if (fireflySystem != null)
                {
                    fireflyParticleSystems.Add(fireflySystem);
                }
            }
        }
        
    }
}
