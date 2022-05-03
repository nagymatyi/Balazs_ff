let carriers = [];
let connection = null;
let selectedRow = 0;
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

    connection.on("CarrierUpdated", (user, message) => {
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
        "<tr><td>" + t.carrierID +
        "</td><td>" + t.name +
        "</td><td>" + t.age +
        "</td><td>" + t.salary +
        "</td><td>" + t.totalNumberOfParcels +
        "</td><td>" + `<button type="button" onclick="editCarrier(${t.carrierID})">Edit</button><button type="button" onclick="removeCarrier(${t.carrierID})">Delete</button>` +
        "</td></tr>";
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
            getCarrierData();
        })
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
            reset();
        })
        .catch((error) => { console.error('Error:', error); });
}

function editCarrier(id) {
    document.getElementById('create').style.display = 'none';
    document.getElementById('edit').style.display = 'block';

    for (var i = 0; i < carriers.length; i++) {
        if (id == carriers[i].carrierID) {
            selectedRow = i;
            document.getElementById('carrierid').value = carriers[i].carrierID;
            document.getElementById('name').value = carriers[i].name;
            document.getElementById('age').value = carriers[i].age;
            document.getElementById('salary').value = carriers[i].salary;
            document.getElementById('tnop').value = carriers[i].totalNumberOfParcels;
        }
    }
}

function updateData() {
    carriers[selectedRow].name = document.getElementById('name').value;
    carriers[selectedRow].age = document.getElementById('age').value;
    carriers[selectedRow].salary = document.getElementById('salary').value;
    carriers[selectedRow].totalNumberOfParcels = document.getElementById('tnop').value;
    displayCarrier();
    reset();
    //fetch('http://localhost:33503/carrier', {
    //    method: 'PUT',
    //    headers: { 'Content-Type': 'application/json', },
    //    body: JSON.stringify(
    //        { name: Name, age: Age, salary: Salary, totalNumberOfParcels: TotalNumberOfParcels })
    //})
    //    .then(response => response)
    //    .then(data => {
    //        console.log('Success:', data);
    //        getCarrierData();
    //    })
    //    .catch((error) => { console.error('Error:', error); });
}

function reset() {
    document.getElementById('create').style.display = 'block';
    document.getElementById('edit').style.display = 'none';

    document.getElementById('carrierid').value = "";
    document.getElementById('name').value = "";
    document.getElementById('age').value = "";
    document.getElementById('salary').value = "";
    document.getElementById('tnop').value = "";
}

function update() {
    let ID = document.getElementById('carrierid').value;
    let NAME = document.getElementById('name').value;
    let AGE = document.getElementById('age').value;
    let SALARY = document.getElementById('salary').value;
    let TNOP = document.getElementById('tnop').value;

    fetch('http://localhost:33503/carrier', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                carrierID: ID,
                name: NAME,
                age: AGE,
                salary: SALARY,
                totalNumberOfParcels: TNOP
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCarrierData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}