using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

using UnityEngine.UI;

public enum EnumOwner {
	State = 0, Society = 1
};

public class GameLogic : MonoBehaviour
{
	private static GameLogic instance;


	public static float contaminacion;
	public static float ruido;
	public static float rawRuido;
	public static float trafico;

	private int actualContaminacion;
	private int actualRuido;
	private int actualTrafico;

	public static float modContaminacion;
	public static float modRuido;
	public static float modTrafico;

	public static float modContaminacionST;
	public static float modRuidoST;
	public static float modTraficoST;
	public static float modContaminacionSO;
	public static float modRuidoSO;
	public static float modTraficoSO;

	public static int balanceState;
	public static int balanceSociety;
	public Text balanceInfo;

	public static int totalStateIncome;
	public static int totalSocietyIncome;
	public static int turnoActual;

	public static int rolTurnoActual = 0;

	public static string vTrafico;

	public int turno;

	public Text bState;
	public Text bSociety;
	public Text textTurnos;

	public Text AQI;
	public Image pbContaminacion;

	public Text intensidadTrafico;
	public Image pbTrafico;

	public Text nivelDBA;
	public Image pbRuido;

	public static float totalContamination;
	public static float totalRuido;
	public static float totalTrafico;
	public static float estadoTrafico;

	public static string[] RolesTurnos = new string[]{"estado", "sociedad"};

	void Start() {
		modContaminacion = 0;
		modRuido = 0;
		modTrafico = 0;
		balanceState = 1;
		balanceSociety = 1;
		totalStateIncome = 0;
		totalSocietyIncome = 0;
		turno = 1;

	}

	void Update() {
		UpdateValues();
		bState.text = "Estado: " + balanceState.ToString() + " (" + totalStateIncome.ToString("+0;-#")  + " por turno)";
		bSociety.text = "Sociedad: " + balanceSociety.ToString() + " (" + totalSocietyIncome.ToString("+0;-#")  + " por turno)";
		textTurnos.text = "<b>Turno " + turno.ToString("#") + " (" + RolesTurnos[rolTurnoActual] + ")</b>";
		balanceInfo.text = "Estado: " + modContaminacionST.ToString("+0.00;-#.##") + "% / " + modRuidoST.ToString("+0.00;-#.##") + "% / "  + modTraficoST.ToString("+0.00;-##") + "% | ";
		balanceInfo.text += "Sociedad: " + modContaminacionSO.ToString("+0.00;-#.##") + "% / " + modRuidoSO.ToString("+0.00;-#.##") + "% / "  + modTraficoSO.ToString("+0.00;-##") + "% | ";
		balanceInfo.text += "( calidad del aire / ruido / tráfico )";
		//print("C " + modContaminacionST);
		//print("R " + modRuidoST);
		//print("T " + modTraficoST);

	}

	void UpdateValues() {
		
		totalContamination = contaminacion + (contaminacion * (modContaminacion/100f));

		float calculoRuido = (rawRuido - 35);
		//totalRuido = ruido + (ruido * (modRuido/100f))*(ruido * (modRuido/100f));
		if (totalRuido<=0) {
			totalRuido=ruido;
			totalRuido += ((modRuido*2)/100f);
		}


		//print("RUIDO: " + ruido);
//		print("TOTAL RUIDO: " + totalRuido);
//		print("MOD RUIDO: " + modRuido);
		//print("RAW RUIDO: " + rawRuido);

		//print(ruido + " + (" + ruido + " * (" + modRuido + "/100f)=" + totalRuido);

		totalTrafico = trafico + (trafico * (modTrafico/100f));


		pbContaminacion.GetComponent<Progress>().Value = totalContamination;

		pbRuido.color = Color.white;
		pbRuido.fillAmount = totalRuido/120f;

		pbTrafico.GetComponent<Progress>().Value = totalTrafico;
	}

	/*
	public void UpdateLegends() {
		intensidadTrafico.text = vTrafico.ToString() + "(<color=red>" + modTrafico.ToString("+0.00;-#.##") + "%</color>)";
		nivelDBA.text = ruido.ToString("##")+ "(<color=red>" + modRuido.ToString("+0.00;-#.##") + "%</color>)";
		AQI.text = contaminacion.ToString("##")+ "(<color=red>" + modContaminacion.ToString("+0.00;-#.##") + "%</color>)";
	}*/
		

	public static void addModContaminacion(float contaminacion) {
		GameLogic.modContaminacion += contaminacion;
	}

	public static void addModRuido(float ruido) {
		GameLogic.modRuido += ruido;
	}

	public static void addModTrafico(float trafico) {
		GameLogic.modTrafico += trafico;
	}

	public static void addModContaminacionS(EnumOwner owner, float contaminacion) {
		if (owner == EnumOwner.Society) {
			GameLogic.modContaminacionSO += contaminacion;
		} else {
			GameLogic.modContaminacionST += contaminacion;
		}
	}

	public static void addModRuidoS(EnumOwner owner,float ruido) {
		if (owner == EnumOwner.Society) {
			GameLogic.modRuidoSO += ruido;
		} else {
			GameLogic.modRuidoST += ruido;
		}
	}

	public static void addModTraficoS(EnumOwner owner, float trafico) {
		if (owner == EnumOwner.Society) {
			GameLogic.modTraficoSO += trafico;
		} else {
			GameLogic.modTraficoST += trafico;
		}
	}

	public static void modifyBalanceState(int money) {
		GameLogic.balanceState += money;
	}

	public static void modifyBalanceSociety(int money) {
		GameLogic.balanceSociety += money;
	}

	public static void modifyIncomeState(int money) {
		GameLogic.totalStateIncome += money;
	}

	public static void modifyIncomeSociety(int money) {
		GameLogic.totalSocietyIncome += money;
	}

	public static float getModContaminacion() {
		return GameLogic.modContaminacion;
	}

	public static GameLogic Instance
	{
		get { return instance ?? (instance = new GameObject("GameConfig").AddComponent<GameLogic>()); }
	}
		

	public void nextTurn() {
		// Cada turno, sigue teniendo efectos.
		//addModRuido((int)GameLogic.modRuido);
		//addModContaminacion((int)GameLogic.modContaminacion);
		//addModTrafico((int)GameLogic.modTrafico);
		modifyBalanceSociety(GameLogic.totalSocietyIncome);
		modifyBalanceState(GameLogic.totalStateIncome);
		UpdateValues();
		if (totalContamination>0) contaminacion = (int)totalContamination;
		//if (totalRuido>0) ruido = (int)totalRuido;
		if (totalTrafico>0) trafico = (int)totalTrafico;
		turno++;
		if (rolTurnoActual==0) {
			rolTurnoActual = 1;
		} else {
			rolTurnoActual = 0;
		}
		totalRuido += ((modRuido*4)/100f);
		GameLogic.turnoActual = turno;
		print(GameLogic.turnoActual);
	}
}