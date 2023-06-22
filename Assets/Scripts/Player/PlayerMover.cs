using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
   [SerializeField] private float _initialSpeed;
   [SerializeField] private float _initialTurningSpeed;
   [SerializeField] private float _accelerationSpeed;

   private Vector2 _startPosition;
   private Vector2 _direction;
   private Rigidbody _rigidbody;
   private StartGame _startGame;
   private float _speed;
   private float _turningSpeed;

   private void Awake()
   {
      _startGame = FindObjectOfType<StartGame>();
      _rigidbody = GetComponent<Rigidbody>();
   }

   private void OnEnable()
   {
      _startGame.GameStarted += OnStartMoving;
   }

   private void OnDisable()
   {
      _startGame.GameStarted -= OnStartMoving;
   }
   private void FixedUpdate()
   {
      transform.position += Vector3.forward * _speed * Time.fixedDeltaTime;
   }

   private void Update()
   {
      Move();
   }
   private void Move()
   {
      if (Input.GetKey(KeyCode.A))
      {
         transform.position += Vector3.left * _turningSpeed * 13f * Time.deltaTime;
      }
      
      if (Input.GetKey(KeyCode.D))
      {
         transform.position += Vector3.right * _turningSpeed * 13f * Time.deltaTime;
      }

      
#if UNITY_ANDROID
      if (Input.touchCount > 0)
      {
         Touch touch = Input.GetTouch(0);

         if (touch.phase == TouchPhase.Moved)
         {
            _rigidbody.velocity = new Vector3(touch.deltaPosition.x * _turningSpeed, _rigidbody.velocity.y,
               _rigidbody.velocity.z);
         }

         if (touch.phase == TouchPhase.Ended)
         {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y,
               _rigidbody.velocity.z);
         }
      }
#endif
   }

   public void Accelerate()
   {
      _speed += _accelerationSpeed;
   }
   
   public void Braking()
   {
      _speed -= _accelerationSpeed;
   }

   public void Finished()
   {
      _speed = 0;
      _turningSpeed = 0;
   }

   private void OnStartMoving()
   {
      _speed = _initialSpeed;
      _turningSpeed = _initialTurningSpeed;
   }
}
