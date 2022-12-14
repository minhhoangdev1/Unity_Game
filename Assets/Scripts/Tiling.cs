using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    public int offsetX=2;//the offset so that we don't get any weird errors

    //these are used for checking if we need to instantiate stuff
    public bool hasARightBuddy=false;
    public bool hasALeftBuddy=false;

    public bool reverseScale=false;//used if the object is not tilable

    private float spriteWidth=0f;//the width of our element
    private Camera cam;
    private Transform myTransform;

    void Awake(){
        cam=Camera.main;
        myTransform=transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer=GetComponent<SpriteRenderer>();
        spriteWidth=sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //does it still need buddies? IF not do nothing
        if(hasALeftBuddy==false ||hasARightBuddy==false){
            //calculate the Cameras extent (hasIf the width) of that the camera can see in world coordinates
            float camHorizontalExtend=cam.orthographicSize * Screen.width/Screen.height;

            //calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisiblePositionRight=(myTransform.position.x+spriteWidth/2)-camHorizontalExtend;
            float edgeVisiblePositionLeft=(myTransform.position.x-spriteWidth/2)+camHorizontalExtend;

            //checking if we can see the edge of the element and then calling MakeNewBuddy if we can see
            if(cam.transform.position.x >= edgeVisiblePositionRight -offsetX && hasARightBuddy==false){
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if(cam.transform.position.x <=edgeVisiblePositionLeft + offsetX && hasALeftBuddy==false){
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }

        }
    }
    //a funtion that creates a buddy on the side required
    void MakeNewBuddy(int rightOrLeft){
        //calculating the new position for our new buddy
        Vector3 newPosition=new Vector3(myTransform.position.x+spriteWidth +rightOrLeft, myTransform.position.y,myTransform.position.z);
        //instantiating uor new body and storing him in a variable variable
        Transform newBuddy=Instantiate(myTransform,newPosition,myTransform.rotation) as Transform;

        //if not tilable let's reverse the x size og our object to get rid of ugly seams
        if(reverseScale==true){
            newBuddy.localScale=new Vector3(newBuddy.localScale.x*-1,newBuddy.localScale.y,newBuddy.localScale.z);
        }

        newBuddy.parent=myTransform.parent;
        if(rightOrLeft > 0){
            newBuddy.GetComponent<Tiling>().hasALeftBuddy=true;
        }else{
            newBuddy.GetComponent<Tiling>().hasARightBuddy=true;
        }

    }
}
