using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BirdList : MonoBehaviour
{
    [SerializeField] private Bird[] birds;
    private Bird[] newBirds = new Bird[5];

    [SerializeField] GameObject[] imags;
    [SerializeField] GameObject[] place;
    private string[] placed;

    [SerializeField] GameObject[] descs;

    private float[] positions;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private Vector3 lastPos;
    private bool carrying;
    private RaycastResult current;
    Camera cam;

    void Start()
    {
        carrying = false;
        cam = Camera.main;
        lastPos = Input.mousePosition;
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        positions = new float[5] {50f, 25f, 0f, -25f, -50f};

        for (int i = 0; i < newBirds.Length; i++)
        {
            while (true)
            {
                Bird newBird = birds[Random.Range(0, newBirds.Length)];
                

                foreach (Bird bird in newBirds)
                {
                    if ( bird == newBird )
                    {
                        break;
                    }
                }
            }
        }

        foreach(GameObject go in descs)
        {
            while (true)
            {
                int random = Random.Range(0, 5);

                if (positions[random] != 999f)
                {
                    go.transform.position += new Vector3(positions[random], 0f, 0f);
                    positions[random] = 999f;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if ( Input.GetKey(KeyCode.Mouse0) )
        {
            if (!carrying)
            {
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                m_Raycaster.Raycast(m_PointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    if ( result.gameObject.CompareTag("Descriptions") )
                    {
                        current = result;
                        carrying = true;
                        break;
                    }
                }
            }
            if ( carrying )
            {
                for (int i = 0; i < placed.Length; i++)
                {
                    if ( placed[i] == current.gameObject.GetComponent<Text>().text )
                    {
                        placed[i] = null;
                    }
                }
                Vector3 moved = Input.mousePosition - lastPos;
                current.gameObject.transform.position += moved;
            }
            
        }
        else
        {
            updatePlaceStates();
        }

        if ( Input.GetKeyUp(KeyCode.Mouse0) )
        {
            carrying = false;
        }
        
        lastPos = Input.mousePosition;
    }

    private void updatePlaceStates()
    {
        foreach (GameObject descrip in descs)
        {
            for (int i = 0; i < place.Length; i++)
            {
                Vector3 pos = descrip.transform.position;
                
                Vector2 Max = place[i].transform.position;
                Max += place[i].GetComponent<RectTransform>().sizeDelta/2;
                Vector2 Min = place[i].transform.position;
                Min -= place[i].GetComponent<RectTransform>().sizeDelta/2;
                
                if ( pos.x < Max.x && pos.x > Min.x && pos.y > Min.y && pos.y < Max.y)
                {
                    descrip.transform.position = place[i].transform.position;

                    placed[i] = descrip.GetComponent<Text>().text;

                    break;
                }
            }
        }
    }

    private Bird[] NewBirds()
    {
        Bird[] newBirds = new Bird[5];

        for (int i = 0; i < 5; i++)
        {
            newBirds[i] = birds[Random.Range(0, birds.Length)];
        }
        
        return newBirds;
    }
}
