using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Item : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		GameObject.Find("ToolTipAndModal").GetComponent<TooltipPanel>().Hide();
	}

	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		if(gameObject==null) return;
		if (!available) return;
		float i = gameObject.transform.position.x;
		float j = gameObject.transform.position.y + gameObject.GetComponent<RectTransform>().rect.height;
		float z = Input.mousePosition.z;
		//ShowTooltip.Instance.Show(title, pollution.ToString(), noise.ToString(), traffic.ToString(), cost.ToString("+0;-#"), income.ToString("+0;-#"), new Vector3(i,j,z));	
		GameObject.Find("ToolTipAndModal").GetComponent<TooltipPanel>().Show(title + "<color=yellow>(" + turnosActivos.ToString() + ")</color>", pollution.ToString(), noise.ToString(), traffic.ToString(), cost.ToString("+0;-#"), income.ToString("+0;-#"), new Vector3(i,j,z));
	}
	#endregion

	
	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		if (!available || bought) return;	

		if ((int)owner != GameLogic.rolTurnoActual) {
			GameObject.Find("ToolTipAndModal").GetComponent<ModalPanel>().Choice(
				"<color=red>Sólo medidas de " + GameLogic.RolesTurnos[GameLogic.rolTurnoActual] + " se pueden aplicar en este turno.</color>"
			);
		}
		else if (!itemNecesarioComprado) {
			GameObject.Find("ToolTipAndModal").GetComponent<ModalPanel>().Choice(
				"Para poder implementar esta acción primero se necesita <b><color=yellow>" + itemNecesario.GetComponent<Item>().title + "</color></b>."
			);
		} else {
			setBought();
			GameObject.Find("ToolTipAndModal").GetComponent<ModalPanel>().Choice(
				"Has implementado <b><color=yellow>" + title + "</color></b> por <b><color=#00ffaa>"+ Mathf.Abs(cost) +"</color> sustaintokens</b>."
			);
			/*
			GameObject.Find("ToolTipAndModal").GetComponent<ModalPanel>().Choice(
				"¿Quieres comprar <b><color=yellow>" + title + "</color></b> por <b><color=#00ffaa>"+ Mathf.Abs(cost) +"</color> sustaintokens</b>?",
				new UnityAction(sayYes),
				new UnityAction(sayNo)
			);*/

		}

	}
	#endregion


	void sayYes() {
		setBought();

	}

	void sayNo() {
		//print("No");
	}

	public string title;
	public float pollution;
	public float noise;
	public float traffic;
	public int cost;
	public int income;
	public bool active;
	public bool bought;
	public bool available;
	public GameObject itemNecesario;
	public bool itemNecesarioComprado = false;

	public int turnosActivos;

	public Color Comprado;
	public Color sinDinero;
	public Color noDisponible;
	public Color fondoState;
	public Color fondoSociedad;

	public EnumOwner owner;
	 
	private Color originalColor;

	private GameObject _item;
	private bool haveFounds;
	private int _activo;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		//originalColor = gameObject.GetComponent<Image>().color;
		available = false;
		haveFounds = false;
		_activo = GameLogic.turnoActual;
	}
	
	// Update is called once per frame
	void Update () {
		if (turnosActivos>0) {
			if ((_activo+turnosActivos+1) == GameLogic.turnoActual && bought) {
			print("TURNO " + GameLogic.turnoActual);
			print("TURNO A " + (_activo+turnosActivos).ToString());

			bought = false;
			available = true;
			_activo = GameLogic.turnoActual;
			GameLogic.addModRuido(-1*noise);
			GameLogic.addModContaminacion(-1*pollution);
			GameLogic.addModTrafico(-1*traffic);

			GameLogic.addModRuidoS(owner, -1*noise);
			GameLogic.addModContaminacionS(owner, -1*pollution);
			GameLogic.addModTraficoS(owner, -1*traffic);
			}
		}

		if ((int)owner == GameLogic.rolTurnoActual) {
			
		} else {
			
		}

		if (!bought) {
			if ((itemNecesario != null && itemNecesario.GetComponent<Item>() && itemNecesario.GetComponent<Item>().bought) || itemNecesario == null) {
				itemNecesarioComprado = true;
			}

			if (owner == EnumOwner.State) {
				if ((GameLogic.balanceState+cost)<0) {
				} else {
					haveFounds = true;
				}
			} 
			if (owner == EnumOwner.Society) {
				if ((GameLogic.balanceSociety+cost)<0) {
				} else {
					haveFounds = true;
				}
			} 

			if (!haveFounds) {
				gameObject.GetComponent<Image>().color = sinDinero;
				gameObject.GetComponent<Image>().CrossFadeAlpha(0.1f, 1f, true);
				available = false;
			} else {
				
				if (!available && haveFounds && gameObject != null && gameObject.GetComponent<Image>()) {
					
					if (owner == EnumOwner.Society) {
						gameObject.GetComponent<Image>().color = fondoSociedad;
					} else {
						gameObject.GetComponent<Image>().color = fondoState;
					}
					gameObject.GetComponent<Image>().CrossFadeAlpha(1f, 1f, true);
					available = true;
				}
			}

			if ((int)owner != GameLogic.rolTurnoActual) {
				gameObject.GetComponent<Image>().color = noDisponible;
			} else {
				if (GameLogic.rolTurnoActual == (int)EnumOwner.Society) {
					gameObject.GetComponent<Image>().color = fondoSociedad;
				} else {
					gameObject.GetComponent<Image>().color = fondoState;
				}
			}
		}

	}

	/*
	private void setAvailable() {
		if (!available) {
			gameObject.GetComponent<Image>().color = originalColor;
			gameObject.GetComponent<Image>().CrossFadeAlpha(1f, 1f, true);
			available = true;
		}
	}
	*/

	void setBought() {
		gameObject.GetComponent<Image>().color = Comprado;
		_activo = GameLogic.turnoActual;
		GameLogic.addModRuido(noise);
		GameLogic.addModContaminacion(pollution);
		GameLogic.addModTrafico(traffic);

		GameLogic.addModRuidoS(owner, noise);
		GameLogic.addModContaminacionS(owner, pollution);
		GameLogic.addModTraficoS(owner, traffic);

		if (owner == EnumOwner.Society) {
			GameLogic.modifyBalanceSociety(cost);
			GameLogic.modifyIncomeSociety(income);
		} else if (owner == EnumOwner.State) {
			GameLogic.modifyBalanceState(cost);
			GameLogic.modifyIncomeState(income);
		}

		GameObject NextTurn = GameObject.Find("NextTurn");
		NextTurn.GetComponent<Button>().onClick.Invoke();

		bought = true;
	}

}
