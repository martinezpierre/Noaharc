using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public static Ship instance;

    List<AnimalInfo> savedAnimals = new List<AnimalInfo>();

    private void Awake()
    {
        instance = this;

        #region createInputKey
        allAvailableKeys = new List<InputKey>();

        allAvailableKeys.Add( new InputKey( KeyCode.A, 'A' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.B, 'B' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.C, 'C' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.D, 'D' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.E, 'E' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.F, 'F' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.G, 'G' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.H, 'H' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.I, 'I' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.J, 'J' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.K, 'K' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.L, 'L' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.M, 'M' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.N, 'N' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.O, 'O' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.P, 'P' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.Q, 'Q' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.R, 'R' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.S, 'S' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.T, 'T' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.U, 'U' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.V, 'V' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.W, 'W' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.X, 'X' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.Y, 'Y' ) );
        allAvailableKeys.Add( new InputKey( KeyCode.Z, 'Z' ) );
        #endregion
    }

    void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            SeekAnimal();
        }
	}

    void SeekAnimal()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;
        if ( Physics.Raycast( ray, out hit ) )
        {
            if ( hit.collider.tag == "Animal")
            {
                LaunchBuoy( hit.transform );
            }
        }
    }

    void LaunchBuoy(Transform animal)
    {
        Buoy buoy = Instantiate( Resources.Load( "Prefab/Buoy", typeof( Buoy ) ),animal.position , Quaternion.identity ) as Buoy;
        buoy.transform.LookAt( transform );
        InputKey key = allAvailableKeys[ Random.Range( 0, allAvailableKeys.Count ) ];
        allAvailableKeys.Remove( key );
        buoy.Initialize( key );
        animal.parent = buoy.transform;
    }

    private void OnCollisionEnter( Collision collision )
    {
        Debug.Log( "A" );
        if (collision.transform.tag == "Buoy")
        {
            Debug.Log( "B" );
            //Add Point or Add Animal on Ship
            InputKey key = collision.transform.GetComponent<Buoy>().input;
            allAvailableKeys.Add( key );
            Destroy( collision.gameObject );
        }
    }

    private List<InputKey> allAvailableKeys;
}

public class InputKey
{
    public InputKey(KeyCode myKey , char myCharacter)
    {
        key = myKey;
        character = myCharacter;
    }

    public KeyCode key;
    public char character;
}
