let citizens = [];
let settlements = [];
let countries = [];
let connection;
getdata();
setupSignalR();

async function getdata() {
    fetch('http://localhost:54726/citizen')
        .then(x => x.json())
        .then(y => {
            citizens = y;
            display();
        });

    fetch('http://localhost:54726/settlement')
        .then(x => x.json())
        .then(y => {
            settlements = y;
            selectSettlement();
        });

    fetch('http://localhost:54726/country')
        .then(x => x.json())
        .then(y => {
            countries = y;
            selectCountry();
        });
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:54726/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on(
        "CitizenCreated", (user, message) => {
            console.log("citizen created");
                getdata();
    });

    connection.on(
        "CitizenUpdated", (user, message) => {
            console.log("citizen updated");
                getdata();
    });

    connection.on(
        "CitizenDeleted", (user, message) => {
            console.log("citizen deleted");
                getdata();
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



function display() {
    document.getElementById('resultarea').innerHTML = "";
    citizens.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            `<tr><td>${t.personID}</td>` +
            `<td><input type="text" id="name_${t.personID}" value="${t.name}"></td >` +
            `<td><input type="date" id="birthdate_${t.personID}" value="${dateFormat(t.birthDate)}"></td>` +
            `<td><input type="checkbox" id="hasCriminalRecord_${t.personID}" value="${(t.hasCriminalRecord)} " ${booleanToChecbox(t.hasCriminalRecord)} ></td >` +
            `<td><input type="number" id="incomeInUSD_${t.personID}" value="${t.incomeInUSD}"></td >` +
            `<td>${t.settlement.settlementName} <input type="number" id="selectSettlement_${t.personID}" value=${t.settlementID} ></td >` +
            `<td>${t.citizenship.name} <input type="number" id="selectCountry_${t.personID}" value=${t.citizenshipID}></td>` +
            `<td>
                <button type="button" onclick="update(${t.personID})">Update citizen</button>
                </br>
                <button type="button" onclick="remove(${t.personID})">Remove citizen</button>
            </td></tr>`;
    });
}

function booleanToChecbox(boolean) {
    if (boolean == true) {
        return "checked";
    }
    else {
        return "";
    }
}

/* https://stackoverflow.com/questions/10830357/javascript-toisostring-ignores-timezone-offset */
function dateFormat(date) {
    date = new Date(date);
    var isoDateTime = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString().substr(0, 10);
    return isoDateTime;
}

/* https://stackoverflow.com/questions/9895082/javascript-populate-drop-down-list-with-array */
function selectSettlement() {
    var selectSettlement = document.getElementById("selectSettlement");
    for (var i = 0; i < settlements.length; i++) {
        var opt = settlements[i];
        var el = document.createElement("option");
        el.textContent = opt.settlementName;
        el.value = opt.settlementID;
        selectSettlement.appendChild(el);
    }
}

function selectCountry() {
    var selectCountry = document.getElementById("selectCountry");
    for (var i = 0; i < countries.length; i++) {
        var opt = countries[i];
        var el = document.createElement("option");
        el.textContent = opt.name;
        el.value = opt.countryID;
        selectCountry.appendChild(el);
    }
}

function create() {
    let name = document.getElementById('name').value;
    let birthDate = document.getElementById('birthDate').value;
    let hasCriminalRecord = document.getElementById('hasCriminalRecord').checked == 1;
    let incomeInUSD = document.getElementById('incomeInUSD').value;
    let settlementID = document.getElementById('selectSettlement').value;
    let citizenshipID = document.getElementById('selectCountry').value;

    fetch('http://localhost:54726/citizen', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: name,
                birthDate: birthDate,
                hasCriminalRecord: hasCriminalRecord,
                incomeInUSD: incomeInUSD,
                settlementID: settlementID,
                citizenshipID: citizenshipID
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function update(id) {
    console.log("update citizen " + id);
    let name = document.getElementById('name_' + id).value;
    let birthDate = document.getElementById('birthdate_' + id).value;
    let hasCriminalRecord = document.getElementById('hasCriminalRecord_' + id).checked == 1;
    let incomeInUSD = document.getElementById('incomeInUSD_' + id).value;
    let settlementID = document.getElementById('selectSettlement_' + id).value;
    let citizenshipID = document.getElementById('selectCountry_' + id).value;

    fetch('http://localhost:54726/citizen', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                personID: id,
                name: name,
                birthDate: birthDate,
                hasCriminalRecord: hasCriminalRecord,
                incomeInUSD: incomeInUSD,
                citizenshipID: citizenshipID,
                settlementID: settlementID
            }),
    })
        .then(response => response)
        .then(data => {
            console.log("Citizen updated");
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function remove(id) {
    fetch('http://localhost:54726/citizen/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

