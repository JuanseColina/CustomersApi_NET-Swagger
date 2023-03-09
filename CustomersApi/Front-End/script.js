document.addEventListener('DOMContentLoaded', init); // se ejecuta cuando se carga la página

const urlApi = "http://localhost:7217/api/"; // buscar en launch.json si esta bien el puerto y la url

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
    var resultado = await response.json();
    
    console.log(resultado)
    
    var html = ''
    for (customer in resultado) {
        var row = `<tr>
        <td>Maria</td>
        <td>Sanchez</td>
        <td>666665555</td>
        <td>marta@gmail.com</td>
        <td>
            <a href="#" class="myButton">Editar</a>
            <a href="#" class="myButtonDelete">Eliminar</a>
        </td>
    </tr>`; // estas comillas permiten ENTER dentro de ellos
        
        html += html + row;
    }
    
    document.querySelector('#customers > tbody').outerHTML = row;
}
