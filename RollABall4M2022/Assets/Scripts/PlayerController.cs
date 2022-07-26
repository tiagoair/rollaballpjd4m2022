using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Numero de moedas coletadas
    public int coins = 0;
    
    // Guarda uma referência para os controles que criamos no InputAction
    private GameControls _gameControls;
    // Guarda referência para o PlayerInput, que é quem conecta o dispositivo de controle ao código
    private PlayerInput _playerInput;
    // Referência para a Câmera Principal (main) do jogo
    private Camera _mainCamera;
    // Guardar o movimento que está sendo lido do controle do jogador
    private Vector2 _moveInput;
    // Guarda a referência para o componente de física do jogador, que usaremos para mover o jogador
    private Rigidbody _rigidbody;
    
    // Diz se o jogador está no chão ou não
    private bool _isGrounded;

    // Velocidade que o jogador vai se mover
    public float moveMultiplier;
    // Velocidade maxima que o jogador vai poder andar em cada eixo (x e z somente pois não queremos limitar o y)
    public float maxVelocity;
    
    // Distancia que o raio vai percorrer procurando algo para bater
    public float rayDistance;
    // Mascara de colisão com o chão
    public LayerMask layerMask;
    
    // Força que o jogador vai usar para pular
    public float jumpForce;

    // Serve para inicializarmos o objeto do jogador
    private void OnEnable()
    {
        // Associa à variável, o componente Rigidbody presente no objeto do jogador na Unity
        _rigidbody = GetComponent<Rigidbody>();
        
        // Instancia um novo objeto da classe GameControls
        _gameControls = new GameControls();

        // Associa à variável, o componente PlayerInput presente no objeto do jogador na Unity
        _playerInput = GetComponent<PlayerInput>();
        
        // Associa a nossa variável o valor presente na variável main da classe Camera, que é a camera principal do jogo
        _mainCamera = Camera.main;

        // Inscrevendo o delegate para a função que é chamada quando uma tecla/botão no controle é apertado
        _playerInput.onActionTriggered += OnActionTriggered;
    }

    // Chamada quando o objeto é desativado
    private void OnDisable()
    {
        // Desinscrever o delegate
        _playerInput.onActionTriggered -= OnActionTriggered;
    }

    // Delegate para adicionarmos funcionalidade quando o jogador aperta um botão
    // o parametro obj, da classe InputAction.CallbackContext traz as informações do botão
    // que foi apertado pelo jogador
    private void OnActionTriggered(InputAction.CallbackContext obj)
    {
        // Compara a informação trazida pelo obj, checando se o nome da ação executada
        // tem o mesmo nome da ação de mover o jogador (Movement.name)
        if (obj.action.name.CompareTo(_gameControls.Gameplay.Movement.name) == 0)
        {
            // caso a ação seja de mover, passamos o valor que está vindo no obj, que,
            // como definimos no InputAction, é um Vector2, para a variável _moveInput
            _moveInput = obj.ReadValue<Vector2>();
        }
        
        // Compara se a informação trazida pelo obj é referente ao comando de pular
        if (obj.action.name.CompareTo(_gameControls.Gameplay.Jump.name) == 0)
        {
            // se o jogador apertar e soltar o botão de pulo, chamamos a função de pular Jump()
            if(obj.performed) Jump();
        }
    }

    // Executa a movimentação do jogador através da física
    private void Move()
    {
        // Pegamos o vetor que aponta para a direção que a camera está olhando
        Vector3 camForward = _mainCamera.transform.forward;
        // pegamos o vetor que aponta para a direita da camera
        Vector3 camRight = _mainCamera.transform.right;
        
        // zeramos as componentes no eixo Y desses vetores
        camForward.y = 0;
        camRight.y = 0;
        
        // Usamos AddForce para adicionar uma força gradual para o jogador, quanto mais
        // tempo seguramos a tecla mais rapido a bolinha vai
        _rigidbody.AddForce(
            // Multiplicamos o input que move o jogador para frente pelo vetor que aponta
            // para a frente da camera
            (camForward * _moveInput.y +
             // Multiplica o input que move o jogador para direita pelo vetor que aponta
             // para a direita da camera
                            camRight * _moveInput.x )
            // Multiplica esse resultado pela velocidade e pela variavel de deltaTime
            * moveMultiplier * Time.fixedDeltaTime);
    }
 
    // Função que é executada todo loop de física da unity
    private void FixedUpdate()
    {
        // Quando a física for atualizada, chama a função de Mover
        Move();
        LimitVelocity();
    }
    
    // Função que vai limitar a velocidade máxima do jogador
    private void LimitVelocity()
    {
        // Pega a velocidade atual do player atraves do rigid body
        Vector3 velocity = _rigidbody.velocity;
        
        // Compara a velocidade no eixo X (usando a funcao Abs para ignorar o sinal negativo caso tenha)
        // utilizamos a função Sign para recuperar o valor do sinal da velocidade e multiplicar pela velocidade maxima
        // permitida
        if (Mathf.Abs(velocity.x) > maxVelocity) velocity.x = Mathf.Sign(velocity.x) * maxVelocity;
        
        // mesma coisa de cima, mas para o eixo z, porém usando o método Clamp,
        // onde -maxVelocity < velocity.z < maxVelocity
        velocity.z = Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity);
        
        // Atribui o vetor que alteramos de volta ao rigid body
        _rigidbody.velocity = velocity;
    }
    
    /* O que eu preciso para pular?
     * - Jogador precisa estar no chão
     * 1 --> Usar a colisão do rigidbody do jogador com o chão e, se a colisão estiver acontecendo, faz uma variável
     * 1 --> que checa se o jogador está no chão ficar verdadeira
     * 2 ---> Usar um raycast = ()---|
     * 2 ---> Atira um raio em alguma direção (no nosso caso, vai ser sempre para baixo)
     * 2 ---> Caso atinja algum objeto, ela retorna uma colisão que podemos usar para fazer a variável que checa se
     * 2 ---> o jogador está no chão ficar verdadeira caso o objeto colidido seja um objeto que represente o chão
     * 2 ---> Podemos usar LayerMask para somente verificar colisões com certos tipos de objeto
     * - Jogador precisa apertar o botão de pular
     * 1 --> Usamos a função OnActionTriggered e comparamos se o nome da ação tem o mesmo nome da ação de pulo
     * 1 --> caso tenha, checamos se o botão foi apertado (started), foi solto (canceled) ou foi pressionado e solto (performed)
     */
    
    // Vai ser usada para checar se o jogador está no chão ou não
    private void CheckGround()
    {
        // variável que guarda o resultado da colisão do raycast
        RaycastHit collision;
        
        // vamos atirar um raio pra baixo e vamo checar se ele bate em algo
        if (Physics.Raycast(transform.position, Vector3.down, out collision, rayDistance, layerMask))
        {
            // Se executou esse código é porquê o jogador estará no chão
            _isGrounded = true;
        }
        else
        {
            // Se executou esse código, o jogador não estará no chão
            _isGrounded = false;
        }
    }
    
    
    // Função que vai ser chamada para fazer o jogador pular
    private void Jump()
    {
        // se o jogador estiver no chão (isGrounded for verdadeira)
        if (_isGrounded)
        {
            // adicionamos uma força para cima do tipo Impulso para fazer o jogador pular
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        // Desenha a linha do raycast no editor da unity para ficar mais facil visualizarmos o tamanho do raycast
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.yellow);
    }

    // Função que checa se o objeto entrou em um colisor setado como IsTrigger
    private void OnTriggerEnter(Collider other)
    {
        // Se o objeto tiver a tag Coin
        if (other.CompareTag("Coin"))
        {
            // Aumente o numero de coins do jogador em uma unidade
            coins++;
            
            // Manda a notificação da mudança do valor de coins
            PlayerObserverManager.CoinsChanged(coins);
            
            // Destrua o objeto da coin
            Destroy(other.gameObject);
        }
    }
} //NAO ESCREVA DEPOIS DESSA ULTIMA CHAVE Ò.Ó
