using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	
	#region IPointerExitHandler implementation
	public void OnPointerExit (PointerEventData eventData)
	{
		//renderer.material.color = originalColor;
		//renderer.material = originalMaterial;
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		/*
		if (!GameConfig.Instance.isSelected(gameObject))
		{
			renderer.material = hoverMaterial;
		}*/
		//renderer.material.color = Color.yellow;     
	}

	#endregion

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{	
		/*
		if (Input.GetButtonDown("Accion")) {
			GameConfig.Instance.setSelected(gameObject, GameConfig.Instance.isSelected(gameObject));
		}*/
		//if (Input.GetMouseButton(1)) GameConfig.Instance.setSelected(null);
	}
	#endregion


    public static bool selected;
    private Renderer renderer;
    private Color originalColor;
    
	public Material hoverMaterial;
    public Material originalMaterial;

    private GameObject currentObject;

	void Awake() {
		if (!GetComponent<Renderer>()) {
			Destroy(GetComponent<PointerEvents>());
			return;
		}
	}

    // Use this for initialization
    void Start()
    {        
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        originalMaterial = renderer.material;
        currentObject = gameObject;
	
    }

    // Update is called once per frame
    void Update()
    {
		if (GameConfig.Instance.isSelected(gameObject))
        {            
            renderer.material = hoverMaterial;                                  
        } else
        {
            
        }
        //print(gameObject.name + " " + GameConfig.Instance.getSelected(gameObject));
    }

    public void reset()
    {
		print("Resetting " + gameObject.name);
        renderer.material = originalMaterial;
    }
}
