#pragma strict

public var MoveSpeed :float = 50;
public var jumpSpeed : float = 55;
public var MoveDirection = Vector3.zero;
public var Gravity :float = 45;
public var grounded : boolean = false;

function Start (){
}

function Update () {
    if( grounded ){
        MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);
        MoveDirection *= MoveSpeed ;
    }
    if( grounded ) {
        if(Input.GetKey(KeyCode.Space)){
            MoveDirection.y = jumpSpeed;
        }
    }
    MoveDirection.y -= Gravity * Time.deltaTime;
    var Controller = GetComponent(CharacterController);
    var Flags = Controller.Move( MoveDirection * Time.deltaTime);
    grounded = (Flags & CollisionFlags.CollidedBelow) !=0;
}