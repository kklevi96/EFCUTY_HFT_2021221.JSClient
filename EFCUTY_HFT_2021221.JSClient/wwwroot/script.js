let citizens = [];

fetch('http://localhost:54726/citizen')
    .then(x => x.json())
    .then(y => {
        citizens = y;
        console.log(citizens);
        display();
    });



function display() {
    citizens.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + 
            t.personID + "</td><td>" +
        t.name + "</td><td>" +
            +
            t.hasCriminalRecord + "</td><td>" + 
            t.incomeInUSD + "</td><td>" + 
            t.settlement.settlementName + "</td><td>" +
            t.citizenship.name + "</td></tr>";

        console.log(t.citizenship.name);
    });
}

function toRow() {
    let value = citizens.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr>" +
            `<td>${t.personID}</td>` +
    })
}