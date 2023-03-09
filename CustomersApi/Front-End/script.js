document.addEventListener('DOMContentLoaded', init); // se ejecuta cuando se carga la página

const urlApi = "http://localhost:7217/api/"; // buscar en launch.json si esta bien el puerto y la url

var customer =[];
function init() {
    search();
}
async function search() {
    var url = urlApi + 'controller'
    var response = await fetch(url, {
        "method": "GET",
        "headers": {
            "Content-Type": "application/json"
        }
    })
    customer = await response.json();
    
    
    var html = ''
    
    for (customers of customer) {
        var row = `<tr>
        <td>${customers.firstName}</td>
        <td>${customers.lastName}</td>
        <td>${customers.email}</td>
        <td>${customers.phone}</td>

        <td>
            <a href="#" onclick="edit(${customers.id})" class="myButton">Editar</a>
            <a href="#" onclick="remove(${customers.id})" class="myButtonDelete">Eliminar</a>
        </td>
    </tr>` // estas comillas permiten ENTER dentro de ellos
        
        html += row;
    }
    
    document.querySelector('#customers > tbody').outerHTML = html;
}


async function remove(id) {
    console.log(id)
    respuesta = confirm('¿Estas seguro de eliminar el cliente?')
    if (respuesta) {
        var url = urlApi + 'controller/' + id
       await fetch(url, {
            "method": "DELETE",//nombre en postman 
            "headers": {
                "Content-Type": "application/json"
            }
        })
        window.location.reload()
    }     
}
async function save() {
    var data ={
        "firstName": document.getElementById("txtFirstname").value,
        "lastName": document.getElementById("txtLastname").value,
        "email": document.getElementById("txtEmail").value,
        "phone": document.getElementById("txtPhone").value,
        "address": document.getElementById("txtAddress").value,
    }
    var id = document.getElementById("txtId").value;
    if (id != '')
    {
        data.id = id;
    }
    
        var url = urlApi + 'controller'
        await fetch(url, {
            "method": id != '' ? "PUT" : "POST",//si tiene id put sino post
            "body": JSON.stringify(data),
            "headers": {
                "Content-Type": "application/json"
            }
        })
        window.location.reload()
}
function abrirFormulario() {
    htmlModal = document.getElementById("modal");
    htmlModal.setAttribute("class", "modale opened");
}
function cerrarModal() {
    htmlModal = document.getElementById("modal");
    htmlModal.setAttribute("class", "modale");
}

function agregar() {
    clean();
    abrirFormulario();
}

function edit(id)
{
    abrirFormulario();
    var customers = customer.find(x => x.id == id)
    document.getElementById("txtId").value = customers.id;
    document.getElementById("txtFirstname").value = customers.firstName;
    document.getElementById("txtLastname").value = customers.lastName;
    document.getElementById("txtEmail").value = customers.email;
    document.getElementById("txtPhone").value = customers.phone;
    document.getElementById("txtAddress").value = customers.address;


}

function clean() {
    document.getElementById("txtId").value = '';
    document.getElementById("txtFirstname").value = '';
    document.getElementById("txtLastname").value = '';
    document.getElementById("txtEmail").value = '';
    document.getElementById("txtPhone").value = '';
    document.getElementById("txtAddress").value = '';
    
}
