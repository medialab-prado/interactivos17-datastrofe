using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {


	private bool positioned = false;
	public GameObject[] vehiculos;
	public int totalVehicles;
	private int total;

	void Awake() {
		total = 0;
	}


	void Start () {
	}
	

	void Update () {
		if (GameLogic.vTrafico != null && GameLogic.vTrafico != "") {
			if (int.Parse(GameLogic.vTrafico)<=0) return;
			totalVehicles = int.Parse(GameLogic.vTrafico) / 20;
			var total = GameObject.FindGameObjectsWithTag("coche").Length;

			//print("HAY " +  total + " COCHES");

			if (totalVehicles < total) {
				int contador = 0;
				foreach (GameObject item in GameObject.FindGameObjectsWithTag("coche")) {
					if (contador>totalVehicles) {
						Destroy(item);
						print("DESTRUYENDO OBJECT");
					}
					contador++;
				}
			}

			if (totalVehicles > total) {
				var diff = totalVehicles - total;
				if (diff>0) {
					for (int i = 0; i < diff; i++) {
						CrearVehiculo();
					}
				}
			}
		}

		//print("TOTAL V " + totalVehicles + " ACTUAL: " + total + " GO: " + GameObject.FindGameObjectsWithTag("coche").Length);
	}


	private void CrearVehiculo() {
		GameObject nuevoCoche = Instantiate(vehiculos[Random.Range(0, vehiculos.Length)]) as GameObject;
		nuevoCoche.SetActive(true);
	}

}
