const formulario = document.querySelector("form");
const botao = document.querySelector("button");
const Iemail = document.querySelector(".email");
const Isenha = document.querySelector(".senha");
const mensagemErro = document.getElementById("mensagem-erro");

botao.disabled = true;

function logar() {

    fetch(`http://localhost:5087/api/Usuario/urbtech/login`,
    {
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify({
            email: Iemail.value,
            senha: Isenha.value
        })
    })
    .then(function (res) {
     if (res.ok) {
        localStorage.setItem('usuarioId', res.Id);
        return res.json();
     }else{
        mensagemErro.innerHTML = "Email e/ou senha inválido(os)";
        mensagemErro.style.display = "block";
     }
     console.log(res)
     })
     .then(function (data) {
        console.log(data);
        console.log(data.id);
        localStorage.setItem('usuarioId', data.id);
        console.log("entrei")
        window.location.href = "../home/index.html";
     })
     .catch(function (res) {
        mensagemErro.innerHTML = "Email e/ou senha inválido(os)";
        mensagemErro.style.display = "block";
     })
};

formulario.addEventListener('submit', function(event){
    event.preventDefault();
    logar();
});