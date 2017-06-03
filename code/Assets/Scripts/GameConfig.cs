using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameConfig : MonoBehaviour
{
    private static GameConfig instance;

    //private static bool selected;
    //private GameObject currentObject;

	private List<GameObject> selectedObjects;
	public bool nightMode = false;

	void Awake() {
		selectedObjects = new List<GameObject>();

	}
		
	void Start() {

	}

	private void CargarEscena() {
		SceneManager.LoadScene("Madrid");
	}

	void Update() {
		if (Input.GetButton("Start") && Input.GetButton("Select")) {
			GameLogic.totalContamination = 0;
			GameLogic.totalRuido = 0;
			GameLogic.totalTrafico = 0;
			GameLogic.estadoTrafico = 0;
			GameLogic.modContaminacion = 0;
			GameLogic.modRuido = 0;
			GameLogic.modTrafico = 0;
			GameLogic.modTraficoSO = 0;
			GameLogic.modTraficoST = 0;
			GameLogic.modRuidoSO = 0;
			GameLogic.modRuidoST = 0;
			GameLogic.modTraficoST = 0;
			GameLogic.modTraficoSO = 0;
			GameLogic.modContaminacionST = 0;
			GameLogic.modContaminacionSO = 0;
			GameLogic.balanceState = 1;
			GameLogic.balanceSociety = 1;
			SceneManager.LoadScene("Madrid");
			/*if (SceneManager.GetActiveScene().name == "Madrid") {
				SceneManager.LoadScene("Madrid2");
			} else {
				SceneManager.LoadScene("Madrid");
			}*/
		}
	}

    public static GameConfig Instance
    {
        get { return instance ?? (instance = new GameObject("GameConfig").AddComponent<GameConfig>()); }
    }

	public void setSelected(GameObject gObject, bool state=true) {
		if (!selectedObjects.Contains(gObject) && state) {
			selectedObjects.Add(gObject);
		} else if (selectedObjects.Contains(gObject) && !state) {
			selectedObjects.Remove(gObject);
			gObject.GetComponent<PointerEvents>().reset();
		}
	}

	public void setSelected() {
		print("Clearing selection: " + selectedObjects.Count + " objects.");
		selectedObjects.Clear();
	}

	public bool isSelected(GameObject gObject) {
		return (selectedObjects.Contains(gObject));
	}

	public bool somethingSelected() {
		return (selectedObjects.Count>0);
	}

	/*
    public void _setSelected(GameObject gObject)
    {
        if (gObject==null)
        {
            currentObject.GetComponent<PointerEvents>().reset();
            selected = false;
            currentObject = null;            
            print("SET SELECTED NULL");
        } else
        {
            selected = true;
            currentObject = gObject;
        }
        
    }*/

    /*
    public bool isSelected()
    {
        return selected;
    }
    
    public bool getSelected(GameObject gObject)
    {
        if (currentObject == null) return false;
        return (gObject.name==currentObject.name);
    }*/

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
			if (e.keyCode == KeyCode.Escape) {
				
				setSelected();
			}
        }

    }
		

}