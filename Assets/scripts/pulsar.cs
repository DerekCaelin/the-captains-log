using UnityEngine;
using System.Collections;

public class pulsar : MonoBehaviour {
	public Color alpha;
	public Shader alphab;
	public Material neb;	
	Color alphac;
	public bool fieldFade;
	public bool fade;
	GameObject sol;
	Vector3 solPos;

	// Use this for initialization
	void Start () {
		neb.color = alpha;
		alpha = neb.color;
		sol = GameObject.Find ("Sol");
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "pulsar";
		float period = Random.Range (.2f, 6f); //hmph. actual pulsar is 1.56 milliseconds to .8 seconds?
		GetComponent<ParticleSystem>().startLifetime = period;
		GetComponent<ParticleSystem>().emissionRate = 1 / period;
		//Debug.Log (gameObject.renderer.material);
		solPos = sol.transform.position;
		//MakeEarthField ();
	
	}

	void Update(){

		transform.Rotate (0,.02f,0);

	if(fade == true){


		if(fieldFade == true){
				//Debug.Log(alphac);
				if (alpha.a < 1f) {
					alpha.a += .5f * Time.deltaTime;
				}
				if(alpha.a >=1f){
					fieldFade = false;
					fade = false;
				}
			}
			
		if(fieldFade == false){
				if (alpha.a > 0) {
					alpha.a -= .5f * Time.deltaTime;
				}
				if(alpha.a <=0){
					fieldFade = true;
					fade = false;
				}
			}
		
			neb.color = alpha;
		}
	}

	public void MakeEarthField(){
		float distance = Vector3.Distance (solPos,transform.position) / 2.5f;
		transform.localScale = new Vector3 (distance, distance, distance);

	}

	public void FieldVisible(bool which){
		fieldFade = which;
		fade = true;

	}


}
