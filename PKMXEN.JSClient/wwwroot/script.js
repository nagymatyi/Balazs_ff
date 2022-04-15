let carriers = [];

getdata();

async function getdata() {
    await fetch('http://localhost:33503/carrier')
        .then(x => x.json())
        .then(y => {
            carriers = y;
            console.log(carriers);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    carriers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.carrierID + "</td><td>"
            + t.name + "</td><td>" + t.age + "</td><td>" + t.salary
        + "</td><td>" + t.totalNumberOfParcels + "</td><tr>";
    });
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
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}