import UnityEngine.UI;

#pragma strict
//var playerb;
//other scripts
var introcomplete = false;
var target : Transform;
var distance = 10.0;

var playerScript;
 
var xSpeed = 250.0;
var ySpeed = 250.0;
 
var yMinLimit = -90;
var yMaxLimit = 90;

var maxDist : float = 2000;
var minDist : float = 30;
var zoomSpeed : float = 15;

public var x = 0.0;
public var y = 6.0;
public var ymod = 0.0;
public var ofsetx = 0.0;

var clicking = false;
var zooming = false;

public var EndMarker : GameObject;
public var TargetTexture : Texture;
var screenPos : Vector3;
@script AddComponentMenu("Camera-Control/Mouse Orbit")

public var blackgroundImage : Image;
public var blackgroundcolor : Color;

public var spawnSpawn : GameObject;
var done;

//pinchzoom

public var perspectiveZoomSpeed : float = 0.5f;        // The rate of change of the field of view in perspective mode.
public var orthoZoomSpeed : float = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


 
function Start () {

	SetStartingDetails();    
}
 
function LateUpdate () {
	if(introcomplete == false){
		InitialCameraBehavior();
	}
	if(introcomplete && !done){
		MainCameraBehavior();
	}
	blackgroundImage.color = blackgroundcolor;
	

}
 
static function ClampAngle (angle : float, min : float, max : float) {
    if (angle < -360)
        angle += 360;
    if (angle > 360)
        angle -= 360;
    return Mathf.Clamp (angle, min, max);
    
}

function SetStartingDetails(){
	var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x + 6;
 
    // Make the rigid body not change rotation
    if (GetComponent.<Rigidbody>())
        GetComponent.<Rigidbody>().freezeRotation = true;
        
        var rotation = Quaternion.Euler(6, x, 0);
        var position = rotation * Vector3(0, 0, -10) + target.position;
 
        transform.rotation = rotation;
        transform.position = position;
        
     //play initialspinandzoom
     gameObject.GetComponent.<Animation>().Play("start"); 
     
   
}

function MainCameraBehavior(){

	PinchControls();
 
	if(Input.GetKey(KeyCode.LeftArrow))
		x+= xSpeed * .02;
		
	if(Input.GetKey(KeyCode.RightArrow))
		x+= xSpeed * -.02;
	
	//touch controls
	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !zooming) {
		// Get movement of the finger since last frame
		var touchDeltaPosition: Vector2 = Input.GetTouch(0).deltaPosition;
		// Move object across XY plane
		x += touchDeltaPosition.x * xSpeed *.02;
		y -= touchDeltaPosition.y * ySpeed *.02;
		y = ClampAngle(y, yMinLimit, yMaxLimit);
	 	    clicking = true;     
	 	     GetComponent.<Animation>()["camerabob"].speed = 0F; 
	}	
	//mouse controls	
	if (target && Input.GetMouseButton(1))  {
	        x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
	        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
	        y = ClampAngle(y, yMinLimit, yMaxLimit);
	 	    clicking = true;     
	 	     GetComponent.<Animation>()["camerabob"].speed = 0F; 
	    } 	 		
	  
	    if (target && Input.GetMouseButtonUp(1)){
	    	clicking = false;
	    	GetComponent.<Animation>()["camerabob"].speed = 1F;  
	    }
	    
	     var rotation = Quaternion.Euler(y, x, 0);
	     var offset = Vector3 (ofsetx, 0,0);
	     var position = rotation * Vector3(0.0, ymod, -distance) + target.position + offset;
	     transform.rotation = rotation;
	     transform.position = position;
	        
	    // Aumentado esto:
	    if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < maxDist){           
	           distance += zoomSpeed;                             
	           transform.Translate(Vector3.forward * -zoomSpeed); 
	    }
	   
	    if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > minDist){     
	           distance -= zoomSpeed;                             
	               transform.Translate(Vector3.forward * zoomSpeed); 
	 	}
	 	
	 	if (distance <= 10f)
	 		distance = 10f;
	 		
	 	
	
}

public function SetDistanceTo30(){
Debug.Log("called!");
	distance = 30; 
	ofsetx = 7;
	y = 0;
	GameObject.Find("Sol").GetComponent(Renderer).enabled = false;
	transform.position = Vector3 (-23.8f, 93.2f, 7272.4f);
	transform.LookAt(spawnSpawn.transform);
	done = true;
}

function InitialCameraBehavior(){
	//x+=1f;
	 var rotation = Quaternion.Euler(y, x, 0);
	 var position = rotation * Vector3(0.0, ymod, -distance) + target.position ;
	 transform.rotation = rotation;
	 transform.position = position;
	//     gameObject.GetComponent.<Animation>().Play("startingrotation"); 
}

public function PlayCameraBob(){
	gameObject.GetComponent.<Animation>().Play("camerabob"); 
}

function ActivateDistanceCounter(){
		//GameObject.Find ("Player").GetComponent(playerscript).showEarthDist = true;
}

function IntroComplete(){
	introcomplete = true;
	PlayCameraBob();
}

public function lookAtSpawn(){
	var spawn = GameObject.Find("Spawn Point").transform;
	transform.LookAt(spawn);

}

function PinchControls(){
 // If there are two touches on the device...
    if (Input.touchCount == 2)
    {
    	zooming = true;
    	
        // Store both touches.
        var touchZero = Input.GetTouch(0);
        var touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Find the difference in the distances between each frame.
        var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        // If the camera is orthographic...
        if (GetComponent.<Camera>().orthographic)
        {
            // ... change the orthographic size based on the change in distance between the touches.
            GetComponent.<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            GetComponent.<Camera>().orthographicSize = Mathf.Max(GetComponent.<Camera>().orthographicSize, 0.1f);
        }
        else
        {
            // Otherwise change the field of view based on the chang10e in distance between the touches.
            distance += deltaMagnitudeDiff * perspectiveZoomSpeed *10;

            // Clamp the field of view to make sure it's between 0 and 180.
            distance = Mathf.Clamp(distance,10, 520);        }
    }
    else
    	zooming = false;
}

