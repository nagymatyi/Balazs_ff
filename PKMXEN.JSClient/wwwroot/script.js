let carriers = [];
let connection = null;
getCarrierData();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:33503/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarrierCreated", (user, message) => {
        getCarrierData();
    });

    connection.on("CarrierDeleted", (user, message) => {
        getCarrierData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getCarrierData() {
    await fetch('http://localhost:33503/carrier')
        .then(x => x.json())
        .then(y => {
            carriers = y;
            //console.log(carriers);
            displayCarrier();
        });
}

function displayCarrier() {
    document.getElementById('resultarea').innerHTML = "";
    carriers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.carrierID + "</td><td>"
            + t.name + "</td><td>" + t.age + "</td><td>" + t.salary
        + "</td><td>" + t.totalNumberOfParcels + "</td><td>" +
        `<button type="button" onclick="removeCarrier(${t.carrierID})">Delete</button>`
        + "</td></tr>";
    });
}

function removeCarrier(id) {
    fetch('http://localhost:33503/carrier/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCarrierData();})
        .catch((error) => { console.error('Error:', error); });
}

function createCarrier() {
    let Name = document.getElementById('name').value;
    let Age = document.getElementById('age').value;
    let Salary = document.getElementById('salary').value;
    let TotalNumberOfParcels = document.getElementById('tnop').value;
    fetch('http://localhost:33503/carrier', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: Name, age: Age, salary: Salary, totalNumberOfParcels: TotalNumberOfParcels })})
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCarrierData();
        })
        .catch((error) => { console.error('Error:', error); });
}