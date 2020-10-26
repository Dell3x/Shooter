using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Rigidbody))]

public class CharacterController : Character
{
    private Rigidbody _rigidbody;//кэшированный компонент Rigidbody (чтобы не создавать каждый раз, когда обращаемся)
    private Animator animator;

    [SerializeField]private float movingForce = 80.0f;  //сила для передвижения

    [SerializeField]private float maxSpeed;

    [SerializeField]private float jumpForce = 80f;  //сила прыжка

    [SerializeField]private float maxSlope = 30f;   //Максимальный уклон, по которому может идти персонаж

    private bool onGround = false;  //Стоит ли персонаж на подходящей поверхности (или летит/падает)

    [SerializeField] private float damping = 0.3f;

    [SerializeField] float cameraTurnSpeed = 7f;




    public void Destroyed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    //Инициализация объекта
    void Awake ()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();  //Находим и запоминаем (кэшируем) компонент Rigidbody
        animator = gameObject.GetComponent<Animator>();
        FixedUpdate();
    }
	
	void Start ()
    {
        //Здесь будет взаимодействие с другими объектами в начале игры, после того, как они уже проинициализировались в их методе Awake () 
        //Например, можно найти инвентарь, и вычесть его вес из скорости перемещения. 
    }
    
    //Коллайдер персонажа прекращает взаимодействие с каким-то другим коллайдером
    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }
  
    //Коллайдер персонажа начинает взаимодействие с каким-то другим коллайдером
    private void OnCollisionStay(Collision collision)
    {
        onGround = CheckIsOnGround(collision);
    }

    //Вызывается каждый кадр. Частота может меняться в зависимости от сложности рендеринга и мощности компьтера.
    void Update ()
    {
        
        ShootBullet();
        ShootRocket();

    }

   
    private void MouseLook()
    {
        transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * cameraTurnSpeed);

    }
    //Вызывается каждый шаг просчета физики, вне зависимости от FPS (вне зависимости от скорости рендеринга)
    void FixedUpdate()
    {
        if (onGround)
        {
            animator.SetFloat("vSpeed", Input.GetAxis("Vertical"));
            animator.SetFloat("hSpeed", Input.GetAxis("Horizontal"));

            if (onGround)   //если стоим на земле
            {
                animator.applyRootMotion = true;
                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * movingForce, 0, Input.GetAxis("Vertical") * movingForce));
                if (Input.GetKeyDown(KeyCode.Space))
                //Если игрок нажал "пробел"
                {
                    animator.SetTrigger("jump");
                    _rigidbody.AddForce(Vector3.up * jumpForce);
                }
                animator.SetBool("inAir", false);
            }
            
        }
    }
    

    // Проверяем, подходит ли поверхность коллайдера для того, чтобы персонаж на ней стоял.
    //Объект Collision для проверки.
    //return true, если поверхность подходящая, false - если нет.
    private bool CheckIsOnGround(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++) //Проверяем все точки соприкосновения
        {
            if (collision.contacts[i].point.y < transform.position.y)   //если точка соприкосновения находится ниже центра нашего персонажа
            {
                if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) < maxSlope)   //Если уклон поверхности не превышает допустимое значение
                {
                    return true;    //найдена точка соприкосновения с подходящей поверхностью - выходим из функции, возвращаем значение true.
                }
            }
        }
        return false;   //Подходящая поверхность не найдена, возвращаем значение false.
    }

    // Рассчитываем и прикладываем силу перемещения персонажа в зависимости от значений осей инпута

    /// Разворачиваем персонажа лицом к курсору
    private void LookAtTarget()
    {
        Plane plane = new Plane(Vector3.up, transform.position);



        float distance;

        //Находим главную камеру, и с ее помощью получаем луч, идущий из камеры в ту точку пространства, которая находится под курсором мыши.
        // Input.mousePosition - текущее положение курсора в пространстве экрана (нижний левый угол - 0, 0; верхний правый угол - ширина окна, высота окна)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    

        //С помощью физического движка запускаем полученный луч
        if (plane.Raycast(ray, out distance))  //если луч попал в какой-то коллайдер, метод возвращает true, и выводит параметры столкновения в переменную hit (ключевое слово out)
        {
            Vector3 position = ray.GetPoint(distance);  //Находим на луче точку, находящуюся на заданном расстоянии от начала луча. Это расстояние берем из параметров столкновения - переменной hit.
            transform.LookAt(position); //Поворачиваем трансформ персонажа локальной осью Z (forward) в сторону точки столкновения луча с коллайдером.
        }
    }

}
