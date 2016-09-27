//C# Example

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
class Hero {
	public float[] position;

	public static Hero GetHero() {
		var gameObject = GameObject.FindGameObjectWithTag("Hero");

		Hero hero = new Hero();
		hero.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

		return hero;
	}
}

[System.Serializable]
class Checkpoint {
	public float[] position;

	public static Checkpoint[] GetCheckpoints() {
		var gameObjects = GameObject.FindGameObjectsWithTag("Checkpoint");

		var checkpointList = new List<Checkpoint>();

		foreach(var gameObject in gameObjects) {
			Checkpoint checkpoint = new Checkpoint();
			checkpoint.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

			checkpointList.Add (checkpoint);
		}
		return checkpointList.ToArray();
	}
}

[System.Serializable]
class Pickup {
	public float[] position;
	public bool collected = false;

	public static Pickup[] GetPickups() {
		var gameObjects = GameObject.FindGameObjectsWithTag("Pickup");

		var pickupList = new List<Pickup>();

		foreach(var gameObject in gameObjects) {
			Pickup pickup = new Pickup();
			pickup.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

			pickupList.Add(pickup);
		}
		return pickupList.ToArray();
	}
}

[System.Serializable]
class Flow {
	public float[] center;
	public float[] origin;
	public float[] destiny;

	public static Flow[] GetFlows() {
		var gameObjects = GameObject.FindGameObjectsWithTag("Flow");

		var flowList = new List<Flow>();

		foreach(var gameObject in gameObjects) {
			Flow flow = new Flow();
			flow.center = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

			flow.origin = new float[] {gameObject.transform.FindChild("Origin").transform.position.x, gameObject.transform.FindChild("Origin").transform.position.y};
			flow.destiny = new float[] {gameObject.transform.FindChild("Destiny").transform.position.x, gameObject.transform.FindChild("Destiny").transform.position.y};

			flowList.Add(flow);
		}
		return flowList.ToArray();
	}
}

[System.Serializable]
class Finish {
	public float[] position;

	public static Finish GetFinish() {
		var gameObject = GameObject.FindGameObjectWithTag("Finish");

		Finish finish = new Finish();
		finish.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

		return finish;
	}
}

[System.Serializable]
class Background {
	public float[] size;

	public static Background GetBackground() {
		var gameObject = GameObject.FindGameObjectWithTag ("Background");

		Background background = new Background ();
		background.size = new float[] {gameObject.transform.localScale.x, gameObject.transform.localScale.y};

		return background;
	}
}

[System.Serializable]
class Vortex {
	public float[] position;
	public float radius;

	public static Vortex[] GetVortexes() {
		var gameObjects = GameObject.FindGameObjectsWithTag("Vortex");

		var vortexList = new List<Vortex>();

		foreach(var gameObject in gameObjects) {
			Vortex vortex = new Vortex();
			vortex.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

			vortex.radius = gameObject.transform.FindChild("Radius").transform.localScale.x/2;

			vortexList.Add(vortex);
		}
		return vortexList.ToArray();
	}
}

[System.Serializable]
class BlauCamera {
	public float[] position;

	public static BlauCamera GetCamera() {
		var gameObject = GameObject.FindGameObjectWithTag("Camera");

		BlauCamera camera = new BlauCamera();
		camera.position = new float[] {gameObject.transform.position.x, gameObject.transform.position.y};

		return camera;
	}
}

[System.Serializable]
class Level {
	public Hero hero;
	public BlauCamera camera;
	public Background background;
	public Checkpoint[] checkpoints;
	public Pickup[] pickups;
	public Flow[] flows;
	public Vortex[] vortexes;
	public Finish finish;

	public static Level GetLevel() {

		Level level = new Level ();

		level.hero = Hero.GetHero ();
		level.background = Background.GetBackground ();
		level.checkpoints = Checkpoint.GetCheckpoints ();
		level.pickups = Pickup.GetPickups ();
		level.flows = Flow.GetFlows ();
		level.vortexes = Vortex.GetVortexes ();
		level.finish = Finish.GetFinish ();
		level.camera = BlauCamera.GetCamera ();

		return level;
	}
}

class MyWindow : EditorWindow {

	public string json = "";

	[MenuItem ("blau/Serialize Level")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(MyWindow));
	}

	void OnGUI () {
		// The actual window code goes here'

		if(json == "") {
			json = JsonUtility.ToJson(Level.GetLevel()).ToString();
		}

		EditorGUI.TextArea(this.position, json);
	}
}